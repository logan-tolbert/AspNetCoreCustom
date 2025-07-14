# Container Development Guide

## 🐳 Container Support Overview

The ASP.NET Core Base Template includes comprehensive container support with automatic detection, optimized configuration, and development workflows.

## 🔍 Container Detection

The application automatically detects when running in a container:

- **Environment Variable**: Sets `DOTNET_RUNNING_IN_CONTAINER=true`
- **Configuration Loading**: Automatically loads `appsettings.Container.json`
- **Logging Optimization**: Switches to console-only logging for containerized environments
- **Security**: Maintains security headers and HTTPS enforcement

### How It Works

```csharp
// In Program.cs
var isContainerized = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
if (isContainerized)
{
    builder.Configuration.AddJsonFile("appsettings.Container.json", optional: true);
}
```

## 🚀 Container Development

### Using Docker Launch Profile

The template includes a **Docker** launch profile for easy container development:

```bash
# Run with Docker launch profile
dotnet run --launch-profile Docker
```

This automatically:

- Sets `DOTNET_RUNNING_IN_CONTAINER=true`
- Uses container-specific configuration
- Optimizes logging for containerized environment

### Manual Docker Commands

```bash
# Build the container
docker build -t aspnetcore-base .

# Run the container
docker run -p 5000:80 -p 7113:443 aspnetcore-base

# Run with environment variables
docker run -p 5000:80 -p 7113:443 -e ASPNETCORE_ENVIRONMENT=Production aspnetcore-base
```

## 📁 Container Configuration

### appsettings.Container.json

```json
{
  "Serilog": {
    "Using": ["Serilog.Sinks.Console"],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "Console",
        "Args": {
          "outputTemplate": "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}"
        }
      }
    ],
    "Enrich": ["FromLogContext"]
  }
}
```

### Key Container Optimizations

- **Console-only logging** - No file logging in containers
- **Simplified output format** - Optimized for container logs
- **Reduced verbosity** - Warning level for Microsoft/System logs
- **No file dependencies** - Works in read-only container filesystems

## 🐳 Dockerfile

### Complete Dockerfile Example

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AspNetCoreBase/AspNetCoreBase.csproj", "AspNetCoreBase/"]
RUN dotnet restore "AspNetCoreBase/AspNetCoreBase.csproj"
COPY . .
WORKDIR "/src/AspNetCoreBase"
RUN dotnet build "AspNetCoreBase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AspNetCoreBase.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AspNetCoreBase.dll"]
```

### Dockerfile Features

- **Multi-stage build** - Optimized for size and security
- **.NET 9.0 runtime** - Latest stable version
- **Port exposure** - HTTP (80) and HTTPS (443)
- **Non-root user** - Security best practice
- **Optimized layers** - Efficient caching

## 🔧 Container Development Workflow

### 1. Development with Docker

```bash
# Start container development
dotnet run --launch-profile Docker

# Verify container detection
# Check logs for "Starting in Development on [machine]" message
# Verify console-only logging
```

### 2. Build and Test

```bash
# Build the container
docker build -t my-app .

# Test locally
docker run -p 5000:80 my-app

# Test with different environments
docker run -p 5000:80 -e ASPNETCORE_ENVIRONMENT=Production my-app
```

### 3. Production Deployment

```bash
# Build for production
docker build -t my-app:latest .

# Run in production
docker run -d -p 80:80 -p 443:443 my-app:latest

# With environment variables
docker run -d -p 80:80 -p 443:443 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DOTNET_RUNNING_IN_CONTAINER=true \
  my-app:latest
```

## 📊 Container Monitoring

### Log Access

```bash
# View container logs
docker logs <container-id>

# Follow logs in real-time
docker logs -f <container-id>

# View logs with timestamps
docker logs -t <container-id>
```

### Health Checks

The application includes graceful shutdown handling:

```bash
# Graceful shutdown
docker stop <container-id>

# Force shutdown
docker kill <container-id>
```

## 🔍 Troubleshooting

### Common Issues

#### Container Won't Start

```bash
# Check if port is already in use
netstat -ano | findstr :5000

# Use different ports
docker run -p 8080:80 my-app
```

#### Logs Not Appearing

```bash
# Verify container detection
docker exec <container-id> env | grep DOTNET_RUNNING_IN_CONTAINER

# Check if appsettings.Container.json is loaded
docker exec <container-id> cat appsettings.Container.json
```

#### Performance Issues

```bash
# Check container resource usage
docker stats <container-id>

# Monitor memory usage
docker stats --format "table {{.Container}}\t{{.CPUPerc}}\t{{.MemUsage}}"
```

### Debugging

#### Interactive Debugging

```bash
# Run container interactively
docker run -it --rm -p 5000:80 my-app

# Attach to running container
docker exec -it <container-id> /bin/bash
```

#### Environment Variables

```bash
# Set custom environment variables
docker run -p 5000:80 \
  -e ASPNETCORE_ENVIRONMENT=Development \
  -e DOTNET_RUNNING_IN_CONTAINER=true \
  -e CUSTOM_SETTING=value \
  my-app
```

## 🚀 Production Considerations

### Security

- **HTTPS enforcement** - Enabled in production
- **Security headers** - Applied to all responses
- **Non-root user** - Run container as non-root
- **Read-only filesystem** - Where possible

### Performance

- **Console logging** - Optimized for container logs
- **Graceful shutdown** - Proper resource cleanup
- **Memory optimization** - Efficient .NET 9.0 runtime
- **Layer caching** - Optimized Dockerfile

### Monitoring

- **Structured logging** - JSON format for log aggregation
- **Health endpoints** - Ready for health checks
- **Metrics** - Request timing and correlation IDs
- **Environment detection** - Clear environment identification

## 📚 Additional Resources

- [Docker Documentation](https://docs.docker.com/)
- [.NET Container Documentation](https://docs.microsoft.com/en-us/dotnet/core/docker/)
- [ASP.NET Core Container Best Practices](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)
- [Serilog Container Logging](https://github.com/serilog/serilog/wiki/Enrichment)

## 🤝 Contributing

When extending the container support:

1. **Test container detection** - Verify `DOTNET_RUNNING_IN_CONTAINER` works
2. **Update container config** - Modify `appsettings.Container.json` as needed
3. **Document changes** - Update this guide for new features
4. **Test in container** - Always test with actual Docker containers
