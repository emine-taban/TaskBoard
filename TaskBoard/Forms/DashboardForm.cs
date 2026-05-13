using Microsoft.EntityFrameworkCore;
using TaskBoard.Data;
using TaskBoard.Data.Models;

namespace TaskBoard
{
    public class DashboardForm : Form
    {
        private readonly Project _project;

        public DashboardForm(Project project)
        {
            _project = project;
            BuildUI();
        }

        private void BuildUI()
        {
            this.Text = $"📊 Dashboard — {_project.Name}";
            this.Size = new Size(900, 660);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(12, 12, 24);
            this.Font = new Font("Segoe UI", 9.5f);

            // ─── Veri yükle ──────────────────────────────────────────
            using var db = new AppDbContext();

            var columns = db.BoardColumns
                .Include(c => c.Tasks)
                .Where(c => c.ProjectId == _project.Id)
                .OrderBy(c => c.OrderIndex)
                .ToList();

            var allTasks = columns.SelectMany(c => c.Tasks).ToList();
            int total    = allTasks.Count;
            int done     = columns
                .Where(c => c.Name.Contains("Bitti", StringComparison.OrdinalIgnoreCase))
                .SelectMany(c => c.Tasks).Count();
            int overdue  = allTasks.Count(t => t.DueDate.HasValue && t.DueDate.Value.Date < DateTime.Today);
            int inProg   = total - done;

            // ─── Header ──────────────────────────────────────────────
            var pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(18, 18, 32)
            };
            var lblTitle = new Label
            {
                Text = $"📊  {_project.Name} — Proje İstatistikleri",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(199, 210, 254),
                AutoSize = true,
                Location = new Point(16, 12)
            };
            pnlHeader.Controls.Add(lblTitle);

            // ─── Özet Kartlar ─────────────────────────────────────────
            var pnlStats = new Panel
            {
                Location = new Point(16, 66),
                Size = new Size(856, 110)
            };

            var statData = new[]
            {
                ("📋", "Toplam Görev",   total.ToString(),   Color.FromArgb(99, 102, 241)),
                ("✅", "Tamamlanan",     done.ToString(),    Color.FromArgb(34, 197, 94)),
                ("🔄", "Devam Eden",     inProg.ToString(),  Color.FromArgb(234, 179, 8)),
                ("⚠", "Gecikmiş",       overdue.ToString(), Color.FromArgb(239, 68, 68))
            };

            int sx = 0;
            foreach (var (icon, label, value, color) in statData)
            {
                var card = MakeStatCard(icon, label, value, color, new Point(sx, 0));
                pnlStats.Controls.Add(card);
                sx += 216;
            }

            // ─── Bar Chart: Sütun Bazında Görev ───────────────────────
            var lblChartTitle = new Label
            {
                Text = "SÜTUN BAZINDA GÖREV DAĞILIMI",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(16, 194)
            };

            var pnlChart = new Panel
            {
                Location = new Point(16, 216),
                Size = new Size(856, 180),
                BackColor = Color.FromArgb(18, 18, 30)
            };
            pnlChart.Paint += (s, e) => DrawBarChart(e.Graphics, pnlChart.ClientSize, columns);

            // ─── Priority Dağılımı ────────────────────────────────────
            var lblPriTitle = new Label
            {
                Text = "ÖNCELİK DAĞILIMI",
                Font = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(16, 414)
            };

            var pnlPriority = new Panel
            {
                Location = new Point(16, 436),
                Size = new Size(856, 80),
                BackColor = Color.FromArgb(18, 18, 30)
            };
            pnlPriority.Paint += (s, e) => DrawPriorityBars(e.Graphics, pnlPriority.ClientSize, allTasks);

            // ─── Kapat Butonu ──────────────────────────────────────────
            var btnClose = new Button
            {
                Text = "Kapat",
                Size = new Size(100, 36),
                Location = new Point(776, 568),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 9.5f),
                Cursor = Cursors.Hand,
                DialogResult = DialogResult.Cancel
            };
            btnClose.FlatAppearance.BorderColor = Color.FromArgb(71, 85, 105);

