/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Set;

/**
 * A graph entity expression node.
 */
public class GraphEntityExpression extends Expression {
	private GraphEntity graphEntity;

	public GraphEntityExpression(GraphEntity graphEntity) {
		super("graph entity", graphEntity.getType());
		this.graphEntity = graphEntity;
	}

	/** Returns the graph entity of this graph entity expression. */
	public GraphEntity getGraphEntity() {
		return graphEntity;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectElementsAndVars(Set<Node> nodes, Set<Edge> edges, Set<Variable> vars) {
		if(nodes != null && graphEntity instanceof Node)
			nodes.add((Node) graphEntity);
		else if(edges != null && graphEntity instanceof Edge)
			edges.add((Edge) graphEntity);
	}
}
