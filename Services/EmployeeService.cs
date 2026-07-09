using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Interfaces;
using Microsoft.Extensions.Logging;
namespace EmployeeManagementAPI.Services
{

public class EmployeeService : IEmployeeService
{
    private readonly AppDbContext _context;
    private readonly EmailService emailService;
    private readonly ILogger<EmployeeService> _logger;
    public EmployeeService(AppDbContext context, EmailService emailService, ILogger<EmployeeService> logger)
    {
        _context = context;
        this.emailService = emailService;
        _logger = logger;
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
    //add a new employee to DbContext
    public async Task<Employee>AddEmployee( Employee employee)
    {
            try
            {
              _context.Employees.Add(employee);  
              await _context.SaveChangesAsync();
     _logger.LogInformation("Employee added successfully");
//call email service
//call email service
     await
     emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee {employee.Name} added as {employee.Role}"});
     
     return employee;
    }
    catch(Exception ex)
            {
                _logger.LogError(ex, "Error while adding employee");
                throw;
            }
    }
    //update employee
     public async Task<Employee?>UpdateEmployee(int id, Employee updatedEmployee)
    {
        try{
     var employee = await _context.Employees.FindAsync(id);
     if (employee == null)
     return null; 
     employee.Name = updatedEmployee.Name;
     employee.Role = updatedEmployee.Role;
     await _context.SaveChangesAsync();
      _logger.LogInformation("Employee updated successfully");
      await
     emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee {employee.Name} updated successfully"});
     
     return employee;
        }
        catch(Exception ex)
            {
                _logger.LogError(ex,"Error while updating employee");
                throw;
            }
    }
    //delete employee
     public async Task<bool>DeleteEmployee(int id)
    {
            try
            {
     var employee = await _context.Employees.FindAsync(id);
      if (employee == null)
      return false;
      _context.Employees.Remove(employee);
      await _context.SaveChangesAsync();
       _logger.LogInformation("Employee deleted successfully");
       await
     emailService.SendEmailAsync(new EmailModel
     {
         To = "test@company.com",
         Subject = "Employee Created",
         Body = $"Employee with ID {id} deleted successfully"});
     
      return true;
    }
    catch(Exception ex)
            {
                _logger.LogError(ex,"Error while deleting employee");
                throw;
            }
}
}
}