            this.Controls.AddRange(new Control[]
            {
                pnlHeader, pnlStats, lblChartTitle, pnlChart,
                lblPriTitle, pnlPriority, btnClose
            });
        }

        // ─── Stat Kart ─────────────────────────────────────────────
        private static Panel MakeStatCard(string icon, string label, string value, Color accent, Point loc)
        {
            var card = new Panel
            {
                Size = new Size(200, 100),
                Location = loc,
                BackColor = Color.FromArgb(22, 22, 36)
            };

            // Üst renk çubuğu
            card.Controls.Add(new Panel
            {
                Size = new Size(200, 3),
                Location = new Point(0, 0),
                BackColor = accent
            });

            card.Controls.Add(new Label
            {
                Text = icon + "  " + label,
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true,
                Location = new Point(14, 16)
            });

            card.Controls.Add(new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 28f, FontStyle.Bold),
                ForeColor = accent,
                AutoSize = true,
                Location = new Point(14, 42)
            });

            return card;
        }

        // ─── Bar Chart ─────────────────────────────────────────────
        private static void DrawBarChart(Graphics g, Size size, List<BoardColumn> columns)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int maxTasks = columns.Max(c => c.Tasks.Count);
            if (maxTasks == 0) maxTasks = 1;

            int padding = 12;
            int barAreaH = size.Height - 50;
            int colW = (size.Width - padding * 2) / Math.Max(columns.Count, 1);

            Color[] barColors =
            {
                Color.FromArgb(99, 102, 241),
                Color.FromArgb(34, 197, 94),
                Color.FromArgb(249, 115, 22),
                Color.FromArgb(239, 68, 68),
                Color.FromArgb(234, 179, 8),
                Color.FromArgb(168, 85, 247)
            };

            for (int i = 0; i < columns.Count; i++)
            {
                var col = columns[i];
                int barH = (int)((double)col.Tasks.Count / maxTasks * barAreaH * 0.85);
                int x    = padding + i * colW + colW / 2 - 28;
                int y    = barAreaH - barH;

                var color = barColors[i % barColors.Length];

                // Bar (rounded look via two rects)
                using var brush = new SolidBrush(color);
                g.FillRectangle(brush, x, y, 56, barH);

                // Sayı üstte
                using var numFont = new Font("Segoe UI", 10f, FontStyle.Bold);
                using var numBrush = new SolidBrush(color);
                g.DrawString(col.Tasks.Count.ToString(), numFont, numBrush, x + 18, y - 20);

                // Sütun adı altta
                using var nameFont = new Font("Segoe UI", 8f);
                using var nameBrush = new SolidBrush(Color.FromArgb(148, 163, 184));
                g.DrawString(col.Name, nameFont, nameBrush, x - 4, barAreaH + 6);
            }
        }

        // ─── Priority Bars ─────────────────────────────────────────
        private static void DrawPriorityBars(Graphics g, Size size, List<TaskCard> tasks)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var priorities = new[]
            {
                (TaskPriority.None,     "None",    Color.FromArgb(99, 102, 241)),
                (TaskPriority.Low,      "Düşük",   Color.FromArgb(34, 197, 94)),
                (TaskPriority.Medium,   "Orta",    Color.FromArgb(234, 179, 8)),
                (TaskPriority.High,     "Yüksek",  Color.FromArgb(249, 115, 22)),
                (TaskPriority.Critical, "Kritik",  Color.FromArgb(239, 68, 68))
            };

            int total  = tasks.Count == 0 ? 1 : tasks.Count;
            int blockW = size.Width / priorities.Length;

            for (int i = 0; i < priorities.Length; i++)
            {
                var (p, label, color) = priorities[i];
                int count   = tasks.Count(t => t.Priority == p);
                int barW    = (int)((double)count / total * (blockW - 20));
                int x       = i * blockW + 8;

                // Arka plan bar
                using var bgBrush = new SolidBrush(Color.FromArgb(30, 30, 48));
                g.FillRectangle(bgBrush, x, 14, blockW - 16, 18);

                // Dolu bar
                if (barW > 0)
                {
                    using var fgBrush = new SolidBrush(color);
                    g.FillRectangle(fgBrush, x, 14, barW, 18);
                }

                // Label
                using var font1 = new Font("Segoe UI", 8f, FontStyle.Bold);
                using var brush1 = new SolidBrush(color);
                g.DrawString($"{label} ({count})", font1, brush1, x, 38);
            }
        }
    }
}
