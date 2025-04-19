/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.Graphof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the containing graph of some node/edge.
 */
public class GraphofExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(GraphofExprNode.class, "graphof");
	}

	private ExprNode entity;

	public GraphofExprNode(Coords coords, ExprNode entity)
	{
		super(coords);
		this.entity = entity;
		becomeParent(this.entity);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		if(entity.getType() instanceof NodeTypeNode || entity.getType() instanceof EdgeTypeNode) {
			if(!UnitNode.getRoot().getModel().IsGraphofDefined()) {
				String nodeOrEdge = entity.getType() instanceof NodeTypeNode ? "node" : "edge";
				reportError("The function graphof applied to an argument of " + nodeOrEdge + " type expects a model with graph containment support, but the required node edge graph; declaration is missing in the model specification.");
				return false;
			}
			return true;
		}

		reportError("The function graphof expects as argument (entityToFetchContainingGraphOf) a value of type node or edge"
				+ " (but is given a value of type " + entity.getType().getTypeName() + ").");
		return false;
	}

	@Override
	protected IR constructIR()
	{
		entity = entity.evaluate();
		return new Graphof(entity.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.graphType;
	}
}
