/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.containers;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.exprevals.*;

public class ArrayExtract extends Expression {
	private Expression targetExpr;
	private Entity member;
	
	public ArrayExtract(Expression targetExpr, ArrayType resultingType, Entity member) {
		super("array extract", resultingType);
		this.targetExpr = targetExpr;
		this.member = member;
	}

	public Expression getTargetExpr() {
		return targetExpr;
	}

	public Entity getMember() {
		return member;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		targetExpr.collectNeededEntities(needs);
	}
}
