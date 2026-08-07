/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.type.basic
{
using IR = de.unika.ipd.grgen.ir.IR;
using FloatType = de.unika.ipd.grgen.ir.type.basic.FloatType;

/// <summary>
/// The floating point basic type.
/// </summary>
public class FloatTypeNode : BasicTypeNode
{
	static FloatTypeNode()
	{
		SetClassName(typeof(FloatTypeNode), "float type");
	}

	protected internal override IR ConstructIR()
	{
		return new FloatType(Ident.IRIdent);
	}

	public override string ToString()
	{
		return "float";
	}
}

}
