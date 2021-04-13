using System.Linq;
using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.Services.TimeTracking;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	/// <summary>
	/// Profile page model.
	/// </summary>
	internal class ProfilePageModel : PageModelBase
	{
		private readonly IAccountService accountService;
		private readonly ITrackedPeriodService trackedPeriodService;
		private readonly INavigationService navigationService;

		private readonly ViewAllPageModel viewAllPageModel;
		private readonly TimerPageModel timerPageModel;

		public ProfilePageModel(
			IAccountService accountService,
			ITrackedPeriodService trackedPeriodService,
			INavigationService navigationService,
			ViewAllPageModel viewAllPageModel,
			TimerPageModel timerPageModel)
		{
			this.accountService = accountService;
			this.trackedPeriodService = trackedPeriodService;
			this.navigationService = navigationService;
			this.viewAllPageModel = viewAllPageModel;
			this.timerPageModel = timerPageModel;

			var currentUser = accountService.CurrentUser;
			UsernameEntryViewModel = new LoginEntryViewModel("username", isPassword: false) {Text = currentUser.Username};
			PasswordEntryViewModel = new LoginEntryViewModel("password", isPassword: true) {Text = currentUser.Password};

			UpdatePasswordButtonViewModel = new ButtonViewModel("update password", OnUpdatePasswordButtonPressed);
			ClearUserDataButtonViewModel = new ButtonViewModel("clear data", OnClearDataButtonPressed);
			LogOutButtonViewModel = new ButtonViewModel("log out", OnLogOutButtonPressed);
		}

		/// <summary>
		/// Username input.
		/// </summary>
		public LoginEntryViewModel UsernameEntryViewModel { get; }

		/// <summary>
		/// Password input.
		/// </summary>
		public LoginEntryViewModel PasswordEntryViewModel { get; }

		/// <summary>
		/// Update password button.
		/// </summary>
		public ButtonViewModel UpdatePasswordButtonViewModel { get; }

		/// <summary>
		/// Clear user data button.
		/// </summary>
		public ButtonViewModel ClearUserDataButtonViewModel { get; }

		/// <summary>
		/// Log out button.
		/// </summary>
		public ButtonViewModel LogOutButtonViewModel { get; }

		/// <summary>
		/// Action bound to <see cref="UpdatePasswordButtonViewModel"/> button.
		/// </summary>
		private async void OnUpdatePasswordButtonPressed()
		{
			var updatedCredentials = new UserCredentials(accountService.CurrentUser.Username, PasswordEntryViewModel.Text);
			await accountService.UpdatePasswordAsync(updatedCredentials);
		}

		/// <summary>
		/// Action bound to <see cref="ClearUserDataButtonViewModel"/> button.
		/// </summary>
		private async void OnClearDataButtonPressed()
		{
			await trackedPeriodService.ClearDataAsync(accountService.CurrentUser.Id);

			if (!viewAllPageModel.AllForCurrentUser.Any())
			{
				return;
			}

			var currentPeriod = viewAllPageModel.AllForCurrentUser[0];
			viewAllPageModel.AllForCurrentUser.Clear();

			// If current period is not finished yet, return it to the top of a list.
			if (currentPeriod.End is null)
			{
				viewAllPageModel.AllForCurrentUser.Insert(0, currentPeriod);	
			}
		}

		/// <summary>
		/// Action bound to <see cref="LogOutButtonViewModel"/> button.
		/// </summary>
		private async void OnLogOutButtonPressed()
		{
			if (timerPageModel.TimerIsStarted)
			{
				timerPageModel.TimerButtonViewModel.Command.Execute(parameter: null);
			}

			accountService.LogOut();
			await navigationService.NavigateToAsync<LoginPageModel>(setRoot: true);
		}
	}
}