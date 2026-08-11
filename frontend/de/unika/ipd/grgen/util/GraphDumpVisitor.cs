/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.util
{
/// <summary>
/// A visitor that dumps graphs
/// Every object that is visited must implement Walkable and GraphDumpable </summary>
/// <seealso cref="GraphDumpable"/>
/// <seealso cref="Walkable"/>
public class GraphDumpVisitor : Base, Visitor
{
	protected internal GraphDumper dumper;

	public GraphDumpVisitor(GraphDumper dumper)
	{
		this.dumper = dumper;
	}

	public GraphDumpVisitor()
	{
	}

	public virtual GraphDumper Dumper
	{
		set
		{
			this.dumper = value;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.Visitor.visit(de.unika.ipd.grgen.ast.BaseNode)"/>
	public virtual void Visit(Walkable n)
	{
		GraphDumpable gd = (GraphDumpable)n;
		dumper.Node(gd);

		int i = 0;
		foreach(GraphDumpable target in n.WalkableChildren)
		{
			dumper.Edge(gd, target, gd.GetEdgeLabel(i));
			i++;
		}
	}
}

}
