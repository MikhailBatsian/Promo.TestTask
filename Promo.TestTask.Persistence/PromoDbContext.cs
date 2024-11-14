using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Promo.TestTask.Domain.Account.Entities;

namespace Promo.TestTask.Persistence;
public class PromoDbContext : DbContext
{
    public PromoDbContext(DbContextOptions<PromoDbContext> options) : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
