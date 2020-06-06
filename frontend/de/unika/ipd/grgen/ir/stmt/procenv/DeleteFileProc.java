/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;

public class DeleteFileProc extends BuiltinProcedureInvocationBase
{
	private Expression pathExpr;

	public DeleteFileProc(Expression pathExpr)
	{
		super("deleteFile procedure");
		this.pathExpr = pathExpr;
	}

	public Expression getPathExpr()
	{
		return pathExpr;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		pathExpr.collectNeededEntities(needs);
	}
}
