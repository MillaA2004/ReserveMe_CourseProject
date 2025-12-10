namespace Shared.Services.Venues
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Shared.Dtos.Venues;
	using Shared.Providers;

	public class VenuesService : IVenuesService
	{
		private readonly IApiProvider _provider;

		public VenuesService(IApiProvider apiProvider)
		{
			this._provider = apiProvider;
		}

		#region GET

		public async Task<List<VenueAdminDto>> GetVenues()
		{
			try
			{
				var result = await _provider.GetAsync<List<VenueAdminDto>>(Endpoints.GetVenues, null, null);

				return result;
			}
			catch (Exception ex)
			{
				return new List<VenueAdminDto>();
			}
		}

		#endregion
	}
}
