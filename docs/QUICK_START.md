# Quick Start Guide

## 🚀 Getting Started in 5 Minutes

### 1. Clone/Copy the Template

```bash
# Copy the AspNetCoreBase folder to your new project
cp -r AspNetCoreBase MyNewProject
cd MyNewProject
```

### 2. Update Project Name

```bash
# Rename the project folder and files
mv AspNetCoreBase.csproj MyNewProject.csproj
# Update namespace references in code files
```

### 3. Run the Application

```bash
dotnet run
```

### 4. Test Different Environments

```bash
# Local development
dotnet run

# Container development
dotnet run --launch-profile Docker

# Production simulation
ASPNETCORE_ENVIRONMENT=Production dotnet run
```

## 🔧 Common Customizations

### Add API Endpoints

```csharp
// In Program.cs, add after existing endpoints
app.MapGet("/api/health", () => new { Status = "Healthy", Timestamp = DateTime.UtcNow });
app.MapGet("/api/version", () => new { Version = "1.0.0" });
```

### Add Required Configuration

```csharp
// In StartupValidation.cs, uncomment and customize
string? requiredValue = config["MyRequiredSetting"];
if (string.IsNullOrWhiteSpace(requiredValue))
{
    throw new InvalidOperationException("Missing required configuration: MyRequiredSetting");
}
```

### Add Custom Middleware

```csharp
// Create AspNetCoreBase/Custom/CustomExtensions.cs
public static class CustomExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
    {
        return app.Use(async (context, next) =>
        {
            // Your middleware logic
            await next();
        });
    }
}

// In Program.cs, add after other middleware
app.UseCustomMiddleware();
```

## 📋 Development Workflow

### 1. Development

- Use **https** launch profile (recommended)
- Check logs in console and `logs/` folder
- Add endpoints in `Program.cs`

### 2. Testing

- Use `template.TestProject.csproj` as starting point
- Add tests for your endpoints
- Test configuration validation

### 3. Container Development

- Use **Docker** launch profile
- Verify container-specific logging
- Test with Docker build/run
- **📖 See [CONTAINER_GUIDE.md](CONTAINER_GUIDE.md) for detailed container instructions**

### 4. Production

- Set `ASPNETCORE_ENVIRONMENT=Production`
- Verify HTTPS enforcement
- Check file logging in `logs/` folder

## 🐛 Troubleshooting

### Common Issues

**Build Errors:**

```bash
dotnet clean
dotnet restore
dotnet build
```

**Port Already in Use:**

```bash
# Change ports in launchSettings.json
# Or kill existing process
netstat -ano | findstr :5000
taskkill /PID <PID> /F
```

**Container Issues:**

```bash
# Verify Docker is running
docker --version

# Check container logs
docker logs <container-id>
```

**📖 See [CONTAINER_GUIDE.md](CONTAINER_GUIDE.md) for detailed troubleshooting**

### Log Locations

- **Development**: Console + `logs/log-YYYY-MM-DD.txt`
- **Production**: `logs/log-YYYY-MM-DD.txt`
- **Container**: Console only

## 📚 Next Steps

1. **Add your business logic** - Endpoints, services, models
2. **Configure database** - Entity Framework, Dapper, etc.
3. **Add authentication** - JWT, Identity, etc.
4. **Add API documentation** - Swagger/OpenAPI
5. **Set up CI/CD** - GitHub Actions, Azure DevOps, etc.

## 🆘 Need Help?

- Check the main [README.md](README.md) for detailed documentation
- Review the [template structure](README.md#project-structure) for organization
- Use the [extension examples](README.md#extending-for-different-project-types) as templates
