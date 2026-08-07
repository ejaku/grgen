/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.stmt.procenv
{
using NeededEntities = de.unika.ipd.grgen.ir.NeededEntities;
using BuiltinProcedureInvocationBase = de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class PauseTransactionProc : BuiltinProcedureInvocationBase
{
	public PauseTransactionProc()
		: base("pause transaction procedure")
	{
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		needs.NeedsGraph();
	}
}

}
