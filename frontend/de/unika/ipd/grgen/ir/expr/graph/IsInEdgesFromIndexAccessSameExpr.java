/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.expr.graph;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
import de.unika.ipd.grgen.ir.type.Type;

public class IsInEdgesFromIndexAccessSameExpr extends EdgesFromIndexAccessExpr
{
	private final Expression candidateExpr;
	private final IndexAccessEquality indexAccess;

	public IsInEdgesFromIndexAccessSameExpr(Expression candidateExpr, IndexAccessEquality indexAccess, Type type)
	{
		super(indexAccess.index, type);
		this.candidateExpr = candidateExpr;
		this.indexAccess = indexAccess;
	}

	public Expression getCandidateExpr()
	{
		return candidateExpr;
	}

	public IndexAccessEquality getIndexAccessEquality()
	{
		return indexAccess;
	}

	/** @see de.unika.ipd.grgen.ir.expr.Expression#collectNeededEntities() */
	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.needsGraph();
		candidateExpr.collectNeededEntities(needs);
		indexAccess.collectNeededEntities(needs);
	}
}
