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
 * @author G. Veit Batz
 * @version $Id: ActionDeclNode.java 9940 2005-10-10 12:56:19Z rubino $
 *
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;

public class Cast extends Expression
{
	protected Expression expr;
	
	public Cast(Type type, Expression expr) {
		super("cast", type);
		this.expr = expr;
	}

	public String getNodeLabel() {
		return "Cast to " + type;
	}
	
	public Expression getExpression() {
		return expr;
	}

	public Collection<Expression> getWalkableChildren() {
		Vector<Expression> vec = new Vector<Expression>();
		vec.add(expr);
		return vec;
	}

	/**
	 * Method collectNodesnEdges extracts the nodes and edges occuring in this Expression.
	 * @param    nodes               a  Set to contain the nodes of cond
	 * @param    edges               a  Set to contain the edges of cond
	 * @param    cond                an Expression
	 */
	public void collectNodesnEdges(Set<Node> nodes, Set<Edge> edges) {
		getExpression().collectNodesnEdges(nodes, edges);
	}
}

