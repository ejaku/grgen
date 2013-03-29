/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

public class Visited extends Expression {
	private Expression visitorID;
	private Expression entity;

	public Visited(Expression visitorID, Expression entity) {
		super("visited", BooleanType.getType());
		this.visitorID = visitorID;
		this.entity = entity;
	}

	public Expression getVisitorID() {
		return visitorID;
	}

	public Expression getEntity() {
		return entity;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		entity.collectNeededEntities(needs);
		visitorID.collectNeededEntities(needs);
	}
}
