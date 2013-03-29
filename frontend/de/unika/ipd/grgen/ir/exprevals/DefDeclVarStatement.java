/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ir.exprevals;

import de.unika.ipd.grgen.ir.*;

/**
 * Represents a declaration of a local variable of non-graph-element-type in the IR.
 */
public class DefDeclVarStatement extends EvalStatement {

	private Variable target;

	public DefDeclVarStatement(Variable target) {
		super("def decl var");
		this.target = target;
	}

	public Variable getTarget() {
		return target;
	}

	public String toString() {
		return target.getIdent().toString();
	}

	public void collectNeededEntities(NeededEntities needs)
	{
		//needs.add(target); needed?
		if(target.initialization!=null)
			target.initialization.collectNeededEntities(needs);
	}
}
