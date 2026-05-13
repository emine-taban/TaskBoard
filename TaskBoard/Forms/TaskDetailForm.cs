using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    public partial class TaskDetailForm : Form
    {
        private readonly TaskCard _task;
        private readonly string _attachmentsDir;

        // Kullanıcı listesi: ComboBox için (Id, Username çifti)
        private List<(int Id, string Name)> _userList = new();

        public TaskDetailForm(TaskCard task)
        {
            InitializeComponent();
            _task = task;
            _attachmentsDir = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "Attachments", task.Id.ToString());
            Directory.CreateDirectory(_attachmentsDir);

            txtTitle.Text       = task.Title;
            txtDescription.Text = task.Description;
            lblCreated.Text     = $"Oluşturulma: {task.CreatedAt:dd.MM.yyyy HH:mm}";

            // Priority
            cmbPriority.SelectedIndex = (int)task.Priority;

            // Due Date
            if (task.DueDate.HasValue)
            {
                chkDueDate.Checked = true;
                dtpDueDate.Value   = task.DueDate.Value;
                dtpDueDate.Enabled = true;
            }

            LoadUserList(task.AssignedUserId);
            LoadChecklist();
            LoadAttachments();
        }

        // ─────────────────────────────────────────────
        //  USER LIST (Atanan Kişi)
        // ─────────────────────────────────────────────
        private void LoadUserList(int? selectedUserId)
        {
            using var db = new AppDbContext();
            _userList = db.Users
                .OrderBy(u => u.Username)
                .Select(u => new { u.Id, u.Username })
                .AsEnumerable()
                .Select(u => (u.Id, u.Username))
                .ToList();

            cmbAssignedUser.Items.Clear();
            cmbAssignedUser.Items.Add("— Atanmadı —");
            foreach (var u in _userList)
                cmbAssignedUser.Items.Add(u.Name);

            if (selectedUserId.HasValue)
            {
                int idx = _userList.FindIndex(u => u.Id == selectedUserId.Value);
                cmbAssignedUser.SelectedIndex = idx >= 0 ? idx + 1 : 0;
            }
            else
            {
                cmbAssignedUser.SelectedIndex = 0;
            }
        }

        // ─────────────────────────────────────────────
        //  CHECKLIST
        // ─────────────────────────────────────────────
        private void LoadChecklist()
        {
            clbChecklist.Items.Clear();
            using var db = new AppDbContext();
            var items = db.ChecklistItems
                .Where(c => c.TaskCardId == _task.Id)
                .OrderBy(c => c.OrderIndex)
                .ToList();

            foreach (var item in items)
                clbChecklist.Items.Add(item, item.IsCompleted);

            UpdateChecklistProgress();
        }

        private void UpdateChecklistProgress()
        {
            int total = clbChecklist.Items.Count;
            int done  = clbChecklist.CheckedItems.Count;
            lblChecklistProgress.Text = $"{done} / {total} tamamlandı";
            lblChecklistProgress.ForeColor = (total > 0 && done == total)
                ? Color.FromArgb(34, 197, 94)
                : Color.FromArgb(99, 102, 241);
        }

        private void clbChecklist_ItemCheck(object? sender, ItemCheckEventArgs e)
        {
            if (clbChecklist.Items[e.Index] is not ChecklistItem item) return;
            using var db = new AppDbContext();
            var dbItem = db.ChecklistItems.Find(item.Id);
            if (dbItem != null)
            {
                dbItem.IsCompleted = (e.NewValue == CheckState.Checked);
                db.SaveChanges();
            }
            // Progress'i bir sonraki render döngüsünde güncelle
            this.BeginInvoke(() => UpdateChecklistProgress());
        }

        private void btnAddItem_Click(object? sender, EventArgs e)
        {
            string text = txtNewItem.Text.Trim();
            if (string.IsNullOrEmpty(text)) return;

            using var db = new AppDbContext();
            int maxOrder = db.ChecklistItems
                .Where(c => c.TaskCardId == _task.Id)
                .Select(c => (int?)c.OrderIndex).Max() ?? -1;

            var item = new ChecklistItem
            {
                TaskCardId = _task.Id,
                Text       = text,
                IsCompleted = false,
                OrderIndex  = maxOrder + 1
            };
            db.ChecklistItems.Add(item);
            db.SaveChanges();

            clbChecklist.Items.Add(item, false);
            txtNewItem.Clear();
            UpdateChecklistProgress();
        }

        private void txtNewItem_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnAddItem_Click(sender, e);
        }

        private void btnDeleteItem_Click(object? sender, EventArgs e)
        {
            if (clbChecklist.SelectedIndex < 0) return;
            if (clbChecklist.SelectedItem is not ChecklistItem item) return;

            using var db = new AppDbContext();
            var dbItem = db.ChecklistItems.Find(item.Id);
            if (dbItem != null) { db.ChecklistItems.Remove(dbItem); db.SaveChanges(); }

            clbChecklist.Items.RemoveAt(clbChecklist.SelectedIndex);
            UpdateChecklistProgress();
        }

        // ─────────────────────────────────────────────
        //  SAVE
        // ─────────────────────────────────────────────
        private void btnSave_Click(object? sender, EventArgs e)
        {
            using var db = new AppDbContext();
            var task = db.TaskCards.Find(_task.Id);
            if (task == null) return;

            task.Title       = txtTitle.Text.Trim();
            task.Description = txtDescription.Text.Trim();
            task.Priority    = (TaskPriority)cmbPriority.SelectedIndex;
            task.DueDate     = chkDueDate.Checked ? dtpDueDate.Value.Date : null;

            if (cmbAssignedUser.SelectedIndex <= 0)
                task.AssignedUserId = null;
            else
                task.AssignedUserId = _userList[cmbAssignedUser.SelectedIndex - 1].Id;

            db.SaveChanges();
            MessageBox.Show("Değişiklikler kaydedildi.", "Kaydedildi",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ─────────────────────────────────────────────
        //  ATTACHMENTS
        // ─────────────────────────────────────────────
        private void LoadAttachments()
        {
            lvAttachments.Items.Clear();
            using var db = new AppDbContext();
            var attachments = db.Attachments.Where(a => a.TaskCardId == _task.Id).ToList();
            foreach (var att in attachments)
            {
                var item = new ListViewItem(att.FileName);
                item.SubItems.Add(FormatBytes(att.FileSizeBytes));
                item.SubItems.Add(att.UploadedAt.ToString("dd.MM.yyyy"));
                item.Tag = att;
                lvAttachments.Items.Add(item);
            }
        }

        private void btnAddFile_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog
            {
                Title = "Dosya Seç",
                Filter = "Tüm Dosyalar (*.*)|*.*",
                Multiselect = true
            };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            foreach (var filePath in ofd.FileNames)
            {
                string fileName = Path.GetFileName(filePath);
                string destPath = Path.Combine(_attachmentsDir, fileName);
                int counter = 1;
                while (File.Exists(destPath))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string ext  = Path.GetExtension(fileName);
                    destPath = Path.Combine(_attachmentsDir, $"{name}_{counter++}{ext}");
                }
                File.Copy(filePath, destPath);

                using var db = new AppDbContext();
                db.Attachments.Add(new Attachment
                {
                    TaskCardId    = _task.Id,
                    FileName      = Path.GetFileName(destPath),
                    FilePath      = destPath,
                    FileSizeBytes = new FileInfo(destPath).Length
                });
                db.SaveChanges();
            }
            LoadAttachments();
        }

        private void btnOpenFile_Click(object? sender, EventArgs e)
        {
            if (lvAttachments.SelectedItems.Count == 0) return;
            if (lvAttachments.SelectedItems[0].Tag is Attachment att)
            {
                if (!File.Exists(att.FilePath))
                {
                    MessageBox.Show("Dosya bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = att.FilePath,
                    UseShellExecute = true
                });
            }
        }

        private void btnDeleteFile_Click(object? sender, EventArgs e)
        {
            if (lvAttachments.SelectedItems.Count == 0) return;
            if (lvAttachments.SelectedItems[0].Tag is not Attachment att) return;

            var result = MessageBox.Show($"'{att.FileName}' silinecek. Emin misiniz?",
                "Ek Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try { if (File.Exists(att.FilePath)) File.Delete(att.FilePath); } catch { }

            using var db = new AppDbContext();
            var dbAtt = db.Attachments.Find(att.Id);
            if (dbAtt != null) { db.Attachments.Remove(dbAtt); db.SaveChanges(); }
            LoadAttachments();
        }

        private void lvAttachments_DoubleClick(object? sender, EventArgs e) => btnOpenFile_Click(sender, e);

        private static string FormatBytes(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F1} KB";
            return $"{bytes / (1024.0 * 1024):F1} MB";
        }
    }
}
