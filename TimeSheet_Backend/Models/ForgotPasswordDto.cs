using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }



        [Compare("Password")]
        public string ConfirmPassword { get; set; }



    }
}
