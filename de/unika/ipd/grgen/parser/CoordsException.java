/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import antlr.SemanticException;

public class CoordsException extends SemanticException {
	public CoordsException(String msg, Coords c) {
		super(msg, c.filename, c.line, c.col);
	}
}
