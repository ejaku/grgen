/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author G. Veit Batz
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.*;

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
