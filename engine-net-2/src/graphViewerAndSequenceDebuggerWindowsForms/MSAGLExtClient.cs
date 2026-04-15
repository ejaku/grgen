// by Claude Code with Edgar Jakumeit

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Msagl.Drawing;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    struct SearchResult
    {
        public Microsoft.Msagl.Core.Geometry.Point Center;
        public String Name;
        public bool IsEdge;

        public SearchResult(Microsoft.Msagl.Core.Geometry.Point center, String name, bool isEdge)
        {
            Center = center;
            Name = name;
            IsEdge = isEdge;
        }
    }

    /// <summary>
    /// Extended MSAGL graph viewer UserControl.
    /// Left pane: map-like overview, search pane, node-nesting tree, attributes pane.
    /// Right pane: main interactive graph display (MSAGLClient).
    /// Implements IBasicGraphViewerClient by delegating all graph mutations to the inner MSAGLClient.
    /// </summary>
    public partial class MSAGLExtClient : UserControl, IBasicGraphViewerClient
    {
        MSAGLClient mainClient;
        Form formHost;
        bool hostClosed = false;

        // Selection state
        String selectedEntityName = null;
        bool selectedIsEdge = false;

        // Search state
        List<SearchResult> searchResults = new List<SearchResult>();
        int searchResultIndex = -1;
        String lastSearchTerm = null;
        bool matchCase = false;

        // Hover tooltip tracking (entity name under cursor, tracked to avoid redundant ToolTip.Show calls)
        String lastHoveredEntityName = null;
        bool suppressTooltipHandler = false;

        // Tree sync guard
        bool suppressTreeSelectionHandler = false;
        Dictionary<String, TreeNode> nodeNameToTreeNode = new Dictionary<String, TreeNode>();

        // Map state (updated each paint; used for coordinate conversion)
        double mapScale = 1.0;
        double mapOffsetX = 0.0;
        double mapOffsetY = 0.0;

        // Map drag state (activated by double-click, released on mouse-up)
        bool mapDragging = false;
        System.Drawing.Point mapDragLastPoint;
        System.Drawing.Point mapDragStartPoint;

        public MSAGLExtClient()
        {
            InitializeComponent();

            mainClient = new MSAGLClient();
            mainClient.Dock = DockStyle.Fill;
            outerSplitContainer.Panel2.Controls.Add(mainClient);

            mainClient.gViewer.DrawingPanel.MouseClick += OnMainGViewerMouseClick;
            mainClient.gViewer.MouseMove += OnMainGViewerMouseMoveForToolTip;
            mainClient.gViewer.DrawingPanel.Paint += OnMainGViewerPaintForMapRefresh;
            mainClient.gViewer.KeyDown += OnMainGViewerKeyDown;
            mainClient.gViewer.DrawingPanel.KeyDown += OnMainGViewerKeyDown;
            textBoxAttributes.KeyDown += OnMainGViewerKeyDown;
            mainClient.gViewer.KeyPress += OnMainGViewerKeyPress;
            mainClient.gViewer.DrawingPanel.KeyPress += OnMainGViewerKeyPress;
            textBoxAttributes.KeyPress += OnMainGViewerKeyPress;
            mapPanel.Paint += OnMapPanelPaint;
            mapPanel.MouseDown += OnMapPanelMouseDown;
            mapPanel.MouseMove += OnMapPanelMouseMove;
            mapPanel.MouseUp += OnMapPanelMouseUp;

            treeViewNodeNesting.AfterSelect += OnTreeViewAfterSelect;

            buttonClearSearch.Click += OnButtonClearSearchClick;
            buttonMatchCase.CheckedChanged += OnButtonMatchCaseChanged;
            buttonSearch.Click += OnButtonSearchClick;
            buttonSearchNext.Click += OnButtonSearchNextClick;
            buttonSearchPrev.Click += OnButtonSearchPrevClick;
            textBoxSearch.KeyDown += OnTextBoxSearchKeyDown;
        }

        /// <summary>
        /// Creates a new MSAGLExtClient instance, adding itself to the hosting form.
        /// </summary>
        public MSAGLExtClient(Form host) : this()
        {
            host.SuspendLayout();
            host.Controls.Add(this);
            host.Controls.SetChildIndex(this, 0);
            formHost = host;
            host.ResumeLayout();
            host.Show();
            host.FormClosed += Host_FormClosed;
            Application.DoEvents();
        }

        void Host_FormClosed(object sender, FormClosedEventArgs e)
        {
            hostClosed = true;
        }

        //----------------------------------------------------------------------
        // IBasicGraphViewerClient - infrastructure

        public event ConnectionLostHandler OnConnectionLost;

        public bool CommandAvailable { get { return false; } }

        public bool ConnectionLost { get { return hostClosed; } }

        public String ReadCommand()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            if(formHost != null)
                formHost.Close();
        }

        public void SleepAndDoEvents()
        {
            System.Threading.Thread.Sleep(1);
            Application.DoEvents();
        }

        public bool Sync()
        {
            if(hostClosed)
            {
                if(OnConnectionLost != null)
                    OnConnectionLost();
            }
            return !hostClosed;
        }

        //----------------------------------------------------------------------
        // IBasicGraphViewerClient - layout (delegate to mainClient)

        public void SetLayout(String moduleName)
        {
            mainClient.SetLayout(moduleName);
        }

        public String GetLayoutOptions()
        {
            return mainClient.GetLayoutOptions();
        }

        public String SetLayoutOption(String optionName, String optionValue)
        {
            return mainClient.SetLayoutOption(optionName, optionValue);
        }

        public void ForceLayout()
        {
            mainClient.ForceLayout();
            SyncAfterLayout();
        }

        public new void Show()
        {
            mainClient.Show();
            SyncAfterLayout();
        }

        void SyncAfterLayout()
        {
            RebuildTree();
            InvalidateSearch();
            mapPanel.Invalidate();
        }

        //----------------------------------------------------------------------
        // IBasicGraphViewerClient - graph mutations (delegate, with side effects)

        public void AddSubgraphNode(String name, String nrName, String nodeLabel)
        {
            mainClient.AddSubgraphNode(name, nrName, nodeLabel);
        }
        public void AddSubgraphNode(String name, String nrName, String nodeLabel, String type)
        {
            mainClient.AddSubgraphNode(name, nrName, nodeLabel, type);
        }

        public void AddNode(String name, String nrName, String nodeLabel)
        {
            mainClient.AddNode(name, nrName, nodeLabel);
        }
        public void AddNode(String name, String nrName, String nodeLabel, String type)
        {
            mainClient.AddNode(name, nrName, nodeLabel, type);
        }

        public void SetNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            mainClient.SetNodeAttribute(name, ownerTypeName, attrTypeName, attrTypeString, attrValueString);
            if(name == selectedEntityName)
                textBoxAttributes.Text = mainClient.GetGraphElementAttributes(selectedEntityName) ?? "";
        }

        public void AddEdge(String edgeName, String srcName, String tgtName, String edgeRealizerName, String edgeLabel)
        {
            mainClient.AddEdge(edgeName, srcName, tgtName, edgeRealizerName, edgeLabel);
        }
        public void AddEdge(String edgeName, String srcName, String tgtName, String edgeRealizerName, String edgeLabel, String type)
        {
            mainClient.AddEdge(edgeName, srcName, tgtName, edgeRealizerName, edgeLabel, type);
        }

        public void SetEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            mainClient.SetEdgeAttribute(name, ownerTypeName, attrTypeName, attrTypeString, attrValueString);
            if(name == selectedEntityName)
                textBoxAttributes.Text = mainClient.GetGraphElementAttributes(selectedEntityName) ?? "";
        }

        public void ChangeNode(String nodeName, String realizer)
        {
            mainClient.ChangeNode(nodeName, realizer);
        }

        public void ChangeEdge(String edgeName, String realizer)
        {
            mainClient.ChangeEdge(edgeName, realizer);
        }

        public void SetNodeLabel(String name, String label)
        {
            mainClient.SetNodeLabel(name, label);
            InvalidateSearch();
        }
        public void SetNodeLabel(String name, String label, String type)
        {
            mainClient.SetNodeLabel(name, label, type);
            InvalidateSearch();
        }

        public void SetEdgeLabel(String name, String label)
        {
            mainClient.SetEdgeLabel(name, label);
            InvalidateSearch();
        }
        public void SetEdgeLabel(String name, String label, String type)
        {
            mainClient.SetEdgeLabel(name, label, type);
            InvalidateSearch();
        }

        public void ClearNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            mainClient.ClearNodeAttribute(name, ownerTypeName, attrTypeName, attrTypeString);
            if(name == selectedEntityName)
                textBoxAttributes.Text = mainClient.GetGraphElementAttributes(selectedEntityName) ?? "";
        }

        public void ClearEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            mainClient.ClearEdgeAttribute(name, ownerTypeName, attrTypeName, attrTypeString);
            if(name == selectedEntityName)
                textBoxAttributes.Text = mainClient.GetGraphElementAttributes(selectedEntityName) ?? "";
        }

        public void DeleteNode(String nodeName)
        {
            mainClient.DeleteNode(nodeName);
            if(selectedEntityName == nodeName && !selectedIsEdge)
            {
                Unselect();
            }
            RebuildTree();
            InvalidateSearch();
        }

        public void DeleteEdge(String edgeName)
        {
            mainClient.DeleteEdge(edgeName);
            if(selectedEntityName == edgeName && selectedIsEdge)
            {
                Unselect();
            }
            InvalidateSearch();
        }

        public void RenameNode(String oldName, String newName)
        {
            mainClient.RenameNode(oldName, newName);
        }

        public void RenameEdge(String oldName, String newName)
        {
            mainClient.RenameEdge(oldName, newName);
        }

        public void ClearGraph()
        {
            mainClient.ClearGraph();
            Unselect();
            treeViewNodeNesting.Nodes.Clear();
            nodeNameToTreeNode.Clear();
            InvalidateSearch();
            mapPanel.Invalidate();
        }

        public void WaitForElement(bool val)
        {
            mainClient.WaitForElement(val);
        }

        public void MoveNode(String srcName, String tgtName)
        {
            mainClient.MoveNode(srcName, tgtName);
        }

        public void AddNodeRealizer(String name, GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape)
        {
            mainClient.AddNodeRealizer(name, borderColor, color, textColor, nodeShape);
        }

        public void AddEdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            mainClient.AddEdgeRealizer(name, color, textColor, lineWidth, lineStyle);
        }

        public String Encode(String str)
        {
            return str;
        }

        //----------------------------------------------------------------------
        // Selection

        void OnMainGViewerMouseClick(object sender, MouseEventArgs e)
        {
            IViewerObject obj = mainClient.gViewer.ObjectUnderMouseCursor;
            if(obj == null)
            {
                Unselect();
                suppressTreeSelectionHandler = true;
                treeViewNodeNesting.SelectedNode = null;
                suppressTreeSelectionHandler = false;
                return;
            }

            IViewerNode viewerNode = obj as IViewerNode;
            IViewerEdge viewerEdge = obj as IViewerEdge;

            if(viewerNode != null)
            {
                Select(viewerNode.Node.Id, false);
                SyncTreeToSelection(viewerNode.Node.Id);
            }
            else if(viewerEdge != null)
            {
                Select(mainClient.GetEdgeName(viewerEdge.Edge), true);
            }
        }

        void OnMainGViewerMouseMoveForToolTip(object sender, MouseEventArgs e)
        {
            if(suppressTooltipHandler)
                return;

            Graph graph = mainClient.gViewer.Graph;
            if(graph == null || graph.GeometryGraph == null)
                return;

            // e is in gViewer coordinates; translate to DrawingPanel coordinates for ScreenToSource
            System.Drawing.Point dpPt = mainClient.gViewer.PointToClient(
                mainClient.PointToScreen(e.Location));
            Microsoft.Msagl.Core.Geometry.Point graphPt =
                mainClient.gViewer.ScreenToSource(dpPt);

            String hoveredName = null;
            String tipText = null;

            foreach(Node node in graph.Nodes)
            {
                if(node.GeometryNode != null && node.GeometryNode.BoundingBox.Contains(graphPt))
                {
                    hoveredName = node.Id;
                    tipText = mainClient.GetGraphElementAttributes(hoveredName);
                    break;
                }
            }

            if(hoveredName == null)
            {
                foreach(Edge edge in graph.Edges)
                {
                    if(edge.GeometryEdge != null && edge.GeometryEdge.Curve != null
                        && edge.GeometryEdge.Curve.BoundingBox.Contains(graphPt))
                    {
                        hoveredName = mainClient.GetEdgeName(edge);
                        tipText = mainClient.GetGraphElementAttributes(hoveredName);
                        break;
                    }
                }
            }

            if(hoveredName != lastHoveredEntityName)
            {
                lastHoveredEntityName = hoveredName;
                if(!string.IsNullOrEmpty(tipText))
                    drawingPanelToolTip.Show(tipText, mainClient.gViewer, e.X + 16, e.Y, 8000);
                else
                    drawingPanelToolTip.Hide(mainClient.gViewer);
            }
        }

        void OnMainGViewerKeyDown(object sender, KeyEventArgs e)
        {
            // When DrawingPanel has focus, both DrawingPanel.KeyDown and gViewer.KeyDown fire
            // (MSAGL re-raises the event on the parent). Skip the gViewer firing to avoid double-handling.
            if(ReferenceEquals(sender, mainClient.gViewer) && mainClient.gViewer.DrawingPanel.Focused)
                return;

            if(e.Control && e.KeyCode == Keys.F)
            {
                textBoxSearch.Focus();
                textBoxSearch.SelectAll();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.F3 && !e.Shift)
            {
                SearchNext();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.F3 && e.Shift)
            {
                SearchPrev();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.N && !e.Shift && !e.Control && !e.Alt)
            {
                SearchNext();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.N && e.Shift && !e.Control && !e.Alt)
            {
                SearchPrev();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        void OnMainGViewerKeyPress(object sender, KeyPressEventArgs e)
        {
            // Same double-fire guard as OnMainGViewerKeyDown
            if(ReferenceEquals(sender, mainClient.gViewer) && mainClient.gViewer.DrawingPanel.Focused)
                return;
            if(e.KeyChar == '/')
            {
                textBoxSearch.Focus();
                textBoxSearch.SelectAll();
                e.Handled = true;
            }
        }

        void SyncTreeToSelection(String nodeId)
        {
            TreeNode treeNode;
            if(!nodeNameToTreeNode.TryGetValue(nodeId, out treeNode))
                return;
            suppressTreeSelectionHandler = true;
            try
            {
                TreeNode parent = treeNode.Parent;
                while(parent != null)
                {
                    parent.Expand();
                    parent = parent.Parent;
                }
                treeViewNodeNesting.SelectedNode = treeNode;
                treeNode.EnsureVisible();
            }
            finally
            {
                suppressTreeSelectionHandler = false;
            }
        }

        void OnTreeViewAfterSelect(object sender, TreeViewEventArgs e)
        {
            if(suppressTreeSelectionHandler || e.Node == null)
                return;
            String nodeId = e.Node.Tag as String;
            if(nodeId == null)
                return;
            Node drawingNode = mainClient.gViewer.Graph.FindNode(nodeId);
            if(drawingNode == null)
            {
                // Try as a subgraph (subgraphs are not in graph.Nodes / FindNode)
                Subgraph sub = FindSubgraphById(mainClient.gViewer.Graph.RootSubgraph, nodeId);
                if(sub == null || sub.GeometryNode == null)
                    return;
                drawingNode = sub;
            }
            if(drawingNode.GeometryNode == null)
                return;

            // Simulate a left click at the node center to trigger MSAGL's own selection logic
            SimulateSelectAtPoint(drawingNode.GeometryNode.Center);
            Select(nodeId, false);
        }

        static Subgraph FindSubgraphById(Subgraph parent, String id)
        {
            foreach(Subgraph child in parent.Subgraphs)
            {
                if(child.Id == id)
                    return child;
                Subgraph found = FindSubgraphById(child, id);
                if(found != null)
                    return found;
            }
            return null;
        }

        void SimulateSelectAtPoint(Microsoft.Msagl.Core.Geometry.Point center)
        {
            // Deselect all selected entities, this is ok as we are only interested in single-selection at the moment,
            // and it saves us from issues when the SHIFT-key is pressed, e.g. when handling SHIFT-F3, which would cause an addition of the entity to be selected to the multi-selection in the GViewer.LayoutEditor
            mainClient.gViewer.LayoutEditor.Clear();

            // Center the node first so the Transform is updated before we compute screen coords
            mainClient.gViewer.CenterToPoint(center);

            suppressTooltipHandler = true;
            SimulateClickOnDrawingPanelCenter();
            suppressTooltipHandler = false;
        }

        void SimulateClickOnDrawingPanelCenter()
        {
            // CenterToPoint places the node at the panel center; use that as click target
            // so we don't depend on Transform being synchronously updated.
            Control dp = mainClient.gViewer.DrawingPanel;
            int sx = dp.Width / 2;
            int sy = dp.Height / 2;
            System.Reflection.BindingFlags flags =
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance;
            System.Reflection.MethodInfo onMove = typeof(Control).GetMethod("OnMouseMove", flags);
            System.Reflection.MethodInfo onDown = typeof(Control).GetMethod("OnMouseDown", flags);
            System.Reflection.MethodInfo onUp = typeof(Control).GetMethod("OnMouseUp", flags);
            // MouseMove first so MSAGL sets ObjectUnderMouseCursor before the click
            if(onMove != null) onMove.Invoke(dp, new object[] {
                new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, sx, sy, 0) });
            MouseEventArgs clickArgs = new MouseEventArgs(
                System.Windows.Forms.MouseButtons.Left, 1, sx, sy, 0);
            if(onDown != null) onDown.Invoke(dp, new object[] { clickArgs });
            if(onUp != null) onUp.Invoke(dp, new object[] { clickArgs });
        }

        void Select(String entityName, bool entityIsEdge)
        {
            selectedEntityName = entityName;
            selectedIsEdge = entityIsEdge;
            textBoxAttributes.Text = mainClient.GetGraphElementAttributes(selectedEntityName);
        }

        void Unselect()
        {
            selectedEntityName = null;
            selectedIsEdge = false;
            textBoxAttributes.Text = "Select an entity to display its attributes";
        }

        //----------------------------------------------------------------------
        // Tree

        void RebuildTree()
        {
            suppressTreeSelectionHandler = true;
            treeViewNodeNesting.BeginUpdate();
            treeViewNodeNesting.Nodes.Clear();
            nodeNameToTreeNode.Clear();

            Graph graph = mainClient.gViewer.Graph;
            if(graph != null)
            {
                TreeNode root = new TreeNode("this:graph");
                root.Tag = null;
                treeViewNodeNesting.Nodes.Add(root);
                root.Expand();

                AddSubgraphToTree(root, graph.RootSubgraph);

                foreach(Node node in graph.Nodes)
                {
                    if(!nodeNameToTreeNode.ContainsKey(node.Id))
                        AddNodeToTree(root, node);
                }
            }

            treeViewNodeNesting.EndUpdate();
            suppressTreeSelectionHandler = false;

            if(selectedEntityName != null && !selectedIsEdge)
                SyncTreeToSelection(selectedEntityName);
        }

        void AddSubgraphToTree(TreeNode parentTn, Subgraph subgraph)
        {
            foreach(Subgraph childSub in subgraph.Subgraphs)
            {
                String displayText = childSub.LabelText ?? childSub.Id;
                TreeNode tn = new TreeNode(displayText);
                tn.Tag = childSub.Id;
                parentTn.Nodes.Add(tn);
                nodeNameToTreeNode[childSub.Id] = tn;
                AddSubgraphToTree(tn, childSub);
            }
            foreach(Node node in subgraph.Nodes)
            {
                AddNodeToTree(parentTn, node);
            }
        }

        void AddNodeToTree(TreeNode parentTn, Node node)
        {
            String displayText;
            if(node.LabelText != null)
            {
                int nl = node.LabelText.IndexOf('\n');
                displayText = nl >= 0 ? node.LabelText.Substring(0, nl) : node.LabelText;
            }
            else
            {
                displayText = node.Id;
            }
            TreeNode tn = new TreeNode(displayText);
            tn.Tag = node.Id;
            parentTn.Nodes.Add(tn);
            nodeNameToTreeNode[node.Id] = tn;
        }

        //----------------------------------------------------------------------
        // Map overview

        void OnMainGViewerPaintForMapRefresh(object sender, PaintEventArgs e)
        {
            mapPanel.Invalidate();
        }

        void OnMapPanelPaint(object sender, PaintEventArgs e)
        {
            Graph graph = mainClient.gViewer.Graph;
            if(graph == null || graph.GeometryGraph == null)
                return;

            Microsoft.Msagl.Core.Geometry.Rectangle graphBBox = graph.BoundingBox;
            if(graphBBox.Width <= 0 || graphBBox.Height <= 0)
                return;

            int margin = 4;
            double mapWidth = mapPanel.ClientRectangle.Width - 2 * margin;
            double mapHeight = mapPanel.ClientRectangle.Height - 2 * margin;
            double scaleX = mapWidth / graphBBox.Width;
            double scaleY = mapHeight / graphBBox.Height;
            mapScale = Math.Min(scaleX, scaleY);
            mapOffsetX = margin + (mapWidth - graphBBox.Width * mapScale) / 2.0;
            mapOffsetY = margin + (mapHeight - graphBBox.Height * mapScale) / 2.0;

            // Draw subgraphs (cluster rectangles) behind regular nodes
            DrawSubgraphsOnMap(e.Graphics, graph.RootSubgraph, graphBBox);

            // Draw nodes with their actual fill color, border color, and shape
            DrawNodesOnMap(e.Graphics, graph, graphBBox);

            // Draw viewport rectangle showing the currently visible area of the main graph view
            Control drawingPanel = mainClient.gViewer.DrawingPanel;
            if(drawingPanel == null || drawingPanel.Width <= 0 || drawingPanel.Height <= 0)
                return;
            DrawViewportRectangle(e.Graphics, graphBBox, drawingPanel);
        }

        void DrawSubgraphsOnMap(Graphics g, Subgraph parent, Microsoft.Msagl.Core.Geometry.Rectangle graphBBox)
        {
            foreach(Subgraph sub in parent.Subgraphs)
            {
                if(sub.GeometryNode != null)
                {
                    Microsoft.Msagl.Core.Geometry.Rectangle nb = sub.GeometryNode.BoundingBox;
                    float nx = (float)(mapOffsetX + (nb.Left - graphBBox.Left) * mapScale);
                    float ny = (float)(mapOffsetY + (graphBBox.Top - nb.Top) * mapScale);
                    float nw = Math.Max(1f, (float)(nb.Width * mapScale));
                    float nh = Math.Max(1f, (float)(nb.Height * mapScale));
                    System.Drawing.Color fillColor = MsaglColorToDrawingColor(sub.Attr.FillColor);
                    System.Drawing.Color borderColor = MsaglColorToDrawingColor(sub.Attr.Color);
                    using(SolidBrush subBrush = new SolidBrush(fillColor))
                    using(Pen subPen = new Pen(borderColor, 1))
                    {
                        g.FillRectangle(subBrush, nx, ny, nw, nh);
                        g.DrawRectangle(subPen, nx, ny, nw, nh);
                    }
                }
                DrawSubgraphsOnMap(g, sub, graphBBox);
            }
        }

        private void DrawNodesOnMap(Graphics graphics, Graph graph, Microsoft.Msagl.Core.Geometry.Rectangle graphBBox)
        {
            foreach(Node node in graph.Nodes)
            {
                if(node.GeometryNode == null)
                    continue;

                Microsoft.Msagl.Core.Geometry.Rectangle nb = node.GeometryNode.BoundingBox;
                float nx = (float)(mapOffsetX + (nb.Left - graphBBox.Left) * mapScale);
                float ny = (float)(mapOffsetY + (graphBBox.Top - nb.Top) * mapScale);
                float nw = Math.Max(1f, (float)(nb.Width * mapScale));
                float nh = Math.Max(1f, (float)(nb.Height * mapScale));
                System.Drawing.Color fillColor = MsaglColorToDrawingColor(node.Attr.FillColor);
                System.Drawing.Color borderColor = MsaglColorToDrawingColor(node.Attr.Color);
                using(SolidBrush nodeBrush = new SolidBrush(fillColor))
                using(Pen nodePen = new Pen(borderColor, 1))
                {
                    DrawNodeShape(graphics, nodeBrush, nodePen, node.Attr.Shape, nx, ny, nw, nh);
                }
            }
        }

        private void DrawViewportRectangle(Graphics graphics, Microsoft.Msagl.Core.Geometry.Rectangle graphBBox, Control drawingPanel)
        {
            Microsoft.Msagl.Core.Geometry.Point graphTL =
                mainClient.gViewer.ScreenToSource(new System.Drawing.Point(0, 0));
            Microsoft.Msagl.Core.Geometry.Point graphBR =
                mainClient.gViewer.ScreenToSource(new System.Drawing.Point(drawingPanel.Width, drawingPanel.Height));

            float vx1 = (float)(mapOffsetX + (graphTL.X - graphBBox.Left) * mapScale);
            float vy1 = (float)(mapOffsetY + (graphBBox.Top - graphTL.Y) * mapScale);
            float vx2 = (float)(mapOffsetX + (graphBR.X - graphBBox.Left) * mapScale);
            float vy2 = (float)(mapOffsetY + (graphBBox.Top - graphBR.Y) * mapScale);

            float mapL = (float)mapOffsetX;
            float mapT = (float)mapOffsetY;
            float mapR = (float)(mapOffsetX + graphBBox.Width * mapScale);
            float mapB = (float)(mapOffsetY + graphBBox.Height * mapScale);

            vx1 = Math.Max(mapL, Math.Min(mapR, vx1));
            vy1 = Math.Max(mapT, Math.Min(mapB, vy1));
            vx2 = Math.Max(mapL, Math.Min(mapR, vx2));
            vy2 = Math.Max(mapT, Math.Min(mapB, vy2));

            if(vx2 > vx1 && vy2 > vy1)
            {
                using(Pen viewportPen = new Pen(System.Drawing.Color.Blue, 2))
                {
                    graphics.DrawRectangle(viewportPen, vx1, vy1, vx2 - vx1, vy2 - vy1);
                }
            }
        }

        // Converts a map-panel pixel position to a graph-space point
        Microsoft.Msagl.Core.Geometry.Point MapPanelToGraph(System.Drawing.Point p)
        {
            Graph graph = mainClient.gViewer.Graph;
            Microsoft.Msagl.Core.Geometry.Rectangle graphBBox = graph.BoundingBox;
            double gx = graphBBox.Left + (p.X - mapOffsetX) / mapScale;
            double gy = graphBBox.Top  - (p.Y - mapOffsetY) / mapScale;
            return new Microsoft.Msagl.Core.Geometry.Point(gx, gy);
        }

        void OnMapPanelMouseDown(object sender, MouseEventArgs e)
        {
            Graph graph = mainClient.gViewer.Graph;
            if(graph == null || graph.GeometryGraph == null || mapScale <= 0)
                return;
            mapDragging = true;
            mapDragLastPoint = e.Location;
            mapDragStartPoint = e.Location;
            mapPanel.Capture = true;
        }

        void OnMapPanelMouseMove(object sender, MouseEventArgs e)
        {
            if(!mapDragging)
                return;
            Graph graph = mainClient.gViewer.Graph;
            if(graph == null || graph.GeometryGraph == null || mapScale <= 0)
                return;
            int dx = e.Location.X - mapDragLastPoint.X;
            int dy = e.Location.Y - mapDragLastPoint.Y;
            mapDragLastPoint = e.Location;
            // dx/dy in map pixels → graph-space delta (Y is flipped)
            double gdx = dx / mapScale;
            double gdy = -dy / mapScale;
            Control dp = mainClient.gViewer.DrawingPanel;
            Microsoft.Msagl.Core.Geometry.Point viewCenter =
                mainClient.gViewer.ScreenToSource(new System.Drawing.Point(dp.Width / 2, dp.Height / 2));
            mainClient.gViewer.CenterToPoint(
                new Microsoft.Msagl.Core.Geometry.Point(viewCenter.X + gdx, viewCenter.Y + gdy));
            mapPanel.Refresh();
        }

        void OnMapPanelMouseUp(object sender, MouseEventArgs e)
        {
            if(!mapDragging)
                return;
            mapDragging = false;
            mapPanel.Capture = false;
            // If mouse didn't move (or barely moved), treat as a center-click
            int totalDx = e.Location.X - mapDragStartPoint.X;
            int totalDy = e.Location.Y - mapDragStartPoint.Y;
            if(totalDx * totalDx + totalDy * totalDy <= 9) // within 3px
            {
                Graph graph = mainClient.gViewer.Graph;
                if(graph != null && graph.GeometryGraph != null && mapScale > 0)
                {
                    Microsoft.Msagl.Core.Geometry.Point graphPt = MapPanelToGraph(e.Location);
                    mainClient.gViewer.CenterToPoint(graphPt);
                }
            }
            mapPanel.Refresh();
        }

        static System.Drawing.Color MsaglColorToDrawingColor(Microsoft.Msagl.Drawing.Color c)
        {
            return System.Drawing.Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        static void DrawNodeShape(Graphics g, SolidBrush fill, Pen border,
            Shape shape, float x, float y, float w, float h)
        {
            switch(shape)
            {
                case Shape.Circle:
                case Shape.Ellipse:
                    g.FillEllipse(fill, x, y, w, h);
                    g.DrawEllipse(border, x, y, w, h);
                    break;
                case Shape.Diamond:
                {
                    PointF[] pts = {
                        new PointF(x + w / 2, y),
                        new PointF(x + w, y + h / 2),
                        new PointF(x + w / 2, y + h),
                        new PointF(x, y + h / 2)
                    };
                    g.FillPolygon(fill, pts);
                    g.DrawPolygon(border, pts);
                    break;
                }
                case Shape.Triangle:
                {
                    PointF[] pts = {
                        new PointF(x + w / 2, y),
                        new PointF(x + w, y + h),
                        new PointF(x, y + h)
                    };
                    g.FillPolygon(fill, pts);
                    g.DrawPolygon(border, pts);
                    break;
                }
                case Shape.Hexagon:
                {
                    float dx = w / 4;
                    PointF[] pts = {
                        new PointF(x + dx, y),
                        new PointF(x + w - dx, y),
                        new PointF(x + w, y + h / 2),
                        new PointF(x + w - dx, y + h),
                        new PointF(x + dx, y + h),
                        new PointF(x, y + h / 2)
                    };
                    g.FillPolygon(fill, pts);
                    g.DrawPolygon(border, pts);
                    break;
                }
                default: // Box, Trapezium, Parallelogram, etc.
                    g.FillRectangle(fill, x, y, w, h);
                    g.DrawRectangle(border, x, y, w, h);
                    break;
            }
        }

        //----------------------------------------------------------------------
        // Search

        void InvalidateSearch()
        {
            searchResults.Clear();
            searchResultIndex = -1;
            lastSearchTerm = null;
            textBoxSearchStatus.Text = "";
        }

        void ExecuteSearch()
        {
            String term = textBoxSearch.Text;
            InvalidateSearch();
            lastSearchTerm = term;

            if(String.IsNullOrEmpty(term))
                return;

            Graph graph = mainClient.gViewer.Graph;
            if(graph == null)
            {
                textBoxSearchStatus.Text = "No search results available";
                System.Media.SystemSounds.Beep.Play();
                mainClient.gViewer.Focus();
                return;
            }

            StringComparison comparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            foreach(Node node in graph.Nodes)
            {
                String attrs = mainClient.GetGraphElementAttributes(node.Id);
                if(attrs != null && attrs.IndexOf(term, comparison) >= 0 && node.GeometryNode != null)
                    searchResults.Add(new SearchResult(node.GeometryNode.Center, node.Id, false));
            }
            foreach(Edge edge in graph.Edges)
            {
                if(edge.GeometryEdge == null || edge.GeometryEdge.Curve == null)
                    continue;
                String edgeName = mainClient.GetEdgeName(edge);
                String attrs = mainClient.GetGraphElementAttributes(edgeName);
                if(attrs != null && attrs.IndexOf(term, comparison) >= 0)
                {
                    Microsoft.Msagl.Core.Geometry.Rectangle bb = edge.GeometryEdge.Curve.BoundingBox;
                    Microsoft.Msagl.Core.Geometry.Point center = new Microsoft.Msagl.Core.Geometry.Point(
                        (bb.Left + bb.Right) / 2.0,
                        (bb.Bottom + bb.Top) / 2.0);
                    searchResults.Add(new SearchResult(center, edgeName ?? "", true));
                }
            }

            if(searchResults.Count == 0)
            {
                textBoxSearchStatus.Text = "No search results available";
                System.Media.SystemSounds.Beep.Play();
                mainClient.gViewer.Focus();
                return;
            }

            searchResultIndex = 0;
            ShowCurrentSearchResult();
            mainClient.gViewer.Focus();
        }

        void ShowCurrentSearchResult()
        {
            if(searchResultIndex < 0 || searchResultIndex >= searchResults.Count)
                return;
            textBoxSearchStatus.Text = "Showing search result " + (searchResultIndex + 1)
                + " of " + searchResults.Count;
            SimulateSelectAtPoint(searchResults[searchResultIndex].Center);
            Select(searchResults[searchResultIndex].Name, searchResults[searchResultIndex].IsEdge);
            if(!searchResults[searchResultIndex].IsEdge)
                SyncTreeToSelection(searchResults[searchResultIndex].Name);
            SelectSearchTermInAttributesView();
        }

        void SelectSearchTermInAttributesView()
        {
            if(string.IsNullOrEmpty(lastSearchTerm) || textBoxAttributes.Text == null)
                return;
            StringComparison comparison = matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            int idx = textBoxAttributes.Text.IndexOf(lastSearchTerm, comparison);
            if(idx >= 0)
            {
                textBoxAttributes.SelectionStart = idx;
                textBoxAttributes.SelectionLength = lastSearchTerm.Length;
                textBoxAttributes.ScrollToCaret();
            }
        }

        void SearchNext()
        {
            if(searchResults.Count == 0 || searchResultIndex >= searchResults.Count - 1)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            searchResultIndex++;
            ShowCurrentSearchResult();
        }

        void SearchPrev()
        {
            if(searchResults.Count == 0 || searchResultIndex <= 0)
            {
                System.Media.SystemSounds.Beep.Play();
                return;
            }
            searchResultIndex--;
            ShowCurrentSearchResult();
        }

        void OnButtonClearSearchClick(object sender, EventArgs e)
        {
            textBoxSearch.Clear();
            InvalidateSearch();
        }

        void OnButtonMatchCaseChanged(object sender, EventArgs e)
        {
            matchCase = buttonMatchCase.Checked;
            if(lastSearchTerm != null)
                ExecuteSearch();
        }

        void OnButtonSearchClick(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        void OnButtonSearchNextClick(object sender, EventArgs e)
        {
            SearchNext();
        }

        void OnButtonSearchPrevClick(object sender, EventArgs e)
        {
            SearchPrev();
        }

        void OnTextBoxSearchKeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                ExecuteSearch();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if(e.KeyCode == Keys.F3 && !e.Shift)
            {
                SearchNext();
                e.Handled = true;
            }
            else if(e.KeyCode == Keys.F3 && e.Shift)
            {
                SearchPrev();
                e.Handled = true;
            }
        }

        // this function smells of i18n issues, it works on windows, but could explain linux/mono troubles, potential(AI) TODO: replace by regular key event on control listening
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if(keyData == Keys.OemQuestion || keyData == (Keys.Shift | Keys.D7) // scruffily matches the char "/" on US and DE keyboards, also catching "#" and "&" the other way round
                || keyData == (Keys.Control | Keys.F))
            {
                textBoxSearch.Focus();
                textBoxSearch.SelectAll();
                return true;
            }
            if(keyData == Keys.F3 || keyData == Keys.N)
            {
                SearchNext();
                return true;
            }
            if(keyData == (Keys.Shift | Keys.F3) || keyData == (Keys.Shift | Keys.N))
            {
                SearchPrev();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
