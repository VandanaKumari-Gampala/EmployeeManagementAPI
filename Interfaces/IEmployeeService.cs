using EmployeeManagementAPI.Models;
namespace EmployeeManagementAPI.Interfaces
{
public interface IEmployeeService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployeeById(int id);
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee?> UpdateEmployee(int id, Employee employee);
    Task<bool> DeleteEmployee(int id);

    
}
}