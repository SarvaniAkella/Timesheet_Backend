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
    public IActionResult Signup([FromBody] SignupRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        switch (request.UserType)
        {
            case UserType.User:
                _context.Users.Add(new UserSignup
                {

                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    Mobileno = request.Mobileno,


                }); 
                break;

            case UserType.Admin:
                _context.Admins.Add(new AdminSignup
                {
                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    Mobileno = request.Mobileno,
                    
                });
                break;

            case UserType.HR:
                _context.HRs.Add(new HrSignup
                {
                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    Mobileno = request.Mobileno,
                });
                break;
        }

        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        var admin= await _context.Admins.FirstOrDefaultAsync(u => u.Email == request.Email);
        var hr = await _context.HRs.FirstOrDefaultAsync(u => u.Email == request.Email);


        switch (request.UserType)
        {
            case UserType.User:
                if (user == null)
                {
                    return NotFound("User not found");
                }



                if (user.Password != request.Password)
                {
                    return BadRequest("Invalid password");
                }
                

                break;

            case UserType.Admin:
                if (admin == null)
                {
                    return NotFound("User not found");
                }



                if (admin.Password != request.Password)
                {
                    return BadRequest("Invalid password");
                }

                break;

            case UserType.HR:
      
                if (hr == null)
                {
                    return NotFound("User not found");
                }



                if (hr.Password != request.Password)
                {
                    return BadRequest("Invalid password");
                }

                break;
        }




        return Ok("User login successfull");
    }
}
