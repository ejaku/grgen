/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.IncidenceIndex;

public class IndexedIncidenceIndexAccessExpr extends Expression {
	IncidenceIndex target;
	Expression keyExpr;

	public IndexedIncidenceIndexAccessExpr(IncidenceIndex target, Expression keyExpr) {
		super("indexed incidence index access expression", IntType.getType());
		this.target = target;
		this.keyExpr = keyExpr;
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		keyExpr.collectNeededEntities(needs);
	}

	public IncidenceIndex getTarget() {
		return target;
	}

	public Expression getKeyExpr() {
		return keyExpr;
	}
}
