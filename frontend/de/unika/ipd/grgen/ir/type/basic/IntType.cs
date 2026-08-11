/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.type.basic
{
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ir;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// A Integer type.
	/// </summary>
	public class IntType : PrimitiveType
	{
		public IntType(Ident ident)
			: base("integer type", ident)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
		public override TypeClass Classify()
		{
			return TypeClass.IS_INTEGER;
		}

		public static Type Type
		{
			get
			{
				return BasicTypeNode.intType.CheckIR<Type>(typeof(Type));
			}
		}
	}

}
