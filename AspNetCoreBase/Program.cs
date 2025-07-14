using Serilog;
using Serilog.Events;
using AspNetCoreBase;
using AspNetCoreBase.Serilog;
using AspNetCoreBase.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using AspNetCoreBase.ApplicationLifecycle;

var builder = WebApplication.CreateBuilder(args);

// Container detection
var isContainerized = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
if (isContainerized)
{
    builder.Configuration.AddJsonFile("appsettings.Container.json", optional: true);
}

// Use concise logging by default (use AddVerboseSerilogConfiguration() for verbose mode)
builder.Host.AddSerilogConfiguration();

builder.WebHost.ConfigureGracefulShutdown();

var app = builder.Build();

app.UseGracefulShutdown();

// Uncomment this line to enable request logging
// app.AddSerilogRequestLogging();

StartupValidation.ValidateAndLog(app.Configuration, app.Environment);

app.UseBaseSecurity(app.Environment);

// ASP.NET Core Middleware and Features

app.MapGet("/", (ILogger<Program> logger) => 
{
    logger.LogInformation("Hello World!");
    return "Hello World!";
});

app.Run();
