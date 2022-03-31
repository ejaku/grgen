/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.stmt.graph;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class GraphAddCopyNodeProc extends BuiltinProcedureInvocationBase
{
	private final Expression oldNode;

	private final Type returnType;

	private final boolean deep;

	public GraphAddCopyNodeProc(Expression nodeType, Type returnType, boolean deep)
	{
		super("graph add copy node procedure");
		this.oldNode = nodeType;
		this.returnType = returnType;
		this.deep = deep;
	}

	public Expression getOldNodeExpr()
	{
		return oldNode;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		oldNode.collectNeededEntities(needs);
	}
	
	@Override
	public int returnArity()
	{
		return 1;
	}
	
	@Override
	public Type getReturnType(int index)
	{
		assert(index == 0);
		return returnType;
	}
	
	public boolean getDeep()
	{
		return deep;
	}
}
