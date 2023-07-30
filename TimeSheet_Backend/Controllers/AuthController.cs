using Azure.Core;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TimeSheet_Backend.Models;

namespace TimeSheet_Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly SignupContext _context;

        private IConfiguration _configuration;

        public static User user = new User();

        public AuthController(SignupContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpPost("signUp")]
        public async Task<IActionResult> Signup(User1 model)
        {
            var user = new User();

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict("email already exists.");
            }

            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = model.Username;
            user.Mobileno = model.Mobileno;

            user.Email = model.Email;






            //string[] emailParts = model.Email.Split('@');
            int roleid;
            if (model.Email == "srikanth@smbxl.com" || model.Email == "admin@smbxl.com")
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

            user.roleId = roleid;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

       
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(Login request)
        {
            var email = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            //evar user = new User();

            if (email == null)
            {
                return NotFound("User not found");
            }
            if (!VerifyPasswordHash(request.Password, email.PasswordHash, email.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }
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

            string token = CreateToken(email);
            var response = new
            {
                roleId = roleid,
                userId = email.UserId,
                tokenid = token

            };
            return Ok(response);


        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {


            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }



        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


    }
}



