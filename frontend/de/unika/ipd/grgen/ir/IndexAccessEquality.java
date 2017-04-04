/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.NeededEntities;

/**
 * Class for accessing an index by equality comparison, binding a pattern element
 */
public class IndexAccessEquality extends IndexAccess {
	public Expression expr;
	
	public IndexAccessEquality(Index index, Expression expr) {
		super(index);
		this.expr = expr;
	}
	
	public void collectNeededEntities(NeededEntities needs)
	{
		expr.collectNeededEntities(needs);
	}
}
