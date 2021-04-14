﻿using TimeTracker.Models;
using TimeTracker.PageModels.Base;
using TimeTracker.Services.Account;
using TimeTracker.Services.Navigation;
using TimeTracker.ViewModels;

namespace TimeTracker.PageModels
{
	/// <summary>
	/// Login page's model.
	/// </summary>
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

		/// <summary>
		/// Username input.
		/// </summary>
		public LoginEntryViewModel UsernameEntryViewModel { get; }

		/// <summary>
		/// Password input.
		/// </summary>
		public LoginEntryViewModel PasswordEntryViewModel { get; }

		/// <summary>
		/// Button to create new account.
		/// </summary>
		public ButtonViewModel CreateAccountButtonViewModel { get; }

		/// <summary>
		/// Button to log in as existing user.
		/// </summary>
		public ButtonViewModel LogInButtonViewModel { get; }

		/// <summary>
		/// Handler of <see cref="CreateAccountButtonViewModel"/> button pressed event.
		/// </summary>
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

		/// <summary>
		/// Handler of <see cref="LogInButtonViewModel"/> button pressed event.
		/// </summary>
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
	}
}