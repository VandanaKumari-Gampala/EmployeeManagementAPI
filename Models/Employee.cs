using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace EmployeeManagementAPI.Models
{
    public class Employee
    {
        public int Id { get; set;}

        [Required]
        public string? Name {get; set; }
        public String? Role {get; set; }
        public ICollection<LeaveRequest> LeaveRequests { get; set; }

    }
}