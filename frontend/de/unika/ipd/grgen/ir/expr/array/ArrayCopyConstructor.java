/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.array;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayCopyConstructor extends Expression
{
	private Expression arrayToCopy;
	private ArrayType arrayType;

	public ArrayCopyConstructor(Expression arrayToCopy, ArrayType arrayType)
	{
		super("array copy constructor", arrayType);
		this.arrayToCopy = arrayToCopy;
		this.arrayType = arrayType;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		needs.needsGraph();
		arrayToCopy.collectNeededEntities(needs);
	}

	public Expression getArrayToCopy()
	{
		return arrayToCopy;
	}

	public ArrayType getArrayType()
	{
		return arrayType;
	}
}
