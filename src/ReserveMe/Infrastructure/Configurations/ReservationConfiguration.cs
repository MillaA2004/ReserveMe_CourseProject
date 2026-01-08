namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
	{
		public void Configure(EntityTypeBuilder<Reservation> builder)
		{
			builder
				.HasOne(r => r.ApplicationUser)
				.WithMany(u => u.Reservations)
				.HasForeignKey(r => r.UserId)
				.OnDelete(DeleteBehavior.SetNull);

			builder
				.HasOne(r => r.Venue)
				.WithMany(v => v.Reservations)
				.HasForeignKey(r => r.VenueId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Property(r => r.TableNumber)
				   .IsRequired();

			builder.Property(r => r.GuestsCount)
				   .IsRequired();
		}
	}
}
