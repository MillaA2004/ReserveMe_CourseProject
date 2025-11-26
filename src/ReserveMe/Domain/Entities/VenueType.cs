namespace Domain.Entities
{
	public class VenueType
	{
		public int Id { get; set; }

		public string Name { get; set; }

		// Navigation properties
		public ICollection<Venue> Venues { get; set; } = new HashSet<Venue>();
	}
}