using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Shared.Dtos.Reservations;
using Shared.Dtos.Venues;
using Shared.Services.Venues;

namespace ReserveMe.Pages.Reservations;

public partial class CreateReservation : ComponentBase
{
	[Parameter] public int VenueId { get; set; }

	[Inject] private IVenuesService _venuesService { get; set; } = null!;

	protected bool IsFavorite { get; set; }
	protected string? SuccessMessage { get; set; }
	protected string? ErrorMessage { get; set; }

	protected VenueSearchDto currVenue { get; set; } = new();

	protected ReservationDto reservationDto { get; set; } = new();
	private EditContext reservationCreateContext = default!;

	protected override async Task OnInitializedAsync()
	{
		reservationCreateContext = new EditContext(reservationDto);
		currVenue = await _venuesService.GetVenueById(VenueId);

		await base.OnInitializedAsync();
	}

	protected async Task ReserveAsync()
	{
		ErrorMessage = null;
		SuccessMessage = null;

		try
		{

			SuccessMessage =
				$"Reservation created (mock). Guest: {reservationDto.ContactName}, " +
				$"Guests: {reservationDto.GuestsCount}, " +
				//$"Area: {AreaLabel(Model.Area)}, " +
				$"Date: {reservationDto.ReservationTime:MM/dd/yyyy HH:mm}.";
		}
		catch (Exception ex)
		{
			ErrorMessage = ex.Message;
		}
	}

	protected void ToggleFav() => IsFavorite = !IsFavorite;

	//protected static string AreaLabel(string? area)
	//	=> string.IsNullOrWhiteSpace(area) ? "—" : (area == "smoking" ? "Smoking" : "Non-smoking");
}
