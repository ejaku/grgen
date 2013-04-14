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
 * A computation invocation.
 */
public class ComputationInvocation extends ComputationInvocationBase {
	/** The computation of the computation invocation expression. */
	protected Computation computation;


	public ComputationInvocation(Computation computation) {
		super("computation invocation expr");

		this.computation = computation;
	}

	public ComputationBase getComputationBase() {
		return computation;
	}

	public Computation getComputation() {
		return computation;
	}
}
