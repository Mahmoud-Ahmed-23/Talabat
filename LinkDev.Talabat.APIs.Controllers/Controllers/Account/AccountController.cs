using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Auth.Models;
using LinkDev.Talabat.Core.Application.Abstraction.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Account
{
	public class AccountController(IServiceManager serviceManager) : BaseApiController
	{

		[HttpPost("login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto model)
		{
			var result = await serviceManager.AuthService.LoginAsync(model);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto model)
		{
			var result = await serviceManager.AuthService.RegisterAsync(model);
			return Ok(result);
		}

		[Authorize]
		[HttpGet]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var result = await serviceManager.AuthService.GetCurrentUser(User);
			return Ok(result);

		}

		[Authorize]
		[HttpGet("address")]
		public async Task<ActionResult<UserDto>> GetUserAddress()
		{
			var result = await serviceManager.AuthService.GetUserAddress(User);
			return Ok(result);
		}

		[Authorize]
		[HttpPut("address")]
		public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
		{
			var result = await serviceManager.AuthService.UpdateUserAddress(User, addressDto);
			return Ok(result);
		}
	}
}
