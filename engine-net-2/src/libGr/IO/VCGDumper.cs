/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

namespace de.unika.ipd.grGen.libGr
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Specifies flags how the graph should be displayed by a graph layouter.
    /// </summary>
    /// <remarks>YComp does not support all flags.</remarks>
    [Flags]
    public enum VCGFlags
    {
        /// <summary>
        /// Orient layout from top to bottom.
        /// </summary>
        OrientTopToBottom,

        /// <summary>
        /// Orient layout from bottom to top.
        /// </summary>
        OrientBottomToTop,

        /// <summary>
        /// Orient layout from left to right.
        /// </summary>
        OrientLeftToRight,

        /// <summary>
        /// Orient layout from right to left.
        /// </summary>
        OrientRightToLeft,

        /// <summary>
        /// Mask of orientation bits.
        /// </summary>
        OrientMask = 0x00000003,

        /// <summary>
        /// Show edge labels.
        /// </summary>
        EdgeLabels = 0x00000004,

        /// <summary>
        /// Enable port sharing.
        /// </summary>
        PortSharing = 0x00000008,

        /// <summary>
        /// Use splines for edge drawing.
        /// </summary>
        Splines = 0x00000010,

        /// <summary>
        /// Use manhattan edges.
        /// </summary>
        ManhattanEdges = 0x00000020,

        /// <summary>
        /// Use smanhattan edges.
        /// </summary>
        SManhattanEdges = 0x00000040,

        /// <summary>
        /// Suppress edge drawing.
        /// </summary>
        SuppressEdges = 0x40000000,

        /// <summary>
        /// Suppress node drawing
        /// </summary>
        SuppressNodes = unchecked((int) 0x80000000)
    }

    /// <summary>
    /// A VCG graph dumper.
    /// </summary>
    public class VCGDumper : IDumper
    {
        static private string[] orientation = { "top_to_bottom", "bottom_to_top", "left_to_right", "right_to_left" };
        static private string[] colors = { "black", "blue", "green", "cyan", "red", "purple", "khaki", "darkgrey",
            "lightgrey", "lightblue", "lightgreen", "lightcyan", "lightred", "lightmagenta", "yellow", "white",
            "darkblue", "darkred", "darkgreen", "darkyellow", "darkmagenta", "darkcyan", "gold", "lilac",
            "turquoise", "aquamarine", "khaki", "pink", "orange", "orchid", "lightyellow", "yellowgreen"
        };
        static private string[] lineStyles = { "continuous", "dotted", "dashed", "invisible" };
        static private string[] nodeShapes = { "box", "triangle", "circle", "ellipse", "rhomb", "hexagon",
            "trapeze", "uptrapeze", "lparallelogram", "rparallelogram" };

        private StreamWriter sw;
        private int indent = 0;

        private void Indent()
        {
            for(int i = 0; i < indent; i++)
                sw.Write(' ');
        }

        private void WriteLine(String format, params object[] arg)
        {
            Indent();
            sw.WriteLine(format, arg);
        }

        private static String EncodeString(String str)
        {
            StringBuilder encStr = new StringBuilder(str);
            encStr.Replace("  ", " &nbsp;");
            encStr.Replace("\"", "&quot;");
            return encStr.ToString();
        }

        /// <summary>
        /// Gets the VCG string representation of a GrColor object.
        /// </summary>
        /// <param name="color">The GrColor object.</param>
        /// <returns>The VCG string representation of <c>color</c>.</returns>
        public static String GetColor(GrColor color)
        {
            if((uint) color >= colors.Length) return colors[0];
            else return colors[(int) color];
        }

        /// <summary>
        /// Gets the VCG string representation of a GrLineStyle object.
        /// </summary>
        /// <param name="style">The GrLineStyle object.</param>
        /// <returns>The VCG string representation of <c>style</c>.</returns>
        public static String GetLineStyle(GrLineStyle style)
        {
            if((uint) style >= lineStyles.Length) return lineStyles[0];
            else return lineStyles[(int) style];
        }

        /// <summary>
        /// Gets the VCG string representation of a GrNodeShape object.
        /// </summary>
        /// <param name="shape">The GrNodeShape object.</param>
        /// <returns>The VCG string representation of <c>shape</c>.</returns>
        public static String GetNodeShape(GrNodeShape shape)
        {
            if((uint) shape >= nodeShapes.Length) return nodeShapes[0];
            else return nodeShapes[(int) shape];
        }

        /// <summary>
        /// Initializes a new instance of VCGDump.
        /// </summary>
        /// <param name="filename">Destination file.</param>
        /// <param name="flags">Flags to control the dumper's behavior.</param>
        /// <param name="layouter">Specifies the yComp layouter to be used.</param>
        /// <exception cref="IOException">Thrown when the destination cannot be created.</exception>
        /// <remarks>Currently (YComp 1.3.9) valid layouters are:
        ///  - "Random"
        ///  - "Hierarchic"
        ///  - "Organic"
        ///  - "Orthogonal"
        ///  - "Circular"
        ///  - "Tree"
        ///  - "Diagonal"
        ///  - "Incremental Hierarchic"
        ///  - "Compilergraph"
        /// </remarks>
        public VCGDumper(String filename, VCGFlags flags, String layouter)
        {
            sw = new StreamWriter(filename, false);

            WriteLine("graph:{{");
            indent++;
            WriteLine("infoname 1: \"Attributes\"");
            WriteLine("display_edge_labels: {0}", (flags & VCGFlags.EdgeLabels) != 0 ? "yes" : "no");
            WriteLine("layoutalgorithm: normal //$ \"{0}\"", layouter);
            WriteLine("port_sharing: {0}", (flags & VCGFlags.PortSharing) != 0 ? "yes" : "no");
            WriteLine("splines: {0}", (flags & VCGFlags.Splines) != 0 ? "yes" : "no");
            WriteLine("manhattan_edges: {0}", (flags & VCGFlags.ManhattanEdges) != 0 ? "yes" : "no");
            WriteLine("smanhattan_edges: {0}", (flags & VCGFlags.SManhattanEdges) != 0 ? "yes" : "no");
            WriteLine("orientation: {0}", orientation[(int)(flags & VCGFlags.OrientMask)]);
            WriteLine("edges: {0}", (flags & VCGFlags.SuppressEdges) != 0 ? "no" : "yes");
            WriteLine("nodes: {0}", (flags & VCGFlags.SuppressNodes) != 0 ? "no" : "yes");

            WriteLine("classname 1: \"normal\"");
            WriteLine("classname 2: \"matches\"");
            WriteLine("classname 3: \"hidden\"");
        }

        /// <summary>
        /// Initializes a new instance of VCGDump with standard flags (VCGFlags.OrientBottomToTop) and the "Orthogonal" layouter.
        /// </summary>
        /// <param name="filename">Destination file.</param>
        /// <exception cref="IOException">Thrown when the destination cannot be created.</exception>
        public VCGDumper(String filename) : this(filename, VCGFlags.OrientBottomToTop, "Orthogonal") { }

        /// <summary>
        /// Dump a node to the VCG graph.
        /// </summary>
        /// <param name="node">The node to be dumped.</param>
        /// <param name="label">The label to use for the node.</param>
        /// <param name="attributes">An enumerable of attribute strings.</param>
        /// <param name="textColor">The color of the text.</param>
        /// <param name="nodeColor">The color of the node border.</param>
        /// <param name="borderColor">The color of the node.</param>
        /// <param name="nodeShape">The shape of the node.</param>
        ///
        /// TODO: Check whether GetHashCode should really be used or better Graph.GetElementName()
        ///
        public void DumpNode(INode node, String label, IEnumerable<String> attributes, GrColor textColor,
            GrColor nodeColor, GrColor borderColor, GrNodeShape nodeShape)
        {
            Indent();
            sw.Write("node:{{title:\"n{0}\"", node.GetHashCode());
            if(label != null) sw.Write(" label:\"{0}\"", label);
            if(textColor != GrColor.Default) sw.Write(" textcolor:" + GetColor(textColor));
            if(nodeColor != GrColor.White) sw.Write(" color:" + GetColor(nodeColor));
            if(borderColor != textColor) sw.Write(" bordercolor:" + GetColor(borderColor));
            if(nodeShape != GrNodeShape.Default) sw.Write(" shape:" + GetNodeShape(nodeShape));
            if(attributes != null)
            {
                sw.Write(" info1: \"");
                bool first = true;
                indent++;
                foreach(String attr in attributes)
                {
                    if(first) first = false;
                    else
                    {
                        sw.WriteLine();
                        Indent();
                    }
                    sw.Write(EncodeString(attr));
                }
                indent--;
                sw.Write('\"');
            }
            sw.WriteLine('}');
        }

        /// <summary>
        /// Dump an edge to the VCG graph
        /// </summary>
        /// <param name="srcNode">The source node of the edge</param>
        /// <param name="tgtNode">The target node of the edge</param>
        /// <param name="label">The label of the edge, may be null</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="edgeColor">The color of the edge</param>
        /// <param name="lineStyle">The linestyle of the edge</param>
        /// <param name="thickness">The thickness of the edge (1-5)</param>
        ///
        /// TODO: Check whether GetHashCode should really be used or better Graph.GetElementName()
        ///
        public void DumpEdge(INode srcNode, INode tgtNode, String label, IEnumerable<String> attributes,
            GrColor textColor, GrColor edgeColor, GrLineStyle lineStyle, int thickness)
        {
            Indent();
            sw.Write("edge:{{sourcename:\"n{0}\" targetname:\"n{1}\"", srcNode.GetHashCode(), tgtNode.GetHashCode());

            String attrStr = "";
            if(attributes != null && attributes.GetEnumerator().MoveNext())
            {
                StringBuilder attrStrBuilder = new StringBuilder("\nAttributes:");
                bool first = true;
//                indent++;
                foreach(String attr in attributes)
                {
                    if(first) first = false;
                    else
                    {
                        attrStrBuilder.Append('\n');
                    }
                    attrStrBuilder.Append(EncodeString(attr));
                }
//                indent--;
//                sw.Write('\"');
                attrStr = attrStrBuilder.ToString();
            }

            if(label != null) sw.Write(" label:\"" + label + attrStr + "\"");
            if(textColor != GrColor.Default) sw.Write(" textcolor:" + GetColor(textColor));
            if(edgeColor != GrColor.Default) sw.Write(" color:" + GetColor(edgeColor));
            if(lineStyle != GrLineStyle.Default) sw.Write(" linestyle:" + GetLineStyle(lineStyle));
            if(thickness != 1) sw.Write(" thickness:" + thickness);
            sw.WriteLine('}');
        }

        /// <summary>
        /// Creates a new sub-graph to the VCG graph
        /// </summary>
        /// <param name="node">The node starting the new sub-graph</param>
        /// <param name="label">The label to use for the node</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="subgraphColor">The color of the subgraph node</param>
        /// TODO: Check whether GetHashCode should really be used or better Graph.GetElementName()
        public void StartSubgraph(INode node, string label, IEnumerable<String> attributes, GrColor textColor,
            GrColor subgraphColor)
        {
            Indent();
            sw.Write("graph:{{title:\"n{0}\"", node.GetHashCode());
            if(label != null) sw.Write(" label:\"{0}\" status:clustered", label);
            if(textColor != GrColor.Default) sw.Write(" textcolor:" + GetColor(textColor));
            if(subgraphColor != textColor) sw.Write(" color:" + GetColor(subgraphColor));
            if(attributes != null)
            {
                sw.Write(" info1: \"");
                bool first = true;
                indent++;
                foreach(String attr in attributes)
                {
                    if(first) first = false;
                    else
                    {
                        sw.WriteLine();
                        Indent();
                    }
                    sw.Write(EncodeString(attr));
                }
                indent--;
                sw.Write('\"');
            }
            sw.WriteLine();
            indent++;
        }

        /// <summary>
        /// Finishes a subgraph
        /// </summary>
        public void FinishSubgraph()
        {
            indent--;
            WriteLine("}}");
        }

        /// <summary>
        /// Finishes the dump and closes the file
        /// </summary>
        public void FinishDump()
        {
            sw.WriteLine('}');
            sw.Close();
            sw = null;
        }

        /// <summary>
        /// Disposes this object. If <see cref="FinishDump"/> has not been called yet, it is called.
        /// This allows using "using" with the dumper object.
        /// </summary>
        public void Dispose()
        {
            if (sw != null) FinishDump();
        }
    }
}
