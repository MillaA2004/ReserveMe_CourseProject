namespace Shared.Services.Tables
{
	using Shared.Dtos.Tables;

	public interface ITablesService
	{
		Task<List<TableDto>> GetTablesByVenueId(int venueId);

		Task<List<TableDto>> GetAvailableTables(int venueId, DateTime reservationTime, int guestsCount);
	}
}
