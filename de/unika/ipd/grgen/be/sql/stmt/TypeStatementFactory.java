/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.sql.TypeID;
import de.unika.ipd.grgen.be.sql.meta.StatementFactory;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Node;


/**
 * A SQL statement factory extended by the ability of generating 
 * type constraint expressions.
 */
public interface TypeStatementFactory extends StatementFactory {

	Term isA(Node node, GraphTableFactory factory, TypeID typeID);
	Term isA(Edge edge, GraphTableFactory factory, TypeID typeID);

}
	
