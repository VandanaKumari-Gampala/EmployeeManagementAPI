using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int Id { get; set;} 
        public string? Name {get; set; }
        public String? Role {get; set; }
       
    }
}