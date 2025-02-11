/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.List;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.Identifiable;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A function base.
 */
public abstract class FunctionBase extends Identifiable
{
	/** The return-parameter type */
	protected Type retType = null;

	/**
	 * @param name The name of the function.
	 * @param ident The identifier that identifies this object.
	 * @param retType The return type of this function.
	 */
	public FunctionBase(String name, Ident ident, Type retType)
	{
		super(name, ident);

		this.retType = retType;
	}

	/** Get the return type of this external function. */
	public Type getReturnType()
	{
		return retType;
	}

	/** Get all parameter types of this function. */
	public abstract List<Type> getParameterTypes();
}
