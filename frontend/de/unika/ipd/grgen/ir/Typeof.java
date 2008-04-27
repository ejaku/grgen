/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


import java.util.Set;

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
	public void collectElementsAndVars(Set<Node> nodes, Set<Edge> edges, Set<Variable> vars) {
		if(entity instanceof Node) {
			if(nodes != null)
				nodes.add((Node)entity);
		}
		else if(entity instanceof Edge) {
			if(edges != null)
				edges.add((Edge)entity);
		}
		else
			throw new UnsupportedOperationException("Unsupported Entity (" + entity + ")");
	}
}

