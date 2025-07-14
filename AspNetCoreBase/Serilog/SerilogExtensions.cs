using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreBase.Serilog;

public static class SerilogExtensions
{
    public static IHostBuilder AddSerilogConfiguration(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "AspNetCoreBase")
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName));
    }

    public static IHostBuilder AddVerboseSerilogConfiguration(this IHostBuilder builder)
    {
        return builder.UseSerilog((context, services, configuration) =>
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "AspNetCoreBase")
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("MachineName", Environment.MachineName)
                .Enrich.WithProperty("ProcessId", Environment.ProcessId)
                .Enrich.WithProperty("ThreadId", Environment.CurrentManagedThreadId));
    }

    public static IApplicationBuilder AddSerilogRequestLogging(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("StatusCode", httpContext.Response.StatusCode);
            };
            
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} {StatusCode} {Elapsed:0.0000}ms";
        });
    }

    public static IApplicationBuilder AddVerboseSerilogRequestLogging(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging(options =>
        {
            options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            {
                diagnosticContext.Set("RequestId", httpContext.TraceIdentifier);
                diagnosticContext.Set("RequestPath", httpContext.Request.Path);
                diagnosticContext.Set("RequestMethod", httpContext.Request.Method);
                diagnosticContext.Set("StatusCode", httpContext.Response.StatusCode);
                
                // Optional: Add ClientIP logging if needed for your project
                // diagnosticContext.Set("ClientIP", httpContext.Connection.RemoteIpAddress?.ToString());
                // Optional: Add UserAgent logging if needed for your project
                // diagnosticContext.Set("UserAgent", httpContext.Request.Headers.UserAgent.ToString());
            };
            
            options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        });
    }
}