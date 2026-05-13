namespace TaskBoard.Data.Models
{
    /// <summary>
    /// Proje-Kullanıcı yetkilendirme ilişki tablosu.
    /// Hangi kullanıcının hangi projeye erişimi olduğunu tutar.
    /// </summary>
    public class ProjectUser
    {
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
