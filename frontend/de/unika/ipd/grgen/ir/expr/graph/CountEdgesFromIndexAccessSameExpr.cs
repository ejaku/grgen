/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.expr.graph
{
	using de.unika.ipd.grgen.ir;
	using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class CountEdgesFromIndexAccessSameExpr : EdgesFromIndexAccessExpr
	{
		private readonly IndexAccessEquality indexAccess;

		public CountEdgesFromIndexAccessSameExpr(IndexAccessEquality indexAccess, Type type)
			: base(indexAccess.index, type)
		{
			this.indexAccess = indexAccess;
		}

		public virtual IndexAccessEquality IndexAccessEquality
		{
			get
			{
				return indexAccess;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.NeedsGraph();
			indexAccess.CollectNeededEntities(needs);
		}
	}

}
