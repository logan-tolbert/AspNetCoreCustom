# ASP.NET Core Middleware Placement Guide

A comprehensive guide for placing ASP.NET Core middleware and features in the correct order within the AspNetCoreBase template.

## 🎯 Overview

All Asp.Net Core middleware should be placed after `UseBaseSecurity`.

Excusions:

- `app.UseExceptionHandler("/Error");`
- `app.MapHealthChecks("/health");`

This guide shows you where to place ASP.NET Core features in relation to the template's `UseBaseSecurity` middleware. Following the correct order ensures:

- **Security is applied early** to protect all requests
- **Performance is optimized** by avoiding unnecessary processing
- **Functionality works correctly** without conflicts

## 🔧 Common ASP.NET Core Features

### Static Files

```csharp
// ✅ CORRECT - After UseBaseSecurity
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();

// ❌ INCORRECT - Before UseBaseSecurity
app.UseStaticFiles();
app.UseBaseSecurity(app.Environment);
```

**Why:** Static files should be served after security headers are applied to ensure all static content is protected.

### Authentication & Authorization

```csharp
// ✅ CORRECT - After static files, before your endpoints
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/", () => "Hello World!");
```

**Why:** Authentication should process after static files (to avoid unnecessary auth overhead for static content) but before your application logic.

### MVC Controllers

```csharp
// ✅ CORRECT - After all middleware, before endpoints
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
```

### Razor Pages

```csharp
// ✅ CORRECT - After all middleware
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapRazorPages();
```

### Blazor Server

```csharp
// ✅ CORRECT - After all middleware
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
```

### SignalR

```csharp
// ✅ CORRECT - After all middleware
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.MapHub<ChatHub>("/chatHub");
```

### CORS

```csharp
// ✅ CORRECT - After routing, before endpoints
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseCors();
app.MapGet("/", () => "Hello World!");
```

### Sessions

```csharp
// ✅ CORRECT - After routing, before endpoints
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseSession();
app.MapGet("/", () => "Hello World!");
```

## 🚨 Exceptions: When to Place Middleware Before UseBaseSecurity

Some middleware should be placed **before** `UseBaseSecurity`:

### Exception Handling

```csharp
// ✅ CORRECT - Exception handling should be very early
app.UseExceptionHandler("/Error");
app.UseBaseSecurity(app.Environment);
```

### Health Checks

```csharp
// ✅ CORRECT - Health checks might need to bypass security
app.MapHealthChecks("/health");
app.UseBaseSecurity(app.Environment);
```

### Custom Middleware That Modifies Request Pipeline

```csharp
// ✅ CORRECT - If your middleware needs to run before security
app.Use(async (context, next) =>
{
    // Your custom logic that needs to run before security
    await next();
});
app.UseBaseSecurity(app.Environment);
```

## 📝 Why This Order Matters

### 1. Security First

- `UseBaseSecurity` applies HTTPS redirection and security headers
- These must be applied early to protect all subsequent requests
- Security headers need to be set before any content is served

### 2. Static Files After Security

- Static files should be served after security headers are applied
- This ensures all static content gets the security headers
- Prevents serving unprotected static content

### 3. Authentication After Static Files

- Authentication should process after static files
- This allows static files to be served without authentication overhead
- Improves performance for static content

### 4. Authorization After Authentication

- Authorization depends on authentication
- Must come after `UseAuthentication()`
- Protects your application endpoints

### 5. Routing and Endpoints Last

- Routing and your application endpoints should be last
- This ensures all middleware has processed the request
- Your business logic runs after all infrastructure concerns

## 🔍 Debugging Middleware Order Issues

### Common Problems

**Problem:** Static files not getting security headers

```csharp
// ❌ Wrong order
app.UseStaticFiles();
app.UseBaseSecurity(app.Environment);
```

**Solution:** Place static files after security

```csharp
// ✅ Correct order
app.UseBaseSecurity(app.Environment);
app.UseStaticFiles();
```

**Problem:** Authentication not working

```csharp
// ❌ Wrong order
app.UseAuthentication();
app.UseStaticFiles();
```

**Solution:** Place authentication after static files

```csharp
// ✅ Correct order
app.UseStaticFiles();
app.UseAuthentication();
```

### Testing Your Middleware Order

1. **Check security headers** in browser dev tools
2. **Verify static files** are served with security headers
3. **Test authentication** flows
4. **Monitor performance** - correct order should improve performance

## 🎯 Quick Reference

### Always After UseBaseSecurity

- `app.UseStaticFiles()`
- `app.UseAuthentication()`
- `app.UseAuthorization()`
- `app.UseRouting()`
- `app.UseSession()`
- `app.UseCors()`
- `app.MapControllers()`
- `app.MapRazorPages()`
- `app.MapBlazorHub()`
- `app.MapHub<T>()`
- `app.MapGet()`, `app.MapPost()`, etc.

### Sometimes Before UseBaseSecurity

- `app.UseExceptionHandler()`
- `app.MapHealthChecks()`
- Custom middleware that modifies request pipeline

### Never Before UseBaseSecurity

- Authentication/Authorization
- Static files
- Application endpoints
- Session/CORS (unless you have a specific reason)

## 📚 Related Documentation

- [Quick Start Guide](QUICK_START.md) - Getting started with the template
- [Template Summary](TEMPLATE_SUMMARY.md) - Overview of template features
- [Container Guide](CONTAINER_GUIDE.md) - Container development
- [Project Creation](PROJECT_CREATION.md) - Creating new projects from template
