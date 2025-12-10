namespace ReserveMe.Pages.Venues
{
	using Microsoft.AspNetCore.Components;
	using Shared.Dtos.Venues;
	using Shared.Services.Venues;

	public partial class VenuesPage
	{
		[Inject]
		private IVenuesService _venuesService { get; set; } = null!;

		private List<VenueAdminDto> venues = new List<VenueAdminDto>();

		protected override async Task OnInitializedAsync()
		{
			this.venues = await this._venuesService.GetVenues();
		}

		private string? GetVenueOwner(int menuId)
		{
			//TODO: Get the real owner
			return "Ivan Ivanov";
		}
	}
}
