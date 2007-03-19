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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An edge in a graph.
 */
import de.unika.ipd.grgen.util.Attributes;

public class Edge extends ConstraintEntity {
	
	/**
	 * Make a new edge.
	 * @param ident The identifier for the edge.
	 * @param type The type of the edge.
	 * @param Is the edge nedgated.
	 * @param attr Some attributes.
	 */
	public Edge(Ident ident, EdgeType type, Attributes attr) {
		super("edge", ident, type, attr);
	}
	
	//  public Edge(Ident ident, EdgeType type) {
//		this(ident, type, EmptyAttributes.get());
	//  }
	
	/**
	 * Get the edge type.
	 * @return The type of the edge.
	 */
	public EdgeType getEdgeType() {
		assert getType() instanceof EdgeType : "type of edge must be edge type";
		return (EdgeType) getType();
	}
	
}
