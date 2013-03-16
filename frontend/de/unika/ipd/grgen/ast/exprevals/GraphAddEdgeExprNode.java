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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphAddEdgeExpr;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding an edge to graph.
 */
public class GraphAddEdgeExprNode extends ExprNode {
	static {
		setName(GraphAddEdgeExprNode.class, "reachable edge expr");
	}

	private ExprNode edgeType;
	private ExprNode sourceNode;
	private ExprNode targetNode;
		
	public GraphAddEdgeExprNode(Coords coords, ExprNode edgeType,
			ExprNode sourceNode, ExprNode targetNode) {
		super(coords);
		this.edgeType = edgeType;
		becomeParent(this.edgeType);
		this.sourceNode = sourceNode;
		becomeParent(this.sourceNode);
		this.targetNode = targetNode;
		becomeParent(this.targetNode);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(edgeType);
		children.add(sourceNode);
		children.add(targetNode);
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return getType().resolve();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(!(edgeType.getType() instanceof EdgeTypeNode)) {
			reportError("first argument of add(.,.,.) must be an edge type");
			return false;
		}
		if(!(sourceNode.getType() instanceof NodeTypeNode)) {
			reportError("second argument of add(.,.,.) must be a node (source)");
			return false;
		}
		if(!(targetNode.getType() instanceof NodeTypeNode)) {
			reportError("third argument of add(.,.,.) must be a node (target)");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new GraphAddEdgeExpr(edgeType.checkIR(Expression.class),
								sourceNode.checkIR(Expression.class), 
								targetNode.checkIR(Expression.class), 
								getType().getType());
	}

	@Override
	public TypeNode getType() {
		return edgeType.getType();
	}
}
