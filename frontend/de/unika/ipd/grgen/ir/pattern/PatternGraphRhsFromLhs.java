/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.Set;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * Adapter wrapping an lhs pattern graph TODO: base class handling by wrapping, comments
 * A pattern graph is a graph pattern as it occurs on left and right hand sides of rules.
 * It includes nested alternative-case and iterated rules, as well as nested patterns (negative and independent).
 * It extends the graph base class, additionally offering variables, 
 * conditions that restrict the set of possible matches, 
 * lhs yield statements, rhs imperative statements, and further things.
 */
public class PatternGraphRhsFromLhs extends PatternGraphRhs
{
	PatternGraphLhs patternGraph; // wrapped lhs pattern graph
	
	/** Make a new pattern graph. */
	public PatternGraphRhsFromLhs(PatternGraphLhs patternGraph)
	{
		super(patternGraph.getNameOfGraph());
		this.patternGraph = patternGraph;
		init(patternGraph.nodes, patternGraph.edges,
				patternGraph.subpatternUsages, patternGraph.orderedReplacements);
	}

	@Override
	public void addImperativeStmt(ImperativeStmt emit)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public List<ImperativeStmt> getImperativeStmts()
	{
		return new ArrayList<ImperativeStmt>();
	}

	@Override
	public void addVariable(Variable var)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public Collection<Variable> getVars()
	{
		return patternGraph.getVars();
	}

	@Override
	public boolean hasVar(Variable var)
	{
		return patternGraph.hasVar(var);
	}

	/** Add an assignment to the list of evaluations. */
	@Override
	public void addYield(EvalStatements stmts)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public void addNodeIfNotYetContained(Node node)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public void addEdgeIfNotYetContained(Edge edge)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public void addHomToAll(Node node)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public void addHomToAll(Edge edge)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public void addDeletedElement(GraphEntity entity)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public HashSet<GraphEntity> getDeletedElements()
	{
		throw new RuntimeException("not implemented");
	}

	/** Add a replacement parameter to the rule. */
	@Override
	public void addReplParameter(Entity entity)
	{
		throw new RuntimeException("not implemented");
	}

	/** Get all replacement parameters of this rule (may currently contain only nodes). */
	@Override
	public List<Entity> getReplParameters()
	{
		return new LinkedList<Entity>();
	}

	@Override
	public boolean replParametersContain(Entity entity)
	{
		throw new RuntimeException("not implemented");
	}

	/** @return A collection containing all yield assignments of this graph. */
	@Override
	public Collection<EvalStatements> getYields()
	{
		throw new RuntimeException("not implemented");
	}
}
