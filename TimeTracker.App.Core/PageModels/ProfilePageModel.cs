using System.Linq;
using System.Threading.Tasks;
using TimeTracker.App.Core.PageModels.Base;
using TimeTracker.App.Core.Services.Navigation;
using TimeTracker.App.Core.ViewModels;
using TimeTracker.Services.Account;
using TimeTracker.Services.Models;
using TimeTracker.Services.TimeTracking;

namespace TimeTracker.App.Core.PageModels
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

			UsernameEntryViewModel = new LoginEntryViewModel("username", isPassword: false);
			PasswordEntryViewModel = new LoginEntryViewModel("password", isPassword: true);

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

		/// <inheritdoc />
		public override async Task InitializeAsync(object navigationData)
		{
			await Task.CompletedTask;
			var currentUser = accountService.CurrentUser;
			UsernameEntryViewModel.Text = currentUser.Username;
			PasswordEntryViewModel.Text = currentUser.Password;
		}

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
			await trackedPeriodService.ClearDataAsync(accountService.CurrentUser.Id!.Value);

			if (!viewAllPageModel.AllForCurrentUser.Any())
			{
				return;
			}

			var lastTrackedPeriod = viewAllPageModel.AllForCurrentUser[0];
			viewAllPageModel.AllForCurrentUser.Clear();

			// If last tracked period is not finished yet, return it to the top of a list.
			if (lastTrackedPeriod.End is null)
			{
				viewAllPageModel.AllForCurrentUser.Insert(0, lastTrackedPeriod);
			}
		}

		/// <summary>
		/// Action bound to <see cref="LogOutButtonViewModel"/> button.
		/// </summary>
		private async void OnLogOutButtonPressed()
		{
			await timerPageModel.StopTimer();
			await accountService.LogOutAsync();
			await navigationService.NavigateToAsync<LoginPageModel>(setRoot: true);
		}
	}
}