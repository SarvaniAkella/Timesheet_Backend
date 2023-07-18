namespace TimeSheet_Backend.Models
{
    public class LoginRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserType UserType { get; set; }
    }

}
