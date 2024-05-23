namespace BLL.DTOs
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Avatar { get; set; } = null!;
        public long RoleId { get; set; }
    }
}
