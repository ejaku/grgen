namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    partial class GuiConsoleDebuggerHost
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.theOptionalGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.theOptionalSplitter = new System.Windows.Forms.Splitter();
            this.theGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.SuspendLayout();
            // 
            // theOptionalGuiConsoleControl
            // 
            this.theOptionalGuiConsoleControl.Dock = System.Windows.Forms.DockStyle.Top;
            this.theOptionalGuiConsoleControl.EnableClear = false;
            this.theOptionalGuiConsoleControl.Location = new System.Drawing.Point(0, 0);
            this.theOptionalGuiConsoleControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.theOptionalGuiConsoleControl.Name = "theOptionalGuiConsoleControl";
            this.theOptionalGuiConsoleControl.Size = new System.Drawing.Size(1163, 214);
            this.theOptionalGuiConsoleControl.TabIndex = 0;
            // 
            // theOptionalSplitter
            // 
            this.theOptionalSplitter.Dock = System.Windows.Forms.DockStyle.Top;
            this.theOptionalSplitter.Location = new System.Drawing.Point(0, 214);
            this.theOptionalSplitter.Name = "theOptionalSplitter";
            this.theOptionalSplitter.Size = new System.Drawing.Size(1163, 3);
            this.theOptionalSplitter.TabIndex = 1;
            this.theOptionalSplitter.TabStop = false;
            // 
            // theGuiConsoleControl
            // 
            this.theGuiConsoleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.theGuiConsoleControl.EnableClear = false;
            this.theGuiConsoleControl.Location = new System.Drawing.Point(0, 217);
            this.theGuiConsoleControl.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.theGuiConsoleControl.Name = "theGuiConsoleControl";
            this.theGuiConsoleControl.Size = new System.Drawing.Size(1163, 456);
            this.theGuiConsoleControl.TabIndex = 2;
            // 
            // GuiConsoleDebuggerHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 673);
            this.Controls.Add(this.theGuiConsoleControl);
            this.Controls.Add(this.theOptionalSplitter);
            this.Controls.Add(this.theOptionalGuiConsoleControl);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(168, 123);
            this.Name = "GuiConsoleDebuggerHost";
            this.Text = "GuiConsoleDebuggerHost";
            this.ResumeLayout(false);

        }

        #endregion

        private GuiConsoleControl theOptionalGuiConsoleControl;
        private System.Windows.Forms.Splitter theOptionalSplitter;
        private GuiConsoleControl theGuiConsoleControl;

        public IDebuggerConsoleUICombined GuiConsoleControl
        {
            get { return theGuiConsoleControl; }
        }
        public IDebuggerConsoleUICombined OptionalGuiConsoleControl
        {
            get { return theOptionalGuiConsoleControl; }
        }
        //private Microsoft.Msagl.GraphViewerGdi.GViewer gv = null; -- gets created by the msagl client, a behavior needed if it is used without debugger as a plain graph viewer

        public bool TwoPane
        {
            get { return theOptionalGuiConsoleControl.Visible; }
            set
            {
                if(value)
                {
                    theOptionalGuiConsoleControl.Visible = true;
                    theOptionalGuiConsoleControl.EnableClear = true;
                    theOptionalSplitter.Visible = true;
                }
                else
                {
                    theOptionalGuiConsoleControl.Visible = false;
                    theOptionalGuiConsoleControl.EnableClear = false;
                    theOptionalSplitter.Visible = false;
                }
            }
        }
    }
}