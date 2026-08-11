/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll, Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
	using de.unika.ipd.grgen.ir;

	public class ExpressionPair : IR
	{
		internal Expression keyExpr; // first
		internal Expression valueExpr; // second

		public ExpressionPair(Expression keyExpr, Expression valueExpr)
			: base("pair")
		{
			this.keyExpr = keyExpr;
			this.valueExpr = valueExpr;
		}

		public virtual Expression KeyExpr
		{
			get
			{
				return keyExpr;
			}
		}

		public virtual Expression ValueExpr
		{
			get
			{
				return valueExpr;
			}
		}

		public virtual void CollectNeededEntities(NeededEntities needs)
		{
			keyExpr.CollectNeededEntities(needs);
			valueExpr.CollectNeededEntities(needs);
		}
	}

}
