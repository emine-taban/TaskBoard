namespace TaskBoard.Data.Models
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public int TaskCardId { get; set; }
        public TaskCard TaskCard { get; set; } = null!;

        public string Text { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public int OrderIndex { get; set; }
    }
}
