/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class Visited extends Expression {
	private Expression visitorID;
	private Entity entity;

	public Visited(Expression visitorID, Entity entity) {
		super("visited", BooleanType.getType());
		this.visitorID = visitorID;
		this.entity = entity;
	}

	public Expression getVisitorID() {
		return visitorID;
	}

	public Entity getEntity() {
		return entity;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(!isGlobalVariable(entity))
			needs.add((GraphEntity) entity);
		visitorID.collectNeededEntities(needs);
	}
}
