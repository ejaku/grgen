namespace GGrShell
{
    partial class GGrShellForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GGrShellForm));
            this.console = new de.unika.ipd.grGen.libConsoleAndOS.GuiConsoleControl();
            this.theMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.theMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.EnableClear = false;
            this.console.Location = new System.Drawing.Point(0, 42);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(1186, 454);
            this.console.TabIndex = 0;
            // 
            // theMenuStrip
            // 
            this.theMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.theMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.theMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.theMenuStrip.Name = "theMenuStrip";
            this.theMenuStrip.Size = new System.Drawing.Size(1186, 42);
            this.theMenuStrip.TabIndex = 1;
            this.theMenuStrip.Text = "theMenuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(64, 38);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(324, 38);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // GGrShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1186, 496);
            this.Controls.Add(this.console);
            this.Controls.Add(this.theMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.theMenuStrip;
            this.Name = "GGrShellForm";
            this.Text = "GGrShell";
            this.theMenuStrip.ResumeLayout(false);
            this.theMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GGrShellForm_FormClosed);
            this.Shown += new System.EventHandler(this.GGrShellForm_Shown);
        }

        #endregion

        public de.unika.ipd.grGen.libConsoleAndOS.GuiConsoleControl console;
        private System.Windows.Forms.MenuStrip theMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
    }
}

