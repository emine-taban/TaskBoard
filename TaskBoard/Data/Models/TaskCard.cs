namespace TaskBoard.Data.Models
{
    public class TaskCard
    {
        public int Id { get; set; }
        public int BoardColumnId { get; set; }
        public BoardColumn BoardColumn { get; set; } = null!;

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Yeni alanlar
        public TaskPriority Priority { get; set; } = TaskPriority.None;
        public DateTime? DueDate { get; set; }
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        public ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
        public ICollection<ChecklistItem> ChecklistItems { get; set; } = new List<ChecklistItem>();
    }
}
