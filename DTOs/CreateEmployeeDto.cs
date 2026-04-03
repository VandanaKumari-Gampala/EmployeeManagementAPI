using System.ComponentModel.DataAnnotations;
namespace EmployeeManagementAPI.DTOs
{
    public class CreateEmployeeDto
    {
       [Required]
       [MaxLength(50)]
        public string? Name { get; set;} = string.Empty;

         [Required]
       [MaxLength(50)]
        public string? Role { get; set;} = string.Empty;
        
    }
}