/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeEdgeProc;
import de.unika.ipd.grgen.ir.stmt.graph.GraphRetypeNodeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for retyping a node or an edge to a new type.
 */
public class GraphRetypeProcNode extends BuiltinProcedureInvocationBaseNode
{
	static {
		setName(GraphRetypeProcNode.class, "retype procedure");
	}

	private ExprNode entityExpr;
	private ExprNode entityTypeExpr;

	Vector<TypeNode> returnTypes;

	public GraphRetypeProcNode(Coords coords, ExprNode entity, ExprNode entityType)
	{
		super(coords);
		this.entityExpr = entity;
		becomeParent(this.entityExpr);
		this.entityTypeExpr = entityType;
		becomeParent(this.entityTypeExpr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entityExpr);
		children.add(entityTypeExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		childrenNames.add("new type");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode entityExprType = entityExpr.getType();
		TypeNode entityTypeExprType = entityTypeExpr.getType();
		if(entityExprType instanceof NodeTypeNode && entityTypeExprType instanceof NodeTypeNode) {
			return true;
		}
		if(entityExprType instanceof EdgeTypeNode && entityTypeExprType instanceof EdgeTypeNode) {
			return true;
		}
		reportError("The retype procedure expects as 1. argument (node) a value of type Node and as 2. argument (nodeType) a value of type node type,"
				+ " or as 1. argument (edge) a value of type Edge and as 2. argument (edgeType) a value of type edge type "
				+ " (but is given values of type " + entityExprType.toStringWithDeclarationCoords()
				+ " and " + entityTypeExprType.toStringWithDeclarationCoords() + ").");
		return false;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		entityExpr = entityExpr.evaluate();
		entityTypeExpr = entityTypeExpr.evaluate();
		if(entityTypeExpr.getType() instanceof NodeTypeNode) {
			GraphRetypeNodeProc retypeNode = new GraphRetypeNodeProc(entityExpr.checkIR(Expression.class),
					entityTypeExpr.checkIR(Expression.class), entityTypeExpr.getType().getType());
			return retypeNode;
		} else {
			GraphRetypeEdgeProc retypeEdge = new GraphRetypeEdgeProc(entityExpr.checkIR(Expression.class),
					entityTypeExpr.checkIR(Expression.class), entityTypeExpr.getType().getType());
			return retypeEdge;
		}
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(entityTypeExpr.getType());
		}
		return returnTypes;
	}
}
