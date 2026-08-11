/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.array
{
	using de.unika.ipd.grgen.ir;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using ContainerVarProcedureMethodInvocationBase = de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

	public class ArrayVarAddItem : ContainerVarProcedureMethodInvocationBase
	{
		internal Expression valueExpr;
		internal Expression indexExpr;

		public ArrayVarAddItem(Variable target, Expression valueExpr, Expression indexExpr)
			: base("array var add item", target)
		{
			this.valueExpr = valueExpr;
			this.indexExpr = indexExpr;
		}

		public virtual Expression ValueExpr
		{
			get
			{
				return valueExpr;
			}
		}

		public virtual Expression IndexExpr
		{
			get
			{
				return indexExpr;
			}
		}

		public override void CollectNeededEntities(NeededEntities needs)
		{
			base.CollectNeededEntities(needs);

			valueExpr.CollectNeededEntities(needs);

			if(indexExpr != null)
				indexExpr.CollectNeededEntities(needs);
		}
	}

}
