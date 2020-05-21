/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;
import de.unika.ipd.grgen.ir.typedecl.ProcedureBase;

public class GraphAddCopyNodeProc extends ProcedureInvocationBase
{
	private final Expression oldNode;

	public GraphAddCopyNodeProc(Expression nodeType)
	{
		super("graph add copy node procedure");
		this.oldNode = nodeType;
	}

	public Expression getOldNodeExpr()
	{
		return oldNode;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		oldNode.collectNeededEntities(needs);
	}
}
