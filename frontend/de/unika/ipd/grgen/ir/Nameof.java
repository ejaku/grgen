/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		if(entity==null) {
			return;
		}
		else if(entity instanceof Node) {
			needs.add((Node)entity);
		}
		else if(entity instanceof Edge) {
			needs.add((Edge)entity);
		}
		else
			throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
	}
}

