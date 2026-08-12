/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.util
{

	// originally color definitions from java.awt.Color (including RGB values)
	public enum Color
	{
		BLACK, BLUE, CYAN, GRAY, DARK_GRAY, MAGENTA, ORANGE, GREEN, RED, PINK, YELLOW, WHITE
	}

	public enum Shape
	{
		DEFAULT = -1, BOX, RHOMB, ELLIPSE, TRIANGLE
	}

	public enum Style
	{
		DEFAULT = -1, SOLID, DASHED, DOTTED
	}

	/// <summary>
	/// A Dumper for Graphs
	/// </summary>
	public interface GraphDumper
	{
		void Begin();

		void Finish();

		void BeginSubgraph(GraphDumpable d);

		void BeginSubgraph(string name);

		void EndSubgraph();

		void Node(GraphDumpable d);

		void Edge(GraphDumpable from, GraphDumpable to, string label, Style style, Color color);

		void Edge(GraphDumpable from, GraphDumpable to, string label, Style style);

		void Edge(GraphDumpable from, GraphDumpable to, string label);

		void Edge(GraphDumpable from, GraphDumpable to);
	}

}
