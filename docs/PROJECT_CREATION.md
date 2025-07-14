# Project Creation Guide

## 🚀 Creating New Projects from Template

This guide explains how to create new ASP.NET Core projects using your base template.

## 📋 Template Structure

### **What's in the Template**

```pt
AspNetCoreEdu/
├── AspNetCoreBase/                    # Your base template
├── template.Directory.build.props      # Shared build settings
├── template.Directory.packages.props   # Shared package versions
├── template.TestProject.csproj         # Test project template
├── README.md                          # Template documentation
├── QUICK_START.md                     # Quick start guide
├── CONTAINER_GUIDE.md                 # Container guide
├── TEMPLATE_SUMMARY.md                # Template summary
└── PROJECT_CREATION.md                # This guide
```

### **What to Copy**

- ✅ **AspNetCoreBase folder** - Core template
- ✅ **template.\* files** - Build and package configuration
- ❌ **Solution file** - Create new per project
- ❌ **Documentation** - Keep template docs separate

## 🛠️ Project Creation Process

### **Step 1: Copy Template**

```bash
# Copy the base template
cp -r AspNetCoreBase MyNewProject
cd MyNewProject

# Copy shared configuration files
cp ../template.Directory.build.props .
cp ../template.Directory.packages.props .
```

### **Step 2: Rename Project**

```bash
# Rename project file
mv AspNetCoreBase.csproj MyNewProject.csproj

# Update namespaces in code files
# Find and replace: AspNetCoreBase → MyNewProject
# Find and replace: AspNetCoreBase.Serilog → MyNewProject.Serilog
# Find and replace: AspNetCoreBase.Security → MyNewProject.Security
# Find and replace: AspNetCoreBase.ApplicationLifecycle → MyNewProject.ApplicationLifecycle
```

### **Step 3: Create Solution**

```bash
# Create new solution
dotnet new sln -n MyNewProject

# Add main project to solution
dotnet sln add MyNewProject.csproj
```

### **Step 4: Add Test Project**

```bash
# Create test project using template
cp ../template.TestProject.csproj MyNewProject.Tests.csproj

# Update test project
# Find and replace: TestProject → MyNewProject.Tests
# Find and replace: TestProject → MyNewProject.Tests

# Add test project to solution
dotnet sln add MyNewProject.Tests.csproj
```

## 📁 Recommended Project Structure

### **Simple Project**

```pt
MyNewProject/
├── MyNewProject.sln
├── MyNewProject.csproj
├── Program.cs
├── appsettings.json
├── appsettings.Development.json
├── appsettings.Container.json
├── Properties/
├── Serilog/
├── Security/
├── ApplicationLifecycle/
├── StartupValidation.cs
└── MyNewProject.Tests.csproj
```

### **Complex Project**

```pt
MyNewProject/
├── MyNewProject.sln
├── src/
│   └── MyNewProject/
│       ├── MyNewProject.csproj
│       ├── Program.cs
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       ├── appsettings.Container.json
│       ├── Properties/
│       ├── Serilog/
│       ├── Security/
│       ├── ApplicationLifecycle/
│       └── StartupValidation.cs
├── tests/
│   └── MyNewProject.Tests/
│       └── MyNewProject.Tests.csproj
├── docs/
│   ├── README.md
│   └── API.md
├── docker/
│   ├── Dockerfile
│   └── docker-compose.yml
└── scripts/
    ├── build.ps1
    └── deploy.ps1
```

## 🔧 Solution File Strategy

### **Why Create New Solutions?**

- **Flexibility** - Each project can have different structure
- **Clean template** - No solution-specific files in template
- **Multiple projects** - One template can spawn many solutions
- **Easy maintenance** - Template stays focused on core functionality

### **Solution File Best Practices**

```bash
# Create solution with descriptive name
dotnet new sln -n MyNewProject

# Add projects to solution
dotnet sln add src/MyNewProject/MyNewProject.csproj
dotnet sln add tests/MyNewProject.Tests/MyNewProject.Tests.csproj

# Add additional projects as needed
dotnet sln add src/MyNewProject.API/MyNewProject.API.csproj
dotnet sln add src/MyNewProject.Core/MyNewProject.Core.csproj
```

