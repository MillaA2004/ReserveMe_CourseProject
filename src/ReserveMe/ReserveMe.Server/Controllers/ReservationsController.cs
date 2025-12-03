namespace ReserveMe.Server.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Mvc;

	// only authenticated users can access this now
	[Authorize]
	//[Authorize(Roles = "SuperAdmin")]
	public class ReservationsController : ApiControllerBase
	{
		//TODO: Test purposes only - remove later
		[HttpPost("reserve")]
		public ActionResult<AuthResponse> Reserve([FromBody] LoginRequest request)
		{
			return Ok(request.Email);
		}
	}
}
