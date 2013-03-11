/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeEdgeExpr;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeNodeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for retyping a node or an edge to a new type.
 */
public class GraphRetypeExprNode extends ExprNode {
	static {
		setName(GraphRetypeExprNode.class, "retype expr");
	}

	private IdentNode entityUnresolved;
	private IdentNode typeUnresolved;

	private NodeDeclNode nodeDecl;
	private NodeTypeNode nodeType;

	private EdgeDeclNode edgeDecl;
	private EdgeTypeNode edgeType;
	
	public GraphRetypeExprNode(Coords coords, IdentNode node,
			IdentNode type) {
		super(coords);
		this.entityUnresolved = node;
		becomeParent(this.entityUnresolved);
		this.typeUnresolved = type;
		becomeParent(this.typeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(entityUnresolved, nodeDecl, edgeDecl));
		children.add(getValidVersion(typeUnresolved, nodeType, edgeType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("node");
		childrenNames.add("new type");
		return childrenNames;
	}

	private static final DeclarationPairResolver<EdgeDeclNode, NodeDeclNode> entityResolver =
		new DeclarationPairResolver<EdgeDeclNode, NodeDeclNode>(EdgeDeclNode.class, NodeDeclNode.class);
	private static final DeclarationTypeResolver<TypeNode> typeResolver =
		new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		Pair<EdgeDeclNode, NodeDeclNode> resolved = entityResolver.resolve(entityUnresolved, this);
		if (resolved != null) {
			edgeDecl = resolved.fst;
			nodeDecl = resolved.snd;
		}
		TypeNode typeResolved = typeResolver.resolve(typeUnresolved, this);
		if(typeResolved instanceof NodeTypeNode)
			nodeType = (NodeTypeNode)typeResolved;
		else
			edgeType = (EdgeTypeNode)typeResolved;
		return (nodeDecl!=null && nodeType!=null ) || (edgeDecl!=null && edgeType!=null) && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(nodeDecl!=null)
			return new GraphRetypeNodeExpr(nodeDecl.checkIR(Node.class), 
							nodeType.checkIR(NodeType.class),
							getType().getType());
		else
			return new GraphRetypeEdgeExpr(edgeDecl.checkIR(Edge.class), 
							edgeType.checkIR(EdgeType.class),
							getType().getType());			
	}

	@Override
	public TypeNode getType() {
		return nodeType!=null ? nodeType : edgeType;
	}
	
	public boolean noDefElementInCondition() {
		if(nodeDecl!=null && nodeDecl.defEntityToBeYieldedTo) {
			nodeDecl.reportError("A def entity ("+nodeDecl+") can't be accessed from an if");
			return false;
		}
		if(edgeDecl!=null && edgeDecl.defEntityToBeYieldedTo) {
			edgeDecl.reportError("A def entity ("+edgeDecl+") can't be accessed from an if");
			return false;
		}
		return true;
	}
}
