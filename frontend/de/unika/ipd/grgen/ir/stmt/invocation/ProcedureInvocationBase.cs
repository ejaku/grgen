/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.invocation
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ir;
using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A base class for procedure or builtin procedure invocations.
/// </summary>
public abstract class ProcedureInvocationBase : ProcedureOrBuiltinProcedureInvocationBase
{
	/// <summary>
	/// The arguments of the procedure invocation. </summary>
	protected internal IList<Expression> arguments = new List<Expression>();

	/// <summary>
	/// The return types of the procedure invocation. </summary>
	protected internal IList<Type> returnTypes = new List<Type>();

	protected internal ProcedureInvocationBase(string name)
		: base(name)
	{
	}

	/// <returns> The number of arguments. </returns>
	public virtual int Arity()
	{
		return arguments.Count;
	}

	/// <summary>
	/// Get the ith argument. </summary>
	/// <param name="index"> The index of the argument </param>
	/// <returns> The argument, if <code>index</code> was valid, <code>null</code> if not. </returns>
	public virtual Expression GetArgument(int index)
	{
		return index >= 0 || index < arguments.Count ? arguments[index] : null;
	}

	/// <summary>
	/// Adds an argument e to the expression. </summary>
	public virtual void AddArgument(Expression e)
	{
		arguments.Add(e);
	}

	public override int ReturnArity()
	{
		return returnTypes.Count;
	}

	public override Type GetReturnType(int index)
	{
		return index >= 0 || index < returnTypes.Count ? returnTypes[index] : null;
	}

	/// <summary>
	/// Adds a return type t to the procedure. </summary>
	public virtual void AddReturnType(Type t)
	{
		returnTypes.Add(t);
	}

	public virtual ICollection<Expression> WalkableChildren
	{
		get
		{
		return arguments;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ir.expr.Expression.collectNeededEntities() "/>
	public override void CollectNeededEntities(NeededEntities needs)
	{
		foreach(Expression child in WalkableChildren)
			child.CollectNeededEntities(needs);
	}

	public abstract ProcedureBase ProcedureBase {get;}
}

}
