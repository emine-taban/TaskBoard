namespace TaskBoard.Data.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int TaskCardId { get; set; }
        public TaskCard TaskCard { get; set; } = null!;

        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public long FileSizeBytes { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.Now;
    }
}
