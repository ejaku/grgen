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
import de.unika.ipd.grgen.ir.exprevals.GraphAddEdgeExpr;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding an edge to graph.
 */
public class GraphAddEdgeExprNode extends ExprNode {
	static {
		setName(GraphAddEdgeExprNode.class, "reachable edge expr");
	}

	private IdentNode edgeTypeUnresolved;
	private IdentNode sourceNodeUnresolved;
	private IdentNode targetNodeUnresolved;

	private EdgeTypeNode edgeType;
	private NodeDeclNode sourceNodeDecl;
	private NodeDeclNode targetNodeDecl;
		
	public GraphAddEdgeExprNode(Coords coords, IdentNode edgeType,
			IdentNode sourceNode, IdentNode targetNode) {
		super(coords);
		this.edgeTypeUnresolved = edgeType;
		becomeParent(this.edgeTypeUnresolved);
		this.sourceNodeUnresolved = sourceNode;
		becomeParent(this.sourceNodeUnresolved);
		this.targetNodeUnresolved = targetNode;
		becomeParent(this.targetNodeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(edgeTypeUnresolved, edgeType));
		children.add(getValidVersion(sourceNodeUnresolved, sourceNodeDecl));
		children.add(getValidVersion(targetNodeUnresolved, targetNodeDecl));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("edge type");
		childrenNames.add("source node");
		childrenNames.add("target node");
		return childrenNames;
	}

	private static final DeclarationTypeResolver<EdgeTypeNode> edgeTypeResolver =
		new DeclarationTypeResolver<EdgeTypeNode>(EdgeTypeNode.class);
	private static final DeclarationResolver<NodeDeclNode> nodeResolver =
		new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		edgeType = edgeTypeResolver.resolve(edgeTypeUnresolved, this);
		sourceNodeDecl = nodeResolver.resolve(sourceNodeUnresolved, this);
		targetNodeDecl = nodeResolver.resolve(targetNodeUnresolved, this);
		return edgeType!=null && sourceNodeDecl!=null && targetNodeDecl!=null && getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new GraphAddEdgeExpr(edgeType.checkIR(EdgeType.class),
								sourceNodeDecl.checkIR(Node.class), 
								targetNodeDecl.checkIR(Node.class), 
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return edgeType;
	}
}
