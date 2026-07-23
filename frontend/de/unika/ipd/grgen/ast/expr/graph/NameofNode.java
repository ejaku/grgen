/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.List;
import java.util.ArrayList;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.Nameof;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the name of some node/edge or the graph.
 */
public class NameofNode extends ExprNode
{
	static {
		setClassName(NameofNode.class, "nameof");
	}

	private ExprNode namedEntity; // null if name of main graph is requested

	public NameofNode(Coords coords, ExprNode namedEntity)
	{
		super(coords);
		this.namedEntity = namedEntity;
		becomeParent(this.namedEntity);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		List<BaseNode> children = new ArrayList<BaseNode>();
		if(namedEntity != null)
			children.add(namedEntity);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		List<String> childrenNames = new ArrayList<String>();
		if(namedEntity != null)
			childrenNames.add("named entity");
		return childrenNames;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		if(namedEntity != null) {
			if(namedEntity.getType().isEqual(BasicTypeNode.graphType)) {
				return true;
			}
			if(namedEntity.getType() instanceof EdgeTypeNode) {
				return true;
			}
			if(namedEntity.getType() instanceof NodeTypeNode) {
				return true;
			}

			reportError("The function nameof expects as argument (entityToFetchNameOf) a value of type node or edge or graph"
					+ " (but is given a value of type " + namedEntity.getType().getTypeName() + ").");
			return false;
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		if(namedEntity == null) {
			return new Nameof(null, getType().getIRType());
		}
		namedEntity = namedEntity.evaluate();
		return new Nameof(namedEntity.checkIR(Expression.class), getType().getIRType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.stringType;
	}
}
