using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
//using System.Web.Http;
//using System.Web.Http;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class UserActivityController : ControllerBase
    {
        private readonly SignupContext _context;

        public UserActivityController(SignupContext context)
        {
            _context = context;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveUser(UserActivity request)
        {
            


            var newActivity = new UserActivity
            {
                ProjectName = request.ProjectName,
                Activity = request.Activity,
                Task = request.Task,
                Hours = request.Hours,
                DateOnly = request.DateOnly
                // You can add more properties here as needed
            };



            _context.UserActivities.Add(newActivity);
            await _context.SaveChangesAsync();



            return Ok("Activity added successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.UserActivities == null)
            {
                return NotFound();
            }
            var user = await _context.UserActivities.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            _context.UserActivities.Remove(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> EditUser(int id, UserActivity user)
        {
            if (id  != user.Id) 
            {
                return BadRequest();
            }
            _context.Entry(user).State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            return Ok();
          
        }

        [HttpGet("readdata")]
        public async Task<ActionResult<UserActivity>> ReadData(DateTime date)
        {

            if(_context.UserActivities == null)
            {
                return NotFound();
            }
            var userActivities = await _context.UserActivities
                     .Where(activity => activity.DateOnly.Date == date.Date) // Filter by the date part only
                     .ToListAsync();
            if(userActivities.Count <= 0) 
            {
                return NotFound();
            }

            // Do something with the userActivities
            // ...

            return Ok(userActivities);
          
        }





    }
}
