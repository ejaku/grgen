/**
 * Created on Apr 13, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.meta.Column;
import de.unika.ipd.grgen.ir.Entity;


/**
 * A table holding entities.
 */
public interface AttributeTable extends IdTable {

	Column colEntity(Entity ent);
	
}
