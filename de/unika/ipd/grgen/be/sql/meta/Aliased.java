/**
 * Created on Apr 18, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * Something for which an alias name exists.
 */
public interface Aliased {

	/**
	 * Get the alias name of the aliased object.
	 * @return The alias name.
	 */
	String getAliasName();
	
}
