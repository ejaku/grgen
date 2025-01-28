/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

using System;
using System.Collections.Generic;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Drawing;
using de.unika.ipd.grGen.libGr;
using System.Text;
using System.Windows.Forms;

namespace de.unika.ipd.grGen.graphViewerAndSequenceDebugger
{
    class MSAGLNodeRealizer
    {
        public Color borderColor;
        public Color color;
        public Color textColor;
        public Shape nodeShape;
    }

    class MSAGLEdgeRealizer
    {
        public Color color;
        public Color textColor;
        public int lineWidth;
        public Style lineStyle;
    }

    /// <summary>
    /// Class communicating with the MSAGL library
    /// TODO: still some issues, esp. with node nesting
    /// </summary>
    public class MSAGLClient : IBasicGraphViewerClient
    {
        public GViewer gViewer;
        Form formHost;
        bool hostClosed = false;
        SplitterPanel splitterPanelHost;

        private static Dictionary<String, bool> availableLayouts;


        static MSAGLClient()
        {
            availableLayouts = new Dictionary<string, bool>();
            availableLayouts.Add("SugiyamaScheme", true); // similar to Hierarchic
            availableLayouts.Add("MDS", true); // roughly similar to Organic
            availableLayouts.Add("Ranking", true);
            availableLayouts.Add("IcrementalLayout", true);
        }

        /// <summary>
        /// Creates a new MSAGLClient instance, adding its GViewer control to the hosting form (at position 0).
        /// </summary>
        public MSAGLClient(Form host)
        {
            gViewer = new GViewer(); // if your application doesn't start from this line complaining about System.Resources.Extensions, you have to add a PackageReference to the project file of your app, plus a bindingRedirect to the app.config of your app, see the GrShell project for an example
            Graph graph = new Graph("graph");
            gViewer.Graph = graph;
            host.SuspendLayout();
            gViewer.Dock = DockStyle.Fill;
            gViewer.MinimumSize = new System.Drawing.Size(50, 50);
            host.Controls.Add(gViewer);
            host.Controls.SetChildIndex(gViewer, 0);
            formHost = host;
            host.ResumeLayout();
            host.Show();
            host.FormClosed += Host_FormClosed;
            Application.DoEvents();
        }

        /// <summary>
        /// Creates a new MSAGLClient instance, adding its GViewer control to the hosting splitter panel (at position 0) (not for main graph rendering).
        /// </summary>
        public MSAGLClient(SplitterPanel host)
        {
            gViewer = new GViewer(); // if your application doesn't start from this line complaining about System.Resources.Extensions, you have to add a PackageReference to the project file of your app, plus a bindingRedirect to the app.config of your app, see the GrShell project for an example
            Graph graph = new Graph("graph");
            gViewer.Graph = graph;
            host.SuspendLayout();
            gViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            gViewer.MinimumSize = new System.Drawing.Size(50, 50);
            host.Controls.Add(gViewer);
            host.Controls.SetChildIndex(gViewer, 0);
            splitterPanelHost = host;
            host.ResumeLayout();
            host.Show();
            Application.DoEvents();
        }

        private void Host_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            hostClosed = true; // cannot call OnConnectionLost() directly, wrong thread
        }

        public void Close()
        {
            if(formHost != null)
            {
                formHost.Controls.Remove(gViewer);
                formHost.Close();
            }
            if(splitterPanelHost != null)
                splitterPanelHost.Controls.Remove(gViewer);
            gViewer.Dispose();
            gViewer = null;
        }

        public void SleepAndDoEvents()
        {
            System.Threading.Thread.Sleep(1);
            Application.DoEvents();
        }


        public static IEnumerable<String> AvailableLayouts
        {
            get { return availableLayouts.Keys; }
        }

        public static bool IsValidLayout(String layoutName)     // TODO: allow case insensitive layout name
        {
            return availableLayouts.ContainsKey(layoutName);
        }

        public event ConnectionLostHandler OnConnectionLost;

        public bool CommandAvailable
        {
            get { return false; }
        }

        public bool ConnectionLost
        {
            get { return hostClosed; }
        }

