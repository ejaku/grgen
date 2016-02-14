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
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunction;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.FunctionInvocationExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of a function or an external function
 */
public class FunctionOrExternalFunctionInvocationExprNode extends ExprNode
{
	static {
		setName(FunctionOrExternalFunctionInvocationExprNode.class, "function or external function invocation expression");
	}

	private IdentNode functionOrExternalFunctionUnresolved;
	private ExternalFunctionDeclNode externalFunctionDecl;
	private FunctionDeclNode functionDecl;
	private CollectNode<ExprNode> arguments;

	public FunctionOrExternalFunctionInvocationExprNode(IdentNode functionOrExternalFunctionUnresolved, CollectNode<ExprNode> arguments)
	{
		super(functionOrExternalFunctionUnresolved.getCoords());
		this.functionOrExternalFunctionUnresolved = becomeParent(functionOrExternalFunctionUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(functionOrExternalFunctionUnresolved, functionDecl, externalFunctionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("function or external function");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode> resolver =
			new DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode>(FunctionDeclNode.class, ExternalFunctionDeclNode.class);

	protected boolean resolveLocal() {
		if(!(functionOrExternalFunctionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition((IdentNode)functionOrExternalFunctionUnresolved, functionOrExternalFunctionUnresolved.getScope());
		}
		Pair<FunctionDeclNode, ExternalFunctionDeclNode> resolved = resolver.resolve(functionOrExternalFunctionUnresolved, this);
		if(resolved == null) {
			functionOrExternalFunctionUnresolved.reportError("Unknown function called -- misspelled function name? Or procedure call intended (not possible in expression, assignment target must be given as (param,...)=call in this case)?");
			return false;
		}
		functionDecl = resolved.fst;
		externalFunctionDecl = resolved.snd;
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return checkSignatureAdhered();
	}

	/** Check whether the usage adheres to the signature of the declaration */
	private boolean checkSignatureAdhered() {
		FunctionBase fb = functionDecl!=null ? functionDecl : externalFunctionDecl;
		
		// check if the number of parameters are correct
		int expected = fb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = fb.ident.toString();
			functionOrExternalFunctionUnresolved.reportError("The function \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = fb.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				functionOrExternalFunctionUnresolved.reportError("Cannot convert " + (i + 1) + ". function argument from \""
						+ exprTypeName + "\" to \"" + paramTypeName + "\"");
			}
		}

		return res;
	}
	
	private String getTypeName(TypeNode type) {
		String typeName;
		if(type instanceof InheritanceTypeNode)
			typeName = ((InheritanceTypeNode) type).getIdentNode().toString();
		else
			typeName = type.toString();
		return typeName;
	}

	@Override
	public TypeNode getType() {
		assert isResolved();
		return functionDecl!=null ? functionDecl.getReturnType() : externalFunctionDecl.getReturnType();
	}

	@Override
	protected IR constructIR() {
		if(functionDecl!=null) {
			FunctionInvocationExpr fi = new FunctionInvocationExpr(
					functionDecl.ret.checkIR(Type.class),
					functionDecl.checkIR(Function.class));
			for(ExprNode expr : arguments.getChildren()) {
				fi.addArgument(expr.checkIR(Expression.class));
			}
			return fi;
		} else {
			ExternalFunctionInvocationExpr efi = new ExternalFunctionInvocationExpr(
					externalFunctionDecl.ret.checkIR(Type.class),
					externalFunctionDecl.checkIR(ExternalFunction.class));
			for(ExprNode expr : arguments.getChildren()) {
				efi.addArgument(expr.checkIR(Expression.class));
			}
			return efi;
		}
	}
}
