namespace TaskBoard
{
    partial class TaskDetailForm
    {
        private System.ComponentModel.IContainer components = null;

        // Tab sistemi
        private TabControl tabControl;
        private TabPage tabDetails;
        private TabPage tabChecklist;
        private TabPage tabAttachments;

        // Tab 1 — Detaylar
        private Label lblTitleHint;
        private TextBox txtTitle;
        private Label lblDescHint;
        private TextBox txtDescription;
        private Label lblPriorityHint;
        private ComboBox cmbPriority;
        private Label lblDueDateHint;
        private CheckBox chkDueDate;
        private DateTimePicker dtpDueDate;
        private Label lblAssignedHint;
        private ComboBox cmbAssignedUser;
        private Label lblCreated;
        private Button btnSave;

        // Tab 2 — Checklist
        private Label lblChecklistProgress;
        private CheckedListBox clbChecklist;
        private TextBox txtNewItem;
        private Button btnAddItem;
        private Button btnDeleteItem;

        // Tab 3 — Dosya Ekleri
        private Label lblAttachHint;
        private ListView lvAttachments;
        private ColumnHeader colFileName;
        private ColumnHeader colSize;
        private ColumnHeader colDate;
        private Button btnAddFile;
        private Button btnOpenFile;
        private Button btnDeleteFile;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.Text = "Görev Detayı";
            this.Size = new Size(620, 680);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.FromArgb(18, 18, 30);
            this.Font = new Font("Segoe UI", 9.5f);

            // ── TabControl ───────────────────────────────────────────
            tabControl = new TabControl
            {
                Location = new Point(14, 14),
                Size = new Size(576, 622),
                Font = new Font("Segoe UI", 9.5f)
            };
            tabControl.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabControl.DrawItem += TabControl_DrawItem;

            tabDetails     = new TabPage("🗒  Detaylar");
            tabChecklist   = new TabPage("☑  Checklist");
            tabAttachments = new TabPage("📎  Dosya Ekleri");

            foreach (var tab in new[] { tabDetails, tabChecklist, tabAttachments })
            {
                tab.BackColor = Color.FromArgb(22, 22, 36);
                tab.ForeColor = Color.FromArgb(226, 232, 240);
            }

            // ── Tab 1: Detaylar ──────────────────────────────────────
            lblTitleHint = MakeSectionLabel("GÖREV BAŞLIĞI", new Point(12, 14));
            txtTitle = new TextBox
            {
                Size = new Size(540, 30),
                Location = new Point(12, 36),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 11f)
            };

            lblDescHint = MakeSectionLabel("AÇIKLAMA", new Point(12, 82));
            txtDescription = new TextBox
            {
                Size = new Size(540, 90),
                Location = new Point(12, 104),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10f),
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // --- Öncelik ---
            lblPriorityHint = MakeSectionLabel("ÖNCELİK", new Point(12, 210));
            cmbPriority = new ComboBox
            {
                Size = new Size(155, 28),
                Location = new Point(12, 232),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPriority.Items.AddRange(new object[] { "None", "Düşük", "Orta", "Yüksek", "Kritik" });
            cmbPriority.SelectedIndex = 0;

            // --- Son Tarih ---
            lblDueDateHint = MakeSectionLabel("SON TARİH", new Point(186, 210));
            chkDueDate = new CheckBox
            {
                Text = "Tarih belirle",
                Location = new Point(186, 234),
                ForeColor = Color.FromArgb(148, 163, 184),
                AutoSize = true
            };
            chkDueDate.CheckedChanged += chkDueDate_CheckedChanged;

            dtpDueDate = new DateTimePicker
            {
                Size = new Size(155, 28),
                Location = new Point(186, 258),
                CalendarForeColor = Color.FromArgb(226, 232, 240),
                CalendarMonthBackground = Color.FromArgb(30, 30, 48),
                Format = DateTimePickerFormat.Short,
                Enabled = false
            };

            // --- Atanan Kullanıcı ---
            lblAssignedHint = MakeSectionLabel("ATANAN KİŞİ", new Point(370, 210));
            cmbAssignedUser = new ComboBox
            {
                Size = new Size(182, 28),
                Location = new Point(370, 232),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                FlatStyle = FlatStyle.Flat,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            lblCreated = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 8.5f),
                ForeColor = Color.FromArgb(71, 85, 105),
                AutoSize = true,
                Location = new Point(12, 300)
            };

            btnSave = new Button
            {
                Text = "💾  Kaydet",
                Size = new Size(120, 36),
                Location = new Point(432, 290),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;

            tabDetails.Controls.AddRange(new Control[] {
                lblTitleHint, txtTitle,
                lblDescHint, txtDescription,
                lblPriorityHint, cmbPriority,
                lblDueDateHint, chkDueDate, dtpDueDate,
                lblAssignedHint, cmbAssignedUser,
                lblCreated, btnSave
            });

            // ── Tab 2: Checklist ─────────────────────────────────────
            lblChecklistProgress = new Label
            {
                Text = "0 / 0 tamamlandı",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(99, 102, 241),
                AutoSize = true,
                Location = new Point(12, 14)
            };

            clbChecklist = new CheckedListBox
            {
                Location = new Point(12, 40),
                Size = new Size(540, 420),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 10f),
                CheckOnClick = true
            };
            clbChecklist.ItemCheck += clbChecklist_ItemCheck;

