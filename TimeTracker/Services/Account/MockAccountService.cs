﻿using System;
using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
	internal class MockAccountService : IAccountService
	{
		public Task<double> GetCurrentPayRateAsync()
		{
			return Task.FromResult(10.0);
		}

		public Task<AuthenticatedUser> GetUserAsync()
		{
			throw new NotImplementedException();
		}

		public Task<bool> LoginAsync(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
			{
				return Task.FromResult(false);
			}

			return Task.Delay(1000).ContinueWith(_ => true);
		}
	}
}