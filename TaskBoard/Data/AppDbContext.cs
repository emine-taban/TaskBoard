using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Models;

namespace TaskBoard.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<BoardColumn> BoardColumns { get; set; }
        public DbSet<TaskCard> TaskCards { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\MSSQLLocalDB;Database=TaskBoardDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bileşik anahtar: ProjectUser
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new { pu.ProjectId, pu.UserId });

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.Project)
                .WithMany(p => p.ProjectUsers)
                .HasForeignKey(pu => pu.ProjectId);

            modelBuilder.Entity<ProjectUser>()
                .HasOne(pu => pu.User)
                .WithMany(u => u.ProjectUsers)
                .HasForeignKey(pu => pu.UserId);

            // AssignedUser → kullanıcı silinince NULL yap
            modelBuilder.Entity<TaskCard>()
                .HasOne(t => t.AssignedUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            // ChecklistItem → görev silinince cascade sil
            modelBuilder.Entity<ChecklistItem>()
                .HasOne(c => c.TaskCard)
                .WithMany(t => t.ChecklistItems)
                .HasForeignKey(c => c.TaskCardId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Data
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = "$2a$11$79ndqr1FTZwUq7/.iXBbj.rzxMV9AmVKkyUq/1OD./zexYLTsHUxi",
                    Role = UserRole.Admin
                },
                new User
                {
                    Id = 2,
                    Username = "kullanici1",
                    PasswordHash = "$2a$11$6NtFb0cHrHf54iEZ.hVbF.XRZMYU5rM7WIYegDqw6IB/ObWlEWBeq",
                    Role = UserRole.Standard
                }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { Id = 1, Name = "Demo Proje", Description = "İlk proje", CreatedAt = new DateTime(2026, 1, 1) }
            );

            modelBuilder.Entity<ProjectUser>().HasData(
                new ProjectUser { ProjectId = 1, UserId = 1 },
                new ProjectUser { ProjectId = 1, UserId = 2 }
            );

            modelBuilder.Entity<BoardColumn>().HasData(
                new BoardColumn { Id = 1, ProjectId = 1, Name = "Yapılacak", OrderIndex = 0 },
                new BoardColumn { Id = 2, ProjectId = 1, Name = "Yapılıyor", OrderIndex = 1 },
                new BoardColumn { Id = 3, ProjectId = 1, Name = "Bitti",     OrderIndex = 2 }
            );
        }
    }
}
