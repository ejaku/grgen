/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.string;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.typedecl.StringType;

public class StringToLower extends Expression
{
	private Expression stringExpr;

	public StringToLower(Expression stringExpr)
	{
		super("string toLower", StringType.getType());
		this.stringExpr = stringExpr;
	}

	public Expression getStringExpr()
	{
		return stringExpr;
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		stringExpr.collectNeededEntities(needs);
	}
}
