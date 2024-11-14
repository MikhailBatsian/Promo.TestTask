using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence.Configurations;
public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(e => e.Country);
    }
}
