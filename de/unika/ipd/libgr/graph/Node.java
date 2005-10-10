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
 * Created on Mar 5, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A node in a graph.
 */
public interface Node {

	/**
	 * Get the incoming edges.
	 * @return An iterator iterating over all incoming edges.
	 */
	Iterator<Node> getIncoming();	

	/**
	 * Get the outgoing edges.
	 * @return An iterator iterating over all outgoing edges.
	 */
	Iterator<Node> getOutgoing();	

	/**
	 * Get the node's type
	 * @return the node type of this node.
	 */
	NodeType getType();
	
	
	
}
