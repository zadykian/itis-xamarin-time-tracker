using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	internal class LoginPageModel : PageModelBase
	{
		private readonly IAccountService accountService;
		private readonly INavigationService navigationService;

		public LoginPageModel(INavigationService navigationService,
			IAccountService accountService)
		{
			this.accountService = accountService;
			this.navigationService = navigationService;

			UsernameEntryViewModel = new LoginEntryViewModel("username", false);
			PasswordEntryViewModel = new LoginEntryViewModel("password", true);

			LogInButtonViewModel = new ButtonViewModel("log in", OnLogin);
			CreateAccountButtonViewModel = new ButtonViewModel("create account", OnCreateAccount);
		}

		public LoginEntryViewModel UsernameEntryViewModel { get; }

		public LoginEntryViewModel PasswordEntryViewModel { get; }

		public ButtonViewModel CreateAccountButtonViewModel { get; }

		public ButtonViewModel LogInButtonViewModel { get; }

		private async void OnLogin()
		{
			var userCredentials = new UserCredentials(UsernameEntryViewModel.Text, PasswordEntryViewModel.Text);
			var loginAttempt = await accountService.LoginAsync(userCredentials);

			if (loginAttempt)
			{
				await navigationService.NavigateToAsync<MainPageModel>();
			}
			else
			{
				// todo
			}
		}

		private async void OnCreateAccount()
		{
			var newUser = new User(UsernameEntryViewModel.Text, PasswordEntryViewModel.Text);
			var wasCreated = await accountService.CreateAccountAsync(newUser);

			if (wasCreated)
			{
				await navigationService.NavigateToAsync<MainPageModel>();
			}
			else
			{
				// todo
			}
		}
	}
}