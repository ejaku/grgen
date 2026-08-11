/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.map
{
	using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using MapType = de.unika.ipd.grgen.ir.type.container.MapType;

	public class MapPeekExpr : MapFunctionMethodInvocationBaseExpr
	{
		private Expression numberExpr;

		public MapPeekExpr(Expression targetExpr, Expression numberExpr)
			: base("map peek expr", ((MapType)(targetExpr.Type)).keyType, targetExpr)
		{
			this.numberExpr = numberExpr;
		}

		public virtual Expression NumberExpr
		{
			get
			{
				return numberExpr;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			base.CollectNeededEntities(needs);
			numberExpr.CollectNeededEntities(needs);
		}
	}

}