        public String ReadCommand()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the current layouter of MSAGL
        /// </summary>
        /// <param name="moduleName">The name of the layouter.
        ///     Can be one of:
        ///      - SugiyamaScheme,
        ///      - MDS,
        ///      - Ranking,
        ///      - IcrementalLayout
        /// </param>
        public void SetLayout(String moduleName)
        {
            LayoutMethod layoutMethod;
            if(Enum.TryParse<LayoutMethod>(moduleName, out layoutMethod))
            {
                gViewer.CurrentLayoutMethod = (LayoutMethod)Enum.Parse(typeof(LayoutMethod), moduleName);
            }
        }

        /// <summary>
        /// Retrieves the available options of the current layouter of MSAGL and the current values.
        /// </summary>
        /// <returns>A description of the available options of the current layouter of MSAGL
        /// and the current values.</returns>
        public String GetLayoutOptions()
        {
            //gViewer.Graph.LayoutAlgorithmSettings is Microsoft.Msagl.Layout.Layered.SugiyamaLayoutSettings or Microsoft.Msagl.Layout.MDS.MdsLayoutSettings or Microsoft.Msagl.Prototype.Ranking.RankingLayoutSettings or Microsoft.Msagl.Layout.Incremental.FastIncrementalLayoutSettings
            System.ComponentModel.ExpandableObjectConverter converter = new System.ComponentModel.ExpandableObjectConverter();
            System.ComponentModel.PropertyDescriptorCollection properties = converter.GetProperties(gViewer.Graph.LayoutAlgorithmSettings);
            StringBuilder options = new StringBuilder();
            foreach(System.ComponentModel.PropertyDescriptor property in properties)
            {
                options.AppendLine(property.Name + " = " + property.GetValue(gViewer.Graph.LayoutAlgorithmSettings));
            }
            return options.ToString();
        }

