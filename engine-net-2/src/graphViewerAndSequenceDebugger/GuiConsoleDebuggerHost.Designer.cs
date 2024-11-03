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
            this.theSplitter = new System.Windows.Forms.Splitter();
            this.theOptionalSplitter = new System.Windows.Forms.Splitter();
            this.theOptionalGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.theGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.SuspendLayout();
            // 
            // theSplitter
            // 
            this.theSplitter.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.theSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.theSplitter.Location = new System.Drawing.Point(0, 681);
            this.theSplitter.Name = "theSplitter";
            this.theSplitter.Size = new System.Drawing.Size(1774, 10);
            this.theSplitter.TabIndex = 1;
            this.theSplitter.TabStop = false;
            // 
            // theOptionalSplitter
            // 
            this.theOptionalSplitter.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.theOptionalSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.theOptionalSplitter.Location = new System.Drawing.Point(0, 233);
            this.theOptionalSplitter.Name = "theOptionalSplitter";
            this.theOptionalSplitter.Size = new System.Drawing.Size(1774, 10);
            this.theOptionalSplitter.TabIndex = 3;
            this.theOptionalSplitter.TabStop = false;
            // 
            // theOptionalGuiConsoleControl
            // 
            this.theOptionalGuiConsoleControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.theOptionalGuiConsoleControl.Location = new System.Drawing.Point(0, 243);
            this.theOptionalGuiConsoleControl.MinimumSize = new System.Drawing.Size(50, 50);
            this.theOptionalGuiConsoleControl.Name = "theOptionalGuiConsoleControl";
            this.theOptionalGuiConsoleControl.Size = new System.Drawing.Size(1774, 438);
            this.theOptionalGuiConsoleControl.TabIndex = 2;
            // 
            // theGuiConsoleControl
            // 
            this.theGuiConsoleControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.theGuiConsoleControl.Location = new System.Drawing.Point(0, 691);
            this.theGuiConsoleControl.MinimumSize = new System.Drawing.Size(50, 50);
            this.theGuiConsoleControl.Name = "theGuiConsoleControl";
            this.theGuiConsoleControl.Size = new System.Drawing.Size(1774, 438);
            this.theGuiConsoleControl.TabIndex = 0;
            // 
            // GuiConsoleDebuggerHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1774, 1129);
            this.Controls.Add(this.theOptionalSplitter);
            this.Controls.Add(this.theOptionalGuiConsoleControl);
            this.Controls.Add(this.theSplitter);
            this.Controls.Add(this.theGuiConsoleControl);
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "GuiConsoleDebuggerHost";
            this.Text = "GuiConsoleDebuggerHost";
            this.ResumeLayout(false);

        }

        #endregion

        private GuiConsoleControl theGuiConsoleControl;
        public GuiConsoleControl GuiConsoleControl
        {
            get { return theGuiConsoleControl; }
        }

        private System.Windows.Forms.Splitter theSplitter;

        private GuiConsoleControl theOptionalGuiConsoleControl;
        public GuiConsoleControl OptionalGuiConsoleControl
        {
            get { return theOptionalGuiConsoleControl; }
        }

        private System.Windows.Forms.Splitter theOptionalSplitter;
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