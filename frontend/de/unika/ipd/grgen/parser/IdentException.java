/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @date Jul 13, 2003
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser;

import de.unika.ipd.grgen.ast.IdentNode;

/**
 * An exception concerning an identifier.
 */
public class IdentException extends CoordsException {

	/**
	 *
	 */
	private static final long serialVersionUID = 104818439326249393L;

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
