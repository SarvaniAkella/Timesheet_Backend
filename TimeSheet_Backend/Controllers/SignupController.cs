using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
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
    public async Task<IActionResult> Signup([FromBody] User1 model)
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
        

        //string[] emailParts = model.Email.Split('@');
        int roleid;
        if (model.Email == "srikanth@smbxl.com")
        {
            var record = await _context.roles.Where(r => r.roleName == "Admin").FirstOrDefaultAsync();
             roleid = record.roleId;
        }

        else if (model.Email == "hr@smbxl.com")
        {
            var record = await _context.roles.Where(r => r.roleName == "Hr").FirstOrDefaultAsync();
             roleid = record.roleId;
        }
        else 
        {
            var record = await _context.roles.Where(r => r.roleName == "User").FirstOrDefaultAsync();
            roleid = record.roleId;
        }

        // Create a new user object and set its properties 
        var user = new User
        {
          
            Username = model.Username,
            Password = model.Password, // You should hash the password securely before storing it.
            Mobileno = model.Mobileno,
            Email = model.Email,
            roleId = roleid
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
       // string[] emailParts = request.Email.Split('@');
        int roleid;
        
        if (request.Email == "srikanth@smbxl.com")
        {
            var record1 = await _context.roles.Where(r => r.roleName == "Admin").FirstOrDefaultAsync();
            roleid = record1.roleId;
        }
        else if (request.Email == "hr@smbxl.com")
        {
            var record1 = await _context.roles.Where(r => r.roleName == "Hr").FirstOrDefaultAsync();
            roleid = record1.roleId;
        }
        else
        {
            var record1 = await _context.roles.Where(r => r.roleName == "User").FirstOrDefaultAsync();
            roleid = record1.roleId;
        }
        var response = new
        {
            roleId = roleid,
            userId = user.UserId
        };
        return Ok(response);
    }



}





    

