/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayRemoveIfExpr;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayRemoveIfNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayRemoveIfNode.class, "array removeIf");
	}

	private VarDeclNode arrayAccessVar;

	private VarDeclNode indexVar;
	private VarDeclNode elementVar;
	private ExprNode conditionExpr;

	public ArrayRemoveIfNode(Coords coords, ExprNode targetExpr, VarDeclNode arrayAccessVar,
			VarDeclNode indexVar, VarDeclNode elementVar, ExprNode conditionExpr)
	{
		super(coords, targetExpr);
		this.arrayAccessVar = arrayAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.conditionExpr = conditionExpr;
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetExpr);
		if(arrayAccessVar != null)
			children.add(arrayAccessVar);
		if(indexVar != null)
			children.add(indexVar);
		children.add(elementVar);
		children.add(conditionExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("targetExpr");
		if(arrayAccessVar != null)
			childrenNames.add("arrayAccessVar");
		if(indexVar != null)
			childrenNames.add("indexVar");
		childrenNames.add("elementVar");
		childrenNames.add("conditionExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		boolean ownerResolveResult = targetExpr.resolve();
		if(!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		getTargetType();

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode exprType = conditionExpr.getType();

		if(arrayAccessVar != null) {
			TypeNode arrayAccessVarType = arrayAccessVar.getDeclType();
			if(!(arrayAccessVarType instanceof ArrayTypeNode)) {
				reportError("The array access variable of the array removeIf function method must be of array type"
						+ " (but is of type " + arrayAccessVarType.getTypeName() + ").");
				return false;
			}
			if(!arrayAccessVarType.isEqual(targetExpr.getType())) {
				reportError("The array access variable of the array removeIf function method must be of type " + targetExpr.getType().getTypeName()
				+ " (but is of type " + arrayAccessVarType.getTypeName() + ").");
				return false;
			}
		}

		if(indexVar != null) {
			TypeNode indexVarType = indexVar.getDeclType();
			if(!indexVarType.isEqual(BasicTypeNode.intType)) {
				reportError("The index variable of the array removeIf function method must be of int type"
						+ " (but is of type " + indexVarType.getTypeName() + ").");
				return false;
			}
		}

		if(!exprType.isEqual(BasicTypeNode.booleanType)) {
			reportError("Type mismatch in the array removeIf function method between the lambda expression value of type " + exprType.getTypeName()
					+ " and the expected boolean type.");
			return false;
		}

		TypeNode elementVarType = elementVar.getDeclType();
		TypeNode targetType = ((ArrayTypeNode)targetExpr.getType()).valueType;

		if(targetType instanceof NodeTypeNode && elementVarType instanceof EdgeTypeNode
				|| targetType instanceof EdgeTypeNode && elementVarType instanceof NodeTypeNode) {
			reportError("Cannot bind the element variable of " + elementVarType.getKind() + " " + elementVarType.getTypeName()
					+ " to a value of " + targetType.getKind() + " " + targetType.getTypeName() + " in the array removeIf function method.");
			return false;
		}
		if(!targetType.isCompatibleTo(elementVarType)) {
			reportError("Cannot bind the element variable of type " + elementVarType.toStringWithDeclarationCoords()
					+ " to a value of type " + targetType.toStringWithDeclarationCoords() + " in the array removeIf function method.");
			return false;
		}

		return true;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return getTargetType();
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		conditionExpr = conditionExpr.evaluate();
		return new ArrayRemoveIfExpr(targetExpr.checkIR(Expression.class),
				arrayAccessVar != null ? arrayAccessVar.checkIR(Variable.class) : null,
				indexVar != null ? indexVar.checkIR(Variable.class) : null,
				elementVar.checkIR(Variable.class),
				conditionExpr.checkIR(Expression.class),
				getTargetType().checkIR(ArrayType.class));
	}
}
