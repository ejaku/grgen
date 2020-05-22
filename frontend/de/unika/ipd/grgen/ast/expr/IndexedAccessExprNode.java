/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.type.basic.UntypedExecVarTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.model.InheritanceTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.IndexedAccessExpr;
import de.unika.ipd.grgen.ir.type.ArrayType;
import de.unika.ipd.grgen.ir.type.DequeType;
import de.unika.ipd.grgen.ir.type.MapType;
import de.unika.ipd.grgen.ir.type.Type;
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
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode targetType = targetExpr.getType();
		if(targetType instanceof UntypedExecVarTypeNode) {
			return true;
		}

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

		if(keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;

			String givenTypeName = keyExprType.getTypeName();
			String expectedTypeName = keyType.getTypeName();
			reportError("Cannot convert indexed access argument from \""
					+ givenTypeName + "\" to \"" + expectedTypeName + "\"");
			return false;
		} else {
			if(keyExprType.isEqual(keyType))
				return true;

			keyExpr = becomeParent(keyExpr.adjustType(keyType, getCoords()));
			return keyExpr != ConstNode.getInvalid();
		}
	}

	@Override
	public TypeNode getType()
	{
		TypeNode targetExprType = targetExpr.getType();
		if(targetExprType instanceof MapTypeNode)
			return ((MapTypeNode)targetExprType).valueType;
		else if(targetExprType instanceof ArrayTypeNode)
			return ((ArrayTypeNode)targetExprType).valueType;
		else if(targetExprType instanceof DequeTypeNode)
			return ((DequeTypeNode)targetExprType).valueType;
		else
			return targetExprType; // assuming untyped
	}

	@Override
	protected IR constructIR()
	{
		Expression texp = targetExpr.checkIR(Expression.class);
		Type type = texp.getType();
		Type accessedType;
		if(type instanceof MapType)
			accessedType = ((MapType)type).getValueType();
		else if(type instanceof DequeType)
			accessedType = ((DequeType)type).getValueType();
		else if(type instanceof ArrayType)
			accessedType = ((ArrayType)type).getValueType();
		else
			accessedType = type; // assuming untypedType
		return new IndexedAccessExpr(targetExpr.checkIR(Expression.class),
				keyExpr.checkIR(Expression.class), accessedType);
	}
}
