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
using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
using ProcedureBase = de.unika.ipd.grgen.ir.executable.ProcedureBase;

/// <summary>
/// An external procedure invocation.
/// </summary>
public class ExternalProcedureInvocation : ProcedureInvocationBase
{
	/// <summary>
	/// The procedure of the procedure invocation expression. </summary>
	protected internal ExternalProcedure externalProcedure;

	public ExternalProcedureInvocation(ExternalProcedure externalProcedure)
		: base("external procedure invocation")
	{

		this.externalProcedure = externalProcedure;
	}

	public override ProcedureBase ProcedureBase
	{
		get
		{
		return externalProcedure;
		}
	}

	public virtual ExternalProcedure ExternalProc
	{
		get
		{
		return externalProcedure;
		}
	}
}

}
