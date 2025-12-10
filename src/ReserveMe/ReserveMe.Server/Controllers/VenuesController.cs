namespace ReserveMe.Server.Controllers
{
	using Application.Venues.Queries;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Shared.Dtos.Venues;

	[Authorize]
	public class VenuesController : ApiControllerBase
	{
		#region READ

		[HttpGet("getAll")]
		public async Task<ActionResult<List<VenueAdminDto>>> GetMenus()
		{
			return await Mediator.Send(new GetVenuesQuery());
		}

		#endregion
	}
}
