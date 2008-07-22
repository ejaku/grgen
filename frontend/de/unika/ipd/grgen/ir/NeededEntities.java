/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

package de.unika.ipd.grgen.ir;

import java.util.LinkedHashSet;
import java.util.Set;

/**
 * Holds a collection of entities needed by an expression.
 */
public class NeededEntities {
	/**
	 * Instantiates a new NeededEntities object.
	 * @param collectNodes Specifies, whether needed nodes shall be collected.
	 * @param collectEdges Specifies, whether needed edges shall be collected.
	 * @param collectVars Specifies, whether needed variables shall be collected.
	 * @param collectAllEntities Specifies, whether all needed entities (nodes, edges, vars) shall be collected.
	 */
	public NeededEntities(boolean collectNodes, boolean collectEdges, boolean collectVars, boolean collectAllEntities) {
		if(collectNodes)       nodes     = new LinkedHashSet<Node>();
		if(collectEdges)       edges     = new LinkedHashSet<Edge>();
		if(collectVars)        variables = new LinkedHashSet<Variable>();
		if(collectAllEntities) entities  = new LinkedHashSet<Entity>();
	}

	/**
	 * Specifies whether the graph is needed.
	 */
	public boolean isGraphUsed;

	/**
	 * The nodes needed.
	 */
	public Set<Node> nodes;

	/**
	 * The edges needed.
	 */
	public Set<Edge> edges;

	/**
	 * The variables needed.
	 */
	public Set<Variable> variables;

	/**
	 * The entities needed (nodes, edges, and variables).
	 */
	public Set<Entity> entities;

	/**
	 * Adds a needed node.
	 * @param node The needed node.
	 */
	public void add(Node node) {
		if(nodes != null) nodes.add(node);
		if(entities != null) entities.add(node);
	}

	/**
	 * Adds a needed edge.
	 * @param edge The needed edge.
	 */
	public void add(Edge edge) {
		if(edges != null) edges.add(edge);
		if(entities != null) entities.add(edge);
	}

	/**
	 * Adds a needed variable.
	 * @param var The needed variable.
	 */
	public void add(Variable var) {
		if(variables != null) variables.add(var);
		if(entities != null) entities.add(var);
	}

	public void needsGraph() {
		isGraphUsed = true;
	}
}
