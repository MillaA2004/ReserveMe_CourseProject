namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class VenueFavoriteConfiguration : IEntityTypeConfiguration<VenueFavorite>
	{
		public void Configure(EntityTypeBuilder<VenueFavorite> builder)
		{
			builder.HasKey(f => f.Id);

			builder
				.HasOne(f => f.User)
				.WithMany(u => u.Favorites)
				.HasForeignKey(f => f.UserId)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne(f => f.Venue)
				.WithMany(v => v.Favorites)
				.HasForeignKey(f => f.VenueId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(f => f.CreatedAt)
				   .IsRequired();
		}
	}
}
