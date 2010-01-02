/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * A member expression node.
 */
public class MemberExpression extends Expression {
	private Entity member;

	public MemberExpression(Entity member) {
		super("member", member.getType());
		this.member = member;
	}

	/** Returns the member entity of this member expression. */
	public Entity getMember() {
		return member;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
	}
}
