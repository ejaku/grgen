/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class PauseTransactionProc extends BuiltinProcedureInvocationBase
{
	public PauseTransactionProc()
	{
		super("pause transaction procedure");
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
	}
}
