/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

// by Edgar Jakumeit

namespace de.unika.ipd.grGen.libGr
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// A VCG graph dumper.
    /// </summary>
    public class DOTDumper : IDumper
    {
        // maps by index to GrColor defined in dumpInterface.cs:
        // Black, Blue, Green, Cyan, Red, Purple, Brown, Grey,
        // LightGrey, LightBlue, LightGreen, LightCyan, LightRed, LightPurple, Yellow, White,
        // DarkBlue, DarkRed, DarkGreen, DarkYellow, DarkMagenta, DarkCyan, Gold, Lilac,
        // Turquoise, Aquamarine, Khaki, Pink, Orange, Orchid, LightYellow, YellowGreen
        private static readonly string[] colors = { "black", "blue", "green", "cyan", "red", "purple", "brown", "dimgray",
            "gray", "lightblue", "lightseagreen", "lightcyan", "lightsalmon", "palevioletred", "yellow", "white",
            "darkslateblue", "indianred", "darkgreen", "darkgoldenrod", "darkorchid", "darkturquoise", "gold", "violet",
            "mediumturquoise", "aquamarine", "khaki", "pink", "orange", "orchid", "lightyellow", "yellowgreen"
        };
        private static readonly string[] orientation = { "TB", "BT", "LR", "RL" };
        // maps by index to GrLineStyle defined in dumpInterface.cs:
        // Continuous, Dotted, Dashed, Invisible
        private static readonly string[] lineStyles = { "solid", "dotted", "dashed", "invis" };
        // maps by index to GrNodeShape defined in dumpInterface.cs:
        // Box, Triangle, Circle, Ellipse, Rhomb, Hexagon, 
        // Trapeze, UpTrapeze, LParallelogram, RParallelogram
        private static readonly string[] nodeShapes = { "box", "triangle", "circle", "ellipse", "diamond", "hexagon",
            "trapezium", "invtrapezium", "parallelogram", "parallelogram" };

        private readonly Dictionary<INode, INode> groupNodesToCharacteristicContainedNode;
        private readonly Stack<INode> groupNesting;

        private readonly StreamWriter sw;
        private int indent = 0;

        private void Indent()
        {
            ++indent;
        }

        private void Unindent()
        {
            --indent;
        }

        private void WriteIndentation()
        {
            for(int i = 0; i < indent; ++i)
            {
                sw.Write(' ');
            }
        }

        private void WriteLine(String format, params object[] arg)
        {
            WriteIndentation();
            sw.WriteLine(format, arg);
        }

        private static String EncodeString(String str)
        {
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("\"", "\\\"");
            return sb.ToString();
        }

        /// <summary>
        /// Gets the DOT string representation of a GrColor object.
        /// </summary>
        public static String GetColor(GrColor color)
        {
            if((uint) color >= colors.Length)
                return colors[0];
            else
                return colors[(int) color];
        }

        /// <summary>
        /// Gets the DOT string representation of a GrLineStyle object.
        /// </summary>
        public static String GetLineStyle(GrLineStyle style)
        {
            if((uint) style >= lineStyles.Length)
                return lineStyles[0];
            else
                return lineStyles[(int) style];
        }

        /// <summary>
        /// Gets the DOT string representation of a GrNodeShape object.
        /// </summary>
        public static String GetNodeShape(GrNodeShape shape)
        {
            if((uint) shape >= nodeShapes.Length)
                return nodeShapes[0];
            else
                return nodeShapes[(int) shape];
        }

        /// <summary>
        /// Initializes a new instance of the DOTDumper.
        /// </summary>
        /// <param name="filename">Destination file.</param>
        /// <param name="graphname">Name of the host graph.</param>
        /// <param name="flags">Flags to control the dumper's behavior.</param>
        /// <exception cref="IOException">Thrown when the destination cannot be created.</exception>
        /// TODO: replace VCGFlags by generic GrFlags (to be introduced and mapped to VCG and DOT)
        public DOTDumper(String filename, String graphname, VCGFlags flags)
        {
            groupNodesToCharacteristicContainedNode = new Dictionary<INode, INode>();
            groupNesting = new Stack<INode>();

            sw = new StreamWriter(filename, false);

            WriteLine("digraph {0} {{", graphname);
            Indent();
            WriteLine("splines={0};", (flags & VCGFlags.Splines) != 0 ? "true" : "false");
            WriteLine("rankdir={0};", orientation[(int)(flags & VCGFlags.OrientMask)]);
            WriteLine("compound=true;");

            // suggested layouters mapping in case the layout would not be specified by the "dot" or "neato" viewer parameter:
            //  - "Organic", "Random" -> neato
            //  - "Circular" -> circo
            //  - "Hierarchic", "Orthogonal", "Tree", "Diagonal", "Incremental Hierarchic", "Compilergraph" -> dot
        }

        /// <summary>
        /// Initializes a new instance of the DOTDumper with standard flags (VCGFlags.OrientBottomToTop).
        /// </summary>
        /// <param name="filename">Destination file.</param>
        /// <param name="graphname">Name of the host graph.</param>
        /// <exception cref="IOException">Thrown when the destination cannot be created.</exception>
        public DOTDumper(String filename, String graphname) : this(filename, graphname, VCGFlags.OrientBottomToTop)
        {
        }

        /// <summary>
        /// Dump a node to the DOT language graph.
        /// </summary>
        /// <param name="node">The node to be dumped.</param>
        /// <param name="label">The label to use for the node.</param>
        /// <param name="attributes">An enumerable of attribute strings.</param>
        /// <param name="textColor">The color of the text.</param>
        /// <param name="nodeColor">The color of the node.</param>
        /// <param name="borderColor">The color of the node border.</param>
        /// <param name="nodeShape">The shape of the node.</param>
        /// TODO: Check whether GetHashCode should really be used or better Graph.GetElementName()
        public void DumpNode(INode node, String label, IEnumerable<String> attributes, GrColor textColor,
            GrColor nodeColor, GrColor borderColor, GrNodeShape nodeShape)
        {
            if(groupNesting.Count > 0)
            {
                // edges coming from or going to subgraph nodes require plain nodes as source or target in the DOT format,
                // so if a characteristic node of a group is enlarged itself to a subgraph, we have to choose another - hopefully plain node - as contained characteristic node
                // otherwise we stick to the first node picked as characteristic node (first node added after group opening)
                if(groupNodesToCharacteristicContainedNode[groupNesting.Peek()] == null
                    || groupNodesToCharacteristicContainedNode.ContainsKey(groupNodesToCharacteristicContainedNode[groupNesting.Peek()]))
                {
                    groupNodesToCharacteristicContainedNode[groupNesting.Peek()] = node;
                }
            }

            WriteIndentation();
            sw.Write("n{0} [", node.GetHashCode());

            if(label != null)
                sw.Write(" label=\"{0}\"", label);
            if(textColor != GrColor.Default)
                sw.Write(" fontcolor=" + GetColor(textColor));
            if(nodeColor != GrColor.Default)
                sw.Write(" fillcolor=" + GetColor(nodeColor) + " style=filled");
            if(borderColor != textColor)
                sw.Write(" color=" + GetColor(borderColor));
            if(nodeShape != GrNodeShape.Default)
                sw.Write(" shape=" + GetNodeShape(nodeShape));
            if(attributes != null)
            {
                sw.Write(" tooltip=\"");
                bool first = true;
                Indent();
                foreach(String attr in attributes)
                {
                    if(first)
                        first = false;
                    else
                    {
                        sw.WriteLine();
                        WriteIndentation();
                    }
                    sw.Write(EncodeString(attr));
                }
                Unindent();
                sw.Write('\"');
            }

            sw.WriteLine(']');
        }

        /// <summary>
        /// Dump an edge to the DOT language graph
        /// </summary>
        /// <param name="srcNode">The source node of the edge</param>
        /// <param name="tgtNode">The target node of the edge</param>
        /// <param name="label">The label of the edge, may be null</param>
        /// <param name="attributes">An enumerable of attribute strings</param>
        /// <param name="textColor">The color of the text</param>
        /// <param name="edgeColor">The color of the edge</param>
        /// <param name="lineStyle">The linestyle of the edge</param>
        /// <param name="thickness">The thickness of the edge (1-5)</param>
        /// TODO: Check whether GetHashCode should really be used or better Graph.GetElementName()
        public void DumpEdge(INode srcNode, INode tgtNode, String label, IEnumerable<String> attributes,
            GrColor textColor, GrColor edgeColor, GrLineStyle lineStyle, int thickness)
        {
            INode srcNodeOrCharNodeFromGroupIfGroupNode = srcNode;
            if(groupNodesToCharacteristicContainedNode.ContainsKey(srcNode))
                srcNodeOrCharNodeFromGroupIfGroupNode = groupNodesToCharacteristicContainedNode[srcNode];
            INode tgtNodeOrNodeFromGroupIfGroupNode = tgtNode;
            if(groupNodesToCharacteristicContainedNode.ContainsKey(tgtNode))
                tgtNodeOrNodeFromGroupIfGroupNode = groupNodesToCharacteristicContainedNode[tgtNode];

            WriteIndentation();
            sw.Write("n{0} -> n{1} [", srcNodeOrCharNodeFromGroupIfGroupNode.GetHashCode(), tgtNodeOrNodeFromGroupIfGroupNode.GetHashCode());

            String attrStr = "";
            if(attributes != null && attributes.GetEnumerator().MoveNext())
            {
                StringBuilder attrStrBuilder = new StringBuilder("Attributes:");
                bool first = true;
                foreach(String attr in attributes)
                {
                    if(first)
                        first = false;
                    else
                    {
                        attrStrBuilder.Append('\n');
                    }
                    attrStrBuilder.Append(EncodeString(attr));
                }
                attrStr = attrStrBuilder.ToString();
            }

            if(srcNodeOrCharNodeFromGroupIfGroupNode != srcNode)
                sw.Write(" ltail=cluster" + srcNode.GetHashCode());
            if(tgtNodeOrNodeFromGroupIfGroupNode != tgtNode)
                sw.Write(" lhead=cluster" + tgtNode.GetHashCode());

            if(label != null)
                sw.Write(" label=\"" + label + "\"");
            if(attrStr != "")
                sw.Write(" tooltip=\"" + attrStr + "\"");
            if(textColor != GrColor.Default)
                sw.Write(" fontcolor=" + GetColor(textColor));
            if(edgeColor != GrColor.Default)
                sw.Write(" color=" + GetColor(edgeColor));
            if(lineStyle != GrLineStyle.Default)
                sw.Write(" style=" + GetLineStyle(lineStyle));
            if(thickness != 1)
                sw.Write(" thickness=" + thickness + ".0");

            sw.WriteLine(']');
        }

        /// <summary>
        /// Creates a new sub-graph to the DOT graph
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
            groupNodesToCharacteristicContainedNode.Add(node, null);
            groupNesting.Push(node);

            WriteIndentation();
            sw.Write("subgraph cluster{0} {{", node.GetHashCode());

            if(label != null)
                sw.Write(" label=\"{0}\";", label);
            if(textColor != GrColor.Default)
                sw.Write(" fontcolor=" + GetColor(textColor) + ";");
            if(subgraphColor != textColor)
                sw.Write(" fillcolor=" + GetColor(subgraphColor) + "; style=filled;");
            if(attributes != null)
            {
                sw.Write(" tooltip=\"");
                bool first = true;
                Indent();
                foreach(String attr in attributes)
                {
                    if(first)
                        first = false;
                    else
                    {
                        sw.WriteLine();
                        WriteIndentation();
                    }
                    sw.Write(EncodeString(attr));
                }
                Unindent();
                sw.Write('\"');
            }
            sw.WriteLine(";");
            Indent();
        }

        /// <summary>
        /// Finishes a subgraph
        /// </summary>
        public void FinishSubgraph()
        {
            Unindent();
            WriteLine("}}");

            // it seems dot does not support edges between empty subgraphs - better than crashing while inserting such is to render empty subgraph nodes as plain nodes
            if(groupNodesToCharacteristicContainedNode[groupNesting.Peek()] == null)
                groupNodesToCharacteristicContainedNode.Remove(groupNesting.Peek());
            else
            {
                // the characteristic node of the group is a group node itself -> pick the characteristic node of the contained group
                // because we need plain nodes for edges in between subgraph nodes (here we obtain them transitively)
                if(groupNodesToCharacteristicContainedNode.ContainsKey(groupNodesToCharacteristicContainedNode[groupNesting.Peek()]))
                    groupNodesToCharacteristicContainedNode[groupNesting.Peek()] = groupNodesToCharacteristicContainedNode[groupNodesToCharacteristicContainedNode[groupNesting.Peek()]];
            }

            groupNesting.Pop();
        }

        /// <summary>
        /// Finishes the dump and closes the file
        /// </summary>
        public void FinishDump()
        {
            sw.WriteLine('}');
            sw.Close();
        }

        /// <summary>
        /// Disposes this object. If <see cref="FinishDump"/> has not been called yet, it is called.
        /// This allows using "using" with the dumper object.
        /// </summary>
        public void Dispose()
        {
            if(sw != null)
                FinishDump();
        }
    }
}
