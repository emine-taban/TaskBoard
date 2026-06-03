namespace TaskBoard
{
    public partial class AddUserDialog : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string NewUsername { get; private set; } = string.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string NewPassword { get; private set; } = string.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsAdmin { get; private set; }

        private TextBox txtUsername = null!;
        private TextBox txtPassword = null!;
        private CheckBox chkAdmin = null!;

        public AddUserDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NewUsername = txtUsername.Text.Trim();
            NewPassword = txtPassword.Text;
            IsAdmin = chkAdmin.Checked;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.Text = "Yeni Kullanıcı";
            this.Size = new Size(380, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 10f);

            var lblU = new Label
            {
                Text = "KULLANICI ADI",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            txtUsername = new TextBox
            {
                Size = new Size(330, 30),
                Location = new Point(20, 42),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f)
            };

            var lblP = new Label
            {
                Text = "ŞİFRE",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 86)
            };

            txtPassword = new TextBox
            {
                Size = new Size(330, 30),
                Location = new Point(20, 108),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f),
                UseSystemPasswordChar = true
            };

            chkAdmin = new CheckBox
            {
                Text = "Admin Rolü",
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(20, 152),
                AutoSize = true
            };

            var btnOk = new Button
            {
                Text = "Oluştur",
                Size = new Size(100, 36),
                Location = new Point(250, 180),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderSize = 0 },
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOk.Click += btnOk_Click;

            var btnCancel = new Button
            {
                Text = "İptal",
                Size = new Size(90, 36),
                Location = new Point(152, 180),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = Color.FromArgb(71, 85, 105) },
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblU, txtUsername, lblP, txtPassword, chkAdmin, btnOk, btnCancel });
        }
    }
}
