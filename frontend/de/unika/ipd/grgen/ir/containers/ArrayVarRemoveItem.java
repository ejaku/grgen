/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ir.*;

public class ArrayVarRemoveItem extends EvalStatement {
	Variable target;
	Expression indexExpr;

	public ArrayVarRemoveItem(Variable target, Expression indexExpr) {
		super("array var remove item");
		this.target = target;
		this.indexExpr = indexExpr;
	}

	public Variable getTarget() {
		return target;
	}

	public Expression getIndexExpr() {
		return indexExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(target) && (target.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add(target);

		if(getIndexExpr()!=null)
			getIndexExpr().collectNeededEntities(needs);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
