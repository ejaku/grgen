/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.type.basic
{
	using IR = de.unika.ipd.grgen.ir.IR;
	using VoidType = de.unika.ipd.grgen.ir.type.basic.VoidType;

	/// <summary>
	/// The void basic type. It is compatible to no other type.
	/// </summary>
	public class VoidTypeNode : BasicTypeNode
	{
		static VoidTypeNode()
		{
			SetClassName(typeof(VoidTypeNode), "void type");
		}

		protected internal override IR ConstructIR()
		{
			return new VoidType(Ident.IRIdent);
		}

		public override string ToString()
		{
			return "void";
		}
	}

}
