/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.meta;


/**
 * A join.
 */
public interface Join extends Relation {

	int INNER = 0;
	int LEFT_OUTER = 1;
	int RIGHT_OUTER = 2;

	Relation getLeft();
	Relation getRight();
	Term getCondition();
	void setCondition(Term term);
	
}
