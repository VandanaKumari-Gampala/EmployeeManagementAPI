using Xunit;
using System;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Data;

public class EmployeeServiceTests
{
    private readonly AppDbContext _context;
    private readonly EmployeeService _service;
    public EmployeeServiceTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _context = new AppDbContext(options);
        var emailService = new EmailService(null);
       // _service = new EmployeeService(null);
        _service = new EmployeeService(_context, emailService);
    }
    [Fact]
    public async Task AddEmployee_ShouldAddEmployee()
    {
        var employee = new Employee
        {
            Name = "Test",
            Role = " Developer"
        };
        var result = await _service.AddEmployee(employee);
        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }
    //GET ALL
    [Fact]
    public async Task GetEmployees_ShouldReturnList()
    {
       _context.Employees.Add(new Employee { Name = "A", Role= "Dev"});
        _context.Employees.Add(new Employee { Name = "B", Role = "QA"});
        await _context.SaveChangesAsync();
        var result = await _service.GetEmployees();
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }
    //Get by Id
     [Fact]
    public async Task GetEmployeeById_ShouldReturnEmployee()
    {
       var emp = new Employee {Name = "Test", Role= "Dev"};
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
        var result = await _service.GetEmployeeById(emp.Id);
        Assert.NotNull(result);
        Assert.Equal(emp.Name,result.Name);
    }
    //update
     [Fact]
    public async Task UpdateEmployee_ShouldUpdateEmployee()
    {
       var emp = new Employee {Name = "old", Role= "Dev"};
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
        var updated = new Employee{ Name = "New", Role = "Manager"};
        var result = await _service.UpdateEmployee(emp.Id,updated);
        Assert.NotNull(result);
        Assert.Equal("New",result.Name);
    }
    //Delete
     [Fact]
    public async Task DeleteEmployee_ShouldDeleteEmployee()
    {
       var emp = new Employee {Name = "Delete", Role= "Dev"};
        _context.Employees.Add(emp);
        await _context.SaveChangesAsync();
       
        var result = await _service.DeleteEmployee(emp.Id);
        Assert.True(result);
        
    }

}