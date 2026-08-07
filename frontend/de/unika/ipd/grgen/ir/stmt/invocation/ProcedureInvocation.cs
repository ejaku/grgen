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
using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;

/// <summary>
/// A procedure invocation.
/// </summary>
public class ProcedureInvocation : ProcedureInvocationBase
{
	/// <summary>
	/// The procedure of the procedure invocation. </summary>
	protected internal Procedure procedure;

	public ProcedureInvocation(Procedure procedure)
		: base("procedure invocation")
	{

		this.procedure = procedure;
	}

	public override ProcedureBase ProcedureBase
	{
		get
		{
		return procedure;
		}
	}

	public virtual Procedure Procedure
	{
		get
		{
		return procedure;
		}
	}
}

}
