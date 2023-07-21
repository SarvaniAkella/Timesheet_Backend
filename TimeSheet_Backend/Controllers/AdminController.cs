using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{
  //  [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly SignupContext _context;
        public AdminController(SignupContext context) 
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

        [HttpGet("GetRecordsByEmail")]
        public async Task<ActionResult<List<TimeSheet>>> ReadData(string email)
        {
            // Step 1: Find the user record with the provided email
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }

            // Step 2: Find timesheets associated with the user using the UserId
            var userRecords = await _context.TimeSheets
                .Where(r => r.UserId == userRecord.UserId)
                .ToListAsync();

            if (userRecords.Count == 0)
            {
                // If no timesheets are found for the user, return NotFound
                return NotFound("No timesheets found for the user");
            }

            // Return the list of timesheets associated with the user
            return Ok(userRecords);
        }

        [HttpDelete("DeleteUserByEmail")]
        public async Task<IActionResult> DeleteRecord(string email, int index)
        {
            // Find the record by ID and email
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }

            // Step 2: Find timesheets associated with the user using the UserId
            var record = await _context.TimeSheets
                .Where(r => r.UserId == userRecord.UserId)
                .ToListAsync();

            if (record == null)
            {
                return NotFound("Record not found.");
            }

            // Remove the record from the context
            _context.TimeSheets.Remove(record[index]);
            await _context.SaveChangesAsync();

            return Ok("Record deleted successfully.");
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            // Find the user in the database
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
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
        [HttpPut("EditTimesheet")]
        public async Task<IActionResult> Edit(string email, int index, TimeSheet model)
        {
            var userRecord = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
            if (userRecord == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }

            // Step 2: Find timesheets associated with the user using the UserId
            var record = await _context.TimeSheets
                .Where(r => r.UserId == userRecord.UserId)
                .ToListAsync();
          
            if (record == null)
            {
                return NotFound("Timesheet not found.");
            }

            var timesheet = await _context.TimeSheets
                     .Where(t => t.UserId == userRecord.UserId)
                     .ToListAsync();
            timesheet[index].ProjectId = model.ProjectId;
            timesheet[index].ActivityId = model.ActivityId;
            timesheet[index].task = model.task;
            timesheet[index].hours = model.hours;
            timesheet[index].CreatedDate = model.CreatedDate;

            await _context.SaveChangesAsync();
            return Ok();

        }

        [HttpPut("EditUsers")]
        public async Task<IActionResult> UpdateUser(string email, User model)
             
        {
            // Find the user in the database
            var user = await _context.Users.SingleOrDefaultAsync(r => r.Email == email);
            if (user == null)
            {
                // If the user with the given email is not found, return NotFound
                return NotFound("User not found");
            }

            // Update the properties of the user with the new values
            user.Username = model.Username;
            user.Password = model.Password;
            user.Mobileno = model.Mobileno;
            user.Email = model.Email;

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpGet("oneweek")]
        public IActionResult GetTimesheetsForOneWeek([FromQuery] DateTime startDate)
        {
            // Calculate the end date as one week from the start date
            DateTime endDate = startDate.AddDays(7);

            // Fetch timesheet records for the specified user and within the date range
            List<TimeSheet> timesheets = _context.TimeSheets
                            .Where(t => t.CreatedDate.Date >= startDate.Date && t.CreatedDate.Date < endDate.Date )
                            .ToList();

            return Ok(timesheets);
        }

       

    }
}
