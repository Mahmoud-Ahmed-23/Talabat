using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
using LinkDev.Talabat.APIs.Controllers.Controllers.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.APIs.Controllers.Controllers.Buggy
{
	public class BuggyController : BaseApiController
	{

		[HttpGet("notfound")]
		public IActionResult GetNotFoundRequest()
		{
			return NotFound(new ApiResponse(404));
		}

		[HttpGet("servererror")]
		public IActionResult GetServerErrorRequest()
		{
			throw new Exception();
		}

		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest(new ApiResponse(400));
		}

		[HttpGet("badrequest/{id:int}")] //Get: /api/buggy/badrequest/five
		public IActionResult GetValidationError(int id)
		{
			return Ok();
		}


		[HttpGet("unathorized")]
		public IActionResult GetUnathorized()
		{
			return Unauthorized(new ApiResponse(401));
		}

		[HttpGet("forbidden")]
		public IActionResult GetForbiddenRequest()
		{
			return Forbid();
		}

		[Authorize]
		[HttpGet("athorized")]
		public IActionResult GetAthorizedRequest()
		{
			return Ok();
		}
	}
}
