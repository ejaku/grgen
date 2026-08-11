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
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using Type = de.unika.ipd.grgen.ir.type.Type;

	/// <summary>
	/// A base class for procedure or builtin procedure invocations.
	/// </summary>
	public abstract class ProcedureOrBuiltinProcedureInvocationBase : EvalStatement
	{
		protected internal ProcedureOrBuiltinProcedureInvocationBase(string name)
			: base(name)
		{
		}

		/// <returns> The number of return arguments. </returns>
		public abstract int ReturnArity();

		/// <summary>
		/// Get the ith return type. </summary>
		/// <param name="index"> The index of the return type </param>
		/// <returns> The return type, if <code>index</code> was valid, <code>null</code> if not. </returns>
		public abstract Type GetReturnType(int index);
	}

}
