/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.GraphType;

public class ThisExpr extends Expression {
	public ThisExpr() {
		super("this", GraphType.getType());
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
	}
}
