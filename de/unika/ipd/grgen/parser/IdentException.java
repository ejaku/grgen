/**
 * @file IdentException.java
 * @author Sebastian Hack
 * @date Jul 13, 2003
 */
package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.ast.IdentNode;

/**
 * An exception concerning an identifier. 
 */
public class IdentException extends CoordsException {
	
	/**
	 * @param id The identifier for which the error occurred.
	 * @param msg The message that describes the error
	 * @param filename The filename, the error occurred in. 
	 * @param coords The coordinates where the error happened.
	 */
	public IdentException(IdentNode id, String msg, Coords coords) {
		super(id.getSymDef().symbol + "(defined here: " 
		  + id.getSymDef().coords + "): " + msg, coords); 
	}
}
