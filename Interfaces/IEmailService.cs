using System.Threading.Tasks;
using EmployeeManagementAPI.Models;
namespace EmployeeManagementAPI.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel email);
    }
}