namespace Infrastructure.Configurations
{
	using Domain.Entities;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;

	public class TableConfiguration : IEntityTypeConfiguration<Table>
	{
		public void Configure(EntityTypeBuilder<Table> builder)
		{
			builder.Property(t => t.TableNumber)
				   .IsRequired();

			builder.Property(t => t.Capacity)
				   .IsRequired();
		}
	}
}
