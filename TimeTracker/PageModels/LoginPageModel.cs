using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.ViewModels;
using TimeTracker.ViewModels.Buttons;

namespace TimeTracker.PageModels
{
	internal class LoginPageModel : PageModelBase
	{
		private readonly IAccountService accountService;
		private readonly INavigationService navigationService;

		public LoginEntryViewModel EmailEntryViewModel { get; }

		public LoginEntryViewModel PasswordEntryViewModel { get; }

		public ButtonModel CreateAccountModel { get; }

		public ButtonModel LogInModel { get; }

		public LoginPageModel(INavigationService navigationService,
			IAccountService accountService)
		{
			this.accountService = accountService;
			this.navigationService = navigationService;
			EmailEntryViewModel = new LoginEntryViewModel("username", false);
			PasswordEntryViewModel = new LoginEntryViewModel("password", true);

			LogInModel = new ButtonModel("log in", OnLogin);
			CreateAccountModel = new ButtonModel("create account", OnCreateAccount);
		}

		private async void OnLogin()
		{
			var loginAttempt = await accountService.LoginAsync(EmailEntryViewModel.Text, PasswordEntryViewModel.Text);
			if (loginAttempt)
			{
				// navigate to the Dashboard.
				await navigationService.NavigateToAsync<RecentActivityPageModel>();
			}
			else
			{
				// TODO: Display an Alert for Failure!
			}
		}

		private void OnCreateAccount()
		{
		}
	}
}