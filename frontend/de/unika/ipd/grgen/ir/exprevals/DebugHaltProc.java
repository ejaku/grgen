/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;

public class DebugHaltProc extends ProcedureInvocationBase {
	private Collection<Expression> exprs;

	public DebugHaltProc(Collection<Expression> expressions) {
		super("debug halt procedure");
		this.exprs = expressions;
	}

	public Expression getFirstExpression() {
		for(Expression expr : exprs) {
			return expr;
		}
		return null;
	}

	public Collection<Expression> getExpressions() {
		return exprs;
	}

	public ProcedureBase getProcedureBase() {
		return null; // dummy needed for interface, not accessed because the type of the class already defines the procedure
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.needsGraph();
		for(Expression expr : exprs) {
			expr.collectNeededEntities(needs);
		}
	}
}
