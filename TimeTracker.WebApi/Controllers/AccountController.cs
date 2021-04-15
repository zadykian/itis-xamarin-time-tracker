﻿using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeTracker.Services.Account;
using TimeTracker.Services.Models;
using TimeTracker.WebApi.Controllers.Base;

namespace TimeTracker.WebApi.Controllers
{
	/// <summary>
	/// Controller for users' accounts management.
	/// </summary>
	public class AccountController : ApiControllerBase
	{
		private readonly IAccountService accountService;

		public AccountController(IAccountService accountService)
			=> this.accountService = accountService;

		/// <inheritdoc cref="IAccountService.CreateAccountAsync"/>
		[HttpPost]
		public async Task<IActionResult> CreateAccountAsync([FromBody, Required] User newUser)
		{
			var createdSuccessfully = await accountService.CreateAccountAsync(newUser);
			var responseStatusCode = createdSuccessfully
				? StatusCodes.Status200OK
				: StatusCodes.Status400BadRequest;
			return StatusCode(responseStatusCode);
		}

		/// <inheritdoc cref="IAccountService.LogInAsync"/>
		[HttpPut]
		public async Task<IActionResult> LogInAsync([FromBody, Required] UserCredentials userCredentials)
		{
			var loggedInSuccessfully = await accountService.LogInAsync(userCredentials);
			var responseStatusCode = loggedInSuccessfully
				? StatusCodes.Status200OK
				: StatusCodes.Status401Unauthorized;
			return StatusCode(responseStatusCode);
		}

		/// <inheritdoc cref="IAccountService.UpdatePasswordAsync"/>
		[HttpPut]
		public async Task<IActionResult> UpdatePasswordAsync([FromBody, Required] User newUser)
		{
			var updatedSuccessfully = await accountService.UpdatePasswordAsync(newUser);
			var responseStatusCode = updatedSuccessfully
				? StatusCodes.Status200OK
				: StatusCodes.Status400BadRequest;
			return StatusCode(responseStatusCode);
		}
	}
}