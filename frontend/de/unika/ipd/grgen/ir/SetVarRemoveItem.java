/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: SetVarRemoveItem.java 22945 2008-10-16 16:02:13Z moritz $
 */

package de.unika.ipd.grgen.ir;

public class SetVarRemoveItem extends EvalStatement {
	Variable target;
	Expression valueExpr;

	public SetVarRemoveItem(Variable target, Expression valueExpr) {
		super("set var remove item");
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
