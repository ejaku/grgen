/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.model.Index;

/**
 * Class for accessing an index by equality comparison, binding a pattern element
 */
public class IndexAccessEquality extends IndexAccess
{
	public Expression expr;

	public IndexAccessEquality(Index index, Expression expr)
	{
		super(index);
		this.expr = expr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		expr.collectNeededEntities(needs);
	}
}
