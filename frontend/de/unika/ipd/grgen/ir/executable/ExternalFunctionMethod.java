/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * An external function method.
 */
public class ExternalFunctionMethod extends ExternalFunction
{
	/** The owner of the function method. */
	protected Type owner = null;

	/**
	 * @param name The name of the external function.
	 * @param ident The identifier that identifies this object.
	 */
	public ExternalFunctionMethod(String name, Ident ident, Type retType)
	{
		super(name, ident, retType);
	}

	public Type getOwner()
	{
		return owner;
	}

	public void setOwner(Type type)
	{
		owner = type;
	}
}
