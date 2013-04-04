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

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.*;

/**
 * A function.
 */
public class Function extends Identifiable {
	/** A list of the parameters */
	private List<Entity> params = new LinkedList<Entity>();

	/** The return-parameter type */
	private Type retType = null;
	
	/** The computation statements */
	private List<EvalStatement> computationStatements = new LinkedList<EvalStatement>();


	public Function(String name, Ident ident, Type retType) {
		super(name, ident);

		this.retType = retType;
	}

	/** Add a parameter to the function. */
	public void addParameter(Entity entity) {
		params.add(entity);
	}

	/** Get all parameters of this function. */
	public List<Entity> getParameters() {
		return Collections.unmodifiableList(params);
	}

	/** Get the return type of this function. */
	public Type getReturnType() {
		return retType;
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
