namespace Domain.Entities
{
	public class VenueReview
	{
		public int Id { get; set; }

		public string? UserId { get; set; }

		public int VenueId { get; set; }

		// 1 to 5
		public int? Rating { get; set; }

		public string? Comment { get; set; }

		public DateTime CreatedAt { get; set; }

		// Navigation properties
		public Venue Venue { get; set; }
		public ApplicationUser User { get; set; }
	}
}