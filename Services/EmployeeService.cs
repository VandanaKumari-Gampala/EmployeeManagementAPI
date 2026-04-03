using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Interfaces;
namespace EmployeeManagementAPI.Services
{

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;
    private readonly EmailService _emailService;
    public EmployeeService(AppDbContext context, EmailService emailService)
    {
        _context = context;
        _emailService = emailService;
    }
    //get all employees from database
    public async Task<List<Employee>>GetEmployees()
    {
        return await _context.Employees.ToListAsync();
    }
   //get employee by id
   public async Task<Employee?>GetEmployeeById(int id)
    {
        return await _context.Employees.FindAsync(id);
    }
    //inserts a new employee to DbContext
    public async Task<Employee>AddEmployee( Employee employee)
    {
     _context.Employees.Add(employee);
     await _context.SaveChangesAsync();
//call email service
     await
     _emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee {employee.Name} added as {employee.Role}"});
     
     return employee;
    }
    //update employee
     public async Task<Employee?>UpdateEmployee(int id, Employee updatedEmployee)
    {
     var employee = await _context.Employees.FindAsync(id);
     if (employee == null)
     return null;
     employee.Name = updatedEmployee.Name;
     employee.Role = updatedEmployee.Role;
     await _context.SaveChangesAsync();
      await
     _emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee {employee.Name} updated"});
     
     return employee;
    }
    //delete employee
     public async Task<bool>DeleteEmployee(int id)
    {
     var employee = await _context.Employees.FindAsync(id);
      if (employee == null)
      return false;
      _context.Employees.Remove(employee);
      await _context.SaveChangesAsync();
       await
     _emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee with ID {id} deleted"});
     
      return true;
    }
}
}