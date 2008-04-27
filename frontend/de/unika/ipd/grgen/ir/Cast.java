/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author G. Veit Batz
 * @version $Id$
 *
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;

public class Cast extends Expression
{
	protected Expression expr;

	public Cast(Type type, Expression expr) {
		super("cast", type);
		this.expr = expr;
	}

	public String getNodeLabel() {
		return "Cast to " + type;
	}

	public Expression getExpression() {
		return expr;
	}

	public Collection<Expression> getWalkableChildren() {
		Vector<Expression> vec = new Vector<Expression>();
		vec.add(expr);
		return vec;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectElementsAndVars(Set<Node> nodes, Set<Edge> edges, Set<Variable> vars) {
		getExpression().collectElementsAndVars(nodes, edges, vars);
	}
}

