# ASP.NET Core Base Template

A production-ready, universal foundation for ASP.NET Core web applications with enhanced logging, security, and lifecycle management.

## 🚀 Features

### Universal Infrastructure

- **Enhanced Logging** - Structured logging with Serilog, correlation IDs, and rich request context
- **Security Foundation** - HTTPS enforcement, security headers, and secure-by-default configuration
- **Configuration Management** - Environment-aware configuration with validation
- **Application Lifecycle** - Graceful shutdown handling and proper resource cleanup
- **Container Support** - Container detection and optimized configuration
- **Multi-Environment** - Development, Production, and Container-specific settings

### Architecture

- **Clean Separation** - Modular extension methods for each concern
- **Minimal API** - Modern, lightweight approach that's easy to extend
- **Production Ready** - Industry-standard packages and best practices
- **Universal Foundation** - Works for any ASP.NET Core web application type

## 📁 Project Structure

```pt
AspNetCoreBase/
├── Program.cs                          # Clean, focused application entry point
├── AspNetCoreBase.csproj              # Project configuration
├── StartupValidation.cs               # Configuration validation and environment logging
├── Serilog/
│   └── SerilogExtensions.cs          # Enhanced logging with correlation IDs
├── Security/
│   └── SecurityExtensions.cs         # HTTPS enforcement and security headers
├── ApplicationLifecycle/
│   └── ApplicationLifecycleExtensions.cs # Graceful shutdown and lifecycle management
├── Properties/
│   └── launchSettings.json           # Development launch profiles
├── appsettings.json                   # Production configuration
├── appsettings.Development.json       # Development configuration
└── appsettings.Container.json         # Container-specific configuration
```

## 🛠️ Getting Started

### Prerequisites

- .NET 9.0 SDK
- Visual Studio 2022 or VS Code
- Docker (for container development)

### Running the Application

#### Local Development

```bash
cd AspNetCoreBase
dotnet run
```

### Launch Profiles

The template includes three launch profiles:

1. **http** - Basic HTTP development (`http://localhost:5000`)
2. **https** - HTTPS development (`https://localhost:7113`) - **Recommended**
3. **Docker** - Container development with container-specific configuration

#### Launch Profile Selection

- **Visual Studio**: Select from the dropdown in the toolbar
- **VS Code**: Choose from the debug configuration
- **Command Line**: Use `--launch-profile` parameter

## 🔧 Configuration

### Environment-Specific Settings

- **Development**: Console + File logging, debug level
- **Production**: File-only logging, info level
- **Container**: Console-only logging, optimized for containers

### Logging

Structured logging with Serilog includes:

- **Clean, single log entries** - No duplicate logging
- **Request correlation IDs** - Automatic request tracking
- **Structured data** - Application, Environment, RequestId, RequestPath
- **Environment-specific configuration**:
  - **Development**: Console + File logging with Debug level
  - **Production**: File-only logging with Information level
  - **Container**: Console-only logging optimized for containers
- **Performance timing** - Request processing times
- **Rich context** - Machine, process, and thread information

#### Logging Configuration Options

**Default Configuration** (recommended for most projects):

```csharp
builder.Host.AddSerilogConfiguration();
```

**Verbose Configuration** (for debugging and detailed analysis):

```csharp
builder.Host.AddVerboseSerilogConfiguration();
```

The verbose configuration includes additional enrichment:

- Machine name
- Process ID
- Thread ID
- Enhanced diagnostic context

### Security

- HTTPS redirection (except in development)
- Security headers (XSS protection, content type options, etc.)
- Secure-by-default configuration

## 🚀 Extending for Different Project Types

### Web API

```csharp
// Add API endpoints
app.MapGet("/api/health", () => new { Status = "Healthy" });
app.MapPost("/api/data", (DataRequest request) => { /* ... */ });
```

### MVC Application

```csharp
// Add MVC services
builder.Services.AddControllersWithViews();

// Add MVC middleware
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
```

### Razor Pages

```csharp
// Add Razor Pages services
builder.Services.AddRazorPages();

// Add Razor Pages middleware
app.MapRazorPages();
```

### Blazor Server

```csharp
// Add Blazor services
builder.Services.AddServerSideBlazor();

// Add Blazor middleware
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
```

### SignalR

```csharp
// Add SignalR services
builder.Services.AddSignalR();

// Add SignalR middleware
app.MapHub<ChatHub>("/chatHub");
```

## 📦 Package Management

### Centralized Package Versions

Package versions are managed centrally in `template.Directory.packages.props`:

- **Serilog.AspNetCore**: 9.0.0
- **Serilog.Sinks.Console**: 6.0.0
- **Serilog.Sinks.File**: 7.0.0

### Adding New Packages

1. Add package reference to `AspNetCoreBase.csproj`
2. Add version to `template.Directory.packages.props` (if not already present)

## 🐳 Container Support

The template includes comprehensive container support with automatic detection, optimized configuration, and development workflows.

**📖 See [CONTAINER_GUIDE.md](CONTAINER_GUIDE.md) for detailed container development instructions.**

### Quick Start

```bash
# Development with Docker launch profile
dotnet run --launch-profile Docker

# Build and run with Docker
docker build -t aspnetcore-base .
docker run -p 5000:80 -p 7113:443 aspnetcore-base
```

## 🔍 Monitoring and Debugging

### Log Files

Logs are written to `logs/log-YYYY-MM-DD.txt` with rolling retention:

- **Development**: 7 days retention
- **Production**: 31 days retention
- **Container**: Console-only logging

### Request Logging

**Optional** - Uncomment in `Program.cs` to enable:

```csharp
app.AddSerilogRequestLogging();
```

When enabled, every HTTP request is logged with:

- Request method and path
- Response status code
- Processing time
- Correlation ID

### Environment Detection

The application logs its environment at startup:

- Development/Production/Container detection
- Machine name and environment details
- Configuration validation results

## 🧪 Testing

### Test Project Template

Use `template.TestProject.csproj` as a starting point for your test projects:

- **xUnit v3** for testing
- **FluentAssertions** for readable assertions
- **NSubstitute** for mocking

## 📋 Best Practices

### Configuration Validation

Add required configuration checks in `StartupValidation.cs`:

```csharp
public static void ValidateAndLog(IConfiguration config, IHostEnvironment env)
{
    // Add your validation logic here
    string? requiredValue = config["MyRequiredSetting"];
    if (string.IsNullOrWhiteSpace(requiredValue))
    {
        throw new InvalidOperationException("Missing required configuration: MyRequiredSetting");
    }
}
```

### Adding Custom Middleware

Create extension methods in appropriate namespaces:

```csharp
// AspNetCoreBase/Custom/CustomExtensions.cs
public static class CustomExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        // Your middleware logic
        return app;
    }
}
```

## 🔄 Migration from Other Templates

### From Minimal API Template

- ✅ Already using Minimal API approach
- ✅ Enhanced with production-ready features

### From MVC Template

- Add MVC services and middleware as shown above
- All existing infrastructure remains intact

### From Blazor Template

- Add Blazor services and middleware as shown above
- Security and logging work seamlessly with Blazor

## 📚 Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Serilog Documentation](https://serilog.net/)
- [.NET 9.0 Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Docker Documentation](https://docs.docker.com/)

## 🤝 Contributing

This template is designed to be extended and customized for your specific needs. The modular architecture makes it easy to add new features while maintaining the universal foundation.

## 📄 License

MIT License - see LICENSE file for details.
