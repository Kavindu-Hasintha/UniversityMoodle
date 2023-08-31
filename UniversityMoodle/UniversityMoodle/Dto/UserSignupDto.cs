namespace UniversityMoodle.Dto
{
    public class UserSignupDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DoB { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
