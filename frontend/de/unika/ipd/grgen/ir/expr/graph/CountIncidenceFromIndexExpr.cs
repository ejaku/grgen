/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.graph
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using IncidenceCountIndex = de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
	using IntType = de.unika.ipd.grgen.ir.type.basic.IntType;

	public class CountIncidenceFromIndexExpr : Expression
	{
		internal IncidenceCountIndex index;
		internal Expression keyExpr;

		public CountIncidenceFromIndexExpr(IncidenceCountIndex target, Expression keyExpr)
			: base("count incidence from index access expression", IntType.Type)
		{
			this.index = target;
			this.keyExpr = keyExpr;
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			needs.Add(this);
			keyExpr.CollectNeededEntities(needs);
		}

		public virtual IncidenceCountIndex Index
		{
			get
			{
				return index;
			}
		}

		public virtual Expression KeyExpr
		{
			get
			{
				return keyExpr;
			}
		}
	}

}
