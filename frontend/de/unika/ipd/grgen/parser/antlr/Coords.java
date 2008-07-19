/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser.antlr;

/**
 * Coordinates more suitable for an ANTLR parser.
 */
public class Coords extends de.unika.ipd.grgen.parser.Coords {

	/**
	 * Construct coordinates from an ANTLR token. The filename is set
	 * to the default filename.
	 * @param tok The ANTLR token.
	 */
	public Coords(antlr.Token tok) {
		super(tok.getLine(), tok.getColumn());
	}

	/**
	 * Get the coordinates from an ANTLR recognition exception.
	 * @param e The ANTLR recognition exception.
	 */
	public Coords(antlr.RecognitionException e) {
		super(e.getLine(), e.getColumn(), e.getFilename());
	}

	public Coords(antlr.Token tok, antlr.Parser parser) {
		this(tok);
		filename = parser.getFilename();
	}


}
