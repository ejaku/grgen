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
	
	public SymbolTableException(String text) {
		super(text);
	}
	
	public SymbolTableException(Coords coords, String text) {
		this(coords + ": " + text);
	}
	
}

