/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
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
