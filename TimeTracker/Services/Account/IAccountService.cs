﻿using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(string username, string password);
        Task<double> GetCurrentPayRateAsync();
        Task<bool> SendOtpCodeAsync(string phoneNumber);
        Task<bool> VerifyOtpCodeAsync(string code);

        Task<AuthenticatedUser> GetUserAsync();
    }
}
