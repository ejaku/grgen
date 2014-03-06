/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * An external procedure.
 */
public class ExternalProcedure extends ProcedureBase {
	/** A list of the pattern parameters */
	private final List<Type> paramTypes = new LinkedList<Type>();


	/**
	 * @param name The name of the external procedure.
	 * @param ident The identifier that identifies this object.
	 */
	public ExternalProcedure(String name, Ident ident) {
		super(name, ident);
	}

	/** Add a parameter type to the external procedure. */
	public void addParameterType(Type paramType) {
		paramTypes.add(paramType);
	}

	/** Get all parameter types of this external procedure. */
	public List<Type> getParameterTypes() {
		return Collections.unmodifiableList(paramTypes);
	}
}
