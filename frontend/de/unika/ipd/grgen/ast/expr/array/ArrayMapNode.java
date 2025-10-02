/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.array;

import java.util.Collection;
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.ContainerTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.array.ArrayMapExpr;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.container.ArrayType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class ArrayMapNode extends ArrayFunctionMethodInvocationBaseExprNode
{
	static {
		setName(ArrayMapNode.class, "array map");
	}

	private IdentNode resultValueTypeUnresolved;
	private TypeNode resultValueType;

	private VarDeclNode arrayAccessVar;
	
	private VarDeclNode indexVar;
	private VarDeclNode elementVar;
	private ExprNode mappingExpr;

	private ArrayTypeNode resultArrayType;

	public ArrayMapNode(Coords coords, ExprNode targetExpr, IdentNode resultValueType, VarDeclNode arrayAccessVar,
			VarDeclNode indexVar, VarDeclNode elementVar, ExprNode mappingExpr)
	{
		super(coords, targetExpr);
		this.resultValueTypeUnresolved = resultValueType;
		this.arrayAccessVar = arrayAccessVar;
		this.indexVar = indexVar;
		this.elementVar = elementVar;
		this.mappingExpr = mappingExpr;
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
		children.add(mappingExpr);
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
		childrenNames.add("mappingExpr");
		return childrenNames;
	}

	private static final DeclarationResolver<TypeDeclNode> typeResolver =
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		boolean ownerResolveResult = targetExpr.resolve();
		if(!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		getTargetType();

		TypeDeclNode resultValueTypeDecl = typeResolver.resolve(resultValueTypeUnresolved, this);
		if(resultValueTypeDecl == null)
			return false;
		if(!resultValueTypeDecl.resolve())
			return false;

		// maybe move to type checking
		resultValueType = resultValueTypeDecl.getDeclType();
		if(!(resultValueType instanceof DeclaredTypeNode)
				|| resultValueType instanceof ContainerTypeNode) {
			reportError("The type " + resultValueType.getTypeName()
					+ " is not an allowed type - set, map, array, deque are forbidden.");
			return false;
		}

		DeclaredTypeNode declResultValueType = (DeclaredTypeNode)resultValueType;

		resultArrayType = new ArrayTypeNode(declResultValueType.getIdentNode());
		if(!resultArrayType.resolve())
			return false;

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		TypeNode resultType = resultArrayType.valueType;
		TypeNode exprType = mappingExpr.getType();

		if(arrayAccessVar != null) {
			TypeNode arrayAccessVarType = arrayAccessVar.getDeclType();
			if(!(arrayAccessVarType instanceof ArrayTypeNode)) {
				reportError("The array access variable of the array map function method must be of array type"
						+ " (but is of type " + arrayAccessVarType.getTypeName() + ").");
				return false;
			}
			if(!arrayAccessVarType.isEqual(targetExpr.getType())) {
				reportError("The array access variable of the array map function method must be of type " + targetExpr.getType().getTypeName()
						+ " (but is of type " + arrayAccessVarType.getTypeName() + ").");
				return false;
			}
		}

		if(indexVar != null) {
			TypeNode indexVarType = indexVar.getDeclType();
			if(!indexVarType.isEqual(BasicTypeNode.intType)) {
				reportError("The index variable of the array map function method must be of int type"
						+ " (but is of type " + indexVarType.getTypeName() + ").");
				return false;
			}
		}

		if(!exprType.isEqual(resultType)) {
			mappingExpr = becomeParent(mappingExpr.adjustType(resultValueType, getCoords()));
			if(mappingExpr == ConstNode.getInvalid())
				return false;

			if(resultType instanceof NodeTypeNode && exprType instanceof NodeTypeNode
					|| resultType instanceof EdgeTypeNode && exprType instanceof EdgeTypeNode) {
				Collection<TypeNode> superTypes = new HashSet<TypeNode>();
				exprType.doGetCompatibleToTypes(superTypes);
				if(!superTypes.contains(resultType)) {
					reportError("Type mismatch in the array map function method between the lambda expression value of type " + exprType.toStringWithDeclarationCoords()
							+ " and the expected element type " + resultType.toStringWithDeclarationCoords() + " of the resulting array.");
					return false;
				}
			}
			if(resultType instanceof NodeTypeNode && exprType instanceof EdgeTypeNode
					|| resultType instanceof EdgeTypeNode && exprType instanceof NodeTypeNode) {
				reportError("Type mismatch in the array map function method between the lambda expression value of " + exprType.getKind() + " " + exprType.getTypeName()
						+ " and the expected " + resultType.getKind() + " element type " + resultType.getTypeName() + " of the resulting array.");
				return false;
			}
		}

		TypeNode elementVarType = elementVar.getDeclType();
		TypeNode targetType = ((ArrayTypeNode)targetExpr.getType()).valueType;

		if(targetType instanceof NodeTypeNode && elementVarType instanceof EdgeTypeNode
				|| targetType instanceof EdgeTypeNode && elementVarType instanceof NodeTypeNode) {
			reportError("Cannot bind the element variable of " + elementVarType.getKind() + " " + elementVarType.getTypeName()
					+ " to a value of " + targetType.getKind() + " " + targetType.getTypeName() + " in the array map function method.");
			return false;
		}
		if(!targetType.isCompatibleTo(elementVarType)) {
			reportError("Cannot bind the element variable of type " + elementVarType.toStringWithDeclarationCoords()
					+ " to a value of type " + targetType.toStringWithDeclarationCoords() + " in the array map function method.");
			return false;
		}

		return true;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		return resultArrayType;
	}

	@Override
	protected IR constructIR()
	{
		targetExpr = targetExpr.evaluate();
		mappingExpr = mappingExpr.evaluate();
		return new ArrayMapExpr(targetExpr.checkIR(Expression.class),
				arrayAccessVar != null ? arrayAccessVar.checkIR(Variable.class) : null,
				indexVar != null ? indexVar.checkIR(Variable.class) : null,
				elementVar.checkIR(Variable.class),
				mappingExpr.checkIR(Expression.class),
				resultArrayType.checkIR(ArrayType.class));
	}
}
