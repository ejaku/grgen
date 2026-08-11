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
using ProcedureOrBuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.invocation.ProcedureOrBuiltinProcedureInvocationBase;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A base class for builtin procedure invocations.
/// </summary>
public abstract class BuiltinProcedureInvocationBase : ProcedureOrBuiltinProcedureInvocationBase
{
	protected internal BuiltinProcedureInvocationBase(string name)
		: base(name)
	{
	}

	public override int ReturnArity()
	{
		return 0;
	}

	public override Type GetReturnType(int index)
	{
		return null;
	}
}

}
