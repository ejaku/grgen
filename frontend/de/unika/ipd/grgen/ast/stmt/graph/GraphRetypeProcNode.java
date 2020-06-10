/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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

	private ExprNode entity;
	private ExprNode entityType;

	Vector<TypeNode> returnTypes;

	public GraphRetypeProcNode(Coords coords, ExprNode entity, ExprNode entityType)
	{
		super(coords);
		this.entity = entity;
		becomeParent(this.entity);
		this.entityType = entityType;
		becomeParent(this.entityType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		children.add(entityType);
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
		if(entity.getType() instanceof NodeTypeNode && entityType.getType() instanceof NodeTypeNode) {
			return true;
		}
		if(entity.getType() instanceof EdgeTypeNode && entityType.getType() instanceof EdgeTypeNode) {
			return true;
		}
		reportError("retype(.,.) can only retype a node to a node type, or an edge to an edge type");
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
		entity = entity.evaluate();
		entityType = entityType.evaluate();
		if(entityType.getType() instanceof NodeTypeNode) {
			GraphRetypeNodeProc retypeNode = new GraphRetypeNodeProc(entity.checkIR(Expression.class),
					entityType.checkIR(Expression.class), entityType.getType().getType());
			return retypeNode;
		} else {
			GraphRetypeEdgeProc retypeEdge = new GraphRetypeEdgeProc(entity.checkIR(Expression.class),
					entityType.checkIR(Expression.class), entityType.getType().getType());
			return retypeEdge;
		}
	}

	@Override
	public Vector<TypeNode> getType()
	{
		if(returnTypes == null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(entityType.getType());
		}
		return returnTypes;
	}
}
