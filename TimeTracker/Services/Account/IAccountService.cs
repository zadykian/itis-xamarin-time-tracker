using System.Threading.Tasks;

namespace TimeTracker.Services.Account
{
	internal interface IAccountService
	{
		Task<bool> LoginAsync(string username, string password);
	}
}