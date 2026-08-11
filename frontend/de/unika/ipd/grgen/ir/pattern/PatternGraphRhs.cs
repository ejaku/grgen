/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{

using System.Collections.Generic;

using Entity = de.unika.ipd.grgen.ir.Entity;
using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/// <summary>
/// A pattern graph rhs is a graph pattern as it occurs on the right hand side of rules.
/// It extends the pattern graph base class, additionally offering explicitly deleted information, 
/// ordered replacements (eval statements), rhs imperative statements, and further things.
/// </summary>
public class PatternGraphRhs : PatternGraphBase
{
	/// <summary>
	/// A set of the graph elements clearly deleted (in contrast to not mentioned ones) 
	/// This means explicitly deleted, or for edges deleted because their source/target node is explicitly deleted
	/// </summary>
	private readonly HashSet<GraphEntity> deletedElements = new HashSet<GraphEntity>();

	/// <summary>
	/// A list of the replacement parameters </summary>
	private readonly List<Entity> replParams = new List<Entity>();

	private List<OrderedReplacements> orderedReplacements = new List<OrderedReplacements>();

	private List<ImperativeStmt> imperativeStmts = new List<ImperativeStmt>();

	/// <summary>
	/// Make a new pattern graph. </summary>
	public PatternGraphRhs(string nameOfGraph)
		: base(nameOfGraph)
	{
	}

	/// <summary>
	/// Make a new pattern graph with preset nodes, edges, subpatternUsages (copy from another pattern graph). </summary>
	public PatternGraphRhs(string nameOfGraph,
			IDictionary<Node, PatternGraphBase.GraphNode> nodes,
			IDictionary<Edge, PatternGraphBase.GraphEdge> edges,
			ISet<SubpatternUsage> subpatternUsages)
		: base(nameOfGraph, nodes, edges, subpatternUsages)
	{
	}

	public virtual void AddDeletedElement(GraphEntity entity)
	{
		deletedElements.Add(entity);
	}

	public virtual HashSet<GraphEntity> DeletedElements
	{
		get
		{
		return deletedElements;
		}
	}

	/// <summary>
	/// Add a replacement parameter to the rule. </summary>
	public virtual void AddReplParameter(Entity entity)
	{
		replParams.Add(entity);
	}

	/// <summary>
	/// Get all replacement parameters of this rule (may currently contain only nodes). </summary>
	public virtual IList<Entity> ReplParameters
	{
		get
		{
		return replParams.AsReadOnly();
		}
	}

	public virtual bool ReplParametersContain(Entity entity)
	{
		return replParams.Contains(entity);
	}

	/// <summary>
	/// Get a read-only collection containing all ordered replacements
	/// (subpattern dependent replacement, emit here) in this graph. </summary>
	/// <returns> A collection containing all ordered replacements in this graph.
	/// Note: The collection is read-only and may not be modified. </returns>
	public virtual ICollection<OrderedReplacements> OrderedReplacements
	{
		get
		{
		return orderedReplacements.AsReadOnly();
		}
	}

	/// <summary>
	/// Add a ordered replacement (subpattern dependent replacement, emit here) to the pattern graph </summary>
	public virtual void AddOrderedReplacement(OrderedReplacements orderedRepl)
	{
		orderedReplacements.Add(orderedRepl);
	}

	public virtual void AddImperativeStmt(ImperativeStmt emit)
	{
		imperativeStmts.Add(emit);
	}

	public virtual ICollection<ImperativeStmt> ImperativeStmts
	{
		get
		{
		return imperativeStmts.AsReadOnly();
		}
	}
}

}
