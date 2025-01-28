
namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    partial class GuiDebuggerHost
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GuiDebuggerHost));
            this.theMenuStrip = new System.Windows.Forms.MenuStrip();
            this.debuggingCommandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugAtSourceCodeLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakpointEditingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleLazyChoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stateDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.switchViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.theToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.theSplitContainer = new System.Windows.Forms.SplitContainer();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toggleChoicepointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.appendWatchpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertWatchpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editWatchpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteWatchpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleWatchpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainWorkObjectGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.inputOutputAndLogGuiConsoleControl = new de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl();
            this.continueToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextMatchToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.detailedStepToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stepToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stepUpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.stepOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.runToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.abortToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.skipSingleMatchesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.backAbortToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.continueDialogToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.nextMatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailedStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stepOutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.continueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skipSingleMatchesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.breakpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleBreakpointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.choicepointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.watchpointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showVariablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showClassObjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printStacktraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFullStateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highlightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dumpGraphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.theMenuStrip.SuspendLayout();
            this.theToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.theSplitContainer)).BeginInit();
            this.theSplitContainer.Panel1.SuspendLayout();
            this.theSplitContainer.Panel2.SuspendLayout();
            this.theSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // theMenuStrip
            // 
            this.theMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.debuggingCommandsToolStripMenuItem,
            this.breakpointEditingToolStripMenuItem,
            this.stateDisplayToolStripMenuItem,
            this.viewToolStripMenuItem});
            resources.ApplyResources(this.theMenuStrip, "theMenuStrip");
            this.theMenuStrip.Name = "theMenuStrip";
            // 
            // debuggingCommandsToolStripMenuItem
            // 
            this.debuggingCommandsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nextMatchToolStripMenuItem,
            this.detailedStepToolStripMenuItem,
            this.stepToolStripMenuItem,
            this.stepUpToolStripMenuItem,
            this.stepOutToolStripMenuItem,
            this.runToolStripMenuItem,
            this.abortToolStripMenuItem,
            this.continueToolStripMenuItem,
            this.skipSingleMatchesToolStripMenuItem,
            this.debugAtSourceCodeLevelToolStripMenuItem});
            this.debuggingCommandsToolStripMenuItem.Name = "debuggingCommandsToolStripMenuItem";
            resources.ApplyResources(this.debuggingCommandsToolStripMenuItem, "debuggingCommandsToolStripMenuItem");
            // 
            // debugAtSourceCodeLevelToolStripMenuItem
            // 
            resources.ApplyResources(this.debugAtSourceCodeLevelToolStripMenuItem, "debugAtSourceCodeLevelToolStripMenuItem");
            this.debugAtSourceCodeLevelToolStripMenuItem.Name = "debugAtSourceCodeLevelToolStripMenuItem";
            this.debugAtSourceCodeLevelToolStripMenuItem.Click += new System.EventHandler(this.debugAtSourceCodeLevelToolStripMenuItem_Click);
            // 
            // breakpointEditingToolStripMenuItem
            // 
            this.breakpointEditingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.breakpointsToolStripMenuItem,
            this.toggleBreakpointToolStripMenuItem,
            this.toolStripSeparator3,
            this.choicepointsToolStripMenuItem,
            this.toggleChoicepointToolStripMenuItem,
            this.toggleLazyChoiceToolStripMenuItem,
            this.toolStripSeparator2,
            this.watchpointsToolStripMenuItem,
            this.editWatchpointToolStripMenuItem,
            this.toggleWatchpointToolStripMenuItem,
            this.deleteWatchpointToolStripMenuItem,
            this.insertWatchpointToolStripMenuItem,
            this.appendWatchpointToolStripMenuItem});
            this.breakpointEditingToolStripMenuItem.Name = "breakpointEditingToolStripMenuItem";
            resources.ApplyResources(this.breakpointEditingToolStripMenuItem, "breakpointEditingToolStripMenuItem");
            // 
            // toggleLazyChoiceToolStripMenuItem
            // 
            this.toggleLazyChoiceToolStripMenuItem.Checked = true;
            this.toggleLazyChoiceToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            resources.ApplyResources(this.toggleLazyChoiceToolStripMenuItem, "toggleLazyChoiceToolStripMenuItem");
            this.toggleLazyChoiceToolStripMenuItem.Name = "toggleLazyChoiceToolStripMenuItem";
            this.toggleLazyChoiceToolStripMenuItem.Click += new System.EventHandler(this.toggleLazyChoiceToolStripMenuItem_Click);
            // 
            // stateDisplayToolStripMenuItem
            // 
            this.stateDisplayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showVariablesToolStripMenuItem,
            this.showClassObjectToolStripMenuItem,
            this.printStacktraceToolStripMenuItem,
            this.printFullStateToolStripMenuItem,
            this.highlightToolStripMenuItem,
            this.dumpGraphToolStripMenuItem,
            this.asGraphToolStripMenuItem});
            this.stateDisplayToolStripMenuItem.Name = "stateDisplayToolStripMenuItem";
            resources.ApplyResources(this.stateDisplayToolStripMenuItem, "stateDisplayToolStripMenuItem");
            // 
            // asGraphToolStripMenuItem
            // 
            resources.ApplyResources(this.asGraphToolStripMenuItem, "asGraphToolStripMenuItem");
            this.asGraphToolStripMenuItem.Name = "asGraphToolStripMenuItem";
            this.asGraphToolStripMenuItem.Click += new System.EventHandler(this.asGraphToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.switchViewToolStripMenuItem,
            this.refreshViewToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // switchViewToolStripMenuItem
            // 
            this.switchViewToolStripMenuItem.Name = "switchViewToolStripMenuItem";
            resources.ApplyResources(this.switchViewToolStripMenuItem, "switchViewToolStripMenuItem");
            this.switchViewToolStripMenuItem.Click += new System.EventHandler(this.switchViewToolStripMenuItem_Click);
            // 
            // refreshViewToolStripMenuItem
            // 
            this.refreshViewToolStripMenuItem.Name = "refreshViewToolStripMenuItem";
            resources.ApplyResources(this.refreshViewToolStripMenuItem, "refreshViewToolStripMenuItem");
            this.refreshViewToolStripMenuItem.Click += new System.EventHandler(this.refreshViewToolStripMenuItem_Click);
            // 
            // theToolStrip
            // 
            this.theToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.continueToolStripButton,
            this.nextMatchToolStripButton,
            this.detailedStepToolStripButton,
            this.stepToolStripButton,
            this.stepUpToolStripButton,
            this.stepOutToolStripButton,
            this.runToolStripButton,
            this.abortToolStripButton,
            this.skipSingleMatchesToolStripButton,
            this.toolStripSeparator1,
            this.backAbortToolStripButton,
            this.continueDialogToolStripButton});
            resources.ApplyResources(this.theToolStrip, "theToolStrip");
            this.theToolStrip.Name = "theToolStrip";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // theSplitContainer
            // 
            resources.ApplyResources(this.theSplitContainer, "theSplitContainer");
            this.theSplitContainer.Name = "theSplitContainer";
            // 
            // theSplitContainer.Panel1
            // 
            this.theSplitContainer.Panel1.Controls.Add(this.mainWorkObjectGuiConsoleControl);
            // 
            // theSplitContainer.Panel2
            // 
            this.theSplitContainer.Panel2.Controls.Add(this.inputOutputAndLogGuiConsoleControl);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toggleChoicepointToolStripMenuItem
            // 
            resources.ApplyResources(this.toggleChoicepointToolStripMenuItem, "toggleChoicepointToolStripMenuItem");
            this.toggleChoicepointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.ToggleChoicepoint;
            this.toggleChoicepointToolStripMenuItem.Name = "toggleChoicepointToolStripMenuItem";
            this.toggleChoicepointToolStripMenuItem.Click += new System.EventHandler(this.toggleChoicepointToolStripMenuItem_Click);
            // 
            // appendWatchpointToolStripMenuItem
            // 
            resources.ApplyResources(this.appendWatchpointToolStripMenuItem, "appendWatchpointToolStripMenuItem");
            this.appendWatchpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.AddWatchpoint;
            this.appendWatchpointToolStripMenuItem.Name = "appendWatchpointToolStripMenuItem";
            this.appendWatchpointToolStripMenuItem.Click += new System.EventHandler(this.appendWatchpointToolStripMenuItem_Click);
            // 
            // insertWatchpointToolStripMenuItem
            // 
            resources.ApplyResources(this.insertWatchpointToolStripMenuItem, "insertWatchpointToolStripMenuItem");
            this.insertWatchpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.AddWatchpoint;
            this.insertWatchpointToolStripMenuItem.Name = "insertWatchpointToolStripMenuItem";
            this.insertWatchpointToolStripMenuItem.Click += new System.EventHandler(this.insertWatchpointToolStripMenuItem_Click);
            // 
            // editWatchpointToolStripMenuItem
            // 
            resources.ApplyResources(this.editWatchpointToolStripMenuItem, "editWatchpointToolStripMenuItem");
            this.editWatchpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.EditWatchpoint;
            this.editWatchpointToolStripMenuItem.Name = "editWatchpointToolStripMenuItem";
            this.editWatchpointToolStripMenuItem.Click += new System.EventHandler(this.editWatchpointToolStripMenuItem_Click);
            // 
            // deleteWatchpointToolStripMenuItem
            // 
            resources.ApplyResources(this.deleteWatchpointToolStripMenuItem, "deleteWatchpointToolStripMenuItem");
            this.deleteWatchpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.DeleteWatchpoint;
            this.deleteWatchpointToolStripMenuItem.Name = "deleteWatchpointToolStripMenuItem";
            this.deleteWatchpointToolStripMenuItem.Click += new System.EventHandler(this.deleteWatchpointToolStripMenuItem_Click);
            // 
            // toggleWatchpointToolStripMenuItem
            // 
            resources.ApplyResources(this.toggleWatchpointToolStripMenuItem, "toggleWatchpointToolStripMenuItem");
            this.toggleWatchpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.ToggleWatchpoint;
            this.toggleWatchpointToolStripMenuItem.Name = "toggleWatchpointToolStripMenuItem";
            this.toggleWatchpointToolStripMenuItem.Click += new System.EventHandler(this.toggleWatchpointToolStripMenuItem_Click);
            // 
            // mainWorkObjectGuiConsoleControl
            // 
            resources.ApplyResources(this.mainWorkObjectGuiConsoleControl, "mainWorkObjectGuiConsoleControl");
            this.mainWorkObjectGuiConsoleControl.EnableClear = false;
            this.mainWorkObjectGuiConsoleControl.Name = "mainWorkObjectGuiConsoleControl";
            // 
            // inputOutputAndLogGuiConsoleControl
            // 
            resources.ApplyResources(this.inputOutputAndLogGuiConsoleControl, "inputOutputAndLogGuiConsoleControl");
            this.inputOutputAndLogGuiConsoleControl.EnableClear = false;
            this.inputOutputAndLogGuiConsoleControl.Name = "inputOutputAndLogGuiConsoleControl";
            // 
            // continueToolStripButton
            // 
            this.continueToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.continueToolStripButton, "continueToolStripButton");
            this.continueToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Continue;
            this.continueToolStripButton.Name = "continueToolStripButton";
            this.continueToolStripButton.Click += new System.EventHandler(this.continueToolStripButton_Click);
            // 
            // nextMatchToolStripButton
            // 
            this.nextMatchToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.nextMatchToolStripButton, "nextMatchToolStripButton");
            this.nextMatchToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.NextMatch;
            this.nextMatchToolStripButton.Name = "nextMatchToolStripButton";
            this.nextMatchToolStripButton.Click += new System.EventHandler(this.nextMatchToolStripButton_Click);
            // 
            // detailedStepToolStripButton
            // 
            this.detailedStepToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.detailedStepToolStripButton, "detailedStepToolStripButton");
            this.detailedStepToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.DetailedStep;
            this.detailedStepToolStripButton.Name = "detailedStepToolStripButton";
            this.detailedStepToolStripButton.Click += new System.EventHandler(this.detailedStepToolStripButton_Click);
            // 
            // stepToolStripButton
            // 
            this.stepToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.stepToolStripButton, "stepToolStripButton");
            this.stepToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Step;
            this.stepToolStripButton.Name = "stepToolStripButton";
            this.stepToolStripButton.Click += new System.EventHandler(this.stepToolStripButton_Click);
            // 
            // stepUpToolStripButton
            // 
            this.stepUpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.stepUpToolStripButton, "stepUpToolStripButton");
            this.stepUpToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.StepUp;
            this.stepUpToolStripButton.Name = "stepUpToolStripButton";
            this.stepUpToolStripButton.Click += new System.EventHandler(this.stepUpToolStripButton_Click);
            // 
            // stepOutToolStripButton
            // 
            this.stepOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.stepOutToolStripButton, "stepOutToolStripButton");
            this.stepOutToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.StepOut;
            this.stepOutToolStripButton.Name = "stepOutToolStripButton";
            this.stepOutToolStripButton.Click += new System.EventHandler(this.stepOutToolStripButton_Click);
            // 
            // runToolStripButton
            // 
            this.runToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.runToolStripButton, "runToolStripButton");
            this.runToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Run;
            this.runToolStripButton.Name = "runToolStripButton";
            this.runToolStripButton.Click += new System.EventHandler(this.runToolStripButton_Click);
            // 
            // abortToolStripButton
            // 
            this.abortToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.abortToolStripButton, "abortToolStripButton");
            this.abortToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Abort;
            this.abortToolStripButton.Name = "abortToolStripButton";
            this.abortToolStripButton.Click += new System.EventHandler(this.abortToolStripButton_Click);
            // 
            // skipSingleMatchesToolStripButton
            // 
            this.skipSingleMatchesToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.skipSingleMatchesToolStripButton, "skipSingleMatchesToolStripButton");
            this.skipSingleMatchesToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.SkipSingleMatches;
            this.skipSingleMatchesToolStripButton.Name = "skipSingleMatchesToolStripButton";
            this.skipSingleMatchesToolStripButton.Click += new System.EventHandler(this.skipSingleMatchesToolStripButton_Click);
            // 
            // backAbortToolStripButton
            // 
            this.backAbortToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.backAbortToolStripButton, "backAbortToolStripButton");
            this.backAbortToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.BackAbort;
            this.backAbortToolStripButton.Name = "backAbortToolStripButton";
            this.backAbortToolStripButton.Click += new System.EventHandler(this.backAbortToolStripButton_Click);
            // 
            // continueDialogToolStripButton
            // 
            this.continueDialogToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.continueDialogToolStripButton, "continueDialogToolStripButton");
            this.continueDialogToolStripButton.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.ContinueDialog;
            this.continueDialogToolStripButton.Name = "continueDialogToolStripButton";
            this.continueDialogToolStripButton.Click += new System.EventHandler(this.continueDialogToolStripButton_Click);
            // 
            // nextMatchToolStripMenuItem
            // 
            resources.ApplyResources(this.nextMatchToolStripMenuItem, "nextMatchToolStripMenuItem");
            this.nextMatchToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.NextMatch;
            this.nextMatchToolStripMenuItem.Name = "nextMatchToolStripMenuItem";
            this.nextMatchToolStripMenuItem.Click += new System.EventHandler(this.nextMatchToolStripMenuItem_Click);
            // 
            // detailedStepToolStripMenuItem
            // 
            resources.ApplyResources(this.detailedStepToolStripMenuItem, "detailedStepToolStripMenuItem");
            this.detailedStepToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.DetailedStep;
            this.detailedStepToolStripMenuItem.Name = "detailedStepToolStripMenuItem";
            this.detailedStepToolStripMenuItem.Click += new System.EventHandler(this.detailedStepToolStripMenuItem_Click);
            // 
            // stepToolStripMenuItem
            // 
            resources.ApplyResources(this.stepToolStripMenuItem, "stepToolStripMenuItem");
            this.stepToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Step;
            this.stepToolStripMenuItem.Name = "stepToolStripMenuItem";
            this.stepToolStripMenuItem.Click += new System.EventHandler(this.stepToolStripMenuItem_Click);
            // 
            // stepUpToolStripMenuItem
            // 
            resources.ApplyResources(this.stepUpToolStripMenuItem, "stepUpToolStripMenuItem");
            this.stepUpToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.StepUp;
            this.stepUpToolStripMenuItem.Name = "stepUpToolStripMenuItem";
            this.stepUpToolStripMenuItem.Click += new System.EventHandler(this.stepUpToolStripMenuItem_Click);
            // 
            // stepOutToolStripMenuItem
            // 
            resources.ApplyResources(this.stepOutToolStripMenuItem, "stepOutToolStripMenuItem");
            this.stepOutToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.StepOut;
            this.stepOutToolStripMenuItem.Name = "stepOutToolStripMenuItem";
            this.stepOutToolStripMenuItem.Click += new System.EventHandler(this.stepOutToolStripMenuItem_Click);
            // 
            // runToolStripMenuItem
            // 
            resources.ApplyResources(this.runToolStripMenuItem, "runToolStripMenuItem");
            this.runToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Run;
            this.runToolStripMenuItem.Name = "runToolStripMenuItem";
            this.runToolStripMenuItem.Click += new System.EventHandler(this.runToolStripMenuItem_Click);
            // 
            // abortToolStripMenuItem
            // 
            resources.ApplyResources(this.abortToolStripMenuItem, "abortToolStripMenuItem");
            this.abortToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Abort;
            this.abortToolStripMenuItem.Name = "abortToolStripMenuItem";
            this.abortToolStripMenuItem.Click += new System.EventHandler(this.abortToolStripMenuItem_Click);
            // 
            // continueToolStripMenuItem
            // 
            resources.ApplyResources(this.continueToolStripMenuItem, "continueToolStripMenuItem");
            this.continueToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Continue;
            this.continueToolStripMenuItem.Name = "continueToolStripMenuItem";
            this.continueToolStripMenuItem.Click += new System.EventHandler(this.continueToolStripMenuItem_Click);
            // 
            // skipSingleMatchesToolStripMenuItem
            // 
            resources.ApplyResources(this.skipSingleMatchesToolStripMenuItem, "skipSingleMatchesToolStripMenuItem");
            this.skipSingleMatchesToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.SkipSingleMatches;
            this.skipSingleMatchesToolStripMenuItem.Name = "skipSingleMatchesToolStripMenuItem";
            this.skipSingleMatchesToolStripMenuItem.Click += new System.EventHandler(this.skipSingleMatchesToolStripMenuItem_Click);
            // 
            // breakpointsToolStripMenuItem
            // 
            resources.ApplyResources(this.breakpointsToolStripMenuItem, "breakpointsToolStripMenuItem");
            this.breakpointsToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Breakpoints;
            this.breakpointsToolStripMenuItem.Name = "breakpointsToolStripMenuItem";
            this.breakpointsToolStripMenuItem.Click += new System.EventHandler(this.toggleBreakpointsToolStripMenuItem_Click);
            // 
            // toggleBreakpointToolStripMenuItem
            // 
            resources.ApplyResources(this.toggleBreakpointToolStripMenuItem, "toggleBreakpointToolStripMenuItem");
            this.toggleBreakpointToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.ToggleBreakpoint;
            this.toggleBreakpointToolStripMenuItem.Name = "toggleBreakpointToolStripMenuItem";
            this.toggleBreakpointToolStripMenuItem.Click += new System.EventHandler(this.toggleBreakpointToolStripMenuItem_Click);
            // 
            // choicepointsToolStripMenuItem
            // 
            resources.ApplyResources(this.choicepointsToolStripMenuItem, "choicepointsToolStripMenuItem");
            this.choicepointsToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Choicepoints;
            this.choicepointsToolStripMenuItem.Name = "choicepointsToolStripMenuItem";
            this.choicepointsToolStripMenuItem.Click += new System.EventHandler(this.toggleChoicepointsToolStripMenuItem_Click);
            // 
            // watchpointsToolStripMenuItem
            // 
            resources.ApplyResources(this.watchpointsToolStripMenuItem, "watchpointsToolStripMenuItem");
            this.watchpointsToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Watchpoints;
            this.watchpointsToolStripMenuItem.Name = "watchpointsToolStripMenuItem";
            this.watchpointsToolStripMenuItem.Click += new System.EventHandler(this.watchpointsToolStripMenuItem_Click);
            // 
            // showVariablesToolStripMenuItem
            // 
            resources.ApplyResources(this.showVariablesToolStripMenuItem, "showVariablesToolStripMenuItem");
            this.showVariablesToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Variables;
            this.showVariablesToolStripMenuItem.Name = "showVariablesToolStripMenuItem";
            this.showVariablesToolStripMenuItem.Click += new System.EventHandler(this.showVariablesToolStripMenuItem_Click);
            // 
            // showClassObjectToolStripMenuItem
            // 
            resources.ApplyResources(this.showClassObjectToolStripMenuItem, "showClassObjectToolStripMenuItem");
            this.showClassObjectToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Objects;
            this.showClassObjectToolStripMenuItem.Name = "showClassObjectToolStripMenuItem";
            this.showClassObjectToolStripMenuItem.Click += new System.EventHandler(this.showClassObjectToolStripMenuItem_Click);
            // 
            // printStacktraceToolStripMenuItem
            // 
            resources.ApplyResources(this.printStacktraceToolStripMenuItem, "printStacktraceToolStripMenuItem");
            this.printStacktraceToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Stacktrace;
            this.printStacktraceToolStripMenuItem.Name = "printStacktraceToolStripMenuItem";
            this.printStacktraceToolStripMenuItem.Click += new System.EventHandler(this.printStacktraceToolStripMenuItem_Click);
            // 
            // printFullStateToolStripMenuItem
            // 
            resources.ApplyResources(this.printFullStateToolStripMenuItem, "printFullStateToolStripMenuItem");
            this.printFullStateToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.FullState;
            this.printFullStateToolStripMenuItem.Name = "printFullStateToolStripMenuItem";
            this.printFullStateToolStripMenuItem.Click += new System.EventHandler(this.printFullStateToolStripMenuItem_Click);
            // 
            // highlightToolStripMenuItem
            // 
            resources.ApplyResources(this.highlightToolStripMenuItem, "highlightToolStripMenuItem");
            this.highlightToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Highlight;
            this.highlightToolStripMenuItem.Name = "highlightToolStripMenuItem";
            this.highlightToolStripMenuItem.Click += new System.EventHandler(this.highlightToolStripMenuItem_Click);
            // 
            // dumpGraphToolStripMenuItem
            // 
            resources.ApplyResources(this.dumpGraphToolStripMenuItem, "dumpGraphToolStripMenuItem");
            this.dumpGraphToolStripMenuItem.Image = global::graphViewerAndSequenceDebuggerWindowsForms.Properties.Resources.Graph;
            this.dumpGraphToolStripMenuItem.Name = "dumpGraphToolStripMenuItem";
            this.dumpGraphToolStripMenuItem.Click += new System.EventHandler(this.dumpGraphToolStripMenuItem_Click);
            // 
            // GuiDebuggerHost
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.theSplitContainer);
            this.Controls.Add(this.theToolStrip);
            this.Controls.Add(this.theMenuStrip);
            this.MainMenuStrip = this.theMenuStrip;
            this.Name = "GuiDebuggerHost";
            this.theMenuStrip.ResumeLayout(false);
            this.theMenuStrip.PerformLayout();
            this.theToolStrip.ResumeLayout(false);
            this.theToolStrip.PerformLayout();
            this.theSplitContainer.Panel1.ResumeLayout(false);
            this.theSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.theSplitContainer)).EndInit();
            this.theSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip theMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem debuggingCommandsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nextMatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakpointEditingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stateDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailedStepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stepOutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem breakpointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem choicepointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleLazyChoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem watchpointsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip theToolStrip;
        private System.Windows.Forms.ToolStripButton continueToolStripButton;
        private System.Windows.Forms.SplitContainer theSplitContainer;
        private de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl mainWorkObjectGuiConsoleControl;
        private de.unika.ipd.grGen.graphViewerAndSequenceDebugger.GuiConsoleControl inputOutputAndLogGuiConsoleControl;
        private System.Windows.Forms.ToolStripMenuItem continueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showVariablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showClassObjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printStacktraceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printFullStateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem highlightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dumpGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asGraphToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem skipSingleMatchesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton nextMatchToolStripButton;
        private System.Windows.Forms.ToolStripButton detailedStepToolStripButton;
        private System.Windows.Forms.ToolStripButton stepToolStripButton;
        private System.Windows.Forms.ToolStripButton stepUpToolStripButton;
        private System.Windows.Forms.ToolStripButton stepOutToolStripButton;
        private System.Windows.Forms.ToolStripButton runToolStripButton;
        private System.Windows.Forms.ToolStripButton abortToolStripButton;
        private System.Windows.Forms.ToolStripButton skipSingleMatchesToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem debugAtSourceCodeLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem switchViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton backAbortToolStripButton;
        private System.Windows.Forms.ToolStripButton continueDialogToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem toggleBreakpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toggleChoicepointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editWatchpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleWatchpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteWatchpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertWatchpointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem appendWatchpointToolStripMenuItem;
    }
}