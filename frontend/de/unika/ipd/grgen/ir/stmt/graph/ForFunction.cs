/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.graph
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
using BlockNestingStatement = de.unika.ipd.grgen.ir.stmt.BlockNestingStatement;

/// <summary>
/// Represents a for lookup of a neighborhood function in the IR.
/// </summary>
public class ForFunction : BlockNestingStatement
{
	private Variable iterationVar;
	private Expression function;

	public ForFunction(Variable iterationVar, Expression function)
		: base("for function")
	{
		this.iterationVar = iterationVar;
		this.function = function;
	}

	public virtual void AddLoopedStatement(EvalStatement loopedStatement)
	{
		statements.Add(loopedStatement);
	}

	public virtual Variable IterationVar
	{
		get
		{
			return iterationVar;
		}
	}

	public virtual Expression Function
	{
		get
		{
			return function;
		}
	}

	public virtual ICollection<EvalStatement> LoopedStatements
	{
		get
		{
			return statements;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		function.CollectNeededEntities(needs);
		foreach(EvalStatement loopedStatement in statements)
			loopedStatement.CollectNeededEntities(needs);
		if(needs.variables != null)
			needs.variables.Remove(iterationVar);
	}
}

}
