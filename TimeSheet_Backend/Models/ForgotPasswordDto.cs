using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models
{
    public class ForgotPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
  
    }
}
