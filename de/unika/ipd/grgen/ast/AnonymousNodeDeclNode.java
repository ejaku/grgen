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

import de.unika.ipd.grgen.ir.AnonymousNode;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.IR;

/**
 * An anonymous edge decl node.
 */
public class AnonymousNodeDeclNode extends NodeDeclNode {
	
	static {
		setName(AnonymousNodeDeclNode.class, "anonymous node");
	}
	
	/**
	 * @param n The identifier of the anonymous edge.
	 * @param e The type of the edge.
	 */
	public AnonymousNodeDeclNode(IdentNode id, BaseNode type) {
		this(id, type, TypeExprNode.getEmpty());
	}
	
	public AnonymousNodeDeclNode(IdentNode id, BaseNode type, BaseNode constr) {
		super(id, type, constr);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		TypeNode tn = (TypeNode) getDeclType();
		NodeType nt = (NodeType) tn.checkIR(NodeType.class);
		
		Node node = new AnonymousNode(getIdentNode().getIdent(), nt);
		node.setConstraints(getConstraints());
		
		return node;
	}
	
}
