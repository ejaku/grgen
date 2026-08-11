/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

using System.Collections.Generic;
using System.Diagnostics;

using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using GraphDumpVisitor = de.unika.ipd.grgen.util.GraphDumpVisitor;
using GraphDumpable = de.unika.ipd.grgen.util.GraphDumpable;
using GraphDumpableProxy = de.unika.ipd.grgen.util.GraphDumpableProxy;
using GraphDumper = de.unika.ipd.grgen.util.GraphDumper;
using Walkable = de.unika.ipd.grgen.util.Walkable; // does not make sense, Walkable references AST-children, unusable in IR

/// <summary>
/// A IR pretty graph dumper.
/// </summary>
public class DumpVisitor : GraphDumpVisitor
{
	private class PrefixNode : GraphDumpableProxy
	{
		private readonly DumpVisitor outerInstance;

		internal string prefix;

		public PrefixNode(DumpVisitor outerInstance, GraphDumpable dumpable, string prefix)
			: base(dumpable)
		{
			this.outerInstance = outerInstance;
			this.prefix = prefix;
		}

		/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeId()"/>
		public override string NodeId
		{
			get
			{
				return prefix + GraphDumpable.GetNodeId();
			}
		}

		public override string ToString()
		{
			return NodeId;
		}
	}

	private void DumpGraph(PatternGraphBase patternGraph, string prefix)
	{
		IDictionary<Entity, DumpVisitor.PrefixNode> prefixMap = new Dictionary<Entity, DumpVisitor.PrefixNode>();
		ICollection<Node> nodes = patternGraph.Nodes;

		dumper.BeginSubgraph(patternGraph);

		foreach(Node node in nodes)
		{
			debug.Report(NOTE, "node: " + node);
			PrefixNode prefixNode = new PrefixNode(this, node, prefix);
			prefixMap[node] = prefixNode;
			dumper.Node(prefixNode);
		}

		ICollection<Edge> edges = patternGraph.Edges;

		foreach(Edge edge in edges)
		{
			PrefixNode prefixFrom, prefixTo, prefixEdge;

			prefixEdge = new PrefixNode(this, edge, prefix);
			prefixMap[edge] = prefixEdge;

			debug.Report(NOTE, "true edge from: " + patternGraph.GetSource(edge)
					+ " to: " + patternGraph.GetTarget(edge));

			prefixFrom = prefixMap[patternGraph.GetSource(edge)];
			prefixTo = prefixMap[patternGraph.GetTarget(edge)];

			debug.Report(NOTE, "edge from: " + prefixFrom + " to: " + prefixTo);

			dumper.Node(prefixEdge);
			dumper.Edge(prefixFrom, prefixEdge);
			dumper.Edge(prefixEdge, prefixTo);
		}

		if(patternGraph is PatternGraphLhs)
		{
			PatternGraphLhs patternGraphLhs = (PatternGraphLhs)patternGraph;

			foreach(ICollection<GraphEntity> homSet in patternGraphLhs.Homomorphic)
			{
				if(homSet.Count > 0)
				{
					foreach(Entity hom1 in homSet)
					{
						foreach(Entity hom2 in homSet)
						{
							PrefixNode prefixFrom = prefixMap[hom1];
							PrefixNode prefixTo = prefixMap[hom2];
							dumper.Edge(prefixFrom, prefixTo, "hom", GraphDumper.DASHED);
						}
					}
				}
			}
		}

		dumper.EndSubgraph();
	}

	/// <seealso cref="de.unika.ipd.grgen.util.Visitor.visit(de.unika.ipd.grgen.util.Walkable)"/>
	public override void Visit(Walkable walkable)
	{
		Debug.Assert(walkable is IR, "must have an ir object to dump");

		if(walkable is Node || walkable is Edge || walkable is PatternGraphBase)
			return;

		if(walkable is Rule && ((Rule)walkable).Right != null)
		{
			Rule rule = (Rule)walkable;
			dumper.BeginSubgraph(rule);
			if(rule.Right == null)
			{
				DumpGraph(rule.Pattern, "");
				dumper.EndSubgraph();
			}
			DumpGraph(rule.Left, "l");
			DumpGraph(rule.Right, "r");

			// Draw edges from left nodes that occur also on the right side.
			foreach(Node node in rule.CommonNodes)
			{
				PrefixNode prefixLeft = new PrefixNode(this, node, "l");
				PrefixNode prefixRight = new PrefixNode(this, node, "r");

				dumper.Edge(prefixLeft, prefixRight, null, GraphDumper.DOTTED);
			}

			foreach(Edge edge in rule.CommonEdges)
			{
				PrefixNode prefixLeft = new PrefixNode(this, edge, "l");
				PrefixNode prefixRight = new PrefixNode(this, edge, "r");

				dumper.Edge(prefixLeft, prefixRight, null, GraphDumper.DOTTED);
			}

			// dump evalations
			//dumper.beginSubgraph(r);
			//dumper.endSubgraph();

			dumper.EndSubgraph();
		}
		else
			base.Visit(walkable);
	}
}

}
