/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A table.
 */
public interface Table extends Relation, Declared, Aliased {
 
	String getName();
	
}
