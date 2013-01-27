/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the incident/incoming/outgoing edges of a node.
 */
public class IncidentEdgeExprNode extends ExprNode {
	static {
		setName(IncidentEdgeExprNode.class, "incident edge expr");
	}

	private IdentNode nodeUnresolved;
	private IdentNode incidentTypeUnresolved;
	private IdentNode adjacentTypeUnresolved;

	private NodeDeclNode nodeDecl;
	private EdgeTypeNode incidentType;
	private int direction;
	private NodeTypeNode adjacentType;
	
	public static final int INCIDENT = 0;
	public static final int INCOMING = 1;
	public static final int OUTGOING = 2;
	
	public IncidentEdgeExprNode(Coords coords, IdentNode node,
			IdentNode incidentType, int direction,
			IdentNode adjacentType) {
		super(coords);
		this.nodeUnresolved = node;
		becomeParent(this.nodeUnresolved);
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
		children.add(getValidVersion(nodeUnresolved, nodeDecl));
		children.add(getValidVersion(incidentTypeUnresolved, incidentType));
		children.add(getValidVersion(adjacentTypeUnresolved, adjacentType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("node");
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
		nodeDecl = nodeResolver.resolve(nodeUnresolved, this);
		incidentType = edgeTypeResolver.resolve(incidentTypeUnresolved, this);
		adjacentType = nodeTypeResolver.resolve(adjacentTypeUnresolved, this);
		return nodeDecl!=null && incidentType!=null && adjacentType!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		// assumes that the direction:int of the AST node uses the same values as the direction of the IR expression
		return new IncidentEdgeExpr(nodeDecl.checkIR(Node.class), 
								incidentType.checkIR(EdgeType.class), direction,
								adjacentType.checkIR(NodeType.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(incidentTypeUnresolved);
	}
	
	public boolean noDefElementInCondition() {
		if(nodeDecl.defEntityToBeYieldedTo) {
			nodeDecl.reportError("A def entity ("+nodeDecl+") can't be accessed from an if");
			return false;
		}
		return true;
	}
}
