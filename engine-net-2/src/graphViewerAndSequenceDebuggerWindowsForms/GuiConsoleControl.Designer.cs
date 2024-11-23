namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    partial class GuiConsoleControl
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

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.theRichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // theRichTextBox
            // 
            this.theRichTextBox.BackColor = System.Drawing.Color.Black;
            this.theRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theRichTextBox.Font = new System.Drawing.Font("Courier New", 11F);
            this.theRichTextBox.ForeColor = System.Drawing.Color.White;
            this.theRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.theRichTextBox.Name = "theRichTextBox";
            this.theRichTextBox.Size = new System.Drawing.Size(1800, 400);
            this.theRichTextBox.TabIndex = 0;
            this.theRichTextBox.Text = "";
            // 
            // GuiConsoleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.theRichTextBox);
            this.Name = "GuiConsoleControl";
            this.Size = new System.Drawing.Size(1800, 400);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox theRichTextBox;
    }
}
