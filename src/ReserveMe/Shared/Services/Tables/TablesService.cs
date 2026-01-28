namespace Shared.Services.Tables
{
	using Shared.Dtos.Tables;
	using Shared.Providers;

	public class TablesService : ITablesService
	{
		private readonly IApiProvider _provider;

		public TablesService(IApiProvider apiProvider)
		{
			this._provider = apiProvider;
		}

		#region GET

		public async Task<List<TableDto>> GetTablesByVenueId(int venueId)
		{
			try
			{
				object[] uriParams = new object[]
				{
					venueId
				};

				var result = await _provider.GetAsync<List<TableDto>>(Endpoints.GetTablesByVenueId + "/{0}", uriParams, null, null);

				return result;
			}
			catch (Exception ex)
			{
				return new List<TableDto>();
			}
		}

		public async Task<List<TableDto>> GetAvailableTables(int venueId, DateTime reservationTime, int guestsCount)
		{
			try
			{
				var queryParams = new Dictionary<string, object>
				{
					{ "venueId", venueId },
					{ "reservationTime", reservationTime.ToString("O") }, // Use ISO 8601 for DateTime
					{ "guestsCount", guestsCount }
				};

				var result = await _provider.GetAsync<List<TableDto>>(Endpoints.GetAvailableTables, null, queryParams, null);

				return result;
			}
			catch (Exception ex)
			{
				return new List<TableDto>();
			}
		}

		#endregion
	}
}
