/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.stmt.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.graph.GraphAddCopyEdgeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for adding a copy of an edge to graph.
 */
public class GraphAddCopyEdgeProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GraphAddCopyEdgeProcNode.class, "graph add copy edge procedure");
	}

	private ExprNode oldEdge;
	private ExprNode sourceNode;
	private ExprNode targetNode;

	Vector<TypeNode> returnTypes;

	private boolean deep;

	public GraphAddCopyEdgeProcNode(Coords coords, ExprNode edgeType, ExprNode sourceNode, ExprNode targetNode, boolean deep)
	{
		super(coords);
		this.oldEdge = edgeType;
		becomeParent(this.oldEdge);
		this.sourceNode = sourceNode;
		becomeParent(this.sourceNode);
		this.targetNode = targetNode;
		becomeParent(this.targetNode);
		this.deep = deep;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(oldEdge);
		children.add(sourceNode);
		children.add(targetNode);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("old edge");
		childrenNames.add("source node");
		childrenNames.add("target node");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode oldEdgeType = oldEdge.getType();
		if(!(oldEdgeType instanceof EdgeTypeNode)) {
			reportError("The addCopy procedure expects as 1. argument (oldEdge)"
					+ " a value of type Edge"
					+ " (but is given a value of type " + oldEdgeType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode sourceNodeType = sourceNode.getType();
		if(!(sourceNodeType instanceof NodeTypeNode)) {
			reportError("The addCopy procedure expects as 2. argument (sourceNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + sourceNodeType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode targetNodeType = targetNode.getType();
		if(!(targetNodeType instanceof NodeTypeNode)) {
			reportError("The addCopy procedure expects as 3. argument (targetNode)"
					+ " a value of type Node"
					+ " (but is given a value of type " + targetNodeType.toStringWithDeclarationCoords() + ").");
			return false;
		}
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		oldEdge = oldEdge.evaluate();
		sourceNode = sourceNode.evaluate();
		targetNode = targetNode.evaluate();
		GraphAddCopyEdgeProc addCopyEdge = new GraphAddCopyEdgeProc(oldEdge.checkIR(Expression.class),
				sourceNode.checkIR(Expression.class), targetNode.checkIR(Expression.class),
				oldEdge.getType().getType(), deep);
		return addCopyEdge;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(oldEdge.getType());
		}
		return returnTypes;
	}
}
