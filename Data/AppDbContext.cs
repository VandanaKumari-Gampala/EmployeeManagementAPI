using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;

namespace EmployeeManagementAPI.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor used by Entity Framework Core
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Represents Employees table in the database
        public DbSet<Employee> Employees { get; set; }

        // Represents LeaveRequests table in the database
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        // Represents Users table in the database
        // Used for Login and JWT Authentication
        public DbSet<User> Users { get; set; }

        // Configure models and seed initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Employee data
            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Ravi",
                    Role = "Developer"
                },
                new Employee
                {
                    Id = 2,
                    Name = "Sita",
                    Role = "Tester"
                }
            );

            // Seed Login User data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    UserName = "admin",
                    Password = "admin123"
                }
            );

            // Configure One-to-Many Relationship
            // One Employee -> Many Leave Requests
            modelBuilder.Entity<LeaveRequest>()
                .HasOne<Employee>()
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l => l.EmployeeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}