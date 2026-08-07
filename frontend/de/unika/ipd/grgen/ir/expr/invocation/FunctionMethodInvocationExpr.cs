/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.expr.invocation
{
using de.unika.ipd.grgen.ir;
using Function = de.unika.ipd.grgen.ir.executable.Function;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A function method invocation is an expression.
/// </summary>
public class FunctionMethodInvocationExpr : FunctionInvocationBaseExpr
{
	/// <summary>
	/// The owner of the function method. </summary>
	private Entity owner;

	/// <summary>
	/// The function of the function method invocation expression. </summary>
	protected internal Function function;

	public FunctionMethodInvocationExpr(Entity owner, Type type, Function function)
		: base("function method invocation expr", type)
	{

		this.owner = owner;
		this.function = function;
	}

	public virtual Entity Owner
	{
		get
		{
		return owner;
		}
	}

	public virtual Function Function
	{
		get
		{
		return function;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(!IsGlobalVariable(owner))
		{
			if(owner is GraphEntity)
				needs.Add((GraphEntity)owner);
			else
				needs.Add((Variable)owner);
		}
		foreach(Expression child in WalkableChildren)
			child.CollectNeededEntities(needs);
	}
}

}
