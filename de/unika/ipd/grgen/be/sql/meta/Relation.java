/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A SQL table. 
 */
public interface Relation extends MetaBase {

	int columnCount();
	Column getColumn(int i);
	
}