            txtNewItem = new TextBox
            {
                Size = new Size(400, 28),
                Location = new Point(12, 472),
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5f),
                PlaceholderText = "Yeni madde girin..."
            };
            txtNewItem.KeyDown += txtNewItem_KeyDown;

            btnAddItem = new Button
            {
                Text = "＋ Ekle",
                Size = new Size(80, 28),
                Location = new Point(420, 472),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(99, 102, 241),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnAddItem.FlatAppearance.BorderSize = 0;
            btnAddItem.Click += btnAddItem_Click;

            btnDeleteItem = new Button
            {
                Text = "🗑 Sil",
                Size = new Size(80, 28),
                Location = new Point(510, 472),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(248, 113, 113),
                Font = new Font("Segoe UI", 9f),
                Cursor = Cursors.Hand
            };
            btnDeleteItem.FlatAppearance.BorderColor = Color.FromArgb(248, 113, 113);
            btnDeleteItem.Click += btnDeleteItem_Click;

            tabChecklist.Controls.AddRange(new Control[] {
                lblChecklistProgress, clbChecklist,
                txtNewItem, btnAddItem, btnDeleteItem
            });

            // ── Tab 3: Dosya Ekleri ──────────────────────────────────
            lblAttachHint = MakeSectionLabel("DOSYA EKLERİ", new Point(12, 14));

            colFileName = new ColumnHeader { Text = "Dosya Adı", Width = 280 };
            colSize     = new ColumnHeader { Text = "Boyut",     Width = 80  };
            colDate     = new ColumnHeader { Text = "Tarih",     Width = 110 };

            lvAttachments = new ListView
            {
                Location = new Point(12, 38),
                Size = new Size(540, 430),
                View = View.Details,
                FullRowSelect = true,
                BackColor = Color.FromArgb(30, 30, 48),
                ForeColor = Color.FromArgb(226, 232, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Segoe UI", 9.5f),
                GridLines = false
            };
            lvAttachments.Columns.AddRange(new[] { colFileName, colSize, colDate });
            lvAttachments.DoubleClick += lvAttachments_DoubleClick;

            btnAddFile = MakeAttachBtn("＋ Dosya Ekle", new Point(12, 480),  Color.FromArgb(99, 102, 241));
            btnOpenFile = MakeAttachBtn("📂 Aç",         new Point(146, 480), Color.Transparent);
            btnDeleteFile = MakeAttachBtn("🗑 Sil",        new Point(250, 480), Color.Transparent);
            btnOpenFile.ForeColor = Color.FromArgb(148, 163, 184);
            btnDeleteFile.ForeColor = Color.FromArgb(248, 113, 113);
            btnAddFile.Click += btnAddFile_Click;
            btnOpenFile.Click += btnOpenFile_Click;
            btnDeleteFile.Click += btnDeleteFile_Click;

            tabAttachments.Controls.AddRange(new Control[] {
                lblAttachHint, lvAttachments,
                btnAddFile, btnOpenFile, btnDeleteFile
            });

            // ── Assemble ─────────────────────────────────────────────
            tabControl.TabPages.AddRange(new[] { tabDetails, tabChecklist, tabAttachments });
            this.Controls.Add(tabControl);
            this.ResumeLayout(false);
        }

        // ── Helpers ──────────────────────────────────────────────────
        private static Label MakeSectionLabel(string text, Point loc) => new Label
        {
            Text = text,
            Font = new Font("Segoe UI", 8f, FontStyle.Bold),
            ForeColor = Color.FromArgb(99, 102, 241),
            AutoSize = true,
            Location = loc
        };

        private static Button MakeAttachBtn(string text, Point loc, Color bg)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(120, 34),
                Location = loc,
                FlatStyle = FlatStyle.Flat,
                BackColor = bg,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = bg == Color.Transparent ? 1 : 0;
            return btn;
        }

        private void TabControl_DrawItem(object? sender, DrawItemEventArgs e)
        {
            var tab = tabControl.TabPages[e.Index];
            var rect = tabControl.GetTabRect(e.Index);
            bool selected = e.Index == tabControl.SelectedIndex;

            using var bgBrush = new SolidBrush(selected
                ? Color.FromArgb(22, 22, 36)
                : Color.FromArgb(14, 14, 26));
            e.Graphics.FillRectangle(bgBrush, rect);

            using var fgBrush = new SolidBrush(selected
                ? Color.FromArgb(199, 210, 254)
                : Color.FromArgb(100, 116, 139));
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            e.Graphics.DrawString(tab.Text, e.Font ?? tabControl.Font, fgBrush, rect, sf);
        }

        private void chkDueDate_CheckedChanged(object? sender, EventArgs e)
        {
            dtpDueDate.Enabled = chkDueDate.Checked;
        }
    }
}
