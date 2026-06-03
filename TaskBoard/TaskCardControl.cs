using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskBoard
{
    public partial class TaskCardControl : Panel
    {
        public Label lblTitle;
        public Label lblDescription;
        public Label lblDueDate;
        private Panel pnlPrioritySide;

        // Tasarımcı tarafından içerik serileştirmesi beklentisi oluşturmaması için
        // Priority'nin set erişimini sınırlıyoruz ve tasarımcıya görünmez yapıyoruz.
        // Eğer Priority bir koleksiyon/nesne içeriyorsa, o zaman DesignerSerializationVisibility.Content kullanılmalıdır.
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string Priority { get; private set; }

        public TaskCardControl(string title, string description, string priority, DateTime dueDate)
        {
            this.Priority = priority;
            InitializeComponent();
            SetupAdvancedUI(title, description, dueDate);
        }

        // Designer çakışmasını önermek için boş tanımlıyoruz
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }

        private void SetupAdvancedUI(string title, string description, DateTime dueDate)
        {
            this.Size = new Size(260, 110);
            this.BackColor = Color.White;
            this.Margin = new Padding(0, 0, 0, 12);
            this.Cursor = Cursors.Hand;

            Color priorityColor = Color.FromArgb(156, 163, 175);
            if (Priority == "Yüksek") priorityColor = Color.FromArgb(239, 68, 68);
            else if (Priority == "Orta") priorityColor = Color.FromArgb(245, 158, 11);
            else if (Priority == "Düşük") priorityColor = Color.FromArgb(16, 185, 129);

            pnlPrioritySide = new Panel
            {
                Dock = DockStyle.Left,
                Width = 6,
                BackColor = priorityColor
            };

            // Font konstrüktöründe bazı platformlarda string yerine FontFamily bekleyen overload tercih edilebiliyor.
            // Bu nedenle açıkça FontFamily kullanıyoruz ve boyutları float olarak veriyoruz.
            lblTitle = new Label
            {
                Text = title,
                Font = new Font(new FontFamily("Segoe UI"), 11f, FontStyle.Bold),
                Location = new Point(16, 12),
                Size = new Size(230, 22),
                ForeColor = Color.FromArgb(17, 24, 39)
            };

            lblDescription = new Label
            {
                Text = description.Length > 60 ? description.Substring(0, 57) + "..." : description,
                Font = new Font(new FontFamily("Segoe UI"), 9f, FontStyle.Regular),
                Location = new Point(16, 38),
                Size = new Size(230, 40),
                ForeColor = Color.FromArgb(107, 114, 128)
            };

            // FontStyle enum'unda 'Medium' yok; Regular kullanmak uygun olacaktır.
            lblDueDate = new Label
            {
                Text = "📅 " + dueDate.ToShortDateString(),
                Font = new Font(new FontFamily("Segoe UI"), 8f, FontStyle.Regular),
                Location = new Point(16, 84),
                Size = new Size(120, 15),
                ForeColor = Color.FromArgb(156, 163, 175)
            };

            this.Controls.AddRange(new Control[] { pnlPrioritySide, lblTitle, lblDescription, lblDueDate });

            this.MouseDown += (s, e) => { if (e.Button == MouseButtons.Left) this.DoDragDrop(this, DragDropEffects.Move); };
            this.MouseEnter += (s, e) => { this.BackColor = Color.FromArgb(249, 250, 251); };
            this.MouseLeave += (s, e) => { this.BackColor = Color.White; };
        }
    }
}