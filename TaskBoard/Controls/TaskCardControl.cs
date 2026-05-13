using Microsoft.EntityFrameworkCore;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    /// <summary>
    /// Kanban panolarındaki her bir görev kartını temsil eden UserControl.
    /// </summary>
    public class TaskCardControl : Panel
    {
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int TaskId { get; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public string TaskTitle { get; private set; }
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public TaskPriority TaskPriority { get; private set; }

        public event Action<TaskCardControl>? CardDoubleClicked;
        public event Action<TaskCardControl>? CardDeleteClicked;

        private Label _lblTitle = null!;
        private Label _lblDueDate = null!;
        private Label _lblChecklist = null!;
        private Label _lblAttach = null!;
        private Label _lblAvatar = null!;
        private Button _btnDelete = null!;
        private Panel _leftBar = null!;

        public TaskCardControl(TaskCard task)
        {
            TaskId = task.Id;
            TaskTitle = task.Title;
            TaskPriority = task.Priority;
            BuildUI(task);
        }

        private void BuildUI(TaskCard task)
        {
            this.Size = new Size(246, 88);
            this.Margin = new Padding(8, 6, 8, 0);
            this.BackColor = Color.FromArgb(30, 30, 50);
            this.Cursor = Cursors.SizeAll;

            // Sol renk çubuğu (priority'ye göre)
            _leftBar = new Panel
            {
                Size = new Size(4, 88),
                Location = new Point(0, 0),
                BackColor = GetPriorityColor(task.Priority)
            };

            // Başlık
            _lblTitle = new Label
            {
                Text = task.Title,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                ForeColor = Color.FromArgb(226, 232, 240),
                Location = new Point(13, 7),
                Size = new Size(196, 36),
                AutoEllipsis = true
            };

            // --- Alt bilgi satırı ---
            // Due Date
            _lblDueDate = new Label
            {
                Font = new Font("Segoe UI", 7.5f),
                AutoSize = true,
                Location = new Point(13, 62)
            };
            UpdateDueDateLabel(task.DueDate);

            // Checklist progress
            _lblChecklist = new Label
            {
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Location = new Point(85, 62)
            };
            int total = task.ChecklistItems.Count;
            int done = task.ChecklistItems.Count(c => c.IsCompleted);
            _lblChecklist.Text = total > 0 ? $"☑ {done}/{total}" : "";

            // Ek dosya göstergesi
            _lblAttach = new Label
            {
                Text = task.Attachments.Any() ? $"📎{task.Attachments.Count}" : "",
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(148, 62)
            };

            // Atanan kullanıcı avatar
            _lblAvatar = new Label
            {
                Size = new Size(22, 22),
                Location = new Point(218, 58),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(99, 102, 241),
                Visible = false
            };
            UpdateAvatar(task.AssignedUser);

            // Sil butonu
            _btnDelete = new Button
            {
                Text = "✕",
                Size = new Size(22, 22),
                Location = new Point(220, 4),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand,
                Visible = false
            };
            _btnDelete.FlatAppearance.BorderSize = 0;
            _btnDelete.Click += (s, e) => CardDeleteClicked?.Invoke(this);

            this.Controls.AddRange(new Control[] { _leftBar, _lblTitle, _lblDueDate, _lblChecklist, _lblAttach, _lblAvatar, _btnDelete });

            // Hover events
            AttachHoverEvents(this);
            AttachHoverEvents(_lblTitle);
            AttachHoverEvents(_lblDueDate);
            AttachHoverEvents(_lblChecklist);
            AttachHoverEvents(_lblAttach);
            AttachHoverEvents(_leftBar);

            this.DoubleClick += (s, e) => CardDoubleClicked?.Invoke(this);
            _lblTitle.DoubleClick += (s, e) => CardDoubleClicked?.Invoke(this);
        }

        private void AttachHoverEvents(Control c)
        {
            c.MouseEnter += (s, e) => { this.BackColor = Color.FromArgb(38, 38, 62); _btnDelete.Visible = true; };
            c.MouseLeave += (s, e) =>
            {
                if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                {
                    this.BackColor = Color.FromArgb(30, 30, 50);
                    _btnDelete.Visible = false;
                }
            };
        }

        // ─── Public Refresh Methods ───────────────────────────────────

        public void RefreshCard()
        {
            using var db = new Data.AppDbContext();
            var task = db.TaskCards
                .Include(t => t.Attachments)
                .Include(t => t.ChecklistItems)
                .Include(t => t.AssignedUser)
                .FirstOrDefault(t => t.Id == TaskId);
            if (task == null) return;

            TaskTitle = task.Title;
            TaskPriority = task.Priority;
            _lblTitle.Text = task.Title;
            _leftBar.BackColor = GetPriorityColor(task.Priority);

            UpdateDueDateLabel(task.DueDate);

            int done = task.ChecklistItems.Count(c => c.IsCompleted);
            int total = task.ChecklistItems.Count;
            _lblChecklist.Text = total > 0 ? $"☑ {done}/{total}" : "";

            _lblAttach.Text = task.Attachments.Any() ? $"📎{task.Attachments.Count}" : "";

            UpdateAvatar(task.AssignedUser);
        }

        public void RefreshTitle()
        {
            using var db = new Data.AppDbContext();
            var task = db.TaskCards.Find(TaskId);
            if (task != null) { TaskTitle = task.Title; _lblTitle.Text = task.Title; }
        }

        public void RefreshAttachmentCount(int count)
        {
            _lblAttach.Text = count > 0 ? $"📎{count}" : "";
        }

        // ─── Helpers ─────────────────────────────────────────────────

        private void UpdateDueDateLabel(DateTime? due)
        {
            if (due == null) { _lblDueDate.Text = ""; return; }
            bool overdue = due.Value.Date < DateTime.Today;
            _lblDueDate.ForeColor = overdue ? Color.FromArgb(248, 113, 113) : Color.FromArgb(100, 116, 139);
            _lblDueDate.Text = (overdue ? "⚠ " : "📅 ") + due.Value.ToString("dd.MM.yy");
        }

        private void UpdateAvatar(Data.Models.User? user)
        {
            if (user == null) { _lblAvatar.Visible = false; return; }
            _lblAvatar.Text = user.Username.Length > 0 ? user.Username[0].ToString().ToUpper() : "?";
            _lblAvatar.Visible = true;
        }

        public static Color GetPriorityColor(TaskPriority p) => p switch
        {
            TaskPriority.Low      => Color.FromArgb(34, 197, 94),
            TaskPriority.Medium   => Color.FromArgb(234, 179, 8),
            TaskPriority.High     => Color.FromArgb(249, 115, 22),
            TaskPriority.Critical => Color.FromArgb(239, 68, 68),
            _                     => Color.FromArgb(99, 102, 241)
        };
    }
}
