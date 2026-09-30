using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveId { get; set; }

        [Required(ErrorMessage = "Employee Id is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; } = "Pending";
    }
}