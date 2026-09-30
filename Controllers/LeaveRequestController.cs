using EmployeeManagementAPI.Data;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeManagementAPI.Controllers
{
    // API Route
    // URL will be: api/LeaveRequest
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        // Database context object
        private readonly AppDbContext _context;

        // Constructor Dependency Injection
        public LeaveRequestController(AppDbContext context)
        {
            _context = context;
        }

        // ==================================================
        // CREATE LEAVE REQUEST
        // POST: api/LeaveRequest
        // ==================================================
        [HttpPost]
        public IActionResult CreateLeaveRequest(LeaveRequest request)
        {
            // New leave request will always start as Pending
            request.Status = "Pending";

            // Add leave request to database
            _context.LeaveRequests.Add(request);

            // Save changes to database
            _context.SaveChanges();

            // Return created leave request
            return Ok(request);
        }

        // ==================================================
        // APPROVE LEAVE REQUEST
        // PUT: api/LeaveRequest/approve/1
        // ==================================================
        [HttpPut("approve/{id}")]
        public IActionResult ApproveLeave(int id)
        {
            // Find leave request by LeaveId
            var leave = _context.LeaveRequests.Find(id);

            // Check if leave request exists
            if (leave == null)
            {
                return NotFound("Leave Request Not Found");
            }

            // Update status
            leave.Status = "Approved";

            // Save changes
            _context.SaveChanges();

            // Return updated record
            return Ok(leave);
        }

        // ==================================================
        // REJECT LEAVE REQUEST
        // PUT: api/LeaveRequest/reject/1
        // ==================================================
        [HttpPut("reject/{id}")]
        public IActionResult RejectLeave(int id)
        {
            // Find leave request by LeaveId
            var leave = _context.LeaveRequests.Find(id);

            // Check if leave request exists
            if (leave == null)
            {
                return NotFound("Leave Request Not Found");
            }

            // Update status
            leave.Status = "Rejected";

            // Save changes
            _context.SaveChanges();

            // Return updated record
            return Ok(leave);
        }

        // ==================================================
        // GET EMPLOYEE LEAVE HISTORY
        // GET: api/LeaveRequest/history/1
        // ==================================================
        [HttpGet("history/{employeeId}")]
        public IActionResult GetLeaveHistory(int employeeId)
        {
            // Fetch all leaves for selected employee
            var history = _context.LeaveRequests
                                  .Where(l => l.EmployeeId == employeeId)
                                  .ToList();



            // Return leave history
            return Ok(history);
        }
    }
}