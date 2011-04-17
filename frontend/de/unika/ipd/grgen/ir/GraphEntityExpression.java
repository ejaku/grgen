/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

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

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(graphEntity);
	}

	public boolean equals(Object other) {
		if(!(other instanceof GraphEntityExpression)) return false;
		return graphEntity == ((GraphEntityExpression) other).getGraphEntity();
	}

	public int hashCode() {
		return graphEntity.hashCode();
	}
}
