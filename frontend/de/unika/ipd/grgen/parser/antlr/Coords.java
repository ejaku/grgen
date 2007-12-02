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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.parser.antlr;

/**
 * Coordinates more suitable for an ANTLR parser.
 */
public class Coords extends de.unika.ipd.grgen.parser.Coords {

	/**
	 * Construct coordinates from an ANTLR token. The filename is set
	 * to the default filename.
	 * @param tok The ANTLR token.
	 */
	public Coords(antlr.Token tok) {
		super(tok.getLine(), tok.getColumn());
	}
	
	/**
	 * Get the coordinates from an ANTLR recognition exception.
	 * @param e The ANTLR recognition exception.
	 */
	public Coords(antlr.RecognitionException e) {
		super(e.getLine(), e.getColumn(), e.getFilename());
	}

	public Coords(antlr.Token tok, antlr.Parser parser) {
		this(tok);
		filename = parser.getFilename();
	}


}
