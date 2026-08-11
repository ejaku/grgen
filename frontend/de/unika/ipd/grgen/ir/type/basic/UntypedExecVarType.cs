/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.type.basic
{
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	//import de.unika.ipd.grgen.ast.BasicTypeNode;

	public class UntypedExecVarType : Type
	{
		/// <param name="ident"> The name of the type. </param>
		public UntypedExecVarType(Ident ident)
			: base("untyped exec var type", ident)
		{
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.type.Type.classify() "/>
		public override TypeClass Classify()
		{
			return TypeClass.IS_UNTYPED_EXEC_VAR_TYPE;
		}

		public static Type Type
		{
			get
			{
				return null;
			}
		}
	}

}
