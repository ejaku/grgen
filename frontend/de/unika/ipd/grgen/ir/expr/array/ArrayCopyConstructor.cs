/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.array
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using ArrayType = de.unika.ipd.grgen.ir.type.container.ArrayType;

public class ArrayCopyConstructor : Expression
{
	private Expression arrayToCopy;
	private ArrayType arrayType;

	public ArrayCopyConstructor(Expression arrayToCopy, ArrayType arrayType)
		: base("array copy constructor", arrayType)
	{
		this.arrayToCopy = arrayToCopy;
		this.arrayType = arrayType;
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.Add(this);
		needs.NeedsGraph();
		arrayToCopy.CollectNeededEntities(needs);
	}

	public virtual Expression ArrayToCopy
	{
		get
		{
			return arrayToCopy;
		}
	}

	public virtual ArrayType ArrayType
	{
		get
		{
			return arrayType;
		}
	}
}

}
