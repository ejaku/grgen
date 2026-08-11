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
using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// An external function method invocation is an expression.
/// </summary>
public class ExternalFunctionMethodInvocationExpr : FunctionInvocationBaseExpr
{
	/// <summary>
	/// The owner of the function method. </summary>
	private Expression owner;

	/// <summary>
	/// The function of the function method invocation expression. </summary>
	protected internal ExternalFunction externalFunction;

	public ExternalFunctionMethodInvocationExpr(Expression owner, Type type, ExternalFunction externalFunction)
		: base("external function method invocation expr", type)
	{

		this.owner = owner;
		this.externalFunction = externalFunction;
	}

	public virtual Expression Owner
	{
		get
		{
			return owner;
		}
	}

	public virtual ExternalFunction ExternalFunc
	{
		get
		{
			return externalFunction;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		owner.CollectNeededEntities(needs);
		foreach(Expression child in WalkableChildren)
			child.CollectNeededEntities(needs);
	}
}

}
