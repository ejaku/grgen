/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// EnumExpression.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
using System;

using EnumItem = de.unika.ipd.grgen.ir.model.EnumItem;
using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;

public class EnumExpression : Constant
{
	private EnumItem item;

	// Constructor for later initialization when EnumType and EnumItem have been constructed.
	// See EnumTypeNode.constructIR().
	public EnumExpression(int value)
		: base(null, Convert.ToInt32(value))
	{
		Name = "enum expression";
	}

	public EnumExpression(EnumType type, EnumItem item)
		: base(type, item.Value.Value)
	{
		this.item = item;
		Name = "enum expression";
	}

	public virtual void LateInit(EnumType type, EnumItem item)
	{
		this.type = type;
		this.item = item;
	}

	public virtual EnumItem EnumItem
	{
		get
		{
			return item;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.util.GraphDumpable.getNodeLabel() "/>
	public override string NodeLabel
	{
		get
		{
			return item + " " + Value;
		}
	}
}

}
