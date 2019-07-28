/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2019 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.Collection;

public class EmitProc extends ProcedureInvocationBase {
	private Collection<Expression> exprs;
	private boolean isDebug;

	public EmitProc(Collection<Expression> expressions, boolean isDebug) {
		super("emit procedure");
		this.exprs = expressions;
		this.isDebug = isDebug;
	}

	public Collection<Expression> getExpressions() {
		return exprs;
	}
	
	public boolean isDebug() {
		return isDebug;
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
