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
package de.unika.ipd.grgen.ir;

import java.util.Set;

public class Constant extends Expression {
	
	/** The value of the constant. */
	protected Object value;
	
	/**
	 * @param type The type of the constant.
	 * @param value The value of the constant.
	 */
	public Constant(Type type, Object value) {
		super("constant", type);
		this.value = value;
	}
	
	/** @return The value of the constant. */
	public Object getValue() {
		return value;
	}
	
	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeLabel() */
	public String getNodeLabel() {
		return getName() + " " + value;
	}
	
	/** @see de.unika.ipd.grgen.ir.Expression#collectNodesnEdges() */
	public void collectNodesnEdges(Set<Node> nodes, Set<Edge> edges) {}
}
