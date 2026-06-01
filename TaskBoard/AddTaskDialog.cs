using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskBoard
{
    // private değil, public olmalı ki proje diğer kısımları onu görebilsin
    public partial class AddTaskDialog : Form
    {
        // Eğer diğer partial'da alanlar/ctor yoksa, o zaman bu dosyaya tekrar eklemeniz gerekir.
        // Eksik olan kontrol alanlarını burada tanımlıyoruz ki SetupDialogUI ve event handler'lar erişebilsin.
        private TextBox titleTextBox;
        private RichTextBox descriptionBox;
        private ComboBox priorityComboBox;
        private DateTimePicker dueDatePicker;
        private Button saveButton;
        private Button cancelButton;

        // Bu partial dosyada aynı üyelerin başka bir partial dosyada da tanımlanması
        // CS0111 hatasına yol açıyordu. Alan ve ctor bildirimleri diğer partial'da
        // (Designer veya başka bir dosya) zaten tanımlıysa buradan kaldırıldı.
        // Bu dosyada yalnızca UI oluşturma/metotları ve event handler'lar bırakıldı.

        // Eğer diğer partial'da alanlar/ctor yoksa, o zaman bu dosyaya tekrar eklemeniz gerekir.

        private void SetupDialogUI()
        {
            this.Text = "Yeni Görev Oluştur";
            this.Size = new Size(400, 480);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(255, 255, 255);

            Label lblT = new Label { Text = "GÖREV BAŞLIĞI", Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(24, 20), ForeColor = Color.FromArgb(75, 85, 99), AutoSize = true };
            titleTextBox = new TextBox { Location = new Point(24, 42), Width = 336, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.FixedSingle };

            Label lblD = new Label { Text = "GÖREV AÇIKLAMASI", Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(24, 90), ForeColor = Color.FromArgb(75, 85, 99), AutoSize = true };
            descriptionBox = new RichTextBox { Location = new Point(24, 112), Width = 336, Height = 120, Font = new Font("Segoe UI", 10), BorderStyle = BorderStyle.FixedSingle };

            Label lblP = new Label { Text = "ÖNCELİK DURUMU", Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(24, 250), ForeColor = Color.FromArgb(75, 85, 99), AutoSize = true };
            priorityComboBox = new ComboBox { Location = new Point(24, 272), Width = 336, Font = new Font("Segoe UI", 10), DropDownStyle = ComboBoxStyle.DropDownList };
            priorityComboBox.Items.AddRange(new string[] { "Düşük", "Orta", "Yüksek" });
            priorityComboBox.SelectedIndex = 0;

            Label lblDt = new Label { Text = "TESLİM TARİHİ", Font = new Font("Segoe UI", 9, FontStyle.Bold), Location = new Point(24, 320), ForeColor = Color.FromArgb(75, 85, 99), AutoSize = true };
            dueDatePicker = new DateTimePicker { Location = new Point(24, 342), Width = 336, Font = new Font("Segoe UI", 10), Format = DateTimePickerFormat.Short };

            saveButton = new Button { Text = "Görevi Ekle", Location = new Point(220, 395), Size = new Size(140, 38), BackColor = Color.FromArgb(37, 99, 235), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Click += BtnSave_Click;

            cancelButton = new Button { Text = "İptal", Location = new Point(24, 395), Size = new Size(100, 38), BackColor = Color.FromArgb(243, 244, 246), ForeColor = Color.FromArgb(107, 114, 128), Font = new Font("Segoe UI", 10, FontStyle.Bold), FlatStyle = FlatStyle.Flat };
            cancelButton.FlatAppearance.BorderSize = 0;
            cancelButton.DialogResult = DialogResult.Cancel;

            this.Controls.AddRange(new Control[] { lblT, titleTextBox, lblD, descriptionBox, lblP, priorityComboBox, lblDt, dueDatePicker, saveButton, cancelButton });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTextBox.Text))
            {
                MessageBox.Show("Görev başlığı boş bırakılamaz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dueDatePicker.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Geçmiş bir teslim tarihi seçemezsiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}