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
/// Represents a condition statement in the IR.
/// </summary>
public class ConditionStatement : BlockNestingStatement
{
	private Expression conditionExpr;
	private List<EvalStatement> falseCaseStatements = null;

	public ConditionStatement(Expression conditionExpr)
		: base("condition statement")
	{
		this.conditionExpr = conditionExpr;
	}

	public virtual void AddFalseCaseStatement(EvalStatement falseCaseStatement)
	{
		if(falseCaseStatements == null)
			falseCaseStatements = new List<EvalStatement>();
		falseCaseStatements.Add(falseCaseStatement);
	}

	public virtual Expression ConditionExpr
	{
		get
		{
		return conditionExpr;
		}
	}

	public virtual ICollection<EvalStatement> FalseCaseStatements
	{
		get
		{
		return falseCaseStatements != null ? falseCaseStatements.AsReadOnly() : null;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		conditionExpr.CollectNeededEntities(needs);
		foreach(EvalStatement trueCaseStatement in statements)
			trueCaseStatement.CollectNeededEntities(needs);
		if(falseCaseStatements != null)
		{
			foreach(EvalStatement falseCaseStatement in falseCaseStatements)
				falseCaseStatement.CollectNeededEntities(needs);
		}
	}
}

}
