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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;


/** A class representing redirections in rules. */
public class Redirection {
	public final Node from;
	public final Node to;
	public final EdgeType edgeType;
	public final NodeType nodeType;
	public final boolean incoming;
	
	public Redirection(Node from, Node to, EdgeType edgeType,
					   NodeType nodeType, boolean incoming) {
		
		this.from = from;
		this.to = to;
		this.edgeType = edgeType;
		this.nodeType = nodeType;
		this.incoming = incoming;
	}
}
