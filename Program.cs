using EmployeeManagementAPI.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Interfaces;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Mappings;
using EmployeeManagementAPI.Middleware;
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
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<EmailService>();
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