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
	 * Construct coordinates from an antlr token. The filename is set
	 * to the default filename.
	 * @param tok The antlr token.
	 */
	public Coords(antlr.Token tok) {
		super(tok.getLine(), tok.getColumn());
	}
  
	/**
	 * Get the coordinates from an antlr recognition exception.
	 * @param e The antlr recognition exception.
	 */
	public Coords(antlr.RecognitionException e) {
		super(e.getLine(), e.getColumn(), e.getFilename());
	}

	public Coords(antlr.Token tok, antlr.Parser parser) {
		this(tok);
		filename = parser.getFilename();
	}


}
