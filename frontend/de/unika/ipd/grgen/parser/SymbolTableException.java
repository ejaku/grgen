/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * SymbolTableException.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.parser;

/**
 * A symbol table exception.
 */
public class SymbolTableException extends Exception {

	/**
	 *
	 */
	private static final long serialVersionUID = -7291849597287733435L;

	public SymbolTableException(String text) {
		super(text);
	}

	public SymbolTableException(Coords coords, String text) {
		this(coords + ": " + text);
	}

}

