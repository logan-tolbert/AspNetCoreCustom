using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AspNetCoreBase;

public static class StartupValidation
{
    public static void ValidateAndLog(IConfiguration config, IHostEnvironment env)
    {
        // Log environment info
        Log.Information("Starting in {EnvironmentName} on {MachineName}", env.EnvironmentName, Environment.MachineName);

        // Example: Validate a required config value (add more as needed)
        // string? requiredValue = config["MyRequiredSetting"];
        // if (string.IsNullOrWhiteSpace(requiredValue))
        // {
        //     throw new InvalidOperationException("Missing required configuration: MyRequiredSetting");
        // }
    }
} 