        /// <summary>
        /// Sets a layout option of the current layouter of MSAGL.
        /// </summary>
        /// <param name="optionName">The name of the option.</param>
        /// <param name="optionValue">The new value.</param>
        /// <returns>"optionset\n", or an error message, if setting the option failed.</returns>
        public String SetLayoutOption(String optionName, String optionValue)
        {
            System.ComponentModel.ExpandableObjectConverter converter = new System.ComponentModel.ExpandableObjectConverter();
            System.ComponentModel.PropertyDescriptorCollection properties = converter.GetProperties(gViewer.Graph.LayoutAlgorithmSettings);
            try
            {
                foreach(System.ComponentModel.PropertyDescriptor property in properties) // TODO: find better way than iterating all properties
                {
                    if(property.Name == optionName)
                    {
                        property.SetValue(gViewer.Graph.LayoutAlgorithmSettings, Convert.ChangeType(optionValue, property.PropertyType));
                        return "optionset\n";
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            return "Unknown property";
        }

        /// <summary>
        /// Forces MSAGL to relayout the graph.
        /// </summary>
        public void ForceLayout()
        {
            //gViewer.NeedToCalculateLayout = true;
            gViewer.Graph = gViewer.Graph;
            //gViewer.NeedToCalculateLayout = false;
            //gViewer.Graph = gViewer.Graph;
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// Shows the graph (without relayout).
        /// </summary>
        public void Show()
        {
            // TODO: implement as intended without relayout
            ForceLayout();
        }

        /// <summary>
        /// We're in synch as long as our hosting form is not closed
        /// </summary>
        public bool Sync()
        {
            if(hostClosed)
            {
                if(OnConnectionLost != null)
                    OnConnectionLost();
            }
            return !hostClosed;
        }

        public void AddSubgraphNode(String name, String nrName, String nodeLabel)
        {
            Subgraph subgraph = new Subgraph(name);
            gViewer.Graph.RootSubgraph.AddSubgraph(subgraph);
            ApplyRealizer(subgraph, nrName);
            subgraph.LabelText = nodeLabel;
        }

        public void AddNode(String name, String nrName, String nodeLabel)
        {
            Node node = new Node(name);
            gViewer.Graph.AddNode(node);
            ApplyRealizer(node, nrName);
            node.LabelText = nodeLabel;
        }

        public void SetNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            // TODO - for node name show tooltip like N::a:T = v when hovered by the user
        }

        public void AddEdge(String edgeName, String srcName, String tgtName, String edgeRealizerName, String edgeLabel)
        {
            Edge edge = gViewer.Graph.AddEdge(srcName, edgeName, tgtName);
            nameToEdge.Add(edgeName, edge);
            edgeToName.Add(edge, edgeName);
            ApplyRealizer(edge, edgeRealizerName);
            edge.LabelText = edgeLabel;
        }

        public void SetEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString, String attrValueString)
        {
            // TODO - for edge name show tooltip like E::a:T = v when hovered by the user
        }

        /// <summary>
        /// Sets the node realizer of the given node.
        /// If realizer is null, the realizer for the type of the node is used.
        /// </summary>
        public void ChangeNode(String nodeName, String realizer)
        {
            Node node = gViewer.Graph.AddNode(nodeName); // AddNode(string) means return old node or add if not exists, but (hopefully) searches also subgraphs in contrast to FindNode
            ApplyRealizer(node, realizer);
        }

        /// <summary>
        /// Sets the edge realizer of the given edge.
        /// If realizer is null, the realizer for the type of the edge is used.
        /// </summary>
        public void ChangeEdge(String edgeName, String realizer)
        {
            Edge edge;
            if(nameToEdge.TryGetValue(edgeName, out edge)) // name should be available, TryGetValue for increased robustness (in the face of nesting, and bugs/qwirks)
                ApplyRealizer(edge, realizer);
        }

        public void SetNodeLabel(String name, String label)
        {
            Node node = gViewer.Graph.AddNode(name);
            node.LabelText = label;
        }

        public void SetEdgeLabel(String name, String label)
        {
            Edge edge;
            if(nameToEdge.TryGetValue(name, out edge))
                edge.LabelText = label;
        }

        public void ClearNodeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            // TODO - for node name remove tooltip of kind E::a:T, used during retyping
        }

        public void ClearEdgeAttribute(String name, String ownerTypeName, String attrTypeName, String attrTypeString)
        {
            // TODO - for edge name remove tooltip of kind E::a:T, used during retyping
        }

        public void DeleteNode(String nodeName)
        {
            Node node = gViewer.Graph.FindNode(nodeName);

            if(node != null)
            {
                DeleteAllEdges(node);

                gViewer.Graph.RemoveNode(node);

                Subgraph root = gViewer.Graph.RootSubgraph;
                foreach(Subgraph sub in root.AllSubgraphsDepthFirst())
                {
                    sub.RemoveNode(node);
                }
            }
            else
            {
                if(gViewer.Graph.SubgraphMap.ContainsKey(nodeName))
                {
                    Subgraph subgraph = gViewer.Graph.SubgraphMap[nodeName];

                    MoveChildrenToParent(subgraph);

                    DeleteAllEdges(subgraph);

                    subgraph.ParentSubgraph.RemoveSubgraph(subgraph);
                }
                else
                {
                    Console.Error.WriteLine("Warning: DeleteNode: Unknown node: " + nodeName);
                }
            }
        }

        private void MoveChildrenToParent(Subgraph subgraph)
        {
            List<Node> subgraphNodes = new List<Node>(subgraph.Nodes);
            foreach(Node n in subgraphNodes)
            {
                subgraph.RemoveNode(n);
                subgraph.ParentSubgraph.AddNode(n);
            }
            List<Subgraph> subgraphSubgraphs = new List<Subgraph>(subgraph.Subgraphs);
            foreach(Subgraph sub in subgraphSubgraphs)
            {
                subgraph.RemoveSubgraph(sub);
                subgraph.ParentSubgraph.AddSubgraph(sub);
            }
        }

        public void DeleteAllEdges(Node node)
        {
            List<Edge> edges = new List<Edge>(node.Edges);
            foreach(Edge edge in edges)
            {
                String edgeName = edgeToName[edge];
                nameToEdge.Remove(edgeName);
                edgeToName.Remove(edge);
                gViewer.Graph.RemoveEdge(edge);
            }
        }

        public void DeleteAllEdges(Subgraph subgraph)
        {
            List<Edge> edges = new List<Edge>(subgraph.Edges);
            foreach(Edge edge in edges)
            {
                String edgeName = edgeToName[edge];
                nameToEdge.Remove(edgeName);
                edgeToName.Remove(edge);
                gViewer.Graph.RemoveEdge(edge);
            }
        }

        public void DeleteEdge(String edgeName)
        {
            // TODO: Update group relation // GUI TODO: it seems EdgeById does not work at all
            /*Edge edge = gViewer.Graph.EdgeById(edgeName);
            if(edge == null && oldEdgeName != null)
                edge = gViewer.Graph.EdgeById(oldEdgeName);
            gViewer.Graph.RemoveEdge(edge);*/

            if(nameToEdge.ContainsKey(edgeName))
            {
                Edge edge = nameToEdge[edgeName];
                gViewer.Graph.RemoveEdge(edge);
                nameToEdge.Remove(edgeName);
                edgeToName.Remove(edge);
            }
            else
            {
                Console.Error.WriteLine("Warning: DeleteEdge: Unknown edge: " + edgeName);
            }
        }

        public void RenameNode(String oldName, String newName)
        {
            throw new NotImplementedException(); // the simple solution of just assigning node.Id or node.Attr.Id was not sufficient, but as it is not needed anymore, I just leave it like this (future TODO: purge RenameNode (alternative: implement it))
        }

        public void RenameEdge(String oldName, String newName)
        {
            throw new NotImplementedException(); // the simple solution of just assigning edge.Attr.Id was not sufficient, but as it is not needed anymore, I just leave it like this (future TODO: purge RenameEdge (alternative: implement it))
        }

        public void ClearGraph()
        {
            List<Node> nodes = new List<Node>(gViewer.Graph.Nodes);
            foreach(Node node in nodes)
            {
                gViewer.Graph.RemoveNode(node);
            }
            foreach(KeyValuePair<String, Subgraph> nameToSubgraph in gViewer.Graph.SubgraphMap) // maybe recursive, maybe exclude root subgraph, but it seems to work this way...
            {
                gViewer.Graph.RootSubgraph.RemoveSubgraph(nameToSubgraph.Value);
            }
            foreach(Edge edge in gViewer.Graph.Edges)
            {
                throw new Exception("Should be empty after clearing nodes and subgraphs");
            }
            nameToEdge.Clear();
            edgeToName.Clear();
        }

        public void WaitForElement(bool val)
        {
            // TODO val==true: ask user to select node or edge in the graph, val==false: stop asking. The name of the element is to be returned with ReadCommand, in case of CommandAvailable.
        }

        public void MoveNode(String srcName, String tgtName)
        {
            Node node = gViewer.Graph.AddNode(srcName);
            if(node is Subgraph)
                gViewer.Graph.SubgraphMap[tgtName].AddSubgraph(node as Subgraph);
            else
                gViewer.Graph.SubgraphMap[tgtName].AddNode(node);
        }

        public void AddNodeRealizer(String name, GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape)
        {
            nodeRealizers.Add(name, ToMSAGLNodeRealizer(borderColor, color, textColor, nodeShape));
        }

        public void AddEdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            edgeRealizers.Add(name, ToMSAGLEdgeRealizer(color, textColor, lineWidth, lineStyle));
        }

