namespace TaskBoard
{
    partial class ProjectListForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblLogo;
        private Label lblWelcome;
        private Button btnAddProject;
        private Button btnManageUsers;
        private Button btnLogout;
        private FlowLayoutPanel flpProjects;
        private Label lblProjectsTitle;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblLogo = new Label();
            this.lblWelcome = new Label();
            this.btnAddProject = new Button();
            this.btnManageUsers = new Button();
            this.btnLogout = new Button();
            this.flpProjects = new FlowLayoutPanel();
            this.lblProjectsTitle = new Label();
            this.SuspendLayout();

            // === Form ===
            this.Text = "TaskBoard — Projeler";
            this.Size = new Size(1000, 680);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(12, 12, 24);
            this.Font = new Font("Segoe UI", 9.5f);

            // === Header ===
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 64;
            pnlHeader.BackColor = Color.FromArgb(18, 18, 32);
            pnlHeader.Padding = new Padding(16, 0, 16, 0);

            lblLogo.Text = "📋 TaskBoard";
            lblLogo.Font = new Font("Segoe UI", 15f, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(99, 102, 241);
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(16, 18);

            lblWelcome.Text = "";
            lblWelcome.Font = new Font("Segoe UI", 9f);
            lblWelcome.ForeColor = Color.FromArgb(148, 163, 184);
            lblWelcome.AutoSize = true;
            lblWelcome.Location = new Point(190, 24);

            // Logout btn
            btnLogout.Text = "⏻  Çıkış";
            btnLogout.Size = new Size(90, 36);
            btnLogout.Location = new Point(890, 14);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnLogout.FlatAppearance.BorderSize = 1;
            btnLogout.BackColor = Color.Transparent;
            btnLogout.ForeColor = Color.FromArgb(148, 163, 184);
            btnLogout.Font = new Font("Segoe UI", 9.5f);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.Click += btnLogout_Click;

            // Manage Users btn
            btnManageUsers.Text = "👥  Kullanıcılar";
            btnManageUsers.Size = new Size(130, 36);
            btnManageUsers.Location = new Point(750, 14);
            btnManageUsers.FlatStyle = FlatStyle.Flat;
            btnManageUsers.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnManageUsers.FlatAppearance.BorderSize = 1;
            btnManageUsers.BackColor = Color.Transparent;
            btnManageUsers.ForeColor = Color.FromArgb(148, 163, 184);
            btnManageUsers.Font = new Font("Segoe UI", 9.5f);
            btnManageUsers.Cursor = Cursors.Hand;
            btnManageUsers.Click += btnManageUsers_Click;

            // Add project btn
            btnAddProject.Text = "＋  Yeni Proje";
            btnAddProject.Size = new Size(130, 36);
            btnAddProject.Location = new Point(610, 14);
            btnAddProject.FlatStyle = FlatStyle.Flat;
            btnAddProject.FlatAppearance.BorderSize = 0;
            btnAddProject.BackColor = Color.FromArgb(99, 102, 241);
            btnAddProject.ForeColor = Color.White;
            btnAddProject.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            btnAddProject.Cursor = Cursors.Hand;
            btnAddProject.Click += btnAddProject_Click;

            pnlHeader.Controls.AddRange(new Control[] { lblLogo, lblWelcome, btnAddProject, btnManageUsers, btnLogout });

            // === Projects Title ===
            lblProjectsTitle.Text = "PROJELERİM";
            lblProjectsTitle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            lblProjectsTitle.ForeColor = Color.FromArgb(99, 102, 241);
            lblProjectsTitle.AutoSize = true;
            lblProjectsTitle.Location = new Point(24, 80);

            // === Projects FlowPanel ===
            flpProjects.Location = new Point(10, 104);
            flpProjects.Size = new Size(966, 540);
            flpProjects.AutoScroll = true;
            flpProjects.Padding = new Padding(10);
            flpProjects.BackColor = Color.Transparent;
            flpProjects.FlowDirection = FlowDirection.LeftToRight;
            flpProjects.WrapContents = true;

            this.Controls.AddRange(new Control[] { pnlHeader, lblProjectsTitle, flpProjects });

            this.ResumeLayout(false);
        }
    }
}
