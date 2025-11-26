namespace Domain.Entities
{
	using Microsoft.AspNetCore.Identity;

	public class ApplicationUser : IdentityUser
	{
		public int? VenueId { get; set; }

		public required string FirstName { get; set; }

		public required string LastName { get; set; }

		public byte[]? ProfilePicture { get; set; }

		public bool IsActive { get; set; } = true;

		// Navigation properties
		public Venue? Venue { get; set; }
		public ICollection<VenueFavorite> Favorites { get; set; } = new HashSet<VenueFavorite>();
		public ICollection<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
		public ICollection<VenueReview> VenueReviews { get; set; } = new HashSet<VenueReview>();
	}
}