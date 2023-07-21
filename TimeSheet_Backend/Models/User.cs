using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TimeSheet_Backend.Models
{
  
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobileno { get; set; }

       //public List<TimeSheet> TimeSheets { get; set; }
    }
}
