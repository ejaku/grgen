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
import java.util.LinkedHashSet;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * A pattern graph is a graph pattern as it occurs on left and right hand sides of rules.
 * It includes nested alternative-case and iterated rules, as well as nested patterns (negative and independent).
 * It extends the graph base class, additionally offering variables, 
 * conditions that restrict the set of possible matches, 
 * lhs yield statements, rhs imperative statements, and further things.
 */
public class PatternGraphRhs extends PatternGraphBase
{
	private final Collection<Variable> vars = new LinkedHashSet<Variable>();

	/** A list of all yield assignments. */
	private final List<EvalStatements> yields = new LinkedList<EvalStatements>();

	/** A set of nodes which will be matched homomorphically to any other node in the pattern.
	 *  they appear if they're not referenced within the pattern, but some nested component uses them */
	private final HashSet<Node> homToAllNodes = new HashSet<Node>();

	/** A set of edges which will be matched homomorphically to any other edge in the pattern.
	 *  they appear if they're not referenced within the pattern, but some nested component uses them  */
	private final HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	/** A set of the graph elements clearly deleted (in contrast to not mentioned ones) 
	 * This means explicitly deleted, or for edges deleted because their source/target node is explicitly deleted*/
	private final HashSet<GraphEntity> deletedElements = new HashSet<GraphEntity>();

	private List<ImperativeStmt> imperativeStmts = new ArrayList<ImperativeStmt>();

	/** A list of the replacement parameters */
	private final List<Entity> replParams = new LinkedList<Entity>();

	/** Make a new pattern graph. */
	public PatternGraphRhs(String nameOfGraph)
	{
		super(nameOfGraph);
	}

	public void addImperativeStmt(ImperativeStmt emit)
	{
		imperativeStmts.add(emit);
	}

	public List<ImperativeStmt> getImperativeStmts()
	{
		return imperativeStmts;
	}

	public void addVariable(Variable var)
	{
		vars.add(var);
	}

	public Collection<Variable> getVars()
	{
		return Collections.unmodifiableCollection(vars);
	}

	public boolean hasVar(Variable var)
	{
		return vars.contains(var);
	}

	/** Add an assignment to the list of evaluations. */
	public void addYield(EvalStatements stmts)
	{
		yields.add(stmts);
	}

	public void addNodeIfNotYetContained(Node node)
	{
		if(hasNode(node))
			return;
		
		addSingleNode(node);
		addHomToAll(node);
	}

	public void addEdgeIfNotYetContained(Edge edge)
	{
		if(hasEdge(edge))
			return;
		
		addSingleEdge(edge);
		addHomToAll(edge);
	}

	public void addHomToAll(Node node)
	{
		homToAllNodes.add(node);
	}

	public void addHomToAll(Edge edge)
	{
		homToAllEdges.add(edge);
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

	/** @return A collection containing all yield assignments of this graph. */
	public Collection<EvalStatements> getYields()
	{
		return Collections.unmodifiableCollection(yields);
	}
}
