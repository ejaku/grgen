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
 * MarkerSource.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.meta;

import java.util.Collection;

/**
 * Something that produces markers in SQL statements.
 * Postgres uses $1, $2, ..., MySQL uses just ? signs.
 */
public interface MarkerSource {
	
	/**
	 * Get a term with a marker for prepared queries.
	 * @param datatype The type of the entity designated by the marker.
	 * @return A new marker term.
	 */
	Term nextMarker(DataType datatype);
	
	/**
	 * An array containing all types for all markers got up to now.
	 * Index the array using the number of the marker in which's type
	 * you are interested in.
	 * @return An array containing all types of gotten markers.
	 */
	Collection getTypes();
	
}

