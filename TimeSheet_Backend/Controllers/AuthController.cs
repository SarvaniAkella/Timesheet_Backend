using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TimeSheet_Backend.Models;
using static TimeSheet_Backend.Services.emailService;


namespace TimeSheet_Backend.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly SignupContext _context;

        private IConfiguration _configuration;

        public static User user = new User();

        private readonly IEmailService _emailService;


        public AuthController(SignupContext context, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailService;
        }


        [HttpPost("signUp")]
        public async Task<IActionResult> Signup(User1 model)
        {
            var user = new User();

            if (await _context.Users.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict("email already exists.");
            }
            var verificationToken = GenerateRandomString();

            CreatePasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);


            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Username = model.Username;
            user.Mobileno = model.Mobileno;
            user.VerificationToken = verificationToken;
            user.Email = model.Email;
            user.IsVerified = false;





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
            await _emailService.SendVerificationEmailAsync(model.Email, verificationToken);
            return Ok("Verification Otp is sent to mail");
        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify(string email, string token)
        {
            // Find the user with the provided email and verification token
            var user = await _context.Users.FirstOrDefaultAsync(r => r.Email == email && r.VerificationToken == token);

            if (user == null)
            {
                return BadRequest("Invalid verification token or email");
            }

            // Mark the user as verified in the database
            user.IsVerified = true;
            user.VerificationToken = null;
            _context.Update(user);
            await _context.SaveChangesAsync();

            // You can return a success message or redirect the user to a success page after verification.
            return Ok("Email verification successful.");
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
            string token = CreateToken(email);
            var response = new
            {
                roleId = email.roleId,
                userId = email.UserId,
                tokenid = token

            };
            return Ok(response);


        }
        [HttpPost("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword(string email)
        {
            var userdt = await _context.Users.FirstOrDefaultAsync(r => r.Email == email);
            if (userdt == null)
            {
                return BadRequest("no user found");
            }
            string token = GenerateOtp();
            userdt.PasswordToken = token;
            await _context.SaveChangesAsync();
            await _emailService.SendOtpVerificationEmailAsync(userdt.Email, userdt.PasswordToken);

            return Ok("Verification Otp is sent to mail");



        }
        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ForgotPasswordDto forgot)
        {
            var userdt = await _context.Users.FirstOrDefaultAsync(r => r.Email == forgot.Email);



       
            if (userdt == null)
            {
                return BadRequest("Not Found The User");
            }
            if (forgot.ConfirmPassword == null)
            {
                return BadRequest("Please Enter the password");
            }
            if (forgot.Token != userdt.PasswordToken)
            {
                return BadRequest("Invalid Token");
            }
            CreatePasswordHash(forgot.ConfirmPassword, out byte[] passwordHash, out byte[] passworSalt);
       



            userdt.PasswordHash = passwordHash;
            userdt.PasswordSalt = passworSalt;
            userdt.PasswordToken = null;

            await _context.SaveChangesAsync();



            return Ok("Password reset successfully");



        }
        public static string GenerateOtp()
        {
            const string allowedChars = "0123456789";
            const int length = 6;



            Random random = new Random();
            char[] chars = new char[length];



            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }



            return new string(chars);
        }

        public static string GenerateRandomString()
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int length = 6;

            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
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



