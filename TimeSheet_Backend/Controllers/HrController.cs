using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;
using static TimeSheet_Backend.Controllers.AdminController;

namespace TimeSheet_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HrController : ControllerBase
    {
        private readonly SignupContext _context;
        public HrController(SignupContext context)
        { 
            _context = context;
        }

        [HttpGet("getAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            // Fetch all projects from the database
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpGet("getAllUserRecords")]

        public async Task<IActionResult> GetAllRecords()
        {
            // Fetch all projects from the database
            List<TimeSheet> users = await _context.TimeSheets.ToListAsync();
            return Ok(users);
        }
        [HttpGet("getTimeSheetsByUserId")]
        public async Task<ActionResult<List<TimeSheetDataDto>>> GetTimeSheetsByUserId(int userId)
        {
            var timeSheetData = (from t in _context.TimeSheets
                                 where t.UserId == userId
                                 join u in _context.Users on t.UserId equals u.UserId
                                 join p in _context.Projects on t.ProjectId equals p.ProjectId
                                 join a in _context.Activities on t.ActivityId equals a.ActivityId
                                 select new TimeSheetDataDto
                                 {
                                     ProjectName = p.ProjectName,
                                     ActivityName = a.ActivityName,
                                     Username = u.Username,
                                     Task = t.task,
                                     HoursWorked = t.hours,
                                     Date = t.CreatedDate
                                 }).ToList();

            return Ok(timeSheetData);
        }



    }
}
