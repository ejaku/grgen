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
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Set;

import de.unika.ipd.grgen.ir.Graph;

/**
 * Something that looks like a connection.
 * @see de.unika.ipd.grgen.ast.ConnectionNode
 */
public interface ConnectionCharacter {

	/**
	 * Add all nodes of this connection to a set.
	 * @param set The set.
	 */
	public void addNodes(Set<BaseNode> set);
	
	/**
	 * Add all edges of this connection to a set.
	 * @param set The set.
	 */
	public void addEdge(Set<BaseNode> set);

	public EdgeCharacter getEdge();

	public NodeCharacter getSrc();
	
	public void setSrc(NodeCharacter src);
	
	public NodeCharacter getTgt();
	
	public void setTgt(NodeCharacter tgt);

	/**
	 * Add this connection character to an IR graph.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr);
	
}
