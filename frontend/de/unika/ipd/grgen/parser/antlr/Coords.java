/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser.antlr;

import org.antlr.runtime.CharStream;

/**
 * Coordinates more suitable for an ANTLR parser.
 */
public class Coords extends de.unika.ipd.grgen.parser.Coords {

	/**
	 * Construct coordinates from an ANTLR token.
	 * @param tok The ANTLR token.
	 */
	public Coords(org.antlr.runtime.Token tok) {
		if(tok!=null) {
			line = tok.getLine();
			col = tok.getCharPositionInLine();

			CharStream stream = tok.getInputStream();
			if (stream != null) {
				filename = tok.getInputStream().getSourceName();
			}
		}
	}

	/**
	 * Get the coordinates from an ANTLR recognition exception.
	 * @param e The ANTLR recognition exception.
	 */
	public Coords(org.antlr.runtime.RecognitionException e) {
		if(e!=null) {
			line = e.line;
			col = e.charPositionInLine;
			if(e.input!=null) {
				filename = e.input.getSourceName();
			}
		}
	}
}
