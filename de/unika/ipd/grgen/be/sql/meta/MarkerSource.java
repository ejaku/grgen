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

