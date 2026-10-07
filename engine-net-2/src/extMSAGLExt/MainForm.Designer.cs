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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemSep = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.msaglExtClient = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.MSAGLExtClient();

            this.menuStrip.SuspendLayout();
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
                this.menuItemOpen, this.menuItemSep, this.menuItemClose
            });
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(37, 20);
            this.menuItemFile.Text = "&File";

            // menuItemOpen
            this.menuItemOpen.Name = "menuItemOpen";
            this.menuItemOpen.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O;
            this.menuItemOpen.Size = new System.Drawing.Size(154, 22);
            this.menuItemOpen.Text = "&Open...";
            this.menuItemOpen.Click += new System.EventHandler(this.MenuFileOpen_Click);

            // menuItemSep
            this.menuItemSep.Name = "menuItemSep";
            this.menuItemSep.Size = new System.Drawing.Size(151, 6);

            // menuItemClose
            this.menuItemClose.Name = "menuItemClose";
            this.menuItemClose.Size = new System.Drawing.Size(154, 22);
            this.menuItemClose.Text = "&Close";
            this.menuItemClose.Click += new System.EventHandler(this.MenuFileClose_Click);

            // msaglExtClient
            this.msaglExtClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.msaglExtClient.Location = new System.Drawing.Point(0, 24);
            this.msaglExtClient.Name = "msaglExtClient";
            this.msaglExtClient.Size = new System.Drawing.Size(1200, 776);
            this.msaglExtClient.TabIndex = 1;

            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(this.msaglExtClient);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainForm";
            this.Text = "extMSAGLExt - GrGen.NET Graph Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);

            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        System.Windows.Forms.MenuStrip menuStrip;
        System.Windows.Forms.ToolStripMenuItem menuItemFile;
        System.Windows.Forms.ToolStripMenuItem menuItemOpen;
        System.Windows.Forms.ToolStripSeparator menuItemSep;
        System.Windows.Forms.ToolStripMenuItem menuItemClose;
    }
}
