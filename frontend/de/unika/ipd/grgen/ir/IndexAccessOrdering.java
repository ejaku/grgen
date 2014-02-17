/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.exprevals.Expression;

/**
 * Class for accessing an index by ordering, binding a pattern element
 */
public class IndexAccessOrdering extends IndexAccess {
	boolean ascending;
	int comp;
	public Expression expr;
	int comp2;
	public Expression expr2;
	
	public IndexAccessOrdering(AttributeIndex index, boolean ascending,
			int comp, Expression expr, int comp2, Expression expr2) {
		super(index);
		this.ascending = ascending;
		this.comp = comp;
		this.expr = expr;
		this.comp2 = comp2;
		this.expr2 = expr2;
	}
}
