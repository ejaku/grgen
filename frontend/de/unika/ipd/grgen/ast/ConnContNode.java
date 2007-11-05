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
package de.unika.ipd.grgen.ast;

/**
 * AST node for ConnCont rule
 * These nodes should never appear in the final ast.
 */
public class ConnContNode extends BaseNode {

	/**
	 * New conn cont node
	 * @param edge The edge
	 * @param node The node
	 */
	public ConnContNode(BaseNode edge, BaseNode node) {
		super();
		addChild(edge);
		addChild(node);
	}
	
	protected boolean check() {
		reportError("Should never appear in the AST");
		checkChild(0, EdgeDeclNode.class);
		checkChild(1, NodeDeclNode.class);
		return false;
	}
	
	public BaseNode getNode() {
		return getChild(1);
	}
	
	public BaseNode getEdge() {
		return getChild(0);
	}

}
