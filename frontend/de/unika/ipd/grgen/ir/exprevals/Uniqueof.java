/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class Uniqueof extends Expression {
	/** The entity whose unique id we want to know. */
	private final Expression entity;

	public Uniqueof(Expression entity, Type type) {
		super("uniqueof", type);
		this.entity = entity;
	}

	public Expression getEntity() {
		return entity;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		if(entity==null || entity.getType() instanceof GraphType)
			needs.needsGraph();
		if(entity!=null)
			entity.collectNeededEntities(needs);
	}
}