## 🚀 Quick Start Commands

### **Basic Project Creation**

```bash
# 1. Copy template
cp -r AspNetCoreBase MyNewProject
cd MyNewProject

# 2. Rename project
mv AspNetCoreBase.csproj MyNewProject.csproj

# 3. Update namespaces (manual find/replace)
# AspNetCoreBase → MyNewProject

# 4. Create solution
dotnet new sln -n MyNewProject
dotnet sln add MyNewProject.csproj

# 5. Test the project
dotnet run
```

### **Advanced Project Creation**

```bash
# 1. Create project structure
mkdir MyNewProject
cd MyNewProject
mkdir src tests docs docker scripts

# 2. Copy template to src
cp -r ../AspNetCoreBase src/MyNewProject
cd src/MyNewProject

# 3. Rename and update namespaces
mv AspNetCoreBase.csproj MyNewProject.csproj
# Update namespaces manually

# 4. Create solution
cd ../..
dotnet new sln -n MyNewProject
dotnet sln add src/MyNewProject/MyNewProject.csproj

# 5. Add test project
cp ../template.TestProject.csproj tests/MyNewProject.Tests/MyNewProject.Tests.csproj
dotnet sln add tests/MyNewProject.Tests/MyNewProject.Tests.csproj
```

## 📝 Namespace Update Checklist

### **Files to Update**

- ✅ **Program.cs** - Update using statements
- ✅ **SerilogExtensions.cs** - Update namespace
- ✅ **SecurityExtensions.cs** - Update namespace
- ✅ **ApplicationLifecycleExtensions.cs** - Update namespace
- ✅ **StartupValidation.cs** - Update namespace
- ✅ **Project file** - Update RootNamespace (if needed)

### **Find/Replace Patterns**

```pt
AspNetCoreBase → MyNewProject
AspNetCoreBase.Serilog → MyNewProject.Serilog
AspNetCoreBase.Security → MyNewProject.Security
AspNetCoreBase.ApplicationLifecycle → MyNewProject.ApplicationLifecycle
```

## 🧪 Testing Your New Project

### **Verify Template Features**

```bash
# Test basic functionality
dotnet run

# Test different environments
dotnet run --launch-profile https
dotnet run --launch-profile Docker

# Test build
dotnet build

# Test with production settings
ASPNETCORE_ENVIRONMENT=Production dotnet run
```

### **Verify Logging**

- Check console output for structured logs
- Verify correlation IDs in request logs
- Check logs folder for file logging (development)

### **Verify Security**

- Test HTTPS redirection (production)
- Verify security headers in responses
- Test container detection and configuration

## 📚 Next Steps

### **After Project Creation**

1. **Add your business logic** - Endpoints, services, models
2. **Configure database** - Entity Framework, Dapper, etc.
3. **Add authentication** - JWT, Identity, etc.
4. **Add API documentation** - Swagger/OpenAPI
5. **Set up CI/CD** - GitHub Actions, Azure DevOps, etc.

### **Project-Specific Customization**

- **Update README.md** - Project-specific documentation
- **Add project configuration** - Environment-specific settings
- **Configure logging** - Project-specific log levels
- **Add health checks** - Application health endpoints
- **Set up monitoring** - Application insights, etc.

## 🆘 Troubleshooting

### **Common Issues**

**Build Errors After Rename:**

```bash
# Clean and rebuild
dotnet clean
dotnet restore
dotnet build
```

**Namespace Errors:**

- Double-check all namespace updates
- Verify using statements in Program.cs
- Check project file RootNamespace

**Missing Configuration:**

- Copy template configuration files
- Verify appsettings files are present
- Check environment variables

## 📖 Additional Resources

- [README.md](README.md) - Template documentation
- [QUICK_START.md](QUICK_START.md) - Quick start guide
- [CONTAINER_GUIDE.md](CONTAINER_GUIDE.md) - Container development
- [TEMPLATE_SUMMARY.md](TEMPLATE_SUMMARY.md) - Template overview
