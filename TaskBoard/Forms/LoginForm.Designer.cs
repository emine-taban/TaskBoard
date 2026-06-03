namespace TaskBoard
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlCard;
        private Label lblLogo;
        private Label lblSubtitle;
        private Label lblUsernameHint;
        private TextBox txtUsername;
        private Label lblPasswordHint;
        private TextBox txtPassword;
        private Button btnLogin;
        private Label lblError;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlCard = new Panel();
            this.lblLogo = new Label();
            this.lblSubtitle = new Label();
            this.lblUsernameHint = new Label();
            this.txtUsername = new TextBox();
            this.lblPasswordHint = new Label();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.lblError = new Label();
            this.pnlCard.SuspendLayout();
            this.SuspendLayout();

            // === Form ===
            this.Text = "TaskBoard — Giriş";
            this.Size = new Size(460, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 9.5f);

            // === Card Panel ===
            pnlCard.Size = new Size(360, 420);
            pnlCard.Location = new Point(
                (this.ClientSize.Width - pnlCard.Width) / 2,
                (this.ClientSize.Height - pnlCard.Height) / 2
            );
            pnlCard.BackColor = Color.FromArgb(30, 30, 48);
            pnlCard.BorderStyle = BorderStyle.None;
            pnlCard.Paint += PnlCard_Paint;

            // === Logo ===
            lblLogo.Text = "📋 TaskBoard";
            lblLogo.Font = new Font("Segoe UI", 20f, FontStyle.Bold);
            lblLogo.ForeColor = Color.FromArgb(99, 102, 241);
            lblLogo.AutoSize = false;
            lblLogo.Size = new Size(340, 45);
            lblLogo.Location = new Point(10, 28);
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            // === Subtitle ===
            lblSubtitle.Text = "Kurumsal Görev Panosu";
            lblSubtitle.Font = new Font("Segoe UI", 10f);
            lblSubtitle.ForeColor = Color.FromArgb(148, 163, 184);
            lblSubtitle.AutoSize = false;
            lblSubtitle.Size = new Size(340, 22);
            lblSubtitle.Location = new Point(10, 75);
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            // === Username hint ===
            lblUsernameHint.Text = "KULLANICI ADI";
            lblUsernameHint.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblUsernameHint.ForeColor = Color.FromArgb(99, 102, 241);
            lblUsernameHint.AutoSize = true;
            lblUsernameHint.Location = new Point(20, 120);

            // === Username input ===
            txtUsername.Size = new Size(320, 38);
            txtUsername.Location = new Point(20, 142);
            txtUsername.BackColor = Color.FromArgb(45, 45, 65);
            txtUsername.ForeColor = Color.FromArgb(226, 232, 240);
            txtUsername.BorderStyle = BorderStyle.FixedSingle;
            txtUsername.Font = new Font("Segoe UI", 11f);
            txtUsername.KeyDown += txtUsername_KeyDown;

            // === Password hint ===
            lblPasswordHint.Text = "ŞİFRE";
            lblPasswordHint.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
            lblPasswordHint.ForeColor = Color.FromArgb(99, 102, 241);
            lblPasswordHint.AutoSize = true;
            lblPasswordHint.Location = new Point(20, 200);

            // === Password input ===
            txtPassword.Size = new Size(320, 38);
            txtPassword.Location = new Point(20, 222);
            txtPassword.BackColor = Color.FromArgb(45, 45, 65);
            txtPassword.ForeColor = Color.FromArgb(226, 232, 240);
            txtPassword.BorderStyle = BorderStyle.FixedSingle;
            txtPassword.Font = new Font("Segoe UI", 11f);
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += txtPassword_KeyDown;

            // === Error label ===
            lblError.Text = "";
            lblError.ForeColor = Color.FromArgb(248, 113, 113);
            lblError.Font = new Font("Segoe UI", 9f);
            lblError.AutoSize = false;
            lblError.Size = new Size(320, 20);
            lblError.Location = new Point(20, 270);
            lblError.Visible = false;

            // === Login button ===
            btnLogin.Text = "GİRİŞ YAP";
            btnLogin.Size = new Size(320, 46);
            btnLogin.Location = new Point(20, 300);
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.BackColor = Color.FromArgb(99, 102, 241);
            btnLogin.ForeColor = Color.White;
            btnLogin.Font = new Font("Segoe UI", 11f, FontStyle.Bold);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Click += btnLogin_Click;

            // === Hint (demo creds) ===
            var lblHint = new Label
            {
                Text = "Demo: admin / admin123  •  kullanici1 / kullanici123",
                Font = new Font("Segoe UI", 8f),
                ForeColor = Color.FromArgb(100, 116, 139),
                AutoSize = false,
                Size = new Size(340, 36),
                Location = new Point(10, 365),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add to card
            pnlCard.Controls.AddRange(new Control[] {
                lblLogo, lblSubtitle,
                lblUsernameHint, txtUsername,
                lblPasswordHint, txtPassword,
                lblError, btnLogin, lblHint
            });

            this.Controls.Add(pnlCard);

            this.pnlCard.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void PnlCard_Paint(object? sender, PaintEventArgs e)
        {
            // Yuvarlak köşe efekti
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
            int radius = 16;
            using var path = GetRoundedRect(rect, radius);
            using var brush = new System.Drawing.SolidBrush(pnlCard.BackColor);
            g.FillPath(brush, path);
            using var pen = new System.Drawing.Pen(Color.FromArgb(55, 55, 80), 1);
            g.DrawPath(pen, path);
        }

        private System.Drawing.Drawing2D.GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
