/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


import java.util.Set;

public class Qualification extends Expression {
	/** The owner of the expression. */
	private final Entity owner;

	/** The member of the qualification. */
	private final Entity member;

	public Qualification(Entity owner, Entity member) {
		super("qual", member.getType());
		this.owner = owner;
		this.member = member;
	}

	public Entity getOwner() {
		return owner;
	}

	public Entity getMember() {
		return member;
	}

	public String getNodeLabel() {
		return "<" + owner + ">.<" + member + ">";
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectElementsAndVars(Set<Node> nodes, Set<Edge> edges, Set<Variable> vars) {
		if(owner instanceof Node) {
			if(nodes != null)
				nodes.add((Node)owner);
		}
		else if(owner instanceof Edge) {
			if(edges != null)
				edges.add((Edge)owner);
		}
		else
			throw new UnsupportedOperationException("Unsupported Entity (" + owner + ")");
	}
}

