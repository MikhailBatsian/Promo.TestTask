using Promo.TestTask.Api.Middlewares;

namespace Promo.TestTask.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void UseCorsPolicy(this WebApplication app)
    {
        var allowedOrigins = app.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
        if (allowedOrigins != null && allowedOrigins.Any())
        {
            app.UseCors(x => x
                .WithOrigins(allowedOrigins)
                .AllowAnyMethod()
                .AllowCredentials()
                .AllowAnyHeader());
        }
    }
    public static void UseExceptionHandler(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}
