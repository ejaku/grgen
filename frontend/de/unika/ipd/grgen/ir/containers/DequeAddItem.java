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

import java.util.HashSet;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.ast.BaseNode;

public class DequeAddItem extends EvalStatement {
	Qualification target;
    Expression valueExpr;
    Expression indexExpr;

	public DequeAddItem(Qualification target, Expression valueExpr, Expression indexExpr) {
		super("deque add item");
		this.target = target;
		this.valueExpr = valueExpr;
		this.indexExpr = indexExpr;
	}

	public Qualification getTarget() {
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
		Entity entity = target.getOwner();
		if(!isGlobalVariable(entity) && (entity.getContext()&BaseNode.CONTEXT_COMPUTATION)!=BaseNode.CONTEXT_COMPUTATION)
			needs.add((GraphEntity) entity);

		// Temporarily do not collect variables for target
		HashSet<Variable> varSet = needs.variables;
		needs.variables = null;
		target.collectNeededEntities(needs);
		needs.variables = varSet;

		getValueExpr().collectNeededEntities(needs);

		if(getIndexExpr()!=null)
			getIndexExpr().collectNeededEntities(needs);

		if(getNext()!=null) {
			getNext().collectNeededEntities(needs);
		}
	}
}
