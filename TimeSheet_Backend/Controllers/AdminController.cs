using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{

  [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SignupContext _context;
        public AdminController(SignupContext context) 
        {
            _context = context;
        }
        [HttpGet("getAllProjects")]

        public async Task<ActionResult<List<ProjectDTO>>> GetAllProjects()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var projects = await _context.Projects
                .Select(a => new ProjectDTO { ProjectId = a.ProjectId, ProjectName = a.ProjectName })
                .ToListAsync();

            // Return the list of activities
            return Ok(projects);
        }
        [HttpGet("getAllActivities")]
        public async Task<ActionResult<List<ActivityDTO>>> GetAllActivities()
        {
            // Query the Activities table and fetch ActivityId and ActivityName using Entity Framework
            var activities = await _context.Activities
                .Select(a => new ActivityDTO { ActivityId = a.ActivityId, ActivityName = a.ActivityName })
                .ToListAsync();

            // Return the list of activities
            return Ok(activities);
        }

        [HttpGet("getAllUsers")]

        public async Task<IActionResult> GetAllUsers()
        {
            // Fetch all projects from the database
            List<User> users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpDelete("deleteProject")]

        public async Task<IActionResult> DeleteProject(Project1 request)
        {
            var record = await _context.Projects.FirstOrDefaultAsync(p => p.ProjectName == request.ProjectName);
            _context.Projects.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Project deleted successfully.");
        }

        [HttpDelete("deleteActivity")]

        public async Task<IActionResult> DeleteActivity(Activity1 request)
        {
            var record = await _context.Activities.FirstOrDefaultAsync(p => p.ActivityName == request.ActivityName);
            _context.Activities.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpGet("getDataForOneWeek")]
        public async Task<ActionResult<TimeSheetDTO>> GetTimesheetForOneWeek(int userid, [FromQuery] DateTime inputDate)
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
                .Where(t => t.CreatedDate.Date >= startOfWeek.Date && t.CreatedDate.Date <= endOfWeek.Date && t.UserId == userid)
                .ToListAsync();

            return Ok(timeSheetData);
        }

       
        public class TimeSheetDataDto
        {

            public string ProjectName { get; set; }
            public string ActivityName { get; set; }
            public string Username { get; set; }
            public string Task { get; set; }
            public int HoursWorked { get; set; }
            public DateTime Date { get; set; }
        }
        [HttpDelete("deleteUserTimesheetByTimesheetId")]
        public async Task<IActionResult> DeleteRecord(int Timesheetid)
        {
            

            // Step 2: Find timesheets associated with the user using the TimesheetId
            var record = await _context.TimeSheets
                .Where(r => r.TimeSheetId == Timesheetid)
                .FirstOrDefaultAsync();

            if (record == null)
            {
                return NotFound("Record not found.");
            }

            // Remove the record from the context
            _context.TimeSheets.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpDelete("deleteUserByUserId")]
        public async Task<IActionResult> DeleteUser(int userid)
        {
            // Find the user in the database
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.UserId == userid);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }
           

          

            // Remove the user from the database
            _context.Users.Remove(userRecord);
            await _context.SaveChangesAsync();

            return Ok("User deleted successfully.");
        }
 


        [HttpGet("getRecordsB/WTwoDates")]
        public async Task<ActionResult<TimeSheetDTO>> GetTimesheetsForOneWeek(DateTime startDate,DateTime endDate)
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

                            .Where(t => t.CreatedDate.Date >= startDate.Date && t.CreatedDate.Date < endDate.Date)
                            .ToListAsync();

            return Ok(userRecords);
        }

         [HttpGet("getAllUserRecords")]
         public async Task<ActionResult<List<TimeSheetDTO>>> GetAllUserRecords()
         {
             var timeSheetData = await _context.TimeSheets
                 .Join(_context.Users, t => t.UserId, u => u.UserId, (t, u) => new { t, u })
                 .Join(_context.Projects, tu => tu.t.ProjectId, p => p.ProjectId, (tu, p) => new { tu.t, tu.u, p })
                 .Join(_context.Activities, tup => tup.t.ActivityId, a => a.ActivityId, (tup, a) => new
                 {
                     tup.t.TimeSheetId,
                     tup.p.ProjectName,
                     a.ActivityName,
                     tup.u.Username,
                     tup.t.task,
                     tup.t.hours,
                     tup.t.CreatedDate
                 })
                 .ToListAsync();

             return Ok(timeSheetData);
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




        [HttpPost("addProject")]
        public async Task<IActionResult> AddProject(Project1 request)

        {
           

            var newActivity = new Project
            {
                ProjectName = request.ProjectName
               

                // You can add more properties here as needed
            };


            _context.Projects.Add(newActivity);
            await _context.SaveChangesAsync();




            return Ok("Project added successfully");
        }

        [HttpPost("addActivity")]
        public async Task<IActionResult> AddActivity(Activity1 request)

        {
           

            var newActivity = new Activity
            {
                ActivityName = request.ActivityName


                // You can add more properties here as needed
            };


            _context.Activities.Add(newActivity);
            await _context.SaveChangesAsync();




            return Ok("Project added successfully");
        }
        [HttpGet("getUserDataForOneMonth")]
        public async Task<ActionResult<TimeSheetDTO>> GetTimesheetsForOneMonth(int userid, [FromQuery] DateTime inputDate)
        {


            // Calculate the end date as one week from the start date
            DateTime date1 = new DateTime(inputDate.Year, inputDate.Month, 01);
            DateTime date2 = new DateTime(inputDate.Year, inputDate.Month, 31);

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
                .Where(t => t.CreatedDate.Date >= date1.Date && t.CreatedDate.Date <= date2.Date && t.UserId == userid)
                .ToListAsync();

            return Ok(timeSheetData);

        }
    }
}
