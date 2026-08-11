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
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using DebugHighlightProc = de.unika.ipd.grgen.ir.stmt.procenv.DebugHighlightProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	public class DebugHighlightProcNode : DebugProcNode
	{
		static DebugHighlightProcNode()
		{
			SetClassName(typeof(DebugHighlightProcNode), "debug highlight procedure");
		}

		public DebugHighlightProcNode(Coords coords)
			: base(coords)
		{
		}

		protected internal override bool CheckLocal()
		{
			int paramNum = 0;
			foreach(ExprNode expr in exprs.ChildrenExact)
			{
				TypeNode exprType = expr.Type;
				if(paramNum % 2 == 0 && !(exprType.Equals(BasicTypeNode.stringType)))
				{
					ReportError("The " + ShortSignature() + " procedure expects as " + paramNum + ". argument"
							+ " a value of type string (a message followed by a sequence of (value, annotation for the value)* must be given)"
							+ " (but is given a value of type " + exprType.ToStringWithDeclarationCoords() + ").");
					return false;
				}
				++paramNum;
			}
			return true;
		}

		protected internal override string ShortSignature()
		{
			return "Debug::highlight()";
		}

		protected internal override IR ConstructIR()
		{
			IList<Expression> expressions = new List<Expression>();
			foreach(ExprNode expr in exprs.ChildrenExact)
			{
				ExprNode exprEvaluated = expr.Evaluate();
				expressions.Add(exprEvaluated.CheckIR<Expression>(typeof(Expression)));
			}
			return new DebugHighlightProc(expressions);
		}
	}

}
