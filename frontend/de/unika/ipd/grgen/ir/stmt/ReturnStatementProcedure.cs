/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt
{

using System.Collections.Generic;

using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;

/// <summary>
/// Represents a return statement of a procedure in the IR.
/// </summary>
public class ReturnStatementProcedure : EvalStatement
{
	private IList<Expression> returnValuesExprs = new List<Expression>();

	public ReturnStatementProcedure()
		: base("return statement (procedure)")
	{
	}

	public virtual void AddReturnValueExpr(Expression returnValueExpr)
	{
		returnValuesExprs.Add(returnValueExpr);
	}

	public virtual IList<Expression> ReturnValueExpr
	{
		get
		{
			return returnValuesExprs;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		foreach(Expression returnValueExpr in returnValuesExprs)
			returnValueExpr.CollectNeededEntities(needs);
	}
}

}
