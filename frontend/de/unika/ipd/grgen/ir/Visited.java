/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
		super("visited", visitorID.getType());
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
		if(entity instanceof Node)
			needs.add((Node) entity);
		else if(entity instanceof Edge)
			needs.add((Edge) entity);
		else
			throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
		
		visitorID.collectNeededEntities(needs);
	}
}
