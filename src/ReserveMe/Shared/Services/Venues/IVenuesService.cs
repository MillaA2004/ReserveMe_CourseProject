namespace Shared.Services.Venues
{
	using Shared.Dtos.Venues;

	public interface IVenuesService
	{
		Task<List<VenueAdminDto>> GetVenues();
	}
}
