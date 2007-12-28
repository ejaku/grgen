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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util.report;

/**
 * Represents a location known to the user, where a message can be
 * generated. For example, in parser, the file and line number in 
 * the file that is parsed. 
 */
public interface Location {

	/**
	 * Get the location's string representation.
	 * This string is only meaningful, if #hasLocation() returns true. 
	 * @return The string representation of the location.
	 */
	String getLocation();

	/**
	 * Checks, if the location is valid. If it is valid, #getLocation()
	 * returns a valid location string, if not, the string returned by
	 * #getLocation() is to be ignored.
	 * @return true, if the location is valid.
	 */
	boolean hasLocation();
}
