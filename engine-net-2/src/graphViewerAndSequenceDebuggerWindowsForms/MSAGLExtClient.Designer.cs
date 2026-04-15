// by Claude Code with Edgar Jakumeit

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    partial class MSAGLExtClient
    {
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.drawingPanelToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.outerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.labelSearch = new System.Windows.Forms.Label();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.buttonClearSearch = new System.Windows.Forms.Button();
            this.buttonMatchCase = new System.Windows.Forms.CheckBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonSearchPrev = new System.Windows.Forms.Button();
            this.textBoxSearchStatus = new System.Windows.Forms.TextBox();
            this.buttonSearchNext = new System.Windows.Forms.Button();
            this.leftInnerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.labelNesting = new System.Windows.Forms.Label();
            this.treeViewNodeNesting = new System.Windows.Forms.TreeView();
            this.labelAttributes = new System.Windows.Forms.Label();
            this.textBoxAttributes = new System.Windows.Forms.TextBox();

            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).BeginInit();
            this.outerSplitContainer.Panel1.SuspendLayout();
            this.outerSplitContainer.Panel2.SuspendLayout();
            this.outerSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.leftInnerSplitContainer)).BeginInit();
            this.leftInnerSplitContainer.Panel1.SuspendLayout();
            this.leftInnerSplitContainer.Panel2.SuspendLayout();
            this.leftInnerSplitContainer.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.SuspendLayout();

            //
            // outerSplitContainer
            //
            this.outerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outerSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.outerSplitContainer.Name = "outerSplitContainer";
            this.outerSplitContainer.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.outerSplitContainer.Size = new System.Drawing.Size(900, 600);
            this.outerSplitContainer.SplitterDistance = 260;
            this.outerSplitContainer.TabIndex = 0;

            //
            // outerSplitContainer.Panel1 (left pane)
            // Controls added in reverse visual order: Fill first, then Top panels from bottom to top,
            // so that the last-added (mapPanel) appears at the very top.
            //
            this.outerSplitContainer.Panel1.Controls.Add(this.leftInnerSplitContainer);
            this.outerSplitContainer.Panel1.Controls.Add(this.searchPanel);
            this.outerSplitContainer.Panel1.Controls.Add(this.mapPanel);

            //
            // mapPanel
            //
            this.mapPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.mapPanel.Location = new System.Drawing.Point(0, 0);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(260, 180);
            this.mapPanel.TabIndex = 0;

            //
            // searchPanel
            // Row 1 (y=3): "Search:" label | search text box | "x" clear | "aA" match-case | "Search" button
            // Row 2 (y=29): "<-" prev | status text box | "->" next
            //
            this.searchPanel.Controls.Add(this.buttonSearchNext);
            this.searchPanel.Controls.Add(this.textBoxSearchStatus);
            this.searchPanel.Controls.Add(this.buttonSearchPrev);
            this.searchPanel.Controls.Add(this.buttonSearch);
            this.searchPanel.Controls.Add(this.buttonMatchCase);
            this.searchPanel.Controls.Add(this.buttonClearSearch);
            this.searchPanel.Controls.Add(this.textBoxSearch);
            this.searchPanel.Controls.Add(this.labelSearch);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.searchPanel.Location = new System.Drawing.Point(0, 180);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(260, 54);
            this.searchPanel.TabIndex = 1;

            //
            // labelSearch  (row 1, left)
            //
            this.labelSearch.AutoSize = true;
            this.labelSearch.Location = new System.Drawing.Point(3, 7);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.TabIndex = 0;
            this.labelSearch.Text = "Search:";

            //
            // textBoxSearch  (row 1, stretches between label and right buttons)
            //
            this.textBoxSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.textBoxSearch.Location = new System.Drawing.Point(48, 4);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(100, 22);
            this.textBoxSearch.TabIndex = 1;

            //
            // buttonClearSearch  (row 1, right-anchored: "x")
            //
            this.buttonClearSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right;
            this.buttonClearSearch.Location = new System.Drawing.Point(151, 3);
            this.buttonClearSearch.Name = "buttonClearSearch";
            this.buttonClearSearch.Size = new System.Drawing.Size(22, 22);
            this.buttonClearSearch.TabIndex = 2;
            this.buttonClearSearch.Text = "X";
            this.buttonClearSearch.UseVisualStyleBackColor = true;

            //
            // buttonMatchCase  (row 1, right-anchored: "aA" toggle)
            //
            this.buttonMatchCase.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right;
            this.buttonMatchCase.Appearance = System.Windows.Forms.Appearance.Button;
            this.buttonMatchCase.Location = new System.Drawing.Point(175, 3);
            this.buttonMatchCase.Name = "buttonMatchCase";
            this.buttonMatchCase.Size = new System.Drawing.Size(28, 22);
            this.buttonMatchCase.TabIndex = 3;
            this.buttonMatchCase.Text = "aA";
            this.buttonMatchCase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.buttonMatchCase.UseVisualStyleBackColor = true;

            //
            // buttonSearch  (row 1, right-anchored: "Search")
            //
            this.buttonSearch.Anchor = System.Windows.Forms.AnchorStyles.Top
                | System.Windows.Forms.AnchorStyles.Right;
            this.buttonSearch.Location = new System.Drawing.Point(205, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(52, 22);
            this.buttonSearch.TabIndex = 4;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;

            //
            // buttonSearchPrev  (row 2, left: "<-")
            //
            this.buttonSearchPrev.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left;
            this.buttonSearchPrev.Location = new System.Drawing.Point(3, 29);
            this.buttonSearchPrev.Name = "buttonSearchPrev";
            this.buttonSearchPrev.Size = new System.Drawing.Size(30, 22);
            this.buttonSearchPrev.TabIndex = 5;
            this.buttonSearchPrev.Text = "<-";
            this.buttonSearchPrev.UseVisualStyleBackColor = true;

            //
            // textBoxSearchStatus  (row 2, stretches between nav buttons)
            //
            this.textBoxSearchStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left
                | System.Windows.Forms.AnchorStyles.Right;
            this.textBoxSearchStatus.Location = new System.Drawing.Point(36, 30);
            this.textBoxSearchStatus.Name = "textBoxSearchStatus";
            this.textBoxSearchStatus.ReadOnly = true;
            this.textBoxSearchStatus.Size = new System.Drawing.Size(185, 20);
            this.textBoxSearchStatus.TabIndex = 6;
            this.textBoxSearchStatus.TabStop = false;

            //
            // buttonSearchNext  (row 2, right: "->")
            //
            this.buttonSearchNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Right;
            this.buttonSearchNext.Location = new System.Drawing.Point(224, 29);
            this.buttonSearchNext.Name = "buttonSearchNext";
            this.buttonSearchNext.Size = new System.Drawing.Size(30, 22);
            this.buttonSearchNext.TabIndex = 7;
            this.buttonSearchNext.Text = "->";
            this.buttonSearchNext.UseVisualStyleBackColor = true;

            //
            // leftInnerSplitContainer
            //
            this.leftInnerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftInnerSplitContainer.Location = new System.Drawing.Point(0, 232);
            this.leftInnerSplitContainer.Name = "leftInnerSplitContainer";
            this.leftInnerSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.leftInnerSplitContainer.Size = new System.Drawing.Size(260, 368);
            this.leftInnerSplitContainer.SplitterDistance = 180;
            this.leftInnerSplitContainer.TabIndex = 2;

            //
            // leftInnerSplitContainer.Panel1 (node nesting tree)
            // Controls added in reverse visual order: TreeView (Fill) first, label (Top) last.
            //
            this.leftInnerSplitContainer.Panel1.Controls.Add(this.treeViewNodeNesting);
            this.leftInnerSplitContainer.Panel1.Controls.Add(this.labelNesting);

            //
            // labelNesting
            //
            this.labelNesting.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelNesting.Location = new System.Drawing.Point(0, 0);
            this.labelNesting.Name = "labelNesting";
            this.labelNesting.Size = new System.Drawing.Size(260, 18);
            this.labelNesting.TabIndex = 0;
            this.labelNesting.Text = "Node nesting:";

            //
            // treeViewNodeNesting
            //
            this.treeViewNodeNesting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewNodeNesting.HideSelection = false;
            this.treeViewNodeNesting.Location = new System.Drawing.Point(0, 18);
            this.treeViewNodeNesting.Name = "treeViewNodeNesting";
            this.treeViewNodeNesting.Size = new System.Drawing.Size(260, 162);
            this.treeViewNodeNesting.TabIndex = 1;

            //
            // leftInnerSplitContainer.Panel2 (attributes)
            // Controls added in reverse visual order: TextBox (Fill) first, label (Top) last.
            //
            this.leftInnerSplitContainer.Panel2.Controls.Add(this.textBoxAttributes);
            this.leftInnerSplitContainer.Panel2.Controls.Add(this.labelAttributes);

            //
            // labelAttributes
            //
            this.labelAttributes.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelAttributes.Location = new System.Drawing.Point(0, 0);
            this.labelAttributes.Name = "labelAttributes";
            this.labelAttributes.Size = new System.Drawing.Size(260, 18);
            this.labelAttributes.TabIndex = 0;
            this.labelAttributes.Text = "Attributes:";

            //
            // textBoxAttributes
            //
            this.textBoxAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxAttributes.HideSelection = false;
            this.textBoxAttributes.Location = new System.Drawing.Point(0, 18);
            this.textBoxAttributes.Multiline = true;
            this.textBoxAttributes.Name = "textBoxAttributes";
            this.textBoxAttributes.ReadOnly = true;
            this.textBoxAttributes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxAttributes.Size = new System.Drawing.Size(260, 168);
            this.textBoxAttributes.TabIndex = 1;
            this.textBoxAttributes.TabStop = false;
            this.textBoxAttributes.Text = "Select an entity to display its attributes";

            //
            // MSAGLExtClient
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.outerSplitContainer);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MinimumSize = new System.Drawing.Size(100, 100);
            this.Name = "MSAGLExtClient";
            this.Size = new System.Drawing.Size(900, 600);

            this.outerSplitContainer.Panel1.ResumeLayout(false);
            this.outerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outerSplitContainer)).EndInit();
            this.outerSplitContainer.ResumeLayout(false);
            this.leftInnerSplitContainer.Panel1.ResumeLayout(false);
            this.leftInnerSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.leftInnerSplitContainer)).EndInit();
            this.leftInnerSplitContainer.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.SplitContainer outerSplitContainer;
        private System.Windows.Forms.Panel mapPanel;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonClearSearch;
        private System.Windows.Forms.CheckBox buttonMatchCase;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.Button buttonSearchPrev;
        private System.Windows.Forms.TextBox textBoxSearchStatus;
        private System.Windows.Forms.Button buttonSearchNext;
        private System.Windows.Forms.SplitContainer leftInnerSplitContainer;
        private System.Windows.Forms.Label labelNesting;
        private System.Windows.Forms.TreeView treeViewNodeNesting;
        private System.Windows.Forms.Label labelAttributes;
        private System.Windows.Forms.TextBox textBoxAttributes;
        private System.Windows.Forms.ToolTip drawingPanelToolTip;
    }
}
