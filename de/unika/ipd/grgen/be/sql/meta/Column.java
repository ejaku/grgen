/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A column.
 */
public interface Column extends MetaBase, Declared, Aliased {

	Relation getRelation();
	DataType getType();
	
}
