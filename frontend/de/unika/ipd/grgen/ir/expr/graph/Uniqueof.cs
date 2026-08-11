/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
	using de.unika.ipd.grgen.ir;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
	using Type = de.unika.ipd.grgen.ir.type.Type;
	using GraphType = de.unika.ipd.grgen.ir.type.basic.GraphType;

	public class Uniqueof : BuiltinFunctionInvocationExpr
	{
		/// <summary>
		/// The entity whose unique id we want to know. </summary>
		private readonly Expression entity;

		public Uniqueof(Expression entity, Type type)
			: base("uniqueof", type)
		{
			this.entity = entity;
		}

		public virtual Expression Entity
		{
			get
			{
				return entity;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			if(entity == null || entity.Type is GraphType)
				needs.NeedsGraph();
			if(entity != null)
				entity.CollectNeededEntities(needs);
		}
	}

}
