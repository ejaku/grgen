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
using ExternalFunction = de.unika.ipd.grgen.ir.executable.ExternalFunction;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// An external function invocation is an expression.
/// </summary>
public class ExternalFunctionInvocationExpr : FunctionInvocationBaseExpr
{
	/// <summary>
	/// The function of the function invocation expression. </summary>
	protected internal ExternalFunction externalFunction;

	public ExternalFunctionInvocationExpr(Type type, ExternalFunction externalFunction)
		: base("external function invocation expr", type)
	{

		this.externalFunction = externalFunction;
	}

	public virtual ExternalFunction ExternalFunc
	{
		get
		{
			return externalFunction;
		}
	}
}

}
