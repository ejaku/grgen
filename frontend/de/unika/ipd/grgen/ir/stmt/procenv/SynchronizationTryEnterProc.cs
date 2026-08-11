/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.procenv
{
using System.Diagnostics;

using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class SynchronizationTryEnterProc : BuiltinProcedureInvocationBase
{
	internal Type returnType;

	private Expression criticalSectionObjectExpr;

	public SynchronizationTryEnterProc(Type returnType, Expression criticalSectionObjectExpr)
		: base("synchronization try enter procedure")
	{
		this.returnType = returnType;
		this.criticalSectionObjectExpr = criticalSectionObjectExpr;
	}

	public virtual Expression CriticalSectionObject
	{
		get
		{
			return criticalSectionObjectExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		criticalSectionObjectExpr.CollectNeededEntities(needs);
	}

	public override int ReturnArity()
	{
		return 1;
	}

	public override Type GetReturnType(int index)
	{
		Debug.Assert((index == 0));
		return returnType;
	}
}

}
