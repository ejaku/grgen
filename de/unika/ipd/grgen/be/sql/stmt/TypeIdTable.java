/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Column;


/**
 * A table which holds typed objects.
 */
public interface TypeIdTable extends IdTable {

	Column colTypeId();
	
}
