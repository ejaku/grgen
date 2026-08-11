/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
	using StringType = de.unika.ipd.grgen.ir.type.basic.StringType;

	public class CanonizeExpr : BuiltinFunctionInvocationExpr
	{
		private Expression graphExpr;

		public CanonizeExpr(Expression graphExpr)
			: base("canonize expr", StringType.Type)
		{
			this.graphExpr = graphExpr;
		}

		public virtual Expression GraphExpr
		{
			get
			{
				return graphExpr;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			graphExpr.CollectNeededEntities(needs);
		}
	}

}
