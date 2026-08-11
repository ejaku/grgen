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
using Function = de.unika.ipd.grgen.ir.executable.Function;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// Real function calls, i.e. calls of user-defined functions.
/// </summary>
public class FunctionInvocationExpr : FunctionInvocationBaseExpr
{
	/// <summary>
	/// The function of the function invocation expression. </summary>
	protected internal Function function;

	public FunctionInvocationExpr(Type type, Function function)
		: base("function invocation expr", type)
	{

		this.function = function;
	}

	public virtual Function Function
	{
		get
		{
			return function;
		}
	}
}

}
