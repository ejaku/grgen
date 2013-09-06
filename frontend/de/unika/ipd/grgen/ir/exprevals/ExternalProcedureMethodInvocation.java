/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.exprevals;

import java.util.ArrayList;
import java.util.List;

import de.unika.ipd.grgen.ir.Entity;

/**
 * An external procedure method invocation.
 */
public class ExternalProcedureMethodInvocation extends ProcedureInvocationBase {
	/** The owner of the procedure method. */
	private Entity owner;

	/** The arguments of the procedure invocation. */
	protected List<Expression> arguments = new ArrayList<Expression>();

	/** The procedure of the procedure method invocation expression. */
	protected ExternalProcedure externalProcedure;

	public ExternalProcedureMethodInvocation(Entity owner, ExternalProcedure externalProcedure) {
		super("external procedure method invocation");

		this.owner = owner;
		this.externalProcedure = externalProcedure;
	}

	public Entity getOwner() {
		return owner;
	}

	public ProcedureBase getProcedureBase() {
		return externalProcedure;
	}

	public ExternalProcedure getExternalProc() {
		return externalProcedure;
	}
}
