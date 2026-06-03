namespace TaskBoard.Data.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<BoardColumn> Columns { get; set; } = new List<BoardColumn>();
        public ICollection<ProjectUser> ProjectUsers { get; set; } = new List<ProjectUser>();
    }
}
