/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

using System;
using System.Collections.Generic;
using de.unika.ipd.grGen.libGr;

namespace de.unika.ipd.grGen.grShell
{
    /// <summary>
    /// Defines the appearace of a node class (e.g. normal, matched, new, deleted)
    /// </summary>
    class NodeRealizer : IEquatable<NodeRealizer>
    {
        public String Name;
        public GrColor Color;
        public GrColor BorderColor;
        public GrColor TextColor;
        public GrNodeShape Shape;

        public NodeRealizer(String name, GrColor color, GrColor borderColor, GrColor textColor, GrNodeShape shape)
        {
            Name = name;
            Color = color;
            BorderColor = borderColor;
            TextColor = textColor;
            Shape = shape;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() ^ BorderColor.GetHashCode() ^ Shape.GetHashCode() ^ TextColor.GetHashCode();
        }

        public bool Equals(NodeRealizer other)
        {
            return Color == other.Color && BorderColor == other.BorderColor && Shape == other.Shape && TextColor == other.TextColor;
        }
    }

    /// <summary>
    /// Defines the appearace of an edge class (e.g. normal, matched, new, deleted)
    /// </summary>
    class EdgeRealizer : IEquatable<EdgeRealizer>
    {
        public String Name;
        public GrColor Color;
        public GrColor TextColor;
        public GrLineStyle LineStyle;
        public int LineWidth;

        public EdgeRealizer(String name, GrColor color, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            Name = name;
            Color = color;
            TextColor = textColor;
            LineWidth = lineWidth;
            LineStyle = lineStyle;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode() ^ TextColor.GetHashCode() ^ LineStyle.GetHashCode() ^ LineWidth.GetHashCode();
        }

        public bool Equals(EdgeRealizer other)
        {
            return Color == other.Color && TextColor == other.TextColor && LineStyle == other.LineStyle && LineWidth == other.LineWidth;
        }
    }

    public enum ElementMode
    {
        Normal = 0, Matched = 1, Created = 2, Deleted = 3, Retyped = 4
    }

    /// <summary>
    /// Helper class for managing the node and edge realizers
    /// </summary>
    public class ElementRealizers
    {
        // the ids of the realizers registered to yComp for display during debugging
        public String NormalNodeRealizer { get { return nodeRealizers[(int)ElementMode.Normal].Name; } }
        public String MatchedNodeRealizer { get { return nodeRealizers[(int)ElementMode.Matched].Name; } }
        public String NewNodeRealizer { get { return nodeRealizers[(int)ElementMode.Created].Name; } }
        public String DeletedNodeRealizer { get { return nodeRealizers[(int)ElementMode.Deleted].Name; } }
        public String RetypedNodeRealizer { get { return nodeRealizers[(int)ElementMode.Retyped].Name; } }

        public String NormalEdgeRealizer { get { return edgeRealizers[(int)ElementMode.Normal].Name; } }
        public String MatchedEdgeRealizer { get { return edgeRealizers[(int)ElementMode.Matched].Name; } }
        public String NewEdgeRealizer { get { return edgeRealizers[(int)ElementMode.Created].Name; } }
        public String DeletedEdgeRealizer { get { return edgeRealizers[(int)ElementMode.Deleted].Name; } }
        public String RetypedEdgeRealizer { get { return edgeRealizers[(int)ElementMode.Retyped].Name; } }

        // the realizers registered to yComp for display during debugging
        NodeRealizer[] nodeRealizers = new NodeRealizer[5];
        EdgeRealizer[] edgeRealizers = new EdgeRealizer[5];

        // set with all the realizers registered to yComp
        Dictionary<NodeRealizer, NodeRealizer> registeredNodeRealizers = new Dictionary<NodeRealizer, NodeRealizer>();
        Dictionary<EdgeRealizer, EdgeRealizer> registeredEdgeRealizers = new Dictionary<EdgeRealizer, EdgeRealizer>();

        // next id to use when registering an unknown realizer to yComp
        int nextNodeRealizerID = 0;
        int nextEdgeRealizerID = 0;

        // the stream to communicate with yComp, null while no debug underway 
        // (but we exist to remember debug node/edge mode command)
        YCompStream ycompStream = null;


