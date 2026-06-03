using TaskBoard.Data.Models;

namespace TaskBoard
{
    public partial class AddTaskDialog : Form
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string TaskTitle { get; private set; } = string.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string TaskDescription { get; private set; } = string.Empty;
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public TaskPriority SelectedPriority { get; private set; } = TaskPriority.None;

        public AddTaskDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Görev başlığı boş bırakılamaz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TaskTitle       = txtTitle.Text.Trim();
            TaskDescription = txtDescription.Text.Trim();
            SelectedPriority = (TaskPriority)cmbPriority.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.Text = "Yeni Görev Ekle";
            this.Size = new Size(420, 340);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 10f);

            var lblTitle = new Label
            {
                Text = "BAŞLIK",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            txtTitle = new TextBox
            {
                Name = "txtTitle",
                Size = new Size(370, 30),
                Location = new Point(20, 42),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f)
            };

            var lblDesc = new Label
            {
                Text = "AÇIKLAMA (İsteğe Bağlı)",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 88)
            };

            txtDescription = new TextBox
            {
                Name = "txtDescription",
                Size = new Size(370, 70),
                Location = new Point(20, 110),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10f),
                Multiline = true
            };

            var lblPriority = new Label
            {
                Text = "ÖNCELİK",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(20, 196)
            };

            cmbPriority = new ComboBox
            {
                Size = new Size(180, 28),
                Location = new Point(20, 218),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPriority.Items.AddRange(new object[] { "None", "Düşük", "Orta", "Yüksek", "Kritik" });
            cmbPriority.SelectedIndex = 0;

            var btnOk = new Button
            {
                Text = "Ekle",
                Size = new Size(100, 36),
                Location = new Point(290, 262),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnOk.FlatAppearance.BorderSize = 0;
            btnOk.Click += btnOk_Click;

            var btnCancel = new Button
            {
                Text = "İptal",
                Size = new Size(90, 36),
                Location = new Point(192, 262),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 10f),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);

            this.Controls.AddRange(new Control[]
                { lblTitle, txtTitle, lblDesc, txtDescription, lblPriority, cmbPriority, btnOk, btnCancel });
        }

        private TextBox txtTitle = null!;
        private TextBox txtDescription = null!;
        private ComboBox cmbPriority = null!;
    }
}
