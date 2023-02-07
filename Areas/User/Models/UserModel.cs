namespace BioNetWork.Areas.User.Models
{
    public class UserModel
    {
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string Biography { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public DateTime DataRegister { get; set; } 
        public byte[]? Avatar { get; set; }
    }
}
