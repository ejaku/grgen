// by Claude Code with Edgar Jakumeit

namespace de.unika.ipd.grGen.extMSAGLExt
{
    partial class MainForm
    {
        System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if(disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemReloadOnChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemShowErrorLog = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSep = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabelState = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.idleLabel = new System.Windows.Forms.Label();
            this.reloadTimer = new System.Windows.Forms.Timer(this.components);

            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();

            // menuStrip
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuItemFile });
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";

            // menuItemFile
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.menuItemOpen, this.menuItemReloadOnChanges, this.menuItemClose, this.menuItemShowErrorLog, this.menuItemSep, this.menuItemExit
            });
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";

            // menuItemOpen
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.menuItemOpen.Size = new System.Drawing.Size(200, 22);
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);

            // menuItemReloadOnChanges
            this.menuItemReloadOnChanges.Name = "menuItemReloadOnChanges";
            this.menuItemReloadOnChanges.Size = new System.Drawing.Size(200, 22);
            this.menuItemReloadOnChanges.Text = "&Reload file on changes";
            this.menuItemReloadOnChanges.Click += new System.EventHandler(this.MenuFileReloadOnChanges_Click);

            // menuItemClose
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W;
            this.menuItemClose.Size = new System.Drawing.Size(200, 22);
            this.menuItemClose.Text = "&Close";
            this.menuItemClose.Click += new System.EventHandler(this.MenuFileClose_Click);

            // menuItemShowErrorLog
            this.menuItemShowErrorLog.Name = "menuItemShowErrorLog";
            this.menuItemShowErrorLog.Size = new System.Drawing.Size(200, 22);
            this.menuItemShowErrorLog.Text = "Show &error log console";
            this.menuItemShowErrorLog.Click += new System.EventHandler(this.MenuFileShowErrorLog_Click);

            // menuItemSep
            this.menuItemSep.Name = "menuItemSep";
            this.menuItemSep.Size = new System.Drawing.Size(197, 6);

            // menuItemExit
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(200, 22);
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.MenuFileExit_Click);

            // statusStrip
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.statusLabelState, this.statusLabelAction });
            this.statusStrip.Location = new System.Drawing.Point(0, 778);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";

            // statusLabelState (takes the remaining space, so the action is shown at the right)
            this.statusLabelState.Name = "statusLabelState";
            this.statusLabelState.Spring = true;
            this.statusLabelState.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            // statusLabelAction
            this.statusLabelAction.Name = "statusLabelAction";
            this.statusLabelAction.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusLabelAction.Visible = false;

            // idleLabel (placeholder shown instead of the graph viewer in state Idle, accepts a dropped vcg file)
            this.idleLabel.AllowDrop = true;
            this.idleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.idleLabel.Location = new System.Drawing.Point(0, 24);
            this.idleLabel.Name = "idleLabel";
            this.idleLabel.Size = new System.Drawing.Size(1200, 754);
            this.idleLabel.TabIndex = 1;
            this.idleLabel.Text = "Open a .vcg file in order to display it (debugging/server mode must be started from the GrShell/GrGen.NET debugger)";
            this.idleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.idleLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.IdleLabel_DragEnter);
            this.idleLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.IdleLabel_DragDrop);

            // reloadTimer (delays the reload after a change notification until the file modifications ended)
            this.reloadTimer.Interval = 500;
            this.reloadTimer.Tick += new System.EventHandler(this.ReloadTimer_Tick);

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.idleLabel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "extMSAGLExt - GrGen.NET Graph Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        System.Windows.Forms.MenuStrip menuStrip;
        System.Windows.Forms.ToolStripMenuItem menuItemFile;
        System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        System.Windows.Forms.ToolStripMenuItem menuItemReloadOnChanges;
        System.Windows.Forms.ToolStripMenuItem menuItemClose;
        System.Windows.Forms.ToolStripMenuItem menuItemShowErrorLog;
        System.Windows.Forms.ToolStripSeparator menuItemSep;
        System.Windows.Forms.ToolStripMenuItem menuItemExit;
        System.Windows.Forms.StatusStrip statusStrip;
        System.Windows.Forms.ToolStripStatusLabel statusLabelState;
        System.Windows.Forms.ToolStripStatusLabel statusLabelAction;
        System.Windows.Forms.Label idleLabel;
        System.Windows.Forms.Timer reloadTimer;
    }
}
