/**
 * SQLDialectSettings.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.meta;

import de.unika.ipd.grgen.be.sql.meta.TypeFactory;

public interface Dialect extends TypeFactory, MarkerSourceFactory {
	
	/**
	 * Get a boolean constant opcode.
	 * @param value The desired value of the opcode ("true" or "false").
	 * @return An opcode representing value given.
	 */
	Op constantOpcode(boolean value);
	
}

