/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class MatchAccess extends Expression {
	Expression expression;
	Node node;
	Edge edge;
	Variable var;
	
	public MatchAccess(Expression expression, Node node) {
		super("match access", node.getNodeType());
		this.expression = expression;
		this.node = node;
	}

	public MatchAccess(Expression expression, Edge edge) {
		super("match access", edge.getEdgeType());
		this.expression = expression;
		this.edge = edge;
	}

	public MatchAccess(Expression expression, Variable var) {
		super("match access", var.getType());
		this.expression = expression;
		this.var = var;
	}
	
	public Expression getExpr() {
		return expression;
	}

	public Entity getEntity() {
		if(node!=null)
			return node;
		else if(edge!=null)
			return edge;
		else
			return var;
	}
	
	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
	}
}

