namespace ReserveMe.Server.Controllers
{
	using Application.Venues.Commands;
	using Application.Venues.Queries;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using Shared.Dtos.Venues;
	using Shared.Requests.Venues;

	[Authorize]
	public class VenuesController : ApiControllerBase
	{
		#region READ

		[HttpGet("getAll")]
		public async Task<ActionResult<List<VenueAdminDto>>> GetVenues()
		{
			return await Mediator.Send(new GetVenuesQuery());
		}

		#endregion

		#region CREATE

		[HttpPost("create")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesDefaultResponseType]
		public async Task<IActionResult> CreateVenue(SaveVenueRequest venue)
		{
			await Mediator.Send(new CreateVenueCommand(venue));

			return NoContent();
		}

		#endregion
	}
}
