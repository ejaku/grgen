namespace ApplicationExample
{
    partial class ApplicationExampleForm
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
            this.buttonOpenShell = new System.Windows.Forms.Button();
            this.buttonExecuteMutexInShell = new System.Windows.Forms.Button();
            this.buttonExecuteMutexInDebugger = new System.Windows.Forms.Button();
            this.textBoxCommandLine = new System.Windows.Forms.TextBox();
            this.buttonSendToConsole = new System.Windows.Forms.Button();
            this.labelCommandLine = new System.Windows.Forms.Label();
            this.buttonExecuteMutex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenShell
            // 
            this.buttonOpenShell.Location = new System.Drawing.Point(103, 81);
            this.buttonOpenShell.Name = "buttonOpenShell";
            this.buttonOpenShell.Size = new System.Drawing.Size(527, 66);
            this.buttonOpenShell.TabIndex = 0;
            this.buttonOpenShell.Text = "Open Shell...";
            this.buttonOpenShell.UseVisualStyleBackColor = true;
            this.buttonOpenShell.Click += new System.EventHandler(this.buttonOpenShell_Click);
            // 
            // buttonExecuteMutexInShell
            // 
            this.buttonExecuteMutexInShell.Location = new System.Drawing.Point(684, 84);
            this.buttonExecuteMutexInShell.Name = "buttonExecuteMutexInShell";
            this.buttonExecuteMutexInShell.Size = new System.Drawing.Size(556, 63);
            this.buttonExecuteMutexInShell.TabIndex = 1;
            this.buttonExecuteMutexInShell.Text = "Execute ApplicationExampleMutex in Shell...";
            this.buttonExecuteMutexInShell.UseVisualStyleBackColor = true;
            this.buttonExecuteMutexInShell.Click += new System.EventHandler(this.buttonExecuteMutexInShell_Click);
            // 
            // buttonExecuteMutexInDebugger
            // 
            this.buttonExecuteMutexInDebugger.Location = new System.Drawing.Point(108, 376);
            this.buttonExecuteMutexInDebugger.Name = "buttonExecuteMutexInDebugger";
            this.buttonExecuteMutexInDebugger.Size = new System.Drawing.Size(522, 70);
            this.buttonExecuteMutexInDebugger.TabIndex = 2;
            this.buttonExecuteMutexInDebugger.Text = "Execute ApplicationExampleMutex in Debugger...";
            this.buttonExecuteMutexInDebugger.UseVisualStyleBackColor = true;
            this.buttonExecuteMutexInDebugger.Click += new System.EventHandler(this.buttonExecuteMutexInDebugger_Click);
            // 
            // textBoxCommandLine
            // 
            this.textBoxCommandLine.Enabled = false;
            this.textBoxCommandLine.Location = new System.Drawing.Point(186, 249);
            this.textBoxCommandLine.Name = "textBoxCommandLine";
            this.textBoxCommandLine.Size = new System.Drawing.Size(651, 31);
            this.textBoxCommandLine.TabIndex = 3;
            // 
            // buttonSendToConsole
            // 
            this.buttonSendToConsole.Enabled = false;
            this.buttonSendToConsole.Location = new System.Drawing.Point(875, 242);
            this.buttonSendToConsole.Name = "buttonSendToConsole";
            this.buttonSendToConsole.Size = new System.Drawing.Size(204, 44);
            this.buttonSendToConsole.TabIndex = 4;
            this.buttonSendToConsole.Text = "Send to Console";
            this.buttonSendToConsole.UseVisualStyleBackColor = true;
            this.buttonSendToConsole.Click += new System.EventHandler(this.buttonSendToConsole_Click);
            // 
            // labelCommandLine
            // 
            this.labelCommandLine.AutoSize = true;
            this.labelCommandLine.Location = new System.Drawing.Point(181, 221);
            this.labelCommandLine.Name = "labelCommandLine";
            this.labelCommandLine.Size = new System.Drawing.Size(149, 25);
            this.labelCommandLine.TabIndex = 5;
            this.labelCommandLine.Text = "Command line";
            // 
            // buttonExecuteMutex
            // 
            this.buttonExecuteMutex.Location = new System.Drawing.Point(684, 376);
            this.buttonExecuteMutex.Name = "buttonExecuteMutex";
            this.buttonExecuteMutex.Size = new System.Drawing.Size(556, 70);
            this.buttonExecuteMutex.TabIndex = 6;
            this.buttonExecuteMutex.Text = "Execute ApplicationExampleMutex...";
            this.buttonExecuteMutex.UseVisualStyleBackColor = true;
            this.buttonExecuteMutex.Click += new System.EventHandler(this.buttonExecuteMutex_Click);
            // 
            // ApplicationExampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 562);
            this.Controls.Add(this.buttonExecuteMutex);
            this.Controls.Add(this.labelCommandLine);
            this.Controls.Add(this.buttonSendToConsole);
            this.Controls.Add(this.textBoxCommandLine);
            this.Controls.Add(this.buttonExecuteMutexInDebugger);
            this.Controls.Add(this.buttonExecuteMutexInShell);
            this.Controls.Add(this.buttonOpenShell);
            this.Name = "ApplicationExampleForm";
            this.Text = "ApplicationExampleForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenShell;
        private System.Windows.Forms.Button buttonExecuteMutexInShell;
        private System.Windows.Forms.Button buttonExecuteMutexInDebugger;
        private System.Windows.Forms.TextBox textBoxCommandLine;
        private System.Windows.Forms.Button buttonSendToConsole;
        private System.Windows.Forms.Label labelCommandLine;
        private System.Windows.Forms.Button buttonExecuteMutex;
    }
}

