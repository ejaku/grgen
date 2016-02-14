/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ReturnStatementFilter;
import de.unika.ipd.grgen.ir.exprevals.ReturnStatementProcedure;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.exprevals.ReturnStatement;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a return statement (of function or procedure).
 */
public class ReturnStatementNode extends EvalStatementNode {
	static {
		setName(ReturnStatementNode.class, "ReturnStatement");
	}

	CollectNode<ExprNode> returnValueExprs;

	boolean isFilterReturn = false;
	boolean isFunctionReturn = false;

	public ReturnStatementNode(Coords coords, CollectNode<ExprNode> returnValueExprs) {
		super(coords);
		this.returnValueExprs = returnValueExprs;
		becomeParent(returnValueExprs);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(returnValueExprs);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("return value expressions");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		if(!(root instanceof FunctionDeclNode) && !(root instanceof ProcedureDeclNode) && !(root instanceof FilterFunctionDeclNode)){
			reportError("return must be nested inside a function or procedure or filter (from where to return?)");
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
			retTypes.add(function.getReturnType());
		} else {
			ProcedureDeclNode procedure = (ProcedureDeclNode)root;
			retTypes = procedure.getReturnTypes();
		}
		return checkReturns(retTypes, root);
	}
	
	/**
	 * Check if actual return arguments are conforming to the formal return parameters.
	 */
	protected boolean checkReturns(Vector<TypeNode> returnFormalParameters, DeclNode ident) {
		boolean res = true;

		int declaredNumRets = returnFormalParameters.size();
		int actualNumRets = returnValueExprs.children.size();
		for (int i = 0; i < Math.min(declaredNumRets, actualNumRets); ++i) {
			ExprNode retExpr = returnValueExprs.children.get(i);
			TypeNode retExprType = retExpr.getType();
			TypeNode retDeclType = returnFormalParameters.get(i);
			if(!retExprType.isCompatibleTo(retDeclType)) {
				res = false;
				String exprTypeName;
				if(retExprType instanceof InheritanceTypeNode)
					exprTypeName = ((InheritanceTypeNode) retExprType).getIdentNode().toString();
				else
					exprTypeName = retExprType.toString();
				reportError("Cannot convert " + (i + 1) + ". return parameter from \""
						+ exprTypeName + "\" to \"" + returnFormalParameters.get(i).toString() + "\"");
			}
		}

		//check the number of returned elements
		if (actualNumRets != declaredNumRets) {
			res = false;
			returnValueExprs.reportError("Trying to return " + actualNumRets + " values, but expected are " + declaredNumRets + " values, for " + ident);
		}
		return res;
	}

	@Override
	protected IR constructIR() {
		if(isFilterReturn) {
			return new ReturnStatementFilter();			
		} else if(isFunctionReturn) {
			return new ReturnStatement(returnValueExprs.get(0).checkIR(Expression.class));
		} else {
			ReturnStatementProcedure rsp = new ReturnStatementProcedure();
			for(ExprNode returnValueExpr : returnValueExprs.getChildren()) {
				rsp.addReturnValueExpr(returnValueExpr.checkIR(Expression.class));
			}
			return rsp;
		}
	}
}
