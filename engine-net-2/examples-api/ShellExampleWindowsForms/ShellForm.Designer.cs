namespace ShellExampleWindowsForms
{
    partial class ShellForm
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
            this.console = new de.unika.ipd.grGen.libConsoleAndOS.GuiConsoleControl();
            this.SuspendLayout();
            // 
            // console
            // 
            this.console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.console.EnableClear = false;
            this.console.Location = new System.Drawing.Point(0, 0);
            this.console.Name = "console";
            this.console.Size = new System.Drawing.Size(1537, 469);
            this.console.TabIndex = 0;
            // 
            // ShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1537, 469);
            this.Controls.Add(this.console);
            this.Name = "ShellForm";
            this.Text = "ShellForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShellForm_FormClosed);
            this.Shown += new System.EventHandler(this.ShellForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        public de.unika.ipd.grGen.libConsoleAndOS.GuiConsoleControl console;
    }
}

