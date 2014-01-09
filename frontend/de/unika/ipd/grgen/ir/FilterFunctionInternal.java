/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.exprevals.EvalStatement;

/**
 * An internal filter function.
 */
public class FilterFunctionInternal extends FilterFunction {
	/** The computation statements */
	private List<EvalStatement> computationStatements = new LinkedList<EvalStatement>();

	public FilterFunctionInternal(String name, Ident ident) {
		super(name, ident);
	}
		
	/** Add a computation statement to the function. */
	public void addComputationStatement(EvalStatement eval) {
		computationStatements.add(eval);
	}

	/** Get all computation statements of this function. */
	public List<EvalStatement> getComputationStatements() {
		return Collections.unmodifiableList(computationStatements);
	}
}
