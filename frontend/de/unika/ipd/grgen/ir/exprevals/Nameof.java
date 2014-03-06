/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

public class Nameof extends Expression {
	/** The entity whose name we want to know. */
	private final Expression namedEntity;

	public Nameof(Expression entity, Type type) {
		super("nameof", type);
		this.namedEntity = entity;
	}

	public Expression getNamedEntity() {
		return namedEntity;
	}

	/** @see de.unika.ipd.grgen.ir.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();

		if(namedEntity!=null)
			namedEntity.collectNeededEntities(needs);
	}
}

