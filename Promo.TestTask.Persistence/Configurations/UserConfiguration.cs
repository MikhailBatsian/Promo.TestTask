using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(b => b.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasOne(b => b.Province);
    }
}
