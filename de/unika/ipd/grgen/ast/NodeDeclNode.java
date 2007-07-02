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

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import java.awt.Color;

/**
 * Declaration of a node.
 */
public class NodeDeclNode extends ConstraintDeclNode implements NodeCharacter {
	
	static {
		setName(NodeDeclNode.class, "node");
	}

	private static final Resolver typeResolver =
		new DeclResolver(new Class[] { NodeDeclNode.class, TypeDeclNode.class });
	
	private static final Checker typeChecker =
		new TypeChecker(NodeTypeNode.class);
	
	/**
	 * Make a new node declaration.
	 * @param id The identifier of the node.
	 * @param type The type of the node.
	 */
	public NodeDeclNode(IdentNode id, BaseNode type, BaseNode constr) {
		super(id, type, constr);
		addResolver(TYPE, typeResolver);
	}

	public NodeDeclNode(IdentNode id, BaseNode type) {
		this(id, type, TypeExprNode.getEmpty());
	}

	/**
	 * Yields a dummy <code>NodeDeclNode</code> needed for dangling edges as
	 * dummy as dummy tgt or src node, respectively.
	 */
	public static NodeDeclNode getDummy(IdentNode id, BaseNode type)
	{
		NodeDeclNode res = new NodeDeclNode(id, type) {

			public Node getNode()
			{
				return null;
			}
			
			public boolean isDummy()
			{
				return true;
			}

			public String toString()
			{
				return "a dummy node";
			}
		};
		
		res.setName("dummy node");
		return res;
	}
	
	public boolean isDummy()
	{
		return false;
	}
	
	/**
	 * The node node is ok if the decl check succeeds and
	 * the second child is a node type node or a node declaration
	 * in case the type is dynamically inherited.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(IDENT, IdentNode.class)
			&& checkChild(CONSTRAINTS, TypeExprNode.class)
			&& checkChild(TYPE, typeChecker);
	}
	
	/**
	 * @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor()
	 */
	public Color getNodeColor() {
		return Color.GREEN;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.NodeCharacter#getNode()
	 */
	public Node getNode() {
		return (Node) checkIR(Node.class);
		
	}
	
	/**
	 * The TYPE child could be a node in case the type is
	 * inherited dynamically via the typeof operator
	 */
	public BaseNode getDeclType() {
		return ((DeclNode)getChild(TYPE)).getDeclType();
	}
	
	protected boolean inheritsType() {
		return (getChild(TYPE) instanceof NodeDeclNode);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();
		
		Node res = new Node(ident.getIdent(), nt, ident.getAttributes());
		res.setConstraints(getConstraints());
		
		if( res.getConstraints().contains(res.getType()) )
			error.error(getCoords(), "self NodeType may not be contained in TypeCondition of Node ("+ res.getType() + ")");
		
		if(inheritsType()) {
			res.setTypeof((Node)getChild(TYPE).checkIR(Node.class));
		}
		
		return res;
	}
	
	public static String getKindStr() {
		return "node declaration";
	}
	public static String getUseStr() {
		return "node";
	}
}
