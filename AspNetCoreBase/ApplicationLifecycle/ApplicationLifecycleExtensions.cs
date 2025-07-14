using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AspNetCoreBase.ApplicationLifecycle;

public static class ApplicationLifecycleExtensions
{
    public static IWebHostBuilder ConfigureGracefulShutdown(this IWebHostBuilder builder)
    {
        return builder.ConfigureKestrel(options =>
        {
            options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);
            options.Limits.RequestHeadersTimeout = TimeSpan.FromSeconds(30);
        });
    }

    public static IApplicationBuilder UseGracefulShutdown(this IApplicationBuilder app)
    {
        var lifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
        
        lifetime.ApplicationStopping.Register(() =>
        {
            Log.Information("Application is stopping...");
        });

        lifetime.ApplicationStopped.Register(() =>
        {
            Log.Information("Application stopped.");
            Log.CloseAndFlush();
        });

        return app;
    }
} 