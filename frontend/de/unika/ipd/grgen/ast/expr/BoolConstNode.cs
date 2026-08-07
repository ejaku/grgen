/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr
{
using StringConstNode = de.unika.ipd.grgen.ast.expr.@string.StringConstNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A boolean constant.
/// </summary>
public class BoolConstNode : ConstNode
{
	public BoolConstNode(Coords coords, bool value)
		: base(coords, "boolean", new bool?(value))
	{
	}

	public override TypeNode Type
	{
		get
		{
		return BasicTypeNode.booleanType;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.expr.ConstNode.doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) "/>
	protected internal override ConstNode DoCastTo(TypeNode type)
	{
		bool? value = (bool?)Value;

		if(type.IsEqual(BasicTypeNode.stringType))
			return new StringConstNode(Coords, value.ToString());
		else
			throw new System.NotSupportedException();
	}
}

}
