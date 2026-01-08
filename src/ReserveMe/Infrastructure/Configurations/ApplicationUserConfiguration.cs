namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.HasOne(u => u.Venue)
				.WithMany(v => v.Users)
				.HasForeignKey(u => u.VenueId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(u => u.FirstName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(u => u.LastName)
				.IsRequired()
				.HasMaxLength(100);
		}
	}
}