        public String Encode(String str)
        {
            return str;
        }

        MSAGLNodeRealizer ToMSAGLNodeRealizer(GrColor borderColor, GrColor color, GrColor textColor, GrNodeShape nodeShape)
        {
            MSAGLNodeRealizer nodeRealizer = new MSAGLNodeRealizer();
            nodeRealizer.borderColor = GetColor(borderColor);
            nodeRealizer.color = GetColor(color);
            nodeRealizer.textColor = GetColor(textColor);
            nodeRealizer.nodeShape = GetNodeShape(nodeShape);
            return nodeRealizer;
        }

        MSAGLEdgeRealizer ToMSAGLEdgeRealizer(GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            MSAGLEdgeRealizer edgeRealizer = new MSAGLEdgeRealizer();
            edgeRealizer.color = GetColor(color);
            edgeRealizer.textColor = GetColor(textColor);
            edgeRealizer.lineWidth = lineWidth;
            edgeRealizer.lineStyle = GetLineStyle(lineStyle);
            return edgeRealizer;
        }

        Dictionary<String, MSAGLNodeRealizer> nodeRealizers = new Dictionary<string, MSAGLNodeRealizer>();
        Dictionary<String, MSAGLEdgeRealizer> edgeRealizers = new Dictionary<string, MSAGLEdgeRealizer>();

