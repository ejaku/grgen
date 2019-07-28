/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class Qualification extends Expression {
	/** The owner of the qualification. */
	private final Entity owner;

	/** The owner of the casted qualification. */
	private final Expression ownerExpr;

	/** The member of the qualification. */
	private final Entity member;

	public Qualification(Entity owner, Entity member) {
		super("qual", member.getType());
		this.owner = owner;
		this.ownerExpr = null;
		this.member = member;
	}

	public Qualification(Expression ownerExpr, Entity member) {
		super("qual", member.getType());
		this.owner = null;
		this.ownerExpr = ownerExpr;
		this.member = member;
	}

	public Entity getOwner() {
		return owner;
	}

	public Expression getOwnerExpr() {
		return ownerExpr;
	}

	public Entity getMember() {
		return member;
	}

	public String getNodeLabel() {
		return "<" + owner + ">.<" + member + ">";
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(owner != null) {
			if(!isGlobalVariable(owner) && !(owner.getType() instanceof MatchType))
			{
				if(owner instanceof GraphEntity)
					needs.addAttr((GraphEntity) owner, member);
				else
					needs.add((Variable)owner);
			}
		} else {
			ownerExpr.collectNeededEntities(needs);
		}
	}
}

