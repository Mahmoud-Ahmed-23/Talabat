using LinkDev.Talabat.APIs.Controllers.Controllers.Base;
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
			return NotFound(new { Request = 404, Message = "Not Found" });
		}

		[HttpGet("servererror")]
		public IActionResult GetServerErrorRequest()
		{
			throw new Exception();
		}

		[HttpGet("badrequest")]
		public IActionResult GetBadRequest()
		{
			return BadRequest(new { Request = 400, Message = "Bad Request" });
		}

		[HttpGet("badrequest/{id:int}")] //Get: /api/buggy/badrequest/five
		public IActionResult GetValidationError(int id)
		{
			return Ok();
		}


		[HttpGet("unathorized")]
		public IActionResult GetUnathorized()
		{
			return Unauthorized(new { Request = 401, Message = "Unauthorized" });
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