        Dictionary<String, Edge> nameToEdge = new Dictionary<string, Edge>();
        Dictionary<Edge, String> edgeToName = new Dictionary<Edge, string>();

        //maps by index to GrColor defined in dumpInterface.cs:
        //Black, Blue, Green, Cyan, Red, Purple, Brown, Grey,
        //LightGrey, LightBlue, LightGreen, LightCyan, LightRed, LightPurple, Yellow, White,
        //DarkBlue, DarkRed, DarkGreen, DarkYellow, DarkMagenta, DarkCyan, Gold, Lilac,
        //Turquoise, Aquamarine, Khaki, Pink, Orange, Orchid, LightYellow, YellowGreen
        private static readonly Color[] colors = {
            Color.Black, Color.Blue, Color.Green, Color.Cyan, Color.Red, Color.Purple, Color.Brown, Color.Gray,
            Color.LightGray, Color.LightBlue, Color.LightGreen, Color.LightCyan, Color.PaleVioletRed, Color.Magenta, Color.Yellow, Color.White,
            Color.DarkBlue, Color.DarkRed, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkCyan, Color.Gold, Color.Violet,
            Color.Turquoise, Color.Aquamarine, Color.Khaki, Color.Pink, Color.Orange, Color.Orchid, Color.LightYellow, Color.YellowGreen
        };

        // maps by index to GrLineStyle defined in dumpInterface.cs:
        // Continuous, Dotted, Dashed, Invisible
        private static readonly Style[] lineStyles = { Style.Solid, Style.Dotted, Style.Dashed, Style.Invis };

        // maps by index to GrNodeShape defined in dumpInterface.cs:
        // Box, Triangle, Circle, Ellipse, Rhomb, Hexagon,
        // Trapeze, UpTrapeze, LParallelogram, RParallelogram
        private static readonly Shape[] nodeShapes = { Shape.Box, Shape.Triangle, Shape.Circle, Shape.Ellipse, Shape.Diamond, Shape.Hexagon,
            Shape.Trapezium, Shape.Trapezium, Shape.Parallelogram, Shape.Parallelogram }; // todo: some shapes are not supported, change them to supported but not fitting ones?

        public static Color GetColor(GrColor color)
        {
            if((uint)color >= colors.Length)
                return colors[0];
            else
                return colors[(int)color];
        }

        public static Style GetLineStyle(GrLineStyle style)
        {
            if((uint)style >= lineStyles.Length)
                return lineStyles[0];
            else
                return lineStyles[(int)style];
        }

        public static Shape GetNodeShape(GrNodeShape shape)
        {
            if((uint)shape >= nodeShapes.Length)
                return nodeShapes[0];
            else
                return nodeShapes[(int)shape];
        }

        void ApplyRealizer(Node node, String nodeRealizerName)
        {
            node.Attr.Color = nodeRealizers[nodeRealizerName].borderColor;
            node.Attr.FillColor = nodeRealizers[nodeRealizerName].color;
            node.Attr.Shape = nodeRealizers[nodeRealizerName].nodeShape;
            node.Label.FontColor = nodeRealizers[nodeRealizerName].textColor;
        }

        public void ApplyRealizer(Edge edge, String edgeRealizerName)
        {
            edge.Attr.Color = edgeRealizers[edgeRealizerName].color;
            edge.Attr.LineWidth = edgeRealizers[edgeRealizerName].lineWidth;
            edge.Attr.ClearStyles();
            edge.Attr.AddStyle(edgeRealizers[edgeRealizerName].lineStyle);
            edge.Label.FontColor = edgeRealizers[edgeRealizerName].textColor;
        }

        //--------------------------------------

        public void HideViewer()
        {
            gViewer.Hide();
        }

        public void ShowViewer()
        {
            gViewer.Show();
        }
    }
}
