namespace TaskBoard
{
    public partial class AddProjectDialog : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string ProjectName { get; private set; } = string.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string ProjectDescription { get; private set; } = string.Empty;

        private TextBox txtName = null!;
        private TextBox txtDesc = null!;

        public AddProjectDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Proje adı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ProjectName = txtName.Text.Trim();
            ProjectDescription = txtDesc.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.Text = "Yeni Proje Oluştur";
            this.Size = new Size(420, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 10f);

            var lblName = new Label
            {
                Text = "PROJE ADI",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            txtName = new TextBox
            {
                Size = new Size(370, 30),
                Location = new Point(20, 42),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f)
            };

            var lblDesc = new Label
            {
                Text = "AÇIKLAMA",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 84)
            };

            txtDesc = new TextBox
            {
                Size = new Size(370, 70),
                Location = new Point(20, 106),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10f),
                Multiline = true
            };

            var btnOk = new Button
            {
                Text = "Oluştur",
                Size = new Size(100, 36),
                Location = new Point(290, 185),
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
                Location = new Point(192, 185),
                FlatStyle = FlatStyle.Flat,
                FlatAppearance = { BorderColor = Color.FromArgb(71, 85, 105) },
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 10f),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };

            this.Controls.AddRange(new Control[] { lblName, txtName, lblDesc, txtDesc, btnOk, btnCancel });
        }
    }
}
