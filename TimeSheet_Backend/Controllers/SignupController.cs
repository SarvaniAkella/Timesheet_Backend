using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet_Backend.Models;

[ApiController]
//[Route("api/signup")]
public class SignupController : ControllerBase
{
    private readonly SignupContext _context;

    public SignupController(SignupContext context)
    {
        _context = context;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] User model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if the username or email already exists in the database
        if (await _context.Users.AnyAsync(u => u.Email == model.Email))
        {
            return Conflict("email already exists.");
        }

        // Create a new user object and set its properties 
        var user = new User
        {
            Username = model.Username,
            Password = model.Password, // You should hash the password securely before storing it.
            Mobileno = model.Mobileno,
            Email = model.Email
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Loginuser(Login request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);



        if (user == null)
        {
            return NotFound("User not found");
        }



        if (user.Password != request.Password)
        {
            return BadRequest("Invalid password");
        }

  
        return Ok("login Success");




    }





    }

