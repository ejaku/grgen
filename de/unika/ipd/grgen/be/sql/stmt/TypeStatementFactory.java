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
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.NodeType;


/**
 * A SQL statement factory extended by the ability of generating
 * type constraint expressions.
 */
public interface TypeStatementFactory extends StatementFactory {

	/**
	 * Make an SQL term that expresses that a node is of a certain type.
	 * @param table The table which represents the node.
	 * @param nt The node type.
	 * @param typeID The one who given integer IDs for types.
	 */
	Term isA(TypeIdTable table, NodeType nt, TypeID typeID);
	
	/**
	 * @see #isA(TypeIdTable, NodeType, GraphTableFactory, TypeID)
	 * The same for edges here.
	 */
	Term isA(TypeIdTable table, EdgeType et, TypeID typeID);

}
	
