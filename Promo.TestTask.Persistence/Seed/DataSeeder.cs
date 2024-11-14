using Microsoft.EntityFrameworkCore;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence.Seed;
public static class DataSeeder
{
    public static async Task Seed(PromoDbContext context)
    {
        await context.Database.MigrateAsync();

        if (!await context.Countries.AnyAsync())
        {
            await context.Countries.AddRangeAsync(new List<Country>{
                new() { Id = 1, Name = "United States", Code = "US" },
                new() { Id = 2, Name = "Canada", Code = "CA" }
            });

            await context.SaveChangesAsync();
        }

        if (!await context.Provinces.AnyAsync())
        {
            await context.Provinces.AddRangeAsync(new List<Province>{
                new() { Id = 1, Name = "California", CountryId = 1 },
                new() { Id = 2, Name = "New York", CountryId = 1 },
                new() { Id = 3, Name = "Ontario", CountryId = 2 },
                new() { Id = 4, Name = "Quebec", CountryId = 2 }
            });

            await context.SaveChangesAsync();
        }
    }
}
