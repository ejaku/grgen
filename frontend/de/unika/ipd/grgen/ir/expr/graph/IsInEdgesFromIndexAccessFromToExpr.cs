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
	using IndexAccessOrdering = de.unika.ipd.grgen.ir.pattern.IndexAccessOrdering;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	public class IsInEdgesFromIndexAccessFromToExpr : EdgesFromIndexAccessExpr
	{
		private readonly Expression candidateExpr;
		private readonly IndexAccessOrdering indexAccess;

		public IsInEdgesFromIndexAccessFromToExpr(Expression candidateExpr, IndexAccessOrdering indexAccess, Type type)
			: base(indexAccess.index, type)
		{
			this.candidateExpr = candidateExpr;
			this.indexAccess = indexAccess;
		}

		public virtual Expression CandidateExpr
		{
			get
			{
				return candidateExpr;
			}
		}

		public virtual IndexAccessOrdering IndexAccessOrdering
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
			candidateExpr.CollectNeededEntities(needs);
			indexAccess.CollectNeededEntities(needs);
		}
	}

}
