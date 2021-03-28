/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class ResumeTransactionProc extends BuiltinProcedureInvocationBase
{
	public ResumeTransactionProc()
	{
		super("resume transaction procedure");
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
	}
}
