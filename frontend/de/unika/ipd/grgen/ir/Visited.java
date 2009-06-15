/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

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
		needs.add((GraphEntity) entity);
		visitorID.collectNeededEntities(needs);
	}
}
