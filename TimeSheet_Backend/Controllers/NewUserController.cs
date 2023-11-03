using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
//using System.Web.Http;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{

    [Authorize]
    [Route("[controller]")]
    [ApiController]
   
    public class NewUserController : ControllerBase
    {
        private readonly SignupContext _context;

        public NewUserController(SignupContext context)
        {
            _context = context;
        }
        [HttpGet("getAllProjects")]

        public async Task<ActionResult<List<ProjectDTO>>> GetAllProjects()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var projects = await _context.Projects
                .Select(a => new ProjectDTO{ ProjectId = a.ProjectId, ProjectName = a.ProjectName })
                .ToListAsync();

            // Return the list of activities
            return Ok(projects);
        }
        [HttpGet("getAllActivities")]
        public async Task<ActionResult<List<ActivityDTO>>> GetAllActivities()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var activities = await _context.Activities
                .Select(a => new ActivityDTO { ProjectId = a.ProjectId, ActivityId = a.ActivityId, ActivityName = a.ActivityName })
                .ToListAsync();



            // Return the list of activities
            return Ok(activities);
        }

        [HttpPost("addTask")]
        public async Task<IActionResult> SaveUser(TimeSheet1 request)
             
        {
            
          
            var newActivity = new TimeSheet
            {
                ProjectId = request.ProjectId,
                ActivityId = request.ActivityId,
                task = request.task,
                hours = request.hours,
                CreatedDate = request.CreatedDate,
                
                UserId = request.UserId

                // You can add more properties here as needed
            };


            _context.TimeSheets.Add(newActivity);
            await _context.SaveChangesAsync();
          



            return Ok("Activity added successfully");
        }

        [HttpPut("editTaskByTimesheetid")]
        public async Task<IActionResult> Edit(TimeSheetDTO model)
        {
            var user = await _context.TimeSheets.FindAsync(model.TimeSheetId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var timesheet = await _context.TimeSheets
                     .Where(t => t.TimeSheetId == user.TimeSheetId).FirstOrDefaultAsync();
                     
            timesheet.ProjectId = model.ProjectId;
            timesheet.ActivityId = model.ActivityId;
            timesheet.task = model.task;
            timesheet.hours = model.hours;
            timesheet.CreatedDate = model.CreatedDate;

            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("deleteTaskByTimesheetid")]
        public async Task<IActionResult> DeleteRecord(int Timesheetid)
        {
            // Find the record by ID and email
            var record = await _context.TimeSheets.Where(r => r.TimeSheetId == Timesheetid).FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound("Record not found.");
            }

            // Remove the record from the context
            _context.TimeSheets.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpGet("getUserDataByUserid")]
        public async Task<ActionResult<TimeSheetDTO>> ReadData(int userid)
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
        [HttpGet("getUserDataByDate")]
        public async Task<ActionResult<TimeSheetDTO>> GetDataByDate(int userid, DateTime date)
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


        [HttpGet("getUserDataForWeek")]
        public async Task<ActionResult<TimeSheetDTO>> GetTimesheetsForOneWeek(int userid, [FromQuery] DateTime inputDate)
        {
            // Calculate the end date as one week from the start date
            DateTime startOfWeek = inputDate.Date.AddDays(-(int)inputDate.DayOfWeek);
            DateTime endOfWeek = startOfWeek.AddDays(6);

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
                .Where(t => t.CreatedDate.Date >= startOfWeek.Date && t.CreatedDate.Date <= endOfWeek.Date && t.UserId==userid)
                .ToListAsync();

            return Ok(timeSheetData);
        }
        [HttpGet("getRecordsWithinRange")]
        public async Task<ActionResult<TimeSheetDTO>> GetTimesheetsoneweek(DateTime startDate, DateTime endDate)
        {
            // Calculate the end date as one week from the start date
            //DateTime endDate = startDate.AddDays(7);

            // Fetch timesheet records for the specified user and within the date range
            var userRecords = await _context.TimeSheets
                          .Join(_context.Users, t => t.UserId, u => u.UserId, (t, u) => new { t, u })
                          .Join(_context.Projects, tu => tu.t.ProjectId, p => p.ProjectId, (tu, p) => new { tu.t, tu.u, p })
                          .Join(_context.Activities, tup => tup.t.ActivityId, a => a.ActivityId, (tup, a) => new
                          {

                              tup.p.ProjectName,
                              a.ActivityName,
                              tup.u.Username,
                              tup.u.UserId,
                              tup.t.task,
                              tup.t.hours,
                              tup.t.CreatedDate
                          })

                            .Where(t => t.CreatedDate.Date >= startDate.Date && t.CreatedDate.Date <= endDate.Date)
                            .ToListAsync();

            return Ok(userRecords);
        }


        [HttpGet("getTotalHoursWorked")]

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
            float totalHoursWorked = 0;
            
            foreach (var t in userRecords)
            {
                totalHoursWorked += t.hours;
               
            }


            return Ok(totalHoursWorked);

        }




    }
}
