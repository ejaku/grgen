/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * A pattern graph rhs is a graph pattern as it occurs on the right hand side of rules.
 * It extends the graph base class, additionally offering explicitly deleted information, 
 * ordered replacements (eval statements), rhs imperative statements, and further things.
 */
public class PatternGraphRhs extends PatternGraphBase
{
	/** A set of the graph elements clearly deleted (in contrast to not mentioned ones) 
	 * This means explicitly deleted, or for edges deleted because their source/target node is explicitly deleted*/
	private final HashSet<GraphEntity> deletedElements = new HashSet<GraphEntity>();

	/** A list of the replacement parameters */
	private final List<Entity> replParams = new LinkedList<Entity>();

	private List<OrderedReplacements> orderedReplacements = new LinkedList<OrderedReplacements>();

	private List<ImperativeStmt> imperativeStmts = new ArrayList<ImperativeStmt>();

	/** Make a new pattern graph. */
	public PatternGraphRhs(String nameOfGraph)
	{
		super(nameOfGraph);
	}

	/** Make a new graph with preset nodes, edges, subpatternUsages (copy from another pattern graph). */
	public PatternGraphRhs(String nameOfGraph,
			Map<Node, PatternGraphBase.GraphNode> nodes,
			Map<Edge, PatternGraphBase.GraphEdge> edges,
			Set<SubpatternUsage> subpatternUsages)
	{
		super(nameOfGraph, nodes, edges, subpatternUsages);
	}

	public void addDeletedElement(GraphEntity entity)
	{
		deletedElements.add(entity);
	}

	public HashSet<GraphEntity> getDeletedElements()
	{
		return deletedElements;
	}

	/** Add a replacement parameter to the rule. */
	public void addReplParameter(Entity entity)
	{
		replParams.add(entity);
	}

	/** Get all replacement parameters of this rule (may currently contain only nodes). */
	public List<Entity> getReplParameters()
	{
		return Collections.unmodifiableList(replParams);
	}

	public boolean replParametersContain(Entity entity)
	{
		return replParams.contains(entity);
	}

	/**
	 * Get a read-only collection containing all ordered replacements
	 * (subpattern dependent replacement, emit here) in this graph.
	 * @return A collection containing all ordered replacements in this graph.
	 * Note: The collection is read-only and may not be modified.
	 */
	public Collection<OrderedReplacements> getOrderedReplacements()
	{
		return Collections.unmodifiableCollection(orderedReplacements);
	}

	/** Add a ordered replacement (subpattern dependent replacement, emit here) to the graph */
	public void addOrderedReplacement(OrderedReplacements orderedRepl)
	{
		orderedReplacements.add(orderedRepl);
	}

	public void addImperativeStmt(ImperativeStmt emit)
	{
		imperativeStmts.add(emit);
	}

	public List<ImperativeStmt> getImperativeStmts()
	{
		return imperativeStmts;
	}
}
