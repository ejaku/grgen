/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayIndexOfOrderedByExpr extends Expression {
	private Expression targetExpr;
	private Entity member;
	private Expression valueExpr;

	public ArrayIndexOfOrderedByExpr(Expression targetExpr, Entity member, Expression valueExpr) {
		super("array indexOfOrderedBy expr", IntType.getType());
		this.targetExpr = targetExpr;
		this.member = member;
		this.valueExpr = valueExpr;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Entity getMember() {
		return member;
	}

	public Expression getValueExpr() {
		return valueExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
	}
}
