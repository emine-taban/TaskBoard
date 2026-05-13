using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    /// <summary>
    /// Admin paneli: Kullanıcı oluşturma ve proje erişim yönetimi.
    /// </summary>
    public partial class UserManagementForm : Form
    {
        private DataGridView dgvUsers = null!;
        private DataGridView dgvProjects = null!;
        private Button btnAddUser = null!;
        private Button btnGrantAccess = null!;
        private Button btnRevokeAccess = null!;
        private Label lblSelected = null!;

        private int _selectedUserId = -1;

        public UserManagementForm()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using var db = new AppDbContext();
            var users = db.Users.ToList();
            dgvUsers.DataSource = users.Select(u => new
            {
                u.Id,
                u.Username,
                Rol = u.Role == UserRole.Admin ? "Admin" : "Standart"
            }).ToList();
            dgvUsers.Columns["Id"].Visible = false;
        }

        private void LoadProjectsForUser(int userId)
        {
            using var db = new AppDbContext();
            var allProjects = db.Projects.ToList();
            var accessedIds = db.ProjectUsers
                .Where(pu => pu.UserId == userId)
                .Select(pu => pu.ProjectId)
                .ToHashSet();

            dgvProjects.DataSource = allProjects.Select(p => new
            {
                p.Id,
                p.Name,
                Erişim = accessedIds.Contains(p.Id) ? "✅ Var" : "❌ Yok"
            }).ToList();
            dgvProjects.Columns["Id"].Visible = false;
        }

        private void DgvUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0) return;
            var row = dgvUsers.SelectedRows[0];
            _selectedUserId = (int)row.Cells["Id"].Value;
            lblSelected.Text = $"Seçili Kullanıcı: {row.Cells["Username"].Value}";
            LoadProjectsForUser(_selectedUserId);
        }

        private void btnGrantAccess_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1 || dgvProjects.SelectedRows.Count == 0) return;
            int projectId = (int)dgvProjects.SelectedRows[0].Cells["Id"].Value;

            using var db = new AppDbContext();
            bool exists = db.ProjectUsers.Any(pu => pu.ProjectId == projectId && pu.UserId == _selectedUserId);
            if (!exists)
            {
                db.ProjectUsers.Add(new ProjectUser { ProjectId = projectId, UserId = _selectedUserId });
                db.SaveChanges();
            }
            LoadProjectsForUser(_selectedUserId);
        }

        private void btnRevokeAccess_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1 || dgvProjects.SelectedRows.Count == 0) return;
            int projectId = (int)dgvProjects.SelectedRows[0].Cells["Id"].Value;

            using var db = new AppDbContext();
            var pu = db.ProjectUsers.Find(projectId, _selectedUserId);
            if (pu != null) { db.ProjectUsers.Remove(pu); db.SaveChanges(); }
            LoadProjectsForUser(_selectedUserId);
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using var dlg = new AddUserDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            using var db = new AppDbContext();
            if (db.Users.Any(u => u.Username == dlg.NewUsername))
            {
                MessageBox.Show("Bu kullanıcı adı zaten alınmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            db.Users.Add(new User
            {
                Username = dlg.NewUsername,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dlg.NewPassword),
                Role = dlg.IsAdmin ? UserRole.Admin : UserRole.Standard
            });
            db.SaveChanges();
            LoadUsers();
        }

        private void InitializeComponent()
        {
            this.Text = "Kullanıcı Yönetimi";
            this.Size = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 9.5f);

            var lblUsers = new Label
            {
                Text = "KULLANICILAR",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 14)
            };

            dgvUsers = CreateDgv(new Point(20, 36), new Size(340, 380));
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;

            btnAddUser = CreateBtn("＋ Yeni Kullanıcı", new Point(20, 428));
            btnAddUser.Click += btnAddUser_Click;

            lblSelected = new Label
            {
                Text = "Seçili Kullanıcı: —",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Location = new Point(380, 14)
            };

            var lblProjects = new Label
            {
                Text = "PROJE ERİŞİM YÖNETİMİ",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(380, 36)
            };

            dgvProjects = CreateDgv(new Point(380, 58), new Size(490, 358));

            btnGrantAccess = CreateBtn("✅ Erişim Ver", new Point(380, 428));
            btnGrantAccess.Click += btnGrantAccess_Click;

            btnRevokeAccess = CreateBtn("❌ Erişim Kaldır", new Point(510, 428));
            btnRevokeAccess.BackColor = Color.FromArgb(185, 28, 28);
            btnRevokeAccess.Click += btnRevokeAccess_Click;

            this.Controls.AddRange(new Control[] {
                lblUsers, dgvUsers, btnAddUser,
                lblSelected, lblProjects, dgvProjects,
                btnGrantAccess, btnRevokeAccess
            });
        }

        private DataGridView CreateDgv(Point loc, Size size)
        {
            var dgv = new DataGridView
            {
                Location = loc,
                Size = size,
                BackgroundColor = Color.FromArgb(22, 22, 36),
                ForeColor = Color.FromArgb(226, 232, 240),
                GridColor = Color.FromArgb(40, 40, 58),
                BorderStyle = BorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9.5f)
            };
            dgv.DefaultCellStyle.BackColor = Color.FromArgb(22, 22, 36);
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(226, 232, 240);
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(55, 55, 90);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30, 30, 48);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(148, 163, 184);
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            return dgv;
        }

        private Button CreateBtn(string text, Point loc)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 36),
                Location = loc,
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            return btn;
        }
    }
}
