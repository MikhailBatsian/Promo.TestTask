using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);
        builder.OwnsOne(u => u.Address, a =>
        {
            a.Property(ad => ad.Country).HasColumnName("Country");
            a.Property(ad => ad.Province).HasColumnName("Province");
        });
        builder.HasIndex(u => u.Email).IsUnique();
    }
}
