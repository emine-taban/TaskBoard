namespace TaskBoard.Data.Models
{
    public class BoardColumn
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public string Name { get; set; } = string.Empty;
        public int OrderIndex { get; set; }

        public ICollection<TaskCard> Tasks { get; set; } = new List<TaskCard>();
    }
}
