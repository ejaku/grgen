/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.decl.executable
{
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using RhsDeclNode = de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;

/// <summary>
/// Base class for top level pattern matching related ast nodes
/// </summary>
public abstract class TopLevelMatcherDeclNode : MatcherDeclNode
{
	public TopLevelMatcherDeclNode(IdentNode id, TypeNode type, PatternGraphLhsNode left)
		: base(id, type, left)
	{
	}

	protected internal virtual bool NoAbstractElementInstantiated(RhsDeclNode right)
	{
		bool abstr = true;

		foreach(NodeDeclNode node in right.patternGraph.Nodes)
		{
			if(!node.InheritsType() && node.DeclInhType.IsAbstract() && !pattern.Nodes.Contains(node)
					&& (node.context & CONTEXT_PARAMETER) != CONTEXT_PARAMETER)
			{
				node.ReportError("Instances of abstract node classes are not allowed (node" + node.EmptyWhenAnonymousPostfix(" ")
						+ " is declared with the abstract type " + node.DeclType.ToStringWithDeclarationCoords() + ").");
				abstr = false;
			}
		}
		foreach(EdgeDeclNode edge in right.patternGraph.Edges)
		{
			if(!edge.InheritsType() && edge.DeclInhType.IsAbstract() && !pattern.Edges.Contains(edge)
					&& (edge.context & CONTEXT_PARAMETER) != CONTEXT_PARAMETER)
			{
				edge.ReportError("Instances of abstract edge classes are not allowed (edge" + edge.EmptyWhenAnonymousPostfix(" ")
						+ " is declared with the abstract type " + edge.DeclType.ToStringWithDeclarationCoords() + ").");
				abstr = false;
			}
		}

		return abstr;
	}
}

}
