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
public interface Query extends Relation, Statement {
	
	List getRelations();
	Term getCondition();
	void setCondition(Term cond);
	void clearColumns();
	
	void graphDump(GraphDumper dumper);

}
