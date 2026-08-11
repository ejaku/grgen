/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt.procenv
{

	using System.Collections.Generic;

	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using DebugAddProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugAddProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class DebugAddProcNode : DebugProcNode
	{
		static DebugAddProcNode()
		{
			SetClassName(typeof(DebugAddProcNode), "debug add procedure");
		}

		public DebugAddProcNode(Coords coords)
			: base(coords)
		{
		}

		protected internal override string ShortSignature()
		{
			return "Debug::add()";
		}

		protected internal override IR ConstructIR()
		{
			IList<Expression> expressions = new List<Expression>();
			foreach(ExprNode expr in exprs.GetChildrenExact())
			{
				ExprNode exprEvaluated = expr.Evaluate();
				expressions.Add(exprEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			return new DebugAddProc(expressions);
		}
	}

}
