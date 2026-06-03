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
            pnlHeader = new Panel();
            lblLogo = new Label();
            lblProjectName = new Label();
            txtSearch = new TextBox();
            cmbPriorityFilter = new ComboBox();
            btnDashboard = new Button();
            btnAddColumn = new Button();
            btnBackToProjects = new Button();
            pnlBoard = new Panel();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(18, 18, 32);
            pnlHeader.Controls.Add(lblLogo);
            pnlHeader.Controls.Add(lblProjectName);
            pnlHeader.Controls.Add(txtSearch);
            pnlHeader.Controls.Add(cmbPriorityFilter);
            pnlHeader.Controls.Add(btnDashboard);
            pnlHeader.Controls.Add(btnAddColumn);
            pnlHeader.Controls.Add(btnBackToProjects);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1264, 58);
            pnlHeader.TabIndex = 0;
            // 
            // lblLogo
            // 
            lblLogo.AutoSize = true;
            lblLogo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(99, 102, 241);
            lblLogo.Location = new Point(16, 16);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(131, 25);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "📋 TaskBoard";
            // 
            // lblProjectName
            // 
            lblProjectName.AutoSize = true;
            lblProjectName.Font = new Font("Segoe UI", 12F);
            lblProjectName.ForeColor = Color.FromArgb(226, 232, 240);
            lblProjectName.Location = new Point(168, 18);
            lblProjectName.Name = "lblProjectName";
            lblProjectName.Size = new Size(0, 21);
            lblProjectName.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.BackColor = Color.FromArgb(30, 30, 48);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;
            txtSearch.Font = new Font("Segoe UI", 9.5F);
            txtSearch.ForeColor = Color.FromArgb(226, 232, 240);
            txtSearch.Location = new Point(380, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "🔍 Görev ara...";
            txtSearch.Size = new Size(185, 24);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // cmbPriorityFilter
            // 
            cmbPriorityFilter.BackColor = Color.FromArgb(30, 30, 48);
            cmbPriorityFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPriorityFilter.FlatStyle = FlatStyle.Flat;
            cmbPriorityFilter.Font = new Font("Segoe UI", 9.5F);
            cmbPriorityFilter.ForeColor = Color.FromArgb(226, 232, 240);
            cmbPriorityFilter.Items.AddRange(new object[] { "Tümü", "None", "Düşük", "Orta", "Yüksek", "Kritik" });
            cmbPriorityFilter.Location = new Point(574, 15);
            cmbPriorityFilter.Name = "cmbPriorityFilter";
            cmbPriorityFilter.Size = new Size(130, 25);
            cmbPriorityFilter.TabIndex = 3;
            cmbPriorityFilter.SelectedIndexChanged += cmbPriorityFilter_SelectedIndexChanged;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(30, 30, 48);
            btnDashboard.Cursor = Cursors.Hand;
            btnDashboard.FlatAppearance.BorderColor = Color.FromArgb(99, 102, 241);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 9.5F);
            btnDashboard.ForeColor = Color.FromArgb(199, 210, 254);
            btnDashboard.Location = new Point(718, 13);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Size = new Size(118, 32);
            btnDashboard.TabIndex = 4;
            btnDashboard.Text = "📊 Dashboard";
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            // 
            // btnAddColumn
            // 
            btnAddColumn.BackColor = Color.Transparent;
            btnAddColumn.Cursor = Cursors.Hand;
            btnAddColumn.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnAddColumn.FlatStyle = FlatStyle.Flat;
            btnAddColumn.Font = new Font("Segoe UI", 9.5F);
            btnAddColumn.ForeColor = Color.FromArgb(148, 163, 184);
            btnAddColumn.Location = new Point(846, 13);
            btnAddColumn.Name = "btnAddColumn";
            btnAddColumn.Size = new Size(100, 32);
            btnAddColumn.TabIndex = 5;
            btnAddColumn.Text = "＋ Sütun";
            btnAddColumn.UseVisualStyleBackColor = false;
            btnAddColumn.Click += btnAddColumn_Click;
            // 
            // btnBackToProjects
            // 
            btnBackToProjects.BackColor = Color.Transparent;
            btnBackToProjects.Cursor = Cursors.Hand;
            btnBackToProjects.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);
            btnBackToProjects.FlatStyle = FlatStyle.Flat;
            btnBackToProjects.Font = new Font("Segoe UI", 9.5F);
            btnBackToProjects.ForeColor = Color.FromArgb(148, 163, 184);
            btnBackToProjects.Location = new Point(1150, 13);
            btnBackToProjects.Name = "btnBackToProjects";
            btnBackToProjects.Size = new Size(110, 32);
            btnBackToProjects.TabIndex = 6;
            btnBackToProjects.Text = "◀  Projeler";
            btnBackToProjects.UseVisualStyleBackColor = false;
            btnBackToProjects.Click += btnBackToProjects_Click;
            // 
            // pnlBoard
            // 
            pnlBoard.AutoScroll = true;
            pnlBoard.BackColor = Color.FromArgb(12, 12, 24);
            pnlBoard.Location = new Point(0, 58);
            pnlBoard.Name = "pnlBoard";
            pnlBoard.Size = new Size(1280, 712);
            pnlBoard.TabIndex = 1;
            pnlBoard.Paint += pnlBoard_Paint;
            // 
            // BoardForm
            // 
            BackColor = Color.FromArgb(12, 12, 24);
            ClientSize = new Size(1264, 761);
            Controls.Add(pnlHeader);
            Controls.Add(pnlBoard);
            Font = new Font("Segoe UI", 9.5F);
            Name = "BoardForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TaskBoard — Pano";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);
        }
    }
}
