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
import de.unika.ipd.grgen.ir.exprevals.GraphAddEdgeComp;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding an edge to graph.
 */
public class GraphAddEdgeCompNode extends ComputationInvocationBaseNode {
	static {
		setName(GraphAddEdgeCompNode.class, "reachable edge computation");
	}

	private ExprNode edgeType;
	private ExprNode sourceNode;
	private ExprNode targetNode;
	
	Vector<TypeNode> returnTypes;
		
	public GraphAddEdgeCompNode(Coords coords, ExprNode edgeType,
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

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		GraphAddEdgeComp addEdge = new GraphAddEdgeComp(edgeType.checkIR(Expression.class),
										sourceNode.checkIR(Expression.class), 
										targetNode.checkIR(Expression.class));
		for(TypeNode type : getType()) {
			addEdge.addReturnType(type.getType());
		}
		return addEdge;
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(edgeType.getType());
		}
		return returnTypes;
	}
}
