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
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class DeleteFileProc : BuiltinProcedureInvocationBase
{
	private Expression pathExpr;

	public DeleteFileProc(Expression pathExpr)
		: base("deleteFile procedure")
	{
		this.pathExpr = pathExpr;
	}

	public virtual Expression PathExpr
	{
		get
		{
		return pathExpr;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		pathExpr.CollectNeededEntities(needs);
	}
}

}
