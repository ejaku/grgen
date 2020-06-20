/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

/**
 * Adapter wrapping an lhs pattern graph, yielding an rhs pattern graph.
 */
public class PatternGraphRhsFromLhs extends PatternGraphRhs
{
	PatternGraphLhs patternGraph; // wrapped and adapted lhs pattern graph
	
	/** Make a new pattern graph. */
	public PatternGraphRhsFromLhs(PatternGraphLhs patternGraph)
	{
		super(patternGraph.getNameOfGraph(),
				patternGraph.nodes, patternGraph.edges, patternGraph.subpatternUsages);
		this.patternGraph = patternGraph;
	}

	@Override
	public void addDeletedElement(GraphEntity entity)
	{
		throw new RuntimeException("not implemented");
	}

	@Override
	public HashSet<GraphEntity> getDeletedElements()
	{
		return new HashSet<GraphEntity>();
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
		return false;
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
}
