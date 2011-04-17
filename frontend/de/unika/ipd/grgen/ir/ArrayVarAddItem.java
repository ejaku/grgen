/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

public class ArrayVarAddItem extends EvalStatement {
	Variable target;
    Expression valueExpr;
    Expression indexExpr;

	public ArrayVarAddItem(Variable target, Expression valueExpr, Expression indexExpr) {
		super("array var add item");
		this.target = target;
		this.valueExpr = valueExpr;
		this.indexExpr = indexExpr;
	}

	public Variable getTarget() {
		return target;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public Expression getIndexExpr() {
		return indexExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(target);

		getValueExpr().collectNeededEntities(needs);

		if(getIndexExpr()!=null)
			getIndexExpr().collectNeededEntities(needs);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
