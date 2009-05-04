/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author G. Veit Batz
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
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

	public void collectNeededEntities(NeededEntities needs) {
		getExpression().collectNeededEntities(needs);
	}
}
