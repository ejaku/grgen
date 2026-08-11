/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.expr.@string
{
	using ConstNode = de.unika.ipd.grgen.ast.expr.ConstNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A string constant.
	/// </summary>
	public class StringConstNode : ConstNode
	{
		public StringConstNode(Coords coords, string value)
			: base(coords, "string", value)
		{
		}

		public override TypeNode Type
		{
			get
			{
				return BasicTypeNode.stringType;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.expr.ConstNode.doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) "/>
		protected internal override ConstNode DoCastTo(TypeNode type)
		{
			throw new System.NotSupportedException();
		}
	}

}
