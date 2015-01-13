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

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.*;

/**
 * A procedure base.
 */
public abstract class ProcedureBase extends Identifiable {
	/** A list of the return types */
	protected List<Type> returnTypes = new LinkedList<Type>();

	/**
	 * @param name The name of the procedure.
	 * @param ident The identifier that identifies this object.
	 */
	public ProcedureBase(String name, Ident ident) {
		super(name, ident);
	}

	/** Add a return type to the procedure. */
	public void addReturnType(Type returnType) {
		returnTypes.add(returnType);
	}

	/** Get all return types of this procedure. */
	public List<Type> getReturnTypes() {
		return Collections.unmodifiableList(returnTypes);
	}
	
	/** Get all parameter types of this procedure. */
	public abstract List<Type> getParameterTypes();
}
