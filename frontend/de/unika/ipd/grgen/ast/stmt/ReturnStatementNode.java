/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.stmt.ReturnStatement;
import de.unika.ipd.grgen.ir.stmt.ReturnStatementFilter;
import de.unika.ipd.grgen.ir.stmt.ReturnStatementProcedure;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a return statement (of function or procedure).
 */
public class ReturnStatementNode extends EvalStatementNode
{
	static {
		setName(ReturnStatementNode.class, "ReturnStatement");
	}

	CollectNode<ExprNode> returnValueExprs;

	boolean isFilterReturn = false;
	boolean isFunctionReturn = false;

	public ReturnStatementNode(Coords coords, CollectNode<ExprNode> returnValueExprs)
	{
		super(coords);
		this.returnValueExprs = returnValueExprs;
		becomeParent(returnValueExprs);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(returnValueExprs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("return value expressions");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		if(!(root instanceof FunctionDeclNode)
				&& !(root instanceof ProcedureDeclNode)
				&& !(root instanceof FilterFunctionDeclNode)) {
			reportError("A return statement must be nested inside a function or procedure or filter (or where do you want to return from otherwise?).");
			return false;
		}
		Vector<TypeNode> retTypes;
		if(root instanceof FilterFunctionDeclNode) {
			isFilterReturn = true;
			retTypes = new Vector<TypeNode>();
		} else if(root instanceof FunctionDeclNode) {
			isFunctionReturn = true;
			FunctionDeclNode function = (FunctionDeclNode)root;
			retTypes = new Vector<TypeNode>();
			retTypes.add(function.getResultType());
		} else {
			ProcedureDeclNode procedure = (ProcedureDeclNode)root;
			retTypes = procedure.getResultTypes();
		}
		return checkReturns(retTypes, root);
	}

	/**
	 * Check if actual return arguments are conforming to the formal return parameters.
	 */
	protected boolean checkReturns(Vector<TypeNode> returnFormalParameters, DeclNode ident)
	{
		boolean res = true;

		int declaredNumRets = returnFormalParameters.size();
		int actualNumRets = returnValueExprs.size();
		for(int i = 0; i < Math.min(declaredNumRets, actualNumRets); ++i) {
			ExprNode retExpr = returnValueExprs.get(i);
			TypeNode retExprType = retExpr.getType();
			TypeNode retDeclType = returnFormalParameters.get(i);
			if(!retExprType.isCompatibleTo(retDeclType)) {
				res = false;
				reportError("Cannot convert the " + (i + 1) + ". return parameter"
						+ " from the type " + retExprType.getTypeName()
						+ " to the expected type " + retDeclType.getTypeName()
						+ retExprType.toStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ retDeclType.toStringWithDeclarationCoordsIfCoordsAreOfInterest()
						+ ".");
			}
		}

		//check the number of returned elements
		if(actualNumRets != declaredNumRets) {
			res = false;
			reportError("Trying to return " + actualNumRets + " values, but expected are "
					+ declaredNumRets + " values (in " + ident + ").");
		}
		return res;
	}

	@Override
	protected IR constructIR()
	{
		if(isFilterReturn) {
			return new ReturnStatementFilter();
		} else if(isFunctionReturn) {
			ExprNode returnValueExpr = returnValueExprs.get(0).evaluate();
			return new ReturnStatement(returnValueExpr.checkIR(Expression.class));
		} else {
			ReturnStatementProcedure rsp = new ReturnStatementProcedure();
			for(ExprNode returnValueExpr : returnValueExprs.getChildren()) {
				returnValueExpr = returnValueExpr.evaluate();
				rsp.addReturnValueExpr(returnValueExpr.checkIR(Expression.class));
			}
			return rsp;
		}
	}
}
