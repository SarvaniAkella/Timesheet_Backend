using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public ActionResult<IEnumerable<string>> GetProjectNames()
        {
            var projectNames = Enum.GetNames(typeof(projectname));
            return Ok(projectNames);
        }
        [HttpGet("GetAllActivities")]

        public ActionResult<IEnumerable<string>> GetActivities()
        {
            var activitynames = Enum.GetNames(typeof(activities));
            return Ok(activitynames);
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateProject([FromBody] TaskTable taskTable)
        {
            // Set the ProjectName enum ID in the TaskTable based on the provided Projectname string
            if (!Enum.TryParse(taskTable.Activityname, out activities activityNameEnum))
            {
                return BadRequest("Invalid ActivityName.");
            }
            if (!Enum.TryParse(taskTable.Projectname, out projectname projectNameEnum))
            {
                return BadRequest("Invalid ProjectName.");
            }

            taskTable.activityid = activityNameEnum;
            taskTable.projectnameid = projectNameEnum;
            // Add the TaskTable to the context and save changes
            _context.Tasks.Add(taskTable);
            await _context.SaveChangesAsync();

            return Ok("TaskTable added successfully");
        }
        [HttpPut("EditTask")]
        public async Task<IActionResult> Edit(int id, TaskTable tasks)
        {
            if (id != tasks.Id)
            {
                return BadRequest();
            }
            _context.Entry(tasks).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRecord(int id, string user)
        {
            // Find the record by ID and email
            var record = await _context.Tasks.SingleOrDefaultAsync(r => r.Id == id && r.UserName == user);

            if (record == null)
            {
                return NotFound("Record not found.");
            }

            // Remove the record from the context
            _context.Tasks.Remove(record);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpGet("UserData")]
        public async Task<ActionResult<UserActivity>> ReadData(string email)
        {

            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var userActivities = await _context.Tasks
                     .Where(activity =>  (activity.email == email)) // Filter by the date part only
                     .ToListAsync();
            if (userActivities.Count <= 0)
            {
                return NotFound();
            }



            return Ok(userActivities);

        }


    }
}
