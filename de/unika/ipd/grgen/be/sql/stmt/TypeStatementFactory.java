/*
  GrGen: graph rewrite generator tool.
  Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

  This library is free software; you can redistribute it and/or
  modify it under the terms of the GNU Lesser General Public
  License as published by the Free Software Foundation; either
  version 2.1 of the License, or (at your option) any later version.

  This library is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
  Lesser General Public License for more details.

  You should have received a copy of the GNU Lesser General Public
  License along with this library; if not, write to the Free Software
  Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
*/


/**
 * Created on Apr 7, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql.stmt;

import de.unika.ipd.grgen.be.TypeID;
import de.unika.ipd.grgen.be.sql.meta.StatementFactory;
import de.unika.ipd.grgen.be.sql.meta.Term;
import de.unika.ipd.grgen.ir.GraphEntity;
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
	Term isA(TypeIdTable table, NodeType node, TypeID typeID);
	
	/**
	 * @see #isA(TypeIdTable, NodeType, GraphTableFactory, TypeID)
	 * The same for edges here.
	 */
	Term isA(TypeIdTable table, EdgeType edge, TypeID typeID);

	Term isA(TypeIdTable table, GraphEntity ent, boolean isNode, TypeID typeID);
	
}
	
