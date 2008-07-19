/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import antlr.SemanticException;

public class CoordsException extends SemanticException {
	/**
	 *
	 */
	private static final long serialVersionUID = -591773510258505385L;

	public CoordsException(String msg, Coords c) {
		super(msg, c.filename, c.line, c.col);
	}
}
