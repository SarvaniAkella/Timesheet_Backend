using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class NewUserController : ControllerBase
    {
        private readonly SignupContext _context;

        public NewUserController(SignupContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllProjects")]

        public async Task<ActionResult<List<Project>>> GetAllProjects()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var activities = await _context.Projects
                .Select(a => new Project{ ProjectId = a.ProjectId, ProjectName = a.ProjectName })
                .ToListAsync();

            // Return the list of activities
            return Ok(activities);
        }
        [HttpGet("GetAllActivities")]
        public async Task<ActionResult<List<Activity>>> GetAllActivities()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var activities = await _context.Activities
                .Select(a => new Activity { ActivityId = a.ActivityId, ActivityName = a.ActivityName })
                .ToListAsync();

            // Return the list of activities
            return Ok(activities);
        }
        [HttpPost("save")]
        public async Task<IActionResult> SaveUser(int userid ,TimeSheet request)
             
        {
            /* var userActivities = await _context.Users
                      .Where(activity => (activity.Email == email))
                      .ToListAsync();// Filter by the date part only*/
          
            var newActivity = new TimeSheet
            {
                ProjectId = request.ProjectId,
                ActivityId = request.ActivityId,
                task = request.task,
                hours = request.hours,
                CreatedDate = request.CreatedDate,
                
                UserId = userid

                // You can add more properties here as needed
            };


            _context.TimeSheets.Add(newActivity);
            await _context.SaveChangesAsync();
          



            return Ok("Activity added successfully");
        }

        [HttpPut("EditTask")]
        public async Task<IActionResult> Edit(int userid, int index,TimeSheet model)
        {
            var user = await _context.Users.FindAsync(userid);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var timesheet = await _context.TimeSheets
                     .Where(t => t.UserId==userid)
                     .ToListAsync();
            timesheet[index].ProjectId = model.ProjectId;
            timesheet[index].ActivityId = model.ActivityId;
            timesheet[index].task = model.task;
            timesheet[index].hours = model.hours;
            timesheet[index].CreatedDate = model.CreatedDate;

            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRecord(int userid,int index)
        {
            // Find the record by ID and email
            var record = await _context.TimeSheets.Where(r => r.UserId == userid).ToListAsync();

            if (record == null)
            {
                return NotFound("Record not found.");
            }

            // Remove the record from the context
            _context.TimeSheets.Remove(record[index]);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpGet("GetUserData")]
        public async Task<ActionResult<TimeSheet>> ReadData(int userid)
        {

            if (_context.TimeSheets == null)
            {
                return NotFound();
            }
            var userRecords = await _context.TimeSheets
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
                       .Where(r =>  (r.UserId == userid)) // Filter by the date part only
                     .ToListAsync();
            if (userRecords.Count <= 0)
            {
                return NotFound();
            }



            return Ok(userRecords);

        }
        [HttpGet("GetUserDataByDate")]
        public async Task<ActionResult<TimeSheet>> GetDataByDate(int userid, DateTime date)
        {

            if (_context.TimeSheets == null)
            {
                return NotFound();
            }
            var userRecords = await _context.TimeSheets
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
                      .Where(r => (r.UserId == userid) && (r.CreatedDate.Date == date.Date) )// Filter by the date part only
                     .ToListAsync();
            if (userRecords.Count <= 0)
            {
                return NotFound();
            }



            return Ok(userRecords);

        }


        [HttpGet("GetUserDataForWeek")]
        public async Task<ActionResult<TimeSheet>> GetTimesheetsForOneWeek(int userid, [FromQuery] DateTime startDate)
        {
            // Calculate the end date as one week from the start date
            DateTime endDate = startDate.AddDays(7);

            // Fetch timesheet records for the specified user and within the date range
            //List<TimeSheet> timesheets = _context.TimeSheets
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
                .Where(t => t.CreatedDate.Date >= startDate.Date && t.CreatedDate.Date < endDate.Date && t.UserId==userid)
                .ToListAsync();

            return Ok(timeSheetData);
        }


        [HttpGet("GetTotalHoursWorked")]

        public async Task<ActionResult<int>> GetTotalHoursWorked(int userid, DateTime date)
        {

            if (_context.TimeSheets == null)
            {
                return NotFound();
            }
            var userRecords = await _context.TimeSheets
                     .Where(r => (r.UserId == userid) && (r.CreatedDate.Date == date.Date))// Filter by the date part only
                     .ToListAsync();
            if (userRecords.Count <= 0)
            {
                return NotFound();
            }
            int totalHoursWorked = 0;
            
            foreach (var t in userRecords)
            {
                totalHoursWorked += t.hours;
               
            }


            return Ok(totalHoursWorked);

        }




    }
}
