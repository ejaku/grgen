/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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
