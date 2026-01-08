namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class VenueConfiguration : IEntityTypeConfiguration<Venue>
	{
		public void Configure(EntityTypeBuilder<Venue> builder)
		{
			builder.Property(v => v.Name)
				   .IsRequired()
				   .HasMaxLength(200);

			builder.Property(v => v.VenueTypeId)
				   .IsRequired(false);

			builder
				.HasOne(v => v.VenueType)
				.WithMany(t => t.Venues)
				.HasForeignKey(v => v.VenueTypeId)
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.HasMany(v => v.Tables)
				.WithOne(t => t.Venue)
				.HasForeignKey(t => t.VenueId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
