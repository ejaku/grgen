/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

public class Typeof extends Expression {
	/** The entity whose type we want to know. */
	private final Entity entity;

	public Typeof(Entity entity) {
		super("typeof", entity.getType());
		this.entity = entity;
	}

	public Entity getEntity() {
		return entity;
	}

	public String getNodeLabel() {
		return "typeof<" + entity + ">";
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.add((GraphEntity) entity);
	}
}

