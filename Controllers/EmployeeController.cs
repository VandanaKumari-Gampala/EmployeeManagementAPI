using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using EmployeeManagementAPI.Interfaces;
using AutoMapper;

namespace EmployeeManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    { 
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;
        //
        
        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
 
        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult>GetEmployees()
        {
            var employees = await _employeeService.GetEmployees();
            return Ok(employees);
        }
 
        // GET: api/Employee/1
        [HttpGet("{id}")]
        public async Task<IActionResult>GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeeById(id);
 
            if (employee == null)
                return NotFound(new { Message = "Employee not found" });
 
            return Ok(employee);
        }
 
        // POST: api/Employee
        [HttpPost]
        public  async Task<IActionResult>AddEmployee(CreateEmployeeDto dto)
        {
           var employee = _mapper.Map<Employee>(dto);
          

            var newEmployee = await _employeeService.AddEmployee(employee);
            return Ok(newEmployee);
        }
 
        // PUT: api/Employee/1
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,UpdateEmployeeDto dto)
        {
             var employee = _mapper.Map<Employee>(dto);

 
            var updatedEmployee = await _employeeService.UpdateEmployee(id,employee);
           if(updatedEmployee == null)
           return NotFound(new
           {
               Message = "Employee not found"
           });
            return Ok(updatedEmployee);
        }
 
        // DELETE: api/Employee/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var deleted = await _employeeService.DeleteEmployee(id);
 
            if (!deleted)
                return NotFound(new { Message = "Employee not found" });
            return Ok(new { Message = "Employee deleted successfully" });
        }
    }
}