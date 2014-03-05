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

/**
 * An index, base class for attribute index and incidence index.
 */
public abstract class Index extends Identifiable {
	/**
	 * @param name The name of the attribute index.
	 * @param ident The identifier that identifies this object.
	 */
	public Index(String name, Ident ident) {
		super(name, ident);
	}
}
