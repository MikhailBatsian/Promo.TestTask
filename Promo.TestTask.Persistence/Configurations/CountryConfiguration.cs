using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence.Configurations;
public class CountryConfiguration: IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.Property(b => b.Name)
            .IsRequired()
            .HasMaxLength(255);


        builder.Property(b => b.Code)
            .IsRequired()
            .HasMaxLength(3);

        builder.HasIndex(x => x.Name).IsUnique();
        builder.HasIndex(x => x.Code).IsUnique();
    }
}
