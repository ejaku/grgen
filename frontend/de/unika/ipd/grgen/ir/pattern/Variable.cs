/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// Represents a "var" parameter of an action.
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{
using Entity = de.unika.ipd.grgen.ir.Entity;
using Ident = de.unika.ipd.grgen.ir.Ident;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;

public class Variable : Entity
{
	// the pattern graph of the variable
	public PatternGraphLhs directlyNestingLHSGraph;

	// null or an expression used to initialize the variable
	public Expression initialization;

	public bool isLambdaExpressionVariable;


	public Variable(string name, Ident ident, Type type, bool isDefToBeYieldedTo,
			PatternGraphLhs directlyNestingLHSGraph, int context, bool isLambdaExpressionVariable)
		: base(name, ident, type, false, isDefToBeYieldedTo, context)
	{
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
		this.isLambdaExpressionVariable = isLambdaExpressionVariable;
	}

	public virtual Expression Initialization
	{
		set
		{
		this.initialization = value;
		}
	}

	public override string Kind
	{
		get
		{
		return "variable";
		}
	}
}

}
