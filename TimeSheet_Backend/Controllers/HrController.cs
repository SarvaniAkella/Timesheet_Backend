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
        [HttpGet("GetAllUserRecordsByemail")]
        public async Task<ActionResult<List<object>>> GetAllUserRecordsByEmail(string email)
        {
            // Step 1: Find the user record with the provided email
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }
            var timeSheetData = await _context.TimeSheets
                        .Join(_context.Users, t => t.UserId, u => u.UserId, (t, u) => new { t, u })
                        .Join(_context.Projects, tu => tu.t.ProjectId, p => p.ProjectId, (tu, p) => new { tu.t, tu.u, p })
                        .Join(_context.Activities, tup => tup.t.ActivityId, a => a.ActivityId, (tup, a) => new
                        {
                            tup.t.TimeSheetId,
                            tup.p.ProjectName,
                            a.ActivityName,
                            tup.u.Username,
                            tup.u.UserId,
                            tup.t.task,
                            tup.t.hours,
                            tup.t.CreatedDate
                        })
                        .Where(data => data.UserId == userRecord.UserId)
                        .ToListAsync();

            return Ok(timeSheetData);
        }



    }
}
