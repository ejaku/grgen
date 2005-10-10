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
 * Created on Mar 8, 2004
 *
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.libgr.graph;

import java.util.Iterator;


/**
 * A type model for graphs.
 */
public interface TypeModel {
	
	/**
	 * Get the root type of the nodes.
	 * @return The node type root.
	 */
	NodeType getNodeRootType();
	
	/**
	 * Get the root type of the edges.
	 * @return The edge type root.
	 */
	EdgeType getEdgeRootType();
	
	/**
	 * Get all node types.
	 * @return An iterator iterating over all node types.
	 */
	Iterator<NodeType> getNodeTypes();

	/**
	 * Get all edge types.
	 * @return An iterator iterating over all edge types.
	 */
	Iterator<EdgeType> getEdgeTypes();

}
