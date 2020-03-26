/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.Type;

// dummy class for multi rule queries from compiled sequences
public class MultiRuleQueryExpr extends Expression {
	public MultiRuleQueryExpr(Type targetType) {
		super("multi rule query", targetType);
	}

	public void collectNeededEntities(NeededEntities needs) {
	}
}
