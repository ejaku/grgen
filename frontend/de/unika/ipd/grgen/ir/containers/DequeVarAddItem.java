/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.5
 * Copyright (C) 2003-2012 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.*;

public class DequeVarAddItem extends EvalStatement {
	Variable target;
    Expression valueExpr;

	public DequeVarAddItem(Variable target, Expression valueExpr) {
		super("deque var add item");
		this.target = target;
		this.valueExpr = valueExpr;
	}

	public Variable getTarget() {
		return target;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(target);

		getValueExpr().collectNeededEntities(needs);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
