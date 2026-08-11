/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.array
{
	using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
	using QualIdentNode = de.unika.ipd.grgen.ast.expr.QualIdentNode;
	using Qualification = de.unika.ipd.grgen.ir.expr.Qualification;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ArrayClear = de.unika.ipd.grgen.ir.stmt.array.ArrayClear;
	using ArrayVarClear = de.unika.ipd.grgen.ir.stmt.array.ArrayVarClear;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class ArrayClearNode : ArrayProcedureMethodInvocationBaseNode
	{
		static ArrayClearNode()
		{
			SetClassName(typeof(ArrayClearNode), "array clear statement");
		}

		public ArrayClearNode(Coords coords, QualIdentNode target)
			: base(coords, target)
		{
		}

		public ArrayClearNode(Coords coords, VarDeclNode targetVar)
			: base(coords, targetVar)
		{
		}

		protected internal override IR ConstructIR()
		{
			if(target != null)
				return new ArrayClear(target.CheckIR(typeof(Qualification)));
			else
				return new ArrayVarClear(targetVar.CheckIR(typeof(Variable)));
		}
	}

}
