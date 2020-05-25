/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.executable.ProcedureBase;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.ProcedureInvocationBase;

public class GraphAddCopyEdgeProc extends ProcedureInvocationBase
{
	private final Expression sourceNode;
	private final Expression targetNode;
	private final Expression oldEdge;

	public GraphAddCopyEdgeProc(Expression edgeType,
			Expression sourceNode,
			Expression targetNode)
	{
		super("graph add copy edge procedure");
		this.oldEdge = edgeType;
		this.sourceNode = sourceNode;
		this.targetNode = targetNode;
	}

	public Expression getOldEdgeExpr()
	{
		return oldEdge;
	}

	public Expression getSourceNodeExpr()
	{
		return sourceNode;
	}

	public Expression getTargetNodeExpr()
	{
		return targetNode;
	}

	public ProcedureBase getProcedureBase()
	{
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		oldEdge.collectNeededEntities(needs);
		sourceNode.collectNeededEntities(needs);
		targetNode.collectNeededEntities(needs);
	}
}