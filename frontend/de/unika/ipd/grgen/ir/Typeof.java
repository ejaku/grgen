/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectNeededEntities(NeededEntities needs) {
		if(entity instanceof Node) {
			needs.add((Node)entity);
		}
		else if(entity instanceof Edge) {
			needs.add((Edge)entity);
		}
		else
			throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
	}
}

