using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Mappings;
using EmployeeManagementAPI.Middleware;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using System.Security.AccessControl;
using Azure.Identity;
//creates application builder
var builder = WebApplication.CreateBuilder(args);
//Add services
// enables controller support
builder.Services.AddControllers();

// (Optional) Swagger for quick testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//registers automapper in di container
builder.Services.AddAutoMapper(typeof(MappingProfile));
//connects app to sql server dtbase
builder.Services.AddDbContext<AppDbContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//register service and interface and using dependency injection
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<EmailService>();
//connects app to application insight telemetry
builder.Services.AddApplicationInsightsTelemetry();
//adds keyvault connectivity
//builder.Configuration.AddAzureKeyVault( new Uri 
//("https://YOURS-KEYVAULT-NAME.vault.azure.net/"), new DefaultAzureCredential());
//builds application
var app = builder.Build();
//handles global exceptions//error
app.UseMiddleware<ExceptionMiddleware>();
//swagger works only in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//checks user is authorized or not
app.UseAuthorization();
//connects urls to controllers
app.MapControllers(); // <-- IMPORTANT: maps attribute-routed controllers
app.Run();