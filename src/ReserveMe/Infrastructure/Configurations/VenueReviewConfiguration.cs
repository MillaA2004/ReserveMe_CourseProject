namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class VenueReviewConfiguration : IEntityTypeConfiguration<VenueReview>
	{
		public void Configure(EntityTypeBuilder<VenueReview> builder)
		{
			builder.HasKey(r => r.Id);

			builder
				.HasOne(r => r.User)
				.WithMany(u => u.VenueReviews)
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.HasOne(r => r.Venue)
				.WithMany(v => v.VenueReviews)
				.HasForeignKey(r => r.VenueId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
