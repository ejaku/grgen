/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id: NameofNode.java 19421 2008-04-28 17:07:35Z eja $
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IncidentEdgeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the incoming/outgoing edges of a node.
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
	private boolean outgoing;
	private NodeTypeNode adjacentType;
	
	private SetTypeNode setType;

	public IncidentEdgeExprNode(Coords coords, IdentNode node,
			IdentNode incidentType, boolean outgoing,
			IdentNode adjacentType) {
		super(coords);
		this.nodeUnresolved = node;
		becomeParent(this.nodeUnresolved);
		this.incidentTypeUnresolved = incidentType;
		becomeParent(this.incidentTypeUnresolved);
		this.outgoing = outgoing;
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
		return new IncidentEdgeExpr(nodeDecl.checkIR(Node.class), 
								incidentType.checkIR(EdgeType.class), outgoing,
								adjacentType.checkIR(NodeType.class),
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return SetTypeNode.getSetType(incidentTypeUnresolved);
	}
}
