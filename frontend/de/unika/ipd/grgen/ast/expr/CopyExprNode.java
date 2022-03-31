/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
import de.unika.ipd.grgen.ast.type.MatchTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.basic.GraphTypeNode;
import de.unika.ipd.grgen.ast.type.basic.ObjectTypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.CopyExpr;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the copy of a subgraph, or a match, or a container.
 */
public class CopyExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(CopyExprNode.class, "copy expr");
	}

	private ExprNode sourceExpr;
	private boolean deep;

	public CopyExprNode(Coords coords, ExprNode sourceExpr, boolean deep)
	{
		super(coords);
		this.sourceExpr = sourceExpr;
		becomeParent(this.sourceExpr);
		this.deep = deep;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(sourceExpr);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("source expression");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode type = sourceExpr.getType();
		if(deep) {
			if(!(type instanceof GraphTypeNode)
					&& !(type instanceof ContainerTypeNode)
					&& !(type instanceof InternalObjectTypeNode)
					&& !(type instanceof InternalTransientObjectTypeNode)
					&& !(type instanceof ExternalObjectTypeNode)
					&& !(type instanceof ObjectTypeNode)) {
				sourceExpr.reportError("container or graph or class object or transient class object or external object expected as argument to copy");
				return false;
			}
		} else {
			if(!(type instanceof MatchTypeNode)
					&& !(type instanceof ContainerTypeNode)
					&& !(type instanceof InternalObjectTypeNode)
					&& !(type instanceof InternalTransientObjectTypeNode)
					&& !(type instanceof ExternalObjectTypeNode)
					&& !(type instanceof ObjectTypeNode)) {
				sourceExpr.reportError("container or match or class object or transient class object or external object expected as argument to clone");
				return false;
			}
		}
		return true;
	}

	@Override
	protected IR constructIR()
	{
		sourceExpr = sourceExpr.evaluate();
		return new CopyExpr(sourceExpr.checkIR(Expression.class), getType().getType(), deep);
	}

	@Override
	public TypeNode getType()
	{
		if(sourceExpr.getType() instanceof MatchTypeNode
				|| sourceExpr.getType() instanceof ContainerTypeNode
				|| sourceExpr.getType() instanceof InternalObjectTypeNode
				|| sourceExpr.getType() instanceof InternalTransientObjectTypeNode
				|| sourceExpr.getType() instanceof ExternalObjectTypeNode
				|| sourceExpr.getType() instanceof ObjectTypeNode)
			return sourceExpr.getType();
		else
			return BasicTypeNode.graphType;
	}
}
