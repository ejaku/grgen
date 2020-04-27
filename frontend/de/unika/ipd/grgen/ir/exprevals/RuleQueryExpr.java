/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.Type;

// dummy class for rule queries from compiled sequences
public class RuleQueryExpr extends Expression {
	public RuleQueryExpr(Type targetType) {
		super("rule query", targetType);
	}

	public void collectNeededEntities(NeededEntities needs) {
	}
}
