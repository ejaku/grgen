/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.numeric
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;

	public class TruncateExpr : BuiltinFunctionInvocationExpr
	{
		private Expression expr;

		public TruncateExpr(Expression expr)
			: base("truncate expr", expr.Type)
		{
			this.expr = expr;
		}

		public virtual Expression Expr
		{
			get
			{
				return expr;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			expr.CollectNeededEntities(needs);
		}
	}

}
