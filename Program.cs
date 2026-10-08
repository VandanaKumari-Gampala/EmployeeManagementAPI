using System.Text;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Mappings;
using EmployeeManagementAPI.Middleware;
using EmployeeManagementAPI.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Microsoft.OpenApi.Models;

// Create Application Builder
var builder = WebApplication.CreateBuilder(args);

// ============================================
// ADD SERVICES
// ============================================

// Enable Controllers
builder.Services.AddControllers();

// Generate API metadata for Swagger
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration
// Includes JWT Authorize Button
builder.Services.AddSwaggerGen(c =>
{
    // API Information
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "EmployeeManagementAPI",
        Version = "v1"
    });

    // JWT Security Definition
    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter JWT Token"
        });

    // Apply JWT Globally
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
});

// AutoMapper Registration
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Database Connection
// Database Connection
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")));

// Dependency Injection

// Email Service
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailService>();

// Employee Service
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
// ============================================
// JWT AUTHENTICATION
// ============================================

builder.Services.AddAuthentication(
JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
options.TokenValidationParameters =
new TokenValidationParameters
{
    // Validate JWT Issuer
ValidateIssuer = true,

    // Validate JWT Audience
ValidateAudience = true,

    // Validate Token Expiry
ValidateLifetime = true,

    // Validate Secret Key
ValidateIssuerSigningKey = true,
    // JWT Issuer
    ValidIssuer =
builder.Configuration["Jwt:Issuer"],

    // JWT Audience
    ValidAudience =
builder.Configuration["Jwt:Audience"],

    // Secret Key
    IssuerSigningKey =
new SymmetricSecurityKey(
Encoding.UTF8.GetBytes(
builder.Configuration["Jwt:Key"]!))
};
});

// Authorization Services
builder.Services.AddAuthorization();

// ============================================
// BUILD APPLICATION
// ============================================

var app = builder.Build();

// ============================================
// MIDDLEWARE PIPELINE
// ============================================

// Global Exception Handling Middleware
app.UseMiddleware<ExceptionMiddleware>();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "EmployeeManagementAPI v1");
    });
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Authentication Middleware
app.UseAuthentication();
// Authorization Middleware
app.UseAuthorization();

// Map Controller Routes
app.MapControllers();

// Start Application
app.Run();

