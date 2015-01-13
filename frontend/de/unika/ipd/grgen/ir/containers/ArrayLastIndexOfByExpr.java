/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayLastIndexOfByExpr extends Expression {
	private Expression targetExpr;
	private Entity member;
	private Expression valueExpr;
	private Expression startIndexExpr;

	public ArrayLastIndexOfByExpr(Expression targetExpr, Entity member, Expression valueExpr) {
		super("array lastIndexOfBy expr", IntType.getType());
		this.targetExpr = targetExpr;
		this.member = member;
		this.valueExpr = valueExpr;
	}

	public ArrayLastIndexOfByExpr(Expression targetExpr, Entity member, Expression valueExpr, Expression startIndexExpr) {
		super("array indexOfBy expr", IntType.getType());
		this.targetExpr = targetExpr;
		this.member = member;
		this.valueExpr = valueExpr;
		this.startIndexExpr = startIndexExpr;
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

	public Expression getStartIndexExpr() {
		return startIndexExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
		valueExpr.collectNeededEntities(needs);
		if(startIndexExpr != null)
			startIndexExpr.collectNeededEntities(needs);
	}
}
