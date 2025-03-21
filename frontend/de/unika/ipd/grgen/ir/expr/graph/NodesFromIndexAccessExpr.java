/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.invocation.BuiltinFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.model.Index;
import de.unika.ipd.grgen.ir.type.Type;

public abstract class NodesFromIndexAccessExpr extends BuiltinFunctionInvocationExpr
{
	public final Index index;

	protected NodesFromIndexAccessExpr(Index index, Type type)
	{
		super("nodes from index access expression", type);
		this.index = index;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public abstract void collectNeededEntities(NeededEntities needs);
}
