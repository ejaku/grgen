/**
 * @file CoordsException.java
 * @author shack
 * @date Jul 13, 2003
 */
package de.unika.ipd.grgen.parser;

import antlr.SemanticException;

/**
 * 
 */
public class CoordsException extends SemanticException {
	public CoordsException(String msg, Coords c) {
		super(msg, c.filename, c.line, c.col);
	}
}
