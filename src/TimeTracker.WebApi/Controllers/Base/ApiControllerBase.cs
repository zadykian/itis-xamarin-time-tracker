using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace TimeTracker.WebApi.Controllers.Base
{
	/// <summary>
	/// Base class for TimeTracker Web API controllers.
	/// </summary>
	[ApiController]
	[Route("[controller]/[action]")]
	[Produces(Application.Json)]
	public abstract class ApiControllerBase : Controller
	{
	}
}