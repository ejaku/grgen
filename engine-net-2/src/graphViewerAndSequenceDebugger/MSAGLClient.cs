/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
        
        private static Dictionary<String, bool> availableLayouts;


        static MSAGLClient()
        {
            availableLayouts = new Dictionary<string, bool>();
            availableLayouts.Add("SugiyamaScheme", true); // similar to Hierarchic
            availableLayouts.Add("MDS", true);
            availableLayouts.Add("Ranking", true);
            availableLayouts.Add("IcrementalLayout", true); // roughly similar to Organic
        }

        /// <summary>
        /// Creates a new MSAGLClient instance, adding its GViewer control to the form.
        /// </summary>
        public MSAGLClient(System.Windows.Forms.Form form)
        {
            gViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer(); // if your application doesn't start from this line complaining about System.Resources.Extensions, you have to add a PackageReference to the project file of your app, plus a bindingRedirect to the app.config of your app, see the GrShell project for an example
            Graph graph = new Microsoft.Msagl.Drawing.Graph("graph");
            gViewer.Graph = graph;
            form.SuspendLayout();
            gViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(gViewer);
            form.ResumeLayout();
            form.Show();
            System.Windows.Forms.Application.DoEvents();
        }

        public void Close()
        {
        }


        public static IEnumerable<String> AvailableLayouts
        {
            get { return availableLayouts.Keys; }
        }

        public static bool IsValidLayout(String layoutName)     // TODO: allow case insensitive layout name
        {
            return availableLayouts.ContainsKey(layoutName);
        }

        public event ConnectionLostHandler OnConnectionLost
        {
            add { ; }
            remove { ; }
        }

        public bool CommandAvailable
        {
            get { return false; }
        }

        public bool ConnectionLost
        {
            get { return false; }
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
        //       - Ranking,
        //       - IcrementalLayout
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
            gViewer.Graph = gViewer.Graph;
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
        /// Sends a "sync" request and waits for a "sync" answer
        /// </summary>
        public bool Sync()
        {
            return true; // no tcp/ip communication in between causing the viewer and the execution thread to get out of synch, so nothing to be done
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
            gViewer.Graph.RemoveNode(gViewer.Graph.FindNode(nodeName));
        }

        public void DeleteEdge(String edgeName)
        {
            // TODO: Update group relation
            gViewer.Graph.RemoveEdge(gViewer.Graph.EdgeById(edgeName));
            nameToEdge.Remove(edgeName);
        }

        public void RenameNode(String oldName, String newName)
        {
            // deleted nodes are shown as zombie_oldName, rename for retyping is not needed anymore, the name is kept
            Node node = gViewer.Graph.FindNode(oldName);
            node.Attr.Id = newName;
        }

        public void RenameEdge(String oldName, String newName)
        {
            // deleted edges are shown as zombie_oldName, rename for retyping is not needed anymore, the name is kept
            Edge edge;
            if(nameToEdge.TryGetValue(oldName, out edge))
            {
                nameToEdge.Remove(oldName);
                nameToEdge.Add(newName, edge);
                edge.Attr.Id = newName;
            }
        }

        public void ClearGraph()
        {
            foreach(Node node in gViewer.Graph.Nodes)
            {
                gViewer.Graph.RemoveNode(node);
            }
            nameToEdge.Clear();
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
            Shape.Trapezium, Shape.Trapezium, Shape.Parallelogram, Shape.Parallelogram };

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
    }
}
