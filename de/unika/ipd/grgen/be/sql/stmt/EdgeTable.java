/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Column;


/**
 * The edge table.
 */
public interface EdgeTable extends TypeIdTable {

	Column colSrcId();
	
	Column colTgtId();
	
	Column colEndId(boolean src);
	
}
