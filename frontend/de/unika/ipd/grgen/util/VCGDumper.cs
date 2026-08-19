using System.Collections.Generic;

/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.util
{

	/// <summary>
	/// A VCG Graph dumper
	/// </summary>
	public class VCGDumper : GraphDumper
	{
		/// <summary>
		/// where to put the graph to </summary>
		private PrintStream ps;

		/// <summary>
		/// Index in the vcg colormap for user defined colors </summary>
		private int currSetColor;

		/// <summary>
		/// Prefix for the nodes. </summary>
		private static string prefix = "n";

		private static Dictionary<Color, string> colorMap;
		private static Dictionary<Shape, string> shapeMap;
		private static Dictionary<Style, string> lineStyleMap;

		static VCGDumper()
		{
			colorMap = new Dictionary<Color, string>();
			shapeMap = new Dictionary<Shape, string>();
			lineStyleMap = new Dictionary<Style, string>();

			colorMap[Color.BLACK] = "black";
			colorMap[Color.BLUE] = "lightblue";
			colorMap[Color.CYAN] = "cyan";
			colorMap[Color.GRAY] = "lightgrey";
			colorMap[Color.DARK_GRAY] = "darkgrey";
			colorMap[Color.MAGENTA] = "magenta";
			colorMap[Color.ORANGE] = "orange";
			colorMap[Color.GREEN] = "green";
			colorMap[Color.RED] = "red";
			colorMap[Color.PINK] = "pink";
			colorMap[Color.YELLOW] = "yellow";
			colorMap[Color.WHITE] = "white";

			shapeMap[Shape.BOX] = "box";
			shapeMap[Shape.RHOMB] = "rhomb";
			shapeMap[Shape.ELLIPSE] = "ellipse";
			shapeMap[Shape.TRIANGLE] = "triangle";

			lineStyleMap[Style.SOLID] = "continuous";
			lineStyleMap[Style.DASHED] = "dashed";
			lineStyleMap[Style.DOTTED] = "dotted";
		}

		/// <summary>
		/// Make a string usable for output.
		/// This escapes anything that has to be escaped. </summary>
		/// <param name="s"> The input string. </param>
		/// <returns> A string ready for dumping. </returns>
		private static string EscapeString(string s)
		{
			return s.ReplaceAll("\"", "\\\\\"");
		}

		/// <summary>
		/// Make a new VCG dumper. </summary>
		/// <param name="ps"> The print stream to dump the graph to. </param>
		public VCGDumper(PrintStream ps)
		{
			this.ps = ps;
			this.currSetColor = 32;
		}

		/// <summary>
		/// Dump graph preamble.
		/// </summary>
		public virtual void Begin()
		{
			ps.Println("graph:{\nlate_edge_labels:yes\ndisplay_edge_labels:yes\n"
					+ "manhattan_edges:yes\nport_sharing:no\n");
		}

		/// <summary>
		/// Dump epilog. </summary>
		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumper.finish()"/>
		public virtual void Finish()
		{
			ps.Println("}");
			ps.Flush();
			ps.Close();
		}

		/// <summary>
		/// Get a VCG color for a Java color. </summary>
		/// <param name="col"> The Java color. </param>
		/// <returns> The VCG color. </returns>
		private string GetColor(Color col)
		{
			string res;

			if(colorMap.ContainsKey(col))
				res = colorMap[col];
			else if(currSetColor < 256)
			{
				/*
				// Get the current index and increment it
				int index = currSetColor++;

				// Convert it to a string and put in the color map
				res = index.ToString();
				colorMap[col] = res;

				// issue a vcg colormap statement
				ps.Println("colorentry " + index + ": " +
						col.GetRed() + " " + col.GetGreen() + " " + col.GetBlue());
				*/
				throw new System.NotImplementedException(); // removed during the porting to C#, re-implement when needed (with System.Drawing.Color) - but using de.unika.ipd.grGen.libGr.GrColor should be sufficient
			}
			else
				res = "white";

			return res;
		}

		private static string Prefix
		{
			get
			{
				return prefix;
			}
		}

		/// <summary>
		/// Make a VCG string from the node's attributes. </summary>
		/// <param name="d"> The node to dump. </param>
		/// <returns> VCG statements describing the node. </returns>
		private string GetNodeAttributes(GraphDumpable d)
		{
			string col = GetColor(d.NodeColor);
			Shape shp = d.NodeShape;

			string info = d.NodeInfo;
			if(!string.ReferenceEquals(info, null))
				info = EscapeString(info);

			string label = EscapeString(d.NodeLabel);

			string s = "title:\"" + Prefix + d.NodeId
					+ "\" label:\"" + label + "\"";

			if(!string.ReferenceEquals(info, null))
				s += " info1:\"" + info + "\"";
			s += " color:" + col;
			if(shapeMap.ContainsKey(shp))
				s += " shape:" + shapeMap[shp];

			return s;
		}

		public virtual void Node(GraphDumpable d)
		{
			ps.Println("node:{" + GetNodeAttributes(d) + "}");
		}

		public virtual void Edge(GraphDumpable from, GraphDumpable to, string label,
				Style style, Color color)
		{
			if(from != null && to != null)
			{
				string col = GetColor(color);

				string s = "edge:{sourcename:\"" + Prefix + from.NodeId
						+ "\" targetname:\"" + Prefix + to.NodeId + "\"";

				if(!string.ReferenceEquals(label, null))
					s += " label:\"" + EscapeString(label) + "\"";

				s += " color:" + col;

				if(style != Style.DEFAULT)
					s += " linestyle:" + lineStyleMap[style];

				s += "}";

				ps.Println(s);
			}
		}

		public virtual void Edge(GraphDumpable from, GraphDumpable to, string label, Style style)
		{
			Edge(from, to, label, style, Color.BLACK);
		}

		public virtual void Edge(GraphDumpable from, GraphDumpable to, string label)
		{
			Edge(from, to, label, Style.DEFAULT, Color.BLACK);
		}

		public virtual void Edge(GraphDumpable from, GraphDumpable to)
		{
			Edge(from, to, null, Style.DEFAULT, Color.BLACK);
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumper.beginSubgraph(java.lang.String)"/>
		public virtual void BeginSubgraph(GraphDumpable d)
		{
			ps.Println("graph:{" + GetNodeAttributes(d)
					+ " status:clustered");
		}

		public virtual void BeginSubgraph(string title)
		{
			ps.Print("graph:{title:\"");
			ps.Print(title);
			ps.Println('\"');
			ps.Print("  label:\"");
			ps.Print(title);
			ps.Println('\"');
			ps.Println("  status:clustered");
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumper.endSubgraph()"/>
		public virtual void EndSubgraph()
		{
			ps.Println("}\n");
		}
	}

}
