using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    public partial class BoardForm : Form
    {
        private readonly Project _project;
        private readonly Dictionary<int, Panel> _columnPanels = new();

        public BoardForm(Project project)
        {
            InitializeComponent();
            _project = project;
            this.Text = $"TaskBoard — {project.Name}";
            lblProjectName.Text = project.Name;
            LoadBoard();
            CheckOverdueTasks();
        }

        // ─────────────────────────────────────────────
        //  BOARD LOADING
        // ─────────────────────────────────────────────
        private void LoadBoard()
        {
            pnlBoard.Controls.Clear();
            _columnPanels.Clear();

            using var db = new AppDbContext();
            var columns = db.BoardColumns
                .Include(c => c.Tasks)
                    .ThenInclude(t => t.Attachments)
                .Include(c => c.Tasks)
                    .ThenInclude(t => t.ChecklistItems)
                .Include(c => c.Tasks)
                    .ThenInclude(t => t.AssignedUser)
                .Where(c => c.ProjectId == _project.Id)
                .OrderBy(c => c.OrderIndex)
                .ToList();

            int x = 12;
            foreach (var col in columns)
            {
                var colPanel = CreateColumnPanel(col);
                colPanel.Location = new Point(x, 12);
                pnlBoard.Controls.Add(colPanel);
                _columnPanels[col.Id] = colPanel;
                x += colPanel.Width + 14;
            }
        }

        private void CheckOverdueTasks()
        {
            using var db = new AppDbContext();
            var overdueCount = db.TaskCards
                .Include(t => t.BoardColumn)
                .Where(t => t.BoardColumn.ProjectId == _project.Id
                         && t.DueDate != null
                         && t.DueDate.Value.Date < DateTime.Today)
                .Count();

            if (overdueCount > 0)
            {
                MessageBox.Show(
                    $"⚠  Bu projede {overdueCount} adet gecikmiş görev var!",
                    "Gecikmiş Görevler",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        // ─────────────────────────────────────────────
        //  COLUMN PANEL
        // ─────────────────────────────────────────────
        private Panel CreateColumnPanel(BoardColumn col)
        {
            var colPanel = new Panel
            {
                Size = new Size(270, pnlBoard.Height - 30),
                BackColor = Color.FromArgb(22, 22, 36),
                Tag = col.Id,
                AllowDrop = true
            };

            var header = new Panel
            {
                Size = new Size(270, 44),
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(30, 30, 48)
            };

            var lblColName = new Label
            {
                Text = col.Name.ToUpper(),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Location = new Point(12, 12)
            };

            var badgeCount = new Label
            {
                Text = col.Tasks.Count.ToString(),
                Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                BackColor = Color.FromArgb(55, 55, 80),
                ForeColor = Color.FromArgb(199, 210, 254),
                AutoSize = false,
                Size = new Size(26, 20),
                Location = new Point(col.Name.Length > 8 ? 145 : 115, 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Name = "badgeCount"
            };

            var btnAdd = new Button
            {
                Text = "＋",
                Size = new Size(30, 26),
                Location = new Point(232, 9),
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(99, 102, 241),
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 12f),
                Cursor = Cursors.Hand,
                Tag = col.Id
            };
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += BtnAddCard_Click;

            header.Controls.AddRange(new Control[] { lblColName, badgeCount, btnAdd });

            var cardsPanel = new FlowLayoutPanel
            {
                Location = new Point(0, 44),
                Size = new Size(270, colPanel.Height - 44),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.Transparent,
                Name = "cardsPanel",
                AllowDrop = true,
                Tag = col.Id
            };

            cardsPanel.DragEnter += CardsPanel_DragEnter;
            cardsPanel.DragOver += CardsPanel_DragOver;
            cardsPanel.DragDrop += CardsPanel_DragDrop;

            foreach (var task in col.Tasks.OrderBy(t => t.OrderIndex))
                cardsPanel.Controls.Add(CreateTaskCardControl(task));

            colPanel.Controls.Add(header);
            colPanel.Controls.Add(cardsPanel);
            return colPanel;
        }

        // ─────────────────────────────────────────────
        //  TASK CARD CONTROL
        // ─────────────────────────────────────────────
        private TaskCardControl CreateTaskCardControl(TaskCard task)
        {
            var ctrl = new TaskCardControl(task);
            ctrl.CardDoubleClicked += OnCardDoubleClicked;
            ctrl.CardDeleteClicked += OnCardDeleted;
            ctrl.MouseDown += TaskCard_MouseDown;
            return ctrl;
        }

        // ─────────────────────────────────────────────
        //  DRAG & DROP
        // ─────────────────────────────────────────────
        private void TaskCard_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && sender is TaskCardControl ctrl)
                ctrl.DoDragDrop(ctrl, DragDropEffects.Move);
        }

        private void CardsPanel_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(typeof(TaskCardControl)) == true)
                e.Effect = DragDropEffects.Move;
        }

        private void CardsPanel_DragOver(object? sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(typeof(TaskCardControl)) == true)
                e.Effect = DragDropEffects.Move;
        }

        private void CardsPanel_DragDrop(object? sender, DragEventArgs e)
        {
            if (sender is not FlowLayoutPanel targetPanel) return;
            if (e.Data?.GetData(typeof(TaskCardControl)) is not TaskCardControl draggedCard) return;

            int targetColumnId = (int)targetPanel.Tag!;
            Point dropPoint = targetPanel.PointToClient(new Point(e.X, e.Y));
            int insertIndex = GetInsertIndex(targetPanel, dropPoint);

            var sourcePanel = draggedCard.Parent as FlowLayoutPanel;
            sourcePanel?.Controls.Remove(draggedCard);

            targetPanel.Controls.Add(draggedCard);
            targetPanel.Controls.SetChildIndex(draggedCard, insertIndex);

            SaveOrderIndexes(targetColumnId, targetPanel, draggedCard.TaskId);
            if (sourcePanel != null && sourcePanel != targetPanel)
            {
                int srcColId = (int)sourcePanel.Tag!;
                SaveOrderIndexes(srcColId, sourcePanel, null);
                UpdateBadge(sourcePanel);
            }
            UpdateBadge(targetPanel);
        }

        private int GetInsertIndex(FlowLayoutPanel panel, Point dropPoint)
        {
            for (int i = 0; i < panel.Controls.Count; i++)
                if (dropPoint.Y < panel.Controls[i].Bottom)
                    return i;
            return panel.Controls.Count;
        }

        private void SaveOrderIndexes(int columnId, FlowLayoutPanel panel, int? movedTaskId)
        {
            using var db = new AppDbContext();
            for (int i = 0; i < panel.Controls.Count; i++)
            {
                if (panel.Controls[i] is TaskCardControl tcc)
                {
                    var task = db.TaskCards.Find(tcc.TaskId);
                    if (task != null)
                    {
                        task.BoardColumnId = columnId;
                        task.OrderIndex = i;
                    }
                }
            }
            db.SaveChanges();
        }

        private void UpdateBadge(FlowLayoutPanel panel)
        {
            var colPanel = panel.Parent as Panel;
            var header = colPanel?.Controls.OfType<Panel>().FirstOrDefault();
            var badge = header?.Controls.Find("badgeCount", false).FirstOrDefault() as Label;
            if (badge != null)
                badge.Text = panel.Controls.OfType<TaskCardControl>().Count(c => c.Visible).ToString();
        }

        // ─────────────────────────────────────────────
        //  FILTER
        // ─────────────────────────────────────────────
        private void txtSearch_TextChanged(object? sender, EventArgs e) => ApplyFilter();
        private void cmbPriorityFilter_SelectedIndexChanged(object? sender, EventArgs e) => ApplyFilter();

        private void ApplyFilter()
        {
            string search = txtSearch.Text.Trim();
            TaskPriority? pFilter = cmbPriorityFilter.SelectedIndex switch
            {
                1 => TaskPriority.None,
                2 => TaskPriority.Low,
                3 => TaskPriority.Medium,
                4 => TaskPriority.High,
                5 => TaskPriority.Critical,
                _ => null
            };

            foreach (var colPanel in _columnPanels.Values)
            {
                var cardsPanel = colPanel.Controls.Find("cardsPanel", false).FirstOrDefault() as FlowLayoutPanel;
                if (cardsPanel == null) continue;

                foreach (Control ctrl in cardsPanel.Controls)
                {
                    if (ctrl is TaskCardControl tcc)
                    {
                        bool matchSearch = string.IsNullOrEmpty(search) ||
                            tcc.TaskTitle.Contains(search, StringComparison.OrdinalIgnoreCase);
                        bool matchPriority = pFilter == null || tcc.TaskPriority == pFilter;
                        ctrl.Visible = matchSearch && matchPriority;
                    }
                }
                UpdateBadge(cardsPanel);
            }
        }

        // ─────────────────────────────────────────────
        //  ADD CARD
        // ─────────────────────────────────────────────
        private void BtnAddCard_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            int columnId = (int)btn.Tag!;

            using var dlg = new AddTaskDialog();
            if (dlg.ShowDialog() != DialogResult.OK) return;

            using var db = new AppDbContext();
            int maxOrder = db.TaskCards
                .Where(t => t.BoardColumnId == columnId)
                .Select(t => (int?)t.OrderIndex).Max() ?? -1;

            var task = new TaskCard
            {
                BoardColumnId = columnId,
                Title = dlg.TaskTitle,
                Description = dlg.TaskDescription,
                Priority = dlg.SelectedPriority,
                OrderIndex = maxOrder + 1
            };
            db.TaskCards.Add(task);
            db.SaveChanges();

            var colPanel = FindColumnPanel(columnId);
            var cardsPanel = colPanel?.Controls.Find("cardsPanel", false).FirstOrDefault() as FlowLayoutPanel;
            if (cardsPanel != null)
            {
                task = db.TaskCards
                    .Include(t => t.Attachments)
                    .Include(t => t.ChecklistItems)
                    .Include(t => t.AssignedUser)
                    .First(t => t.Id == task.Id);
                cardsPanel.Controls.Add(CreateTaskCardControl(task));
                UpdateBadge(cardsPanel);
            }
        }

        private Panel? FindColumnPanel(int columnId) =>
            pnlBoard.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Tag is int id && id == columnId);

        // ─────────────────────────────────────────────
        //  ADD COLUMN
        // ─────────────────────────────────────────────
        private void btnAddColumn_Click(object? sender, EventArgs e)
        {
            string? name = Microsoft.VisualBasic.Interaction.InputBox(
                "Yeni sütun adını girin:", "Sütun Ekle", "Yeni Sütun");
            if (string.IsNullOrWhiteSpace(name)) return;

            using var db = new AppDbContext();
            int maxOrder = db.BoardColumns
                .Where(c => c.ProjectId == _project.Id)
                .Select(c => (int?)c.OrderIndex).Max() ?? -1;

            var col = new BoardColumn
            {
                ProjectId = _project.Id,
                Name = name.Trim(),
                OrderIndex = maxOrder + 1
            };
            db.BoardColumns.Add(col);
            db.SaveChanges();

            var colPanel = CreateColumnPanel(col);
            int x = _columnPanels.Values.Any()
                ? _columnPanels.Values.Max(p => p.Right) + 14
                : 12;
            colPanel.Location = new Point(x, 12);
            pnlBoard.Controls.Add(colPanel);
            _columnPanels[col.Id] = colPanel;
        }

        // ─────────────────────────────────────────────
        //  DASHBOARD
        // ─────────────────────────────────────────────
        private void btnDashboard_Click(object? sender, EventArgs e)
        {
            using var dash = new DashboardForm(_project);
            dash.ShowDialog(this);
        }

        // ─────────────────────────────────────────────
        //  CARD EVENTS
        // ─────────────────────────────────────────────
        private void OnCardDoubleClicked(TaskCardControl ctrl)
        {
            using var db = new AppDbContext();
            var task = db.TaskCards
                .Include(t => t.Attachments)
                .Include(t => t.ChecklistItems)
                .Include(t => t.AssignedUser)
                .FirstOrDefault(t => t.Id == ctrl.TaskId);
            if (task == null) return;

            using var detailForm = new TaskDetailForm(task);
            detailForm.ShowDialog();
            // Kart üzerindeki tüm bilgileri güncelle
            ctrl.RefreshCard();
        }

        private void OnCardDeleted(TaskCardControl ctrl)
        {
            var result = MessageBox.Show(
                $"'{ctrl.TaskTitle}' görevi silinecek. Emin misiniz?",
                "Görev Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            using var db = new AppDbContext();
            var task = db.TaskCards
                .Include(t => t.Attachments)
                .Include(t => t.ChecklistItems)
                .FirstOrDefault(t => t.Id == ctrl.TaskId);
            if (task != null)
            {
                db.ChecklistItems.RemoveRange(task.ChecklistItems);
                db.Attachments.RemoveRange(task.Attachments);
                db.TaskCards.Remove(task);
                db.SaveChanges();
            }

            var panel = ctrl.Parent as FlowLayoutPanel;
            panel?.Controls.Remove(ctrl);
            if (panel != null) UpdateBadge(panel);
        }

        private void btnBackToProjects_Click(object sender, EventArgs e) => this.Close();

        private void pnlBoard_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
