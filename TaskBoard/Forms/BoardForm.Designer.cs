namespace TaskBoard
{
    partial class BoardForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblLogo;
        private Label lblProjectName;
        private Button btnBackToProjects;
        private Panel pnlBoard;
        private TextBox txtSearch;
        private ComboBox cmbPriorityFilter;
        private Button btnDashboard;
        private Button btnAddColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new Panel();
            this.lblLogo = new Label();
            this.lblProjectName = new Label();
            this.btnBackToProjects = new Button();
            this.pnlBoard = new Panel();
            this.txtSearch = new TextBox();
            this.cmbPriorityFilter = new ComboBox();
            this.btnDashboard = new Button();
            this.btnAddColumn = new Button();
            this.SuspendLayout();

            // === Form ===
            this.Text = "TaskBoard — Pano";
            this.Size = new Size(1280, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(12, 12, 24);
            this.Font = new Font("Segoe UI", 9.5f);

            // === Header ===
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 58;
            pnlHeader.BackColor = Color.FromArgb(18, 18, 32);

            lblLogo.Text = "📋 TaskBoard";
            lblLogo.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(99, 102, 241);
            lblLogo.AutoSize = true;
            lblLogo.Location = new Point(16, 16);

            lblProjectName.Text = "";
            lblProjectName.Font = new Font("Segoe UI", 12f);
            lblProjectName.ForeColor = Color.FromArgb(226, 232, 240);
            lblProjectName.AutoSize = true;
            lblProjectName.Location = new Point(168, 18);

            // Arama kutusu
            txtSearch.PlaceholderText = "🔍 Görev ara...";
            txtSearch.Size = new Size(185, 28);
            txtSearch.Location = new Point(380, 15);
            txtSearch.BackColor = Color.FromArgb(30, 30, 48);
            txtSearch.ForeColor = Color.FromArgb(226, 232, 240);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9.5f);
            txtSearch.TextChanged += txtSearch_TextChanged;

            // Priority filtre
            cmbPriorityFilter.Size = new Size(130, 28);
            cmbPriorityFilter.Location = new Point(574, 15);
            cmbPriorityFilter.BackColor = Color.FromArgb(30, 30, 48);
            cmbPriorityFilter.ForeColor = Color.FromArgb(226, 232, 240);
            cmbPriorityFilter.FlatStyle = FlatStyle.Flat;
            cmbPriorityFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPriorityFilter.Font = new Font("Segoe UI", 9.5f);
            cmbPriorityFilter.Items.AddRange(new object[]
                { "Tümü", "None", "Düşük", "Orta", "Yüksek", "Kritik" });
            cmbPriorityFilter.SelectedIndex = 0;
            cmbPriorityFilter.SelectedIndexChanged += cmbPriorityFilter_SelectedIndexChanged;

            // Dashboard butonu
            btnDashboard.Text = "📊 Dashboard";
            btnDashboard.Size = new Size(118, 32);
            btnDashboard.Location = new Point(718, 13);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.FlatAppearance.BorderColor = Color.FromArgb(99, 102, 241);
            btnDashboard.BackColor = Color.FromArgb(30, 30, 48);
            btnDashboard.ForeColor = Color.FromArgb(199, 210, 254);
            btnDashboard.Font = new Font("Segoe UI", 9.5f);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.Click += btnDashboard_Click;

            // + Sütun butonu
            btnAddColumn.Text = "＋ Sütun";
            btnAddColumn.Size = new Size(100, 32);
            btnAddColumn.Location = new Point(846, 13);
            btnAddColumn.FlatStyle = FlatStyle.Flat;
            btnAddColumn.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnAddColumn.BackColor = Color.Transparent;
            btnAddColumn.ForeColor = Color.FromArgb(148, 163, 184);
            btnAddColumn.Font = new Font("Segoe UI", 9.5f);
            btnAddColumn.Cursor = Cursors.Hand;
            btnAddColumn.Click += btnAddColumn_Click;

            // Geri butonu
            btnBackToProjects.Text = "◀  Projeler";
            btnBackToProjects.Size = new Size(110, 32);
            btnBackToProjects.Location = new Point(1150, 13);
            btnBackToProjects.FlatStyle = FlatStyle.Flat;
            btnBackToProjects.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnBackToProjects.BackColor = Color.Transparent;
            btnBackToProjects.ForeColor = Color.FromArgb(148, 163, 184);
            btnBackToProjects.Font = new Font("Segoe UI", 9.5f);
            btnBackToProjects.Cursor = Cursors.Hand;
            btnBackToProjects.Click += btnBackToProjects_Click;

            pnlHeader.Controls.AddRange(new Control[]
            {
                lblLogo, lblProjectName, txtSearch, cmbPriorityFilter,
                btnDashboard, btnAddColumn, btnBackToProjects
            });

            // === Board Panel ===
            pnlBoard.Location = new Point(0, 58);
            pnlBoard.Size = new Size(1280, 712);
            pnlBoard.AutoScroll = true;
            pnlBoard.BackColor = Color.FromArgb(12, 12, 24);

            this.Controls.AddRange(new Control[] { pnlHeader, pnlBoard });
            this.ResumeLayout(false);
        }
    }
}