        internal ElementRealizers()
        {
            ReSetElementRealizers();
        }

        internal void ReSetElementRealizers()
        {
            nodeRealizers[(int)ElementMode.Normal] = GetNodeRealizer(GrColor.Yellow, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            nodeRealizers[(int)ElementMode.Matched] = GetNodeRealizer(GrColor.Khaki, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            nodeRealizers[(int)ElementMode.Created] = GetNodeRealizer(GrColor.YellowGreen, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            nodeRealizers[(int)ElementMode.Deleted] = GetNodeRealizer(GrColor.LightGrey, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);
            nodeRealizers[(int)ElementMode.Retyped] = GetNodeRealizer(GrColor.Aquamarine, GrColor.DarkYellow, GrColor.Black, GrNodeShape.Box);

            edgeRealizers[(int)ElementMode.Normal] = GetEdgeRealizer(GrColor.DarkYellow, GrColor.Black, 1, GrLineStyle.Continuous);
            edgeRealizers[(int)ElementMode.Matched] = GetEdgeRealizer(GrColor.Khaki, GrColor.Black, 3, GrLineStyle.Continuous);
            edgeRealizers[(int)ElementMode.Created] = GetEdgeRealizer(GrColor.YellowGreen, GrColor.Black, 3, GrLineStyle.Continuous);
            edgeRealizers[(int)ElementMode.Deleted] = GetEdgeRealizer(GrColor.LightGrey, GrColor.Black, 3, GrLineStyle.Continuous);
            edgeRealizers[(int)ElementMode.Retyped] = GetEdgeRealizer(GrColor.Aquamarine, GrColor.Black, 3, GrLineStyle.Continuous);
        }

        public void RegisterYComp(YCompClient ycompClient)
        {
            if(this.ycompStream != null)
                throw new Exception("there is already a ycomp stream registered");

            this.ycompStream = ycompClient.ycompStream;

            foreach(NodeRealizer nr in registeredNodeRealizers.Keys)
            {
                ycompStream.Write("addNodeRealizer \"" + nr.Name + "\" \""
                                    + VCGDumper.GetColor(nr.BorderColor) + "\" \""
                                    + VCGDumper.GetColor(nr.Color) + "\" \""
                                    + VCGDumper.GetColor(nr.TextColor) + "\" \""
                                    + VCGDumper.GetNodeShape(nr.Shape) + "\"\n");
            }
            foreach(EdgeRealizer er in registeredEdgeRealizers.Keys)
            {
                ycompStream.Write("addEdgeRealizer \"" + er.Name + "\" \""
                                    + VCGDumper.GetColor(er.Color) + "\" \""
                                    + VCGDumper.GetColor(er.TextColor) + "\" \""
                                    + er.LineWidth + "\" \"" 
                                    + VCGDumper.GetLineStyle(er.LineStyle) + "\"\n");
            }
        }

        public void UnregisterYComp()
        {
            ycompStream = null;
        }

        internal String GetNodeRealizer(NodeType type, DumpInfo dumpInfo)
        {
            return GetNodeRealizer(dumpInfo.GetNodeTypeColor(type),
                dumpInfo.GetNodeTypeBorderColor(type),
                dumpInfo.GetNodeTypeTextColor(type),
                dumpInfo.GetNodeTypeShape(type)).Name;
        }

        internal String GetEdgeRealizer(EdgeType type, DumpInfo dumpInfo)
        {
            return GetEdgeRealizer(dumpInfo.GetEdgeTypeColor(type),
                dumpInfo.GetEdgeTypeTextColor(type),
                dumpInfo.GetEdgeTypeThickness(type),
                dumpInfo.GetEdgeTypeLineStyle(type)).Name;
        }

        internal void ChangeNodeColor(ElementMode mode, GrColor color)
        {
            nodeRealizers[(int)mode] = GetNodeRealizer(
                color,
                nodeRealizers[(int)mode].BorderColor,
                nodeRealizers[(int)mode].TextColor,
                nodeRealizers[(int)mode].Shape);
        }

        internal void ChangeNodeBorderColor(ElementMode mode, GrColor borderColor)
        {
            nodeRealizers[(int)mode] = GetNodeRealizer(
                nodeRealizers[(int)mode].Color,
                borderColor,
                nodeRealizers[(int)mode].TextColor,
                nodeRealizers[(int)mode].Shape);
        }

        internal void ChangeNodeTextColor(ElementMode mode, GrColor textColor)
        {
            nodeRealizers[(int)mode] = GetNodeRealizer(
                nodeRealizers[(int)mode].Color,
                nodeRealizers[(int)mode].BorderColor,
                textColor,
                nodeRealizers[(int)mode].Shape);
        }

        internal void ChangeNodeShape(ElementMode mode, GrNodeShape shape)
        {
            nodeRealizers[(int)mode] = GetNodeRealizer(
                nodeRealizers[(int)mode].Color,
                nodeRealizers[(int)mode].BorderColor,
                nodeRealizers[(int)mode].TextColor,
                shape);
        }

        internal void ChangeEdgeColor(ElementMode mode, GrColor color)
        {
            edgeRealizers[(int)mode] = GetEdgeRealizer(
                color,
                edgeRealizers[(int)mode].TextColor,
                edgeRealizers[(int)mode].LineWidth,
                edgeRealizers[(int)mode].LineStyle);
        }

        internal void ChangeEdgeTextColor(ElementMode mode, GrColor textColor)
        {
            edgeRealizers[(int)mode] = GetEdgeRealizer(
                edgeRealizers[(int)mode].Color,
                textColor,
                edgeRealizers[(int)mode].LineWidth,
                edgeRealizers[(int)mode].LineStyle);
        }

        internal void ChangeEdgeThickness(ElementMode mode, int thickness)
        {
            edgeRealizers[(int)mode] = GetEdgeRealizer(
                edgeRealizers[(int)mode].Color,
                edgeRealizers[(int)mode].TextColor,
                thickness,
                edgeRealizers[(int)mode].LineStyle);
        }

        internal void ChangeEdgeStyle(ElementMode mode, GrLineStyle style)
        {
            edgeRealizers[(int)mode] = GetEdgeRealizer(
                edgeRealizers[(int)mode].Color,
                edgeRealizers[(int)mode].TextColor,
                edgeRealizers[(int)mode].LineWidth,
                style);
        }

        private NodeRealizer GetNodeRealizer(GrColor nodeColor, GrColor borderColor, GrColor textColor, GrNodeShape shape)
        {
            NodeRealizer newNr = new NodeRealizer("nr" + nextNodeRealizerID, nodeColor, borderColor, textColor, shape);

            NodeRealizer nr;
            if(!registeredNodeRealizers.TryGetValue(newNr, out nr))
            {
                if(ycompStream != null)
                {
                    ycompStream.Write("addNodeRealizer \"" + newNr.Name + "\" \""
                        + VCGDumper.GetColor(borderColor) + "\" \""
                        + VCGDumper.GetColor(nodeColor) + "\" \""
                        + VCGDumper.GetColor(textColor) + "\" \""
                        + VCGDumper.GetNodeShape(shape) + "\"\n");
                }
                registeredNodeRealizers.Add(newNr, newNr);
                nextNodeRealizerID++;
                nr = newNr;
            }
            return nr;
        }

        private EdgeRealizer GetEdgeRealizer(GrColor edgeColor, GrColor textColor, int lineWidth, GrLineStyle lineStyle)
        {
            EdgeRealizer newEr = new EdgeRealizer("er" + nextEdgeRealizerID, edgeColor, textColor, lineWidth, lineStyle);

            EdgeRealizer er;
            if(!registeredEdgeRealizers.TryGetValue(newEr, out er))
            {
                if(ycompStream != null)
                {
                    ycompStream.Write("addEdgeRealizer \"" + newEr.Name + "\" \""
                        + VCGDumper.GetColor(newEr.Color) + "\" \""
                        + VCGDumper.GetColor(newEr.TextColor) + "\" \""
                        + lineWidth + "\" \""
                        + VCGDumper.GetLineStyle(newEr.LineStyle) + "\"\n");
                }
                registeredEdgeRealizers.Add(newEr, newEr);
                nextEdgeRealizerID++;
                er = newEr;
            }
            return er;
        }
    }
}
