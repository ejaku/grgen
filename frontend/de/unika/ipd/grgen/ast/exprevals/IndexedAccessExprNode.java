/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.containers.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.IndexedAccessExpr;
import de.unika.ipd.grgen.parser.Coords;

public class IndexedAccessExprNode extends ExprNode
// MAP TODO: hieraus einen operator machen
{
	static {
		setName(IndexedAccessExprNode.class, "indexed access expression");
	}

	private ExprNode targetExpr;
	private ExprNode keyExpr;

	public IndexedAccessExprNode(Coords coords, ExprNode targetExpr, ExprNode keyExpr)
	{
		super(coords);
		this.targetExpr = becomeParent(targetExpr);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = targetExpr.getType();
		if(!(targetType instanceof MapTypeNode) 
				&& !(targetType instanceof ArrayTypeNode)
				&& !(targetType instanceof DequeTypeNode)) {
			reportError("indexed access only supported on map, array, and deque type");
		}

		TypeNode keyType;
		if(targetType instanceof MapTypeNode)
			keyType = ((MapTypeNode)targetType).keyType;
		else
			keyType = IntTypeNode.intType;
		TypeNode keyExprType = keyExpr.getType();

		if (keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;
			
			String givenTypeName;
			if(keyExprType instanceof InheritanceTypeNode)
				givenTypeName = ((InheritanceTypeNode) keyExprType).getIdentNode().toString();
			else
				givenTypeName = keyExprType.toString();
			String expectedTypeName;
			if(keyType instanceof InheritanceTypeNode)
				expectedTypeName = ((InheritanceTypeNode) keyType).getIdentNode().toString();
			else
				expectedTypeName = keyType.toString();
			reportError("Cannot convert map access argument from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		} else {
			if (keyExprType.isEqual(keyType))
				return true;

			keyExpr = becomeParent(keyExpr.adjustType(keyType, getCoords()));
			return keyExpr != ConstNode.getInvalid();
		}
	}

	@Override
	public TypeNode getType() {
		TypeNode targetExprType = targetExpr.getType();
		if(targetExprType instanceof MapTypeNode)
			return ((MapTypeNode)targetExprType).valueType;
		else if(targetExprType instanceof ArrayTypeNode)
			return ((ArrayTypeNode)targetExprType).valueType;
		else
			return ((DequeTypeNode)targetExprType).valueType;
	}

	@Override
	protected IR constructIR() {
		return new IndexedAccessExpr(targetExpr.checkIR(Expression.class),
			keyExpr.checkIR(Expression.class));
	}
}
