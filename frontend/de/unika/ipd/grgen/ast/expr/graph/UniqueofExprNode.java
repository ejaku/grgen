/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.expr.graph.Uniqueof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the name of some node/edge or the graph.
 */
public class UniqueofExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(UniqueofExprNode.class, "uniqueof");
	}

	private ExprNode entity;

	public UniqueofExprNode(Coords coords, ExprNode entity)
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
		if(entity != null)
			children.add(entity);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		if(entity != null)
			childrenNames.add("entity");
		return childrenNames;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		if(entity != null) {
			if(entity.getType().isEqual(BasicTypeNode.graphType)) {
				return true;
			}
			if(entity.getType() instanceof EdgeTypeNode) {
				return true;
			}
			if(entity.getType() instanceof NodeTypeNode) {
				return true;
			}

			reportError("uniqueof(.) expects an entity of node or edge or subgraph type");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(entity == null) {
			return new Uniqueof(null, getType().getType());
		}
		return new Uniqueof(entity.checkIR(Expression.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}
}
