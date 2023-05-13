/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

public class ArrayExtract extends ArrayFunctionMethodInvocationBaseExpr
{
	private Entity member;

	public ArrayExtract(Expression targetExpr, ArrayType resultingType, Entity member)
	{
		super("array extract", resultingType, targetExpr);
		this.member = member;
	}

	public Entity getMember()
	{
		return member;
	}
}
