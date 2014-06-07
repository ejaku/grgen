/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeEdgeProc;
import de.unika.ipd.grgen.ir.exprevals.GraphRetypeNodeProc;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node for retyping a node or an edge to a new type.
 */
public class GraphRetypeProcNode extends ProcedureInvocationBaseNode {
	static {
		setName(GraphRetypeProcNode.class, "retype procedure");
	}

	private ExprNode entity;
	private ExprNode entityType;
	
	Vector<TypeNode> returnTypes;
	
	public GraphRetypeProcNode(Coords coords, ExprNode entity,
			ExprNode entityType) {
		super(coords);
		this.entity = entity;
		becomeParent(this.entity);
		this.entityType = entityType;
		becomeParent(this.entityType);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		children.add(entityType);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		childrenNames.add("new type");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if(entity.getType() instanceof NodeTypeNode && entityType.getType() instanceof NodeTypeNode) {
			return true;
		}
		if(entity.getType() instanceof EdgeTypeNode && entityType.getType() instanceof EdgeTypeNode) {
			return true;
		}
		reportError("retype(.,.) can only retype a node to a node type, or an edge to an edge type");
		return false;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	@Override
	protected IR constructIR() {
		if(entityType.getType() instanceof NodeTypeNode) {
			GraphRetypeNodeProc retypeNode = new GraphRetypeNodeProc(
													entity.checkIR(Expression.class), 
													entityType.checkIR(Expression.class));
			for(TypeNode type : getType()) {
				retypeNode.addReturnType(type.getType());
			}
			return retypeNode;
		} else {
			GraphRetypeEdgeProc retypeEdge = new GraphRetypeEdgeProc(
													entity.checkIR(Expression.class), 
													entityType.checkIR(Expression.class));			
			for(TypeNode type : getType()) {
				retypeEdge.addReturnType(type.getType());
			}
			return retypeEdge;
		}
	}

	@Override
	public Vector<TypeNode> getType() {
		if(returnTypes==null) {
			returnTypes = new Vector<TypeNode>();
			returnTypes.add(entityType.getType());
		}
		return returnTypes;
	}
}
