namespace ReserveMe.Pages.Venues
{
	using Microsoft.AspNetCore.Components;
	using Shared.Dtos.Venues;
	using Shared.Services.Venues;

	public partial class VenueDetails
	{
		[Parameter] public int VenueId { get; set; }

		[Inject] private IVenuesService _venuesService { get; set; } = null!;

		private VenueSearchDto venue { get; set; }

		protected override async Task OnInitializedAsync()
		{
			this.venue = await this._venuesService.GetVenueById(VenueId);
		}
	}
}
