
namespace TestMSAGL
{
    partial class MSAGLTestHost
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
            this.comboBoxGraphViewerChooser = new System.Windows.Forms.ComboBox();
            this.buttonAddNestedGroupThenFillIt = new System.Windows.Forms.Button();
            this.buttonFillNestedGroupThenAddIt = new System.Windows.Forms.Button();
            this.buttonDeleteNestedNestedNode = new System.Windows.Forms.Button();
            this.buttonDeleteNestedGroup = new System.Windows.Forms.Button();
            this.buttonDeleteGroup = new System.Windows.Forms.Button();
            this.buttonClearGraph = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonDeleteNestedNode = new System.Windows.Forms.Button();
            this.buttonDeleteNode = new System.Windows.Forms.Button();
            this.buttonAddEdges = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxGraphViewerChooser
            // 
            this.comboBoxGraphViewerChooser.FormattingEnabled = true;
            this.comboBoxGraphViewerChooser.Items.AddRange(new object[] {
            "yComp",
            "MSAGL"});
            this.comboBoxGraphViewerChooser.Location = new System.Drawing.Point(13, 13);
            this.comboBoxGraphViewerChooser.Name = "comboBoxGraphViewerChooser";
            this.comboBoxGraphViewerChooser.Size = new System.Drawing.Size(92, 21);
            this.comboBoxGraphViewerChooser.TabIndex = 0;
            this.comboBoxGraphViewerChooser.Text = "MSAGL";
            this.comboBoxGraphViewerChooser.SelectedIndexChanged += new System.EventHandler(this.comboBoxGraphViewerChooser_SelectedIndexChanged);
            // 
            // buttonAddNestedGroupThenFillIt
            // 
            this.buttonAddNestedGroupThenFillIt.Location = new System.Drawing.Point(13, 59);
            this.buttonAddNestedGroupThenFillIt.Name = "buttonAddNestedGroupThenFillIt";
            this.buttonAddNestedGroupThenFillIt.Size = new System.Drawing.Size(174, 23);
            this.buttonAddNestedGroupThenFillIt.TabIndex = 1;
            this.buttonAddNestedGroupThenFillIt.Text = "AddNestedGroupThenFillIt";
            this.buttonAddNestedGroupThenFillIt.UseVisualStyleBackColor = true;
            this.buttonAddNestedGroupThenFillIt.Click += new System.EventHandler(this.buttonAddNestedGroupThenFillIt_Click);
            // 
            // buttonFillNestedGroupThenAddIt
            // 
            this.buttonFillNestedGroupThenAddIt.Location = new System.Drawing.Point(13, 88);
            this.buttonFillNestedGroupThenAddIt.Name = "buttonFillNestedGroupThenAddIt";
            this.buttonFillNestedGroupThenAddIt.Size = new System.Drawing.Size(174, 23);
            this.buttonFillNestedGroupThenAddIt.TabIndex = 2;
            this.buttonFillNestedGroupThenAddIt.Text = "FillNestedGroupThenAddIt";
            this.buttonFillNestedGroupThenAddIt.UseVisualStyleBackColor = true;
            this.buttonFillNestedGroupThenAddIt.Click += new System.EventHandler(this.buttonFillNestedGroupThenAddIt_Click);
            // 
            // buttonDeleteNestedNestedNode
            // 
            this.buttonDeleteNestedNestedNode.Location = new System.Drawing.Point(12, 284);
            this.buttonDeleteNestedNestedNode.Name = "buttonDeleteNestedNestedNode";
            this.buttonDeleteNestedNestedNode.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteNestedNestedNode.TabIndex = 3;
            this.buttonDeleteNestedNestedNode.Text = "DeleteNestedNestedNode";
            this.buttonDeleteNestedNestedNode.UseVisualStyleBackColor = true;
            this.buttonDeleteNestedNestedNode.Click += new System.EventHandler(this.buttonDeleteNestedNestedNode_Click);
            // 
            // buttonDeleteNestedGroup
            // 
            this.buttonDeleteNestedGroup.Location = new System.Drawing.Point(11, 226);
            this.buttonDeleteNestedGroup.Name = "buttonDeleteNestedGroup";
            this.buttonDeleteNestedGroup.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteNestedGroup.TabIndex = 4;
            this.buttonDeleteNestedGroup.Text = "DeleteNestedGroup";
            this.buttonDeleteNestedGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteNestedGroup.Click += new System.EventHandler(this.buttonDeleteNestedGroup_Click);
            // 
            // buttonDeleteGroup
            // 
            this.buttonDeleteGroup.Location = new System.Drawing.Point(12, 168);
            this.buttonDeleteGroup.Name = "buttonDeleteGroup";
            this.buttonDeleteGroup.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteGroup.TabIndex = 5;
            this.buttonDeleteGroup.Text = "DeleteGroup";
            this.buttonDeleteGroup.UseVisualStyleBackColor = true;
            this.buttonDeleteGroup.Click += new System.EventHandler(this.buttonDeleteGroup_Click);
            // 
            // buttonClearGraph
            // 
            this.buttonClearGraph.Location = new System.Drawing.Point(12, 342);
            this.buttonClearGraph.Name = "buttonClearGraph";
            this.buttonClearGraph.Size = new System.Drawing.Size(175, 23);
            this.buttonClearGraph.TabIndex = 6;
            this.buttonClearGraph.Text = "ClearGraph";
            this.buttonClearGraph.UseVisualStyleBackColor = true;
            this.buttonClearGraph.Click += new System.EventHandler(this.buttonClearGraph_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(111, 11);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(76, 23);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonDeleteNestedNode
            // 
            this.buttonDeleteNestedNode.Location = new System.Drawing.Point(12, 255);
            this.buttonDeleteNestedNode.Name = "buttonDeleteNestedNode";
            this.buttonDeleteNestedNode.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteNestedNode.TabIndex = 8;
            this.buttonDeleteNestedNode.Text = "DeleteNestedNode";
            this.buttonDeleteNestedNode.UseVisualStyleBackColor = true;
            this.buttonDeleteNestedNode.Click += new System.EventHandler(this.buttonDeleteNestedNode_Click);
            // 
            // buttonDeleteNode
            // 
            this.buttonDeleteNode.Location = new System.Drawing.Point(12, 197);
            this.buttonDeleteNode.Name = "buttonDeleteNode";
            this.buttonDeleteNode.Size = new System.Drawing.Size(175, 23);
            this.buttonDeleteNode.TabIndex = 9;
            this.buttonDeleteNode.Text = "DeleteNode";
            this.buttonDeleteNode.UseVisualStyleBackColor = true;
            this.buttonDeleteNode.Click += new System.EventHandler(this.buttonDeleteNode_Click);
            // 
            // buttonAddEdges
            // 
            this.buttonAddEdges.Location = new System.Drawing.Point(13, 117);
            this.buttonAddEdges.Name = "buttonAddEdges";
            this.buttonAddEdges.Size = new System.Drawing.Size(174, 23);
            this.buttonAddEdges.TabIndex = 10;
            this.buttonAddEdges.Text = "AddEdges";
            this.buttonAddEdges.UseVisualStyleBackColor = true;
            this.buttonAddEdges.Click += new System.EventHandler(this.buttonAddEdges_Click);
            // 
            // MSAGLTestHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 559);
            this.Controls.Add(this.buttonAddEdges);
            this.Controls.Add(this.buttonDeleteNode);
            this.Controls.Add(this.buttonDeleteNestedNode);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonClearGraph);
            this.Controls.Add(this.buttonDeleteGroup);
            this.Controls.Add(this.buttonDeleteNestedGroup);
            this.Controls.Add(this.buttonDeleteNestedNestedNode);
            this.Controls.Add(this.buttonFillNestedGroupThenAddIt);
            this.Controls.Add(this.buttonAddNestedGroupThenFillIt);
            this.Controls.Add(this.comboBoxGraphViewerChooser);
            this.Name = "MSAGLTestHost";
            this.Text = "MSAGLTestHost";
            this.Load += new System.EventHandler(this.MSAGLTestHost_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxGraphViewerChooser;
        private System.Windows.Forms.Button buttonAddNestedGroupThenFillIt;
        private System.Windows.Forms.Button buttonFillNestedGroupThenAddIt;
        private System.Windows.Forms.Button buttonDeleteNestedNestedNode;
        private System.Windows.Forms.Button buttonDeleteNestedGroup;
        private System.Windows.Forms.Button buttonDeleteGroup;
        private System.Windows.Forms.Button buttonClearGraph;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonDeleteNestedNode;
        private System.Windows.Forms.Button buttonDeleteNode;
        private System.Windows.Forms.Button buttonAddEdges;
    }
}

