using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementAPI.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context)
        {
            _context = context;

        }
        //retrieves all employees from database
        public async Task<List<Employee>>GetEmployees()
        {
            return await _context.Employees.ToListAsync();

        }
        //retrieves a single emp by id
         public async Task<Employee?>GetEmployeeById(int id)
        {
            return await 
            _context.Employees.FindAsync(id);

        }
        //inserts a new emp record
         public async Task<Employee>AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee;

        }
        //updates emp details
        public async Task<Employee?>UpdateEmployee(int id,Employee employee)
        {
            var existing = await
            _context.Employees.FindAsync(id);
            if(existing == null)
            return null;
            existing.Name = employee.Name;
            existing.Role = employee.Role;
            await _context.SaveChangesAsync();
            return existing;
        }
        //deletes existing emp
        public async Task<bool>DeleteEmployee(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if(employee == null)
            return false;
            _context.Employees.Remove(employee);
            await 
            _context.SaveChangesAsync();
            return true;
      
          }  }
}