using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
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
		private readonly ViewAllPageModel viewAllPageModel;

		public ProfilePageModel(
			IAccountService accountService,
			ITrackedPeriodService trackedPeriodService,
			ViewAllPageModel viewAllPageModel)
		{
			this.accountService = accountService;
			this.trackedPeriodService = trackedPeriodService;
			this.viewAllPageModel = viewAllPageModel;

			var currentUser = accountService.CurrentUser;
			UsernameEntryViewModel = new LoginEntryViewModel("username", isPassword: false) {Text = currentUser.Username};
			PasswordEntryViewModel = new LoginEntryViewModel("password", isPassword: true) {Text = currentUser.Password};

			UpdatePasswordButtonViewModel = new ButtonViewModel("update password", OnUpdatePasswordButtonPressed);
			ClearUserDataButtonViewModel = new ButtonViewModel("clear data", OnClearDataButtonPressed);
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
			viewAllPageModel.AllForCurrentUser.Clear();
		}
	}
}