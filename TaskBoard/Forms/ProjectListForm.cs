using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    public partial class ProjectListForm : Form
    {
        public ProjectListForm()
        {
            InitializeComponent();
            LoadProjects();
        }

        private void LoadProjects()
        {
            flpProjects.Controls.Clear();
            using var db = new AppDbContext();

            List<Project> projects;
            if (Session.IsAdmin)
            {
                // Admin tüm projeleri görür
                projects = db.Projects.OrderBy(p => p.Name).ToList();
            }
            else
            {
                // Standart kullanıcı sadece yetkili projeleri görür
                projects = db.Projects
                    .Where(p => p.ProjectUsers.Any(pu => pu.UserId == Session.CurrentUser!.Id))
                    .OrderBy(p => p.Name)
                    .ToList();
            }

            lblWelcome.Text = $"Hoşgeldiniz, {Session.CurrentUser?.Username}  ({(Session.IsAdmin ? "Admin" : "Standart")})";

            if (!projects.Any())
            {
                var lbl = new Label
                {
                    Text = "Henüz erişebileceğiniz bir proje yok.",
                    ForeColor = Color.FromArgb(148, 163, 184),
                    Font = new Font("Segoe UI", 11f),
                    AutoSize = true,
                    Margin = new Padding(20)
                };
                flpProjects.Controls.Add(lbl);
                return;
            }

            foreach (var project in projects)
                flpProjects.Controls.Add(CreateProjectCard(project));
        }

        private Panel CreateProjectCard(Project project)
        {
            var card = new Panel
            {
                Size = new Size(240, 130),
                BackColor = Color.FromArgb(30, 30, 48),
                Cursor = Cursors.Hand,
                Margin = new Padding(12),
                Tag = project
            };

            // Gradient renk çubuğu üst
            var topStrip = new Panel
            {
                Size = new Size(240, 4),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(99, 102, 241)
            };

            var lblName = new Label
            {
                Text = project.Name,
                Font = new Font("Segoe UI", 13f, FontStyle.Bold),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(14, 18),
                Size = new Size(212, 30),
                AutoEllipsis = true
            };

            var lblDesc = new Label
            {
                Text = string.IsNullOrEmpty(project.Description) ? "Açıklama yok" : project.Description,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(100, 116, 139),
                Location = new Point(14, 52),
                Size = new Size(212, 38),
                AutoEllipsis = true
            };

            var lblDate = new Label
            {
                Text = project.CreatedAt.ToString("dd.MM.yyyy"),
                Font = new Font("Segoe UI", 8f),
                ForeColor = Color.FromArgb(71, 85, 105),
                Location = new Point(14, 100),
                AutoSize = true
            };

            card.Controls.AddRange(new Control[] { topStrip, lblName, lblDesc, lblDate });

            // Hover efekti
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(40, 40, 62);
            card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(30, 30, 48);
            card.Click += ProjectCard_Click;
            foreach (Control ctrl in card.Controls)
            {
                ctrl.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(40, 40, 62);
                ctrl.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(30, 30, 48);
                ctrl.Click += ProjectCard_Click;
            }

            return card;
        }

        private void ProjectCard_Click(object? sender, EventArgs e)
        {
            Control? ctrl = sender as Control;
            Panel? card = ctrl is Panel p ? p : ctrl?.Parent as Panel;
            if (card?.Tag is Project project)
            {
                var boardForm = new BoardForm(project);
                boardForm.Show();
            }
        }

        private void btnAddProject_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Proje oluşturma yetkisine sahip değilsiniz.", "Yetki Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dlg = new AddProjectDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                using var db = new AppDbContext();
                var project = new Project
                {
                    Name = dlg.ProjectName,
                    Description = dlg.ProjectDescription
                };
                db.Projects.Add(project);
                db.SaveChanges();

                // 3 varsayılan sütun ekle
                db.BoardColumns.AddRange(
                    new BoardColumn { ProjectId = project.Id, Name = "Yapılacak", OrderIndex = 0 },
                    new BoardColumn { ProjectId = project.Id, Name = "Yapılıyor", OrderIndex = 1 },
                    new BoardColumn { ProjectId = project.Id, Name = "Bitti", OrderIndex = 2 }
                );
                // Admin'e otomatik erişim ver
                db.ProjectUsers.Add(new ProjectUser { ProjectId = project.Id, UserId = Session.CurrentUser!.Id });
                db.SaveChanges();

                LoadProjects();
            }
        }

        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            if (!Session.IsAdmin)
            {
                MessageBox.Show("Bu özellik sadece Admin kullanıcılara açıktır.", "Yetki Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var dlg = new UserManagementForm();
            dlg.ShowDialog();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            this.Close();
        }
    }
}
