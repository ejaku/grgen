/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IndexedAccessExpr;
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
		if(!(targetType instanceof MapTypeNode) && !(targetType instanceof ArrayTypeNode)) {
			reportError("indexed access only supported on map and array type");
		}

		TypeNode keyType;
		if(targetType instanceof MapTypeNode)
			keyType = ((MapTypeNode)targetType).keyType;
		else
			keyType = IntTypeNode.intType;
		TypeNode keyExprType = keyExpr.getType();

		if (keyType instanceof InheritanceTypeNode) {
			if(!keyExprType.isCompatibleTo(keyType)) {
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
				reportError("Cannot convert map/array access argument from \""
						+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
				return false;
			}
		}
		else {
			if (!keyExprType.isEqual(keyType)) {
				keyExpr = becomeParent(keyExpr.adjustType(keyType, getCoords()));
	
				if (keyExpr == ConstNode.getInvalid()) {
					return false;
				}
			}
		}
		
		return true;
	}

	@Override
	public TypeNode getType() {
		TypeNode targetExprType = targetExpr.getType();
		if(targetExprType instanceof MapTypeNode)
			return ((MapTypeNode)targetExprType).valueType;
		else
			return ((ArrayTypeNode)targetExprType).valueType;
	}

	@Override
	protected IR constructIR() {
		return new IndexedAccessExpr(targetExpr.checkIR(Expression.class),
			keyExpr.checkIR(Expression.class));
	}
}
