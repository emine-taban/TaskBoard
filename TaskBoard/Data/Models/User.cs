namespace TaskBoard.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.Standard;

        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }

    public enum UserRole
    {
        Standard = 0,
        Admin = 1
    }
}
