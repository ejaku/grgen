/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.type.basic
{
	using IR = de.unika.ipd.grgen.ir.IR;
	using TypeType = de.unika.ipd.grgen.ir.type.basic.TypeType;

	/// <summary>
	/// The type basic type.
	/// </summary>
	public class TypeTypeNode : BasicTypeNode
	{
		static TypeTypeNode()
		{
			SetClassName(typeof(TypeTypeNode), "type type");
		}

		protected internal override IR ConstructIR()
		{
			return new TypeType(Ident.IRIdent);
		}
	}

}
