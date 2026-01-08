namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class VenueTypeConfiguration : IEntityTypeConfiguration<VenueType>
	{
		public void Configure(EntityTypeBuilder<VenueType> builder)
		{
			builder.Property(vt => vt.Name)
				   .IsRequired()
				   .HasMaxLength(100);
		}
	}
}
