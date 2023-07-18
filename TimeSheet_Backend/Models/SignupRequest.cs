namespace TimeSheet_Backend.Models
{
    public class SignupRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string Mobileno { get; set; }
        public UserType UserType { get; set; }
    }

    public enum UserType
    {
        User,
        Admin,
        HR
    }

}
