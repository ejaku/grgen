/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2015 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

/**
 * An external procedure invocation.
 */
public class ExternalProcedureInvocation extends ProcedureInvocationBase {
	/** The procedure of the procedure invocation expression. */
	protected ExternalProcedure externalProcedure;

	public ExternalProcedureInvocation(ExternalProcedure externalProcedure) {
		super("external procedure invocation");

		this.externalProcedure = externalProcedure;
	}

	public ProcedureBase getProcedureBase() {
		return externalProcedure;
	}

	public ExternalProcedure getExternalProc() {
		return externalProcedure;
	}
}
