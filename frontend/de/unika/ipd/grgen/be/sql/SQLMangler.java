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
 * Created on Apr 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.be.sql;

import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Node;


/**
 * Someone who mangles node and edges and produces column names.
 */
public interface SQLMangler {

	/**
	 * Make an SQL table identifier out of a node.
	 * @param n The node to mangle.
	 * @return An identifier usable in SQL statements and unique for each node.
	 */
	String mangleNode(Node n);

	/**
	 * Make an SQL table identifier out of an edge.
	 * @param e The edge to mangle.
	 * @return An identifier usable in SQL statements and unique for each edge.
	 */
	String mangleEdge(Edge e);

	String mangleEntity(Entity ent);

	/**
	 * Make a SQL column expression for a node and a given column name.
	 * @param e The node.
	 * @param col The column.
	 * @return The column expression.
	 */
	// public String getNodeCol(Node n, String col);

	/**
	 * Mangle an identifiable object to a valid SQL identifier.
	 * @param id The identifiable object.
	 * @return A valid SQL identifier.
	 */
	// public String getEdgeCol(Edge e, String col);

	// public String getEntityCol(Entity e, String col);

}
