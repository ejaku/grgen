/**
 * ManipulationStatement.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.meta;

import java.util.Collection;

/**
 * A data manipulation statement.
 */
public interface ManipulationStatement extends Statement {
	
	Table manipulatedTable();
	
	Collection manipulatedColumns();
	
}

