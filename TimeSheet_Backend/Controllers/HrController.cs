using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HrController : ControllerBase
    {
        private readonly SignupContext _context;
        public HrController(SignupContext context)
        { 
            _context = context;
        }

        [HttpGet("GetAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            // Fetch all projects from the database
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("GetAllUserRecords")]

        public async Task<IActionResult> GetAllRecords()
        {
            // Fetch all projects from the database
            List<TimeSheet> users = await _context.TimeSheets.ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetTimesheetsByEmail")]
        public async Task<ActionResult<List<TimeSheet>>> GetTimesheetsByEmail(string email)
        {
            // Step 1: Find the user record with the provided email
            var userRecord = await _context.Users.FirstOrDefaultAsync(r => r.Email == email);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }

            // Step 2: Find timesheets associated with the user using the UserId
            var userTimesheets = await _context.TimeSheets
                .Where(ts => ts.UserId == userRecord.UserId)
                .ToListAsync();
         
            // Return the list of timesheets associated with the user
            return Ok(userTimesheets);
        }


    }
}
