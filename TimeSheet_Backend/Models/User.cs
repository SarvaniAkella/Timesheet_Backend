using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeSheet_Backend.Models
{
  
    public class User
    {
        
        public int UserId { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }
        public string UniqueId { get; set; }
       // public byte[] PasswordHash { get; set; }
       // public byte[] PasswordSalt { get; set; }
      //  public bool IsVerified { get; set; }



      //  public string? VerificationToken { get; set; }
       // public bool IsOtpVerified { get; set; }
       // public string? OtpVerificationToken { get; set; }*/

       // public string? PasswordToken { get; set; }
        public string Mobileno { get; set; }

        public int roleId { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
    }
}
