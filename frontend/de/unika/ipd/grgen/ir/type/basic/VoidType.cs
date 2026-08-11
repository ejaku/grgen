/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.type.basic
{
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using de.unika.ipd.grgen.ir;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// The void type.
/// </summary>
public class VoidType : PrimitiveType
{
	public VoidType(Ident ident)
		: base("void type", ident)
	{
	}

	public override bool IsVoid()
	{
		return true;
	}

	public override bool IsEqual(Type t)
	{
		return t.IsVoid();
	}

	public static Type Type
	{
		get
		{
			return BasicTypeNode.voidType.CheckIR(typeof(Type));
		}
	}
}

}
