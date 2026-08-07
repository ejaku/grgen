/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Coords = de.unika.ipd.grgen.parser.Coords;

public class InvalidConstNode : ConstNode
{
	static InvalidConstNode()
	{
		SetClassName(typeof(InvalidConstNode), "invalid const");
	}

	public InvalidConstNode(Coords coords, string name, object value)
		: base(coords, name, value)
	{
	}

	protected internal override ConstNode DoCastTo(TypeNode type)
	{
		return this;
	}

	public override string ToString()
	{
		return "invalid const";
	}
}

}
