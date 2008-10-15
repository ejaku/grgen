/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

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

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.addAttr((GraphEntity) owner, member);
	}
}

