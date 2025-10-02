/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.stmt.procenv;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.BuiltinProcedureInvocationBase;
import de.unika.ipd.grgen.ir.type.Type;

public class GetEquivalentOrAddProc extends BuiltinProcedureInvocationBase
{
	Type returnType;
	
	private final Expression subgraphExpr;
	private final Expression arrayExpr;
	private final boolean includingAttributes;

	public GetEquivalentOrAddProc(Type returnType,
			Expression subgraphExpr, Expression arrayExpr, boolean includingAttributes)
	{
		super("get equivalent or add procedure");
		this.returnType = returnType;
		this.subgraphExpr = subgraphExpr;
		this.arrayExpr = arrayExpr;
		this.includingAttributes = includingAttributes;
	}

	public Expression getSubgraphExpr()
	{
		return subgraphExpr;
	}

	public Expression getArrayExpr()
	{
		return arrayExpr;
	}

	public boolean getIncludingAttributes()
	{
		return includingAttributes;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		subgraphExpr.collectNeededEntities(needs);
		arrayExpr.collectNeededEntities(needs);
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
}
