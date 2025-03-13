/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.graph;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.decl.IncidenceCountIndexDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.graph.CountIncidenceFromIndexExpr;
import de.unika.ipd.grgen.ir.model.IncidenceCountIndex;
import de.unika.ipd.grgen.parser.Coords;

public class CountIncidenceFromIndexExprNode extends BuiltinFunctionInvocationBaseNode
{
	static {
		setName(CountIncidenceFromIndexExprNode.class, "count incidence from index access expression");
	}

	private BaseNode indexUnresolved;
	private IncidenceCountIndexDeclNode index;
	private ExprNode keyExpr;

	public CountIncidenceFromIndexExprNode(Coords coords, BaseNode index, ExprNode keyExpr)
	{
		super(coords);
		this.indexUnresolved = becomeParent(index);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(indexUnresolved, index));
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("index");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	private static DeclarationResolver<IncidenceCountIndexDeclNode> indexResolver =
			new DeclarationResolver<IncidenceCountIndexDeclNode>(IncidenceCountIndexDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		index = indexResolver.resolve(indexUnresolved, this);
		if(index == null) {
			reportError("The function countFromIndex(.,.) expects as 1. argument (index) an incidence count index"
							+ " (but is given " + indexUnresolved.toStringWithDeclarationCoords() + ").");
		}
		successfullyResolved &= index != null;
		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		TypeNode keyType = index.getType();
		TypeNode keyExprType = keyExpr.getType();

		if(keyExprType instanceof InheritanceTypeNode) {
			if(keyExprType.isCompatibleTo(keyType))
				return true;

			String givenTypeName = keyExprType.getTypeName();
			String expectedTypeName = keyType.getTypeName();
			reportError("The function countFromIndex(.,.) expects as 2. argument (keyExpr) a value of type " + expectedTypeName
							+ " (but is given a value of type " + givenTypeName + ").");

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
		return IntTypeNode.intType;
	}

	@Override
	protected IR constructIR()
	{
		keyExpr = keyExpr.evaluate();
		return new CountIncidenceFromIndexExpr(index.checkIR(IncidenceCountIndex.class),
				keyExpr.checkIR(Expression.class));
	}
}
