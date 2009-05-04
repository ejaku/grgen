/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id: Typeof.java 19827 2008-05-29 21:55:01Z moritz $
 */
package de.unika.ipd.grgen.ir;

public class Nameof extends Expression {
	/** The entity whose name we want to know. */
	private final Entity entity;

	public Nameof(Entity entity, Type type) {
		super("nameof", type);
		this.entity = entity;
	}

	public Entity getEntity() {
		return entity;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		
		if(entity==null)
			return;
		else
			needs.add((GraphEntity) entity);
	}
}

