using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Promo.TestTask.Api.Extensions;
using Promo.TestTask.Api.Validators;
using Promo.TestTask.Domain.Account.Repositories;
using Promo.TestTask.Domain.Account.Services;
using Promo.TestTask.Persistence;
using Promo.TestTask.Persistence.Repositories;
using Promo.TestTask.Persistence.Seed;

namespace Promo.TestTask.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICountryRepository, CountryRepository>();
        builder.Services.AddScoped<IProvinceRepository, ProvinceRepository>();

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<ILocationService, LocationService>();

        builder.Services.AddDbContext<PromoDbContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("Database");
            options.UseSqlite(connectionString);
        });

        builder.Services.AddControllers();

        builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        await SeedData(app.Services);

        app.UseDefaultFiles();
        app.UseStaticFiles();

        if (builder.Environment.IsDevelopment())
        {
            app.UseCorsPolicy();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseExceptionHandler();
        }
        
        app.UseHttpsRedirection();

        app.MapControllers();

        app.MapFallbackToFile("/index.html");

        await app.RunAsync();
    }

    private static async Task SeedData(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var scopedProvider = scope.ServiceProvider;

        var promoDbContext = scopedProvider.GetRequiredService<PromoDbContext>();
        await DataSeeder.Seed(promoDbContext);
    }
}
