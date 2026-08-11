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
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

	public class CountNodesExpr : BuiltinFunctionInvocationExpr
	{
		private readonly Expression nodeType;

		public CountNodesExpr(Expression nodeType)
			: base("count nodes expression", IntType.Type)
		{
			this.nodeType = nodeType;
		}

		public virtual Expression NodeTypeExpr
		{
			get
			{
				return nodeType;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			nodeType.CollectNeededEntities(needs);
		}
	}

}
