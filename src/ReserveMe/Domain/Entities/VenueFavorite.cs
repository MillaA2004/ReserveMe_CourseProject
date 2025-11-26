namespace Domain.Entities
{
	public class VenueFavorite
	{
		public int Id { get; set; }
		public string UserId { get; set; } = null!;
		public int VenueId { get; set; }

		public DateTime CreatedAt { get; set; }

		// Navigation properties
		public ApplicationUser User { get; set; } = null!;
		public Venue Venue { get; set; } = null!;
	}
}