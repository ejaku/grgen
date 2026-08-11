/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.util
{

/// <summary>
/// A Dumper for Graphs
/// </summary>
public interface GraphDumper
{

	public static int DEFAULT = -1;

	public static int BOX = 0;
	public static int RHOMB = 1;
	public static int ELLIPSE = 2;
	public static int TRIANGLE = 3;

	public static int SOLID = 0;
	public static int DASHED = 1;
	public static int DOTTED = 2;

	void Begin();

	void Finish();

	void BeginSubgraph(GraphDumpable d);

	void BeginSubgraph(string name);

	void EndSubgraph();

	void Node(GraphDumpable d);

	void Edge(GraphDumpable from, GraphDumpable to, string label, int style, Color color);

	void Edge(GraphDumpable from, GraphDumpable to, string label, int style);

	void Edge(GraphDumpable from, GraphDumpable to, string label);

	void Edge(GraphDumpable from, GraphDumpable to);
}

}
