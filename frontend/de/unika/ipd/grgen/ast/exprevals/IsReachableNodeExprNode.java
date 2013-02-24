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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.IsReachableNodeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Am ast node telling whether an end node can be reached from a start node, via incoming/outgoing/incident edges of given type, from/to a node of given type.
 */
public class IsReachableNodeExprNode extends ExprNode {
	static {
		setName(IsReachableNodeExprNode.class, "is reachable node expr");
	}

	private IdentNode startNodeUnresolved;
	private IdentNode endNodeUnresolved;
	private IdentNode incidentTypeUnresolved;
	private IdentNode adjacentTypeUnresolved;

	private NodeDeclNode startNodeDecl;
	private NodeDeclNode endNodeDecl;
	private EdgeTypeNode incidentType;
	private int direction;
	private NodeTypeNode adjacentType;
	
	public static final int ADJACENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;
	
	public IsReachableNodeExprNode(Coords coords, 
			IdentNode startNode, IdentNode endNode,
			IdentNode incidentType, int direction,
			IdentNode adjacentType) {
		super(coords);
		this.startNodeUnresolved = startNode;
		becomeParent(this.startNodeUnresolved);
		this.endNodeUnresolved = endNode;
		becomeParent(this.endNodeUnresolved);
		this.incidentTypeUnresolved = incidentType;
		becomeParent(this.incidentTypeUnresolved);
		this.direction = direction;
		this.adjacentTypeUnresolved = adjacentType;
		becomeParent(this.adjacentTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(startNodeUnresolved, startNodeDecl));
		children.add(getValidVersion(endNodeUnresolved, endNodeDecl));
		children.add(getValidVersion(incidentTypeUnresolved, incidentType));
		children.add(getValidVersion(adjacentTypeUnresolved, adjacentType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("start node");
		childrenNames.add("end node");
		childrenNames.add("incident type");
		childrenNames.add("adjacent type");
		return childrenNames;
	}

	private static final DeclarationResolver<NodeDeclNode> nodeResolver =
		new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
	private static final DeclarationTypeResolver<EdgeTypeNode> edgeTypeResolver =
		new DeclarationTypeResolver<EdgeTypeNode>(EdgeTypeNode.class);
	private static final DeclarationTypeResolver<NodeTypeNode> nodeTypeResolver =
		new DeclarationTypeResolver<NodeTypeNode>(NodeTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		startNodeDecl = nodeResolver.resolve(startNodeUnresolved, this);
		endNodeDecl = nodeResolver.resolve(endNodeUnresolved, this);
		incidentType = edgeTypeResolver.resolve(incidentTypeUnresolved, this);
		adjacentType = nodeTypeResolver.resolve(adjacentTypeUnresolved, this);
		return startNodeDecl!=null && endNodeDecl!=null && incidentType!=null && adjacentType!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IsReachableNodeExpr(startNodeDecl.checkIR(Node.class),
								endNodeDecl.checkIR(Node.class), 
								incidentType.checkIR(EdgeType.class), direction,
								adjacentType.checkIR(NodeType.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BooleanTypeNode.booleanType;
	}
	
	public boolean noDefElementInCondition() {
		if(startNodeDecl.defEntityToBeYieldedTo) {
			startNodeDecl.reportError("A def entity ("+startNodeDecl+") can't be accessed from an if");
			return false;
		}
		if(endNodeDecl.defEntityToBeYieldedTo) {
			endNodeDecl.reportError("A def entity ("+endNodeDecl+") can't be accessed from an if");
			return false;
		}
		return true;
	}
}
