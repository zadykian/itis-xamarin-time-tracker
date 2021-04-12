﻿using TimeTracker.PageModels.Base;
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
			var loginAttempt = await accountService.LoginAsync(UsernameEntryViewModel.Text, PasswordEntryViewModel.Text);
			if (loginAttempt)
			{
				await navigationService.NavigateToAsync<MainPageModel>();
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