/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.set
{
using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using ContainerVarProcedureMethodInvocationBase = de.unika.ipd.grgen.ir.stmt.ContainerVarProcedureMethodInvocationBase;

public class SetVarAddAll : ContainerVarProcedureMethodInvocationBase
{
	internal Expression valueExpr;

	public SetVarAddAll(Variable target, Expression valueExpr)
		: base("set var add item", target)
	{
		this.valueExpr = valueExpr;
	}

	public virtual Expression ValueExpr
	{
		get
		{
		return valueExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		base.CollectNeededEntities(needs);

		valueExpr.CollectNeededEntities(needs);
	}
}

}
