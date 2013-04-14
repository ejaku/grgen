/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;

public class HighlightComp extends ComputationInvocationBase {
	private Collection<Expression> toHighlightExpressions;

	public HighlightComp(Collection<Expression> toHighlightExpressions) {
		super("highlight computation");
		this.toHighlightExpressions = toHighlightExpressions;
	}

	public Collection<Expression> getToHighlightExpressions() {
		return toHighlightExpressions;
	}

	public ComputationBase getComputationBase() {
		return null; // dummy needed for interface, not accessed cause real type defines computation
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		for(Expression exprToHighlight : toHighlightExpressions) {
			exprToHighlight.collectNeededEntities(needs);
		}
	}
}
