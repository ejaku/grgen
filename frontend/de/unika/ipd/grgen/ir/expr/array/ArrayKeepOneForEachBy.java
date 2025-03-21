/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.expr.array;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayKeepOneForEachBy extends ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;

	public ArrayKeepOneForEachBy(Expression targetExpr, Entity member)
	{
		super("array keep one for each by expr", (ArrayType)targetExpr.getType(), targetExpr);
		this.member = member;
	}

	public Entity getMember()
	{
		return member;
	}
}
