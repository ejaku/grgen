/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.IncidenceCountIndex;

public class IndexedIncidenceCountIndexAccessExpr extends Expression {
	IncidenceCountIndex target;
	Expression keyExpr;

	public IndexedIncidenceCountIndexAccessExpr(IncidenceCountIndex target, Expression keyExpr) {
		super("indexed incidence count index access expression", IntType.getType());
		this.target = target;
		this.keyExpr = keyExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		keyExpr.collectNeededEntities(needs);
	}

	public IncidenceCountIndex getTarget() {
		return target;
	}

	public Expression getKeyExpr() {
		return keyExpr;
	}
}
