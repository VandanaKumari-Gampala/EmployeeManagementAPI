using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;
namespace EmployeeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {    
        }
        //represent table in database
        public DbSet<Employee> Employees { get;set;}
        //optional:configure database models and add default data.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           modelBuilder.Entity<Employee>().HasData(
            new Employee { Id =1,Name ="Ravi",Role = "Developer"},
            new Employee { Id =2,Name ="sita",Role = "Tester"}
           );
        }
    }
}
#pragma warning restore 