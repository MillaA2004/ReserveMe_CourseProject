using Common.Enums;
using Microsoft.AspNetCore.Components;

namespace ReserveMe.Pages.Reservations
{
	public partial class History : ComponentBase
	{
		protected List<Reservation> Reservations { get; set; } = new();

		[Inject] protected NavigationManager NavigationManager { get; set; } = default!;

		protected override void OnInitialized()
		{
			Reservations = new List<Reservation>
			{
				new Reservation
				{
					Id = 1,
					RestaurantName = "Pizza",
					Number = "#11111",
					Date = new DateTime(2025, 11, 20, 19, 30, 0),
			Status =        ReservationStatus.Pending,
					Address = "ул. „Пицария“ 10, София",
					LogoFallback = "P",
					GuestsCount = 4,
					VenueType = "Bistro"
				},
				new Reservation
				{
					Id = 2,
					RestaurantName = "Garden",
					Number = "#11111",
					Date = new DateTime(2025, 11, 20, 19, 30, 0),
Status =                    ReservationStatus.InProgress,
					Address = "ул. „Пицария“ 10, София",
					LogoFallback = "G",
					GuestsCount = 1,
					VenueType = "Bar"
				},
				new Reservation
				{
					Id = 3,
					RestaurantName = "Bistro",
					Number = "#11111",
					Date = new DateTime(2025, 11, 20, 19, 30, 0),
			Status =        ReservationStatus.Approved,
					Address = "ул. „Пицария“ 10, София",
					LogoFallback = "B",
					GuestsCount = 8,
					VenueType = "Restaurant"
				},
				new Reservation
				{
					Id = 3,
					RestaurantName = "Bistro",
					Number = "#11111",
					Date = new DateTime(2025, 11, 20, 19, 30, 0),
				Status =    ReservationStatus.Declined,
					Address = "ул. „Пицария“ 10, София",
					LogoFallback = "B",
					GuestsCount = 8,
					VenueType = "Restaurant"
				},
				new Reservation
				{
					Id = 3,
					RestaurantName = "Bistro",
					Number = "#11111",
					Date = new DateTime(2025, 11, 20, 19, 30, 0),
				Status =    ReservationStatus.Completed,
					Address = "ул. „Пицария“ 10, София",
					LogoFallback = "B",
					GuestsCount = 8,
					VenueType = "Restaurant"
				}
			};
		}

		private string GetStatusClass(ReservationStatus status) => status switch
		{
			ReservationStatus.Pending => "reservation-status--pending",
			ReservationStatus.Approved => "reservation-status--approved",
			ReservationStatus.InProgress => "reservation-status--inprogress",
			ReservationStatus.Declined => "reservation-status--declined",
			ReservationStatus.Completed => "reservation-status--completed",
			_ => "reservation-status--pending"
		};

		private string GetStatusLabel(ReservationStatus status) => status switch
		{
			ReservationStatus.Pending => "Pending",
			ReservationStatus.Approved => "Approved",
			ReservationStatus.InProgress => "In Progress",
			ReservationStatus.Declined => "Declined",
			ReservationStatus.Completed => "Completed",
			_ => "Pending"
		};

		protected void OpenReservation(int id)
		{
			Console.WriteLine($"Open reservation {id}");
			NavigationManager.NavigateTo($"/reservation/{id}");
		}
	}

	public class Reservation
	{
		public int Id { get; set; }
		public string RestaurantName { get; set; } = string.Empty;
		public string Number { get; set; } = string.Empty;
		public DateTime Date { get; set; }
		public ReservationStatus Status { get; set; }
		public string Address { get; set; } = string.Empty;
		public string LogoFallback { get; set; } = string.Empty;

		public string VenueType { get; set; }
		public int GuestsCount { get; set; }

	}

}
