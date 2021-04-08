using System.Threading.Tasks;
using TimeTracker.Models;

namespace TimeTracker.Services.Account
{
	internal interface IAccountService
	{
		Task<bool> LoginAsync(string username, string password);

		Task<double> GetCurrentPayRateAsync();

		Task<AuthenticatedUser> GetUserAsync();
	}
}