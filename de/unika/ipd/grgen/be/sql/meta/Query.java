/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;

import java.util.List;

import de.unika.ipd.grgen.util.GraphDumper;


/**
 * A query.
 */
public interface Query extends Relation {
	
	List getRelations();
	Term getCondition();
	
	void graphDump(GraphDumper dumper);

}
