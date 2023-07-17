using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private readonly SignupContext _dbContext;
        public SignupController(SignupContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(Signup request)
        {
            if (await _dbContext.Signups.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("Email already exists");
            }

            var newUser = new Signup
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Mobileno = request.Mobileno
                // You can add more properties here as needed
            };

            _dbContext.Signups.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok("User created successfully");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login request)
        {
            var user = await _dbContext.Signups.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Password != request.Password)
            {
                return BadRequest("Invalid password");
            }

            // Generate and return a JWT token or any other authentication response here


            return Ok("User login successfull");
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _dbContext.Signups.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            // Return the user information
            return Ok(user);
        }

        // This is just a placeholder method, you should replace it with your actual token generation logic

    }
}
