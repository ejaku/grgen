/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ir;
	using BuiltinFunctionInvocationExpr = de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class NodesFromIndexAccessMultipleFromToExpr : BuiltinFunctionInvocationExpr
	{
		private readonly IList<IndexAccessOrdering> indexAccesses;

		public NodesFromIndexAccessMultipleFromToExpr(IList<IndexAccessOrdering> indexAccesses, Type type)
			: base("nodes from index access multiple expression", type)
		{
			this.indexAccesses = indexAccesses;
		}

		public virtual IList<IndexAccessOrdering> IndexAccesses
		{
			get
			{
				return indexAccesses;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			foreach(IndexAccessOrdering indexAccess in indexAccesses)
				indexAccess.CollectNeededEntities(needs);
		}
	}

}
