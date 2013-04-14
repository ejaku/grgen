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
 * An external computation invocation.
 */
public class ExternalComputationInvocation extends ComputationInvocationBase {
	/** The computation of the computation invocation expression. */
	protected ExternalComputation externalComputation;

	public ExternalComputationInvocation(ExternalComputation externalComputation) {
		super("external computation invocation");

		this.externalComputation = externalComputation;
	}

	public ComputationBase getComputationBase() {
		return externalComputation;
	}

	public ExternalComputation getExternalComp() {
		return externalComputation;
	}
}
