/**
 * Dialect.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be.sql.meta;

import de.unika.ipd.grgen.be.sql.meta.TypeFactory;
import de.unika.ipd.grgen.be.sql.stmt.GraphTableFactory;
import de.unika.ipd.grgen.be.sql.stmt.TypeStatementFactory;
import java.util.Properties;

/**
 * An SQL dialect.
 */
public interface MetaFactory extends TypeStatementFactory, GraphTableFactory, TypeFactory {

//
//	/**
//	 * Set properties for this SQL dialect.
//	 */
//	Properties getSQLProperties();
	
}

