/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

/**
 * An external function.
 */
public class ExternalFunction extends Identifiable {
	/** A list of the pattern parameters */
	private final List<Type> paramTypes = new LinkedList<Type>();

	/** The return-parameter type */
	private Type retType = null;


	/**
	 * @param name The name of the external function.
	 * @param ident The identifier that identifies this object.
	 */
	public ExternalFunction(String name, Ident ident, Type retType) {
		super(name, ident);

		this.retType = retType;
	}

	/** Add a parameter type to the external function. */
	public void addParameterType(Type paramType) {
		paramTypes.add(paramType);
	}

	/** Get all parameter types of this external function. */
	public List<Type> getParameterTypes() {
		return Collections.unmodifiableList(paramTypes);
	}

	/** Get the return type of this external function. */
	public Type getReturnType() {
		return retType;
	}
}
