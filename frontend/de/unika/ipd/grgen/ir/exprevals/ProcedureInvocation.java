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

/**
 * A procedure invocation.
 */
public class ProcedureInvocation extends ProcedureInvocationBase {
	/** The procedure of the procedure invocation. */
	protected Procedure procedure;


	public ProcedureInvocation(Procedure procedure) {
		super("procedure invocation");

		this.procedure = procedure;
	}

	public ProcedureBase getProcedureBase() {
		return procedure;
	}

	public Procedure getProcedure() {
		return procedure;
	}
}
