/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunction;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.FunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of a function method
 */
public class FunctionMethodOrExternalFunctionMethodInvocationExprNode extends ExprNode
{
	static {
		setName(FunctionMethodOrExternalFunctionMethodInvocationExprNode.class, "function method or external function method invocation expression");
	}

	private IdentNode ownerUnresolved;
	private DeclNode owner;
	
	private IdentNode functionOrExternalFunctionUnresolved;
	private ExternalFunctionDeclNode externalFunctionDecl;
	private FunctionDeclNode functionDecl;
	
	private CollectNode<ExprNode> arguments;

	public FunctionMethodOrExternalFunctionMethodInvocationExprNode(IdentNode owner, IdentNode functionOrExternalFunctionUnresolved, CollectNode<ExprNode> arguments)
	{
		super(functionOrExternalFunctionUnresolved.getCoords());
		this.ownerUnresolved = becomeParent(owner);
		this.functionOrExternalFunctionUnresolved = becomeParent(functionOrExternalFunctionUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(functionOrExternalFunctionUnresolved, functionDecl, externalFunctionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("function method or external function method");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver = new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode> resolver =
			new DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode>(FunctionDeclNode.class, ExternalFunctionDeclNode.class);

	protected boolean resolveLocal() {
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		boolean res = fixupDefinition(ownerUnresolved, ownerUnresolved.getScope());
		if(!res)
			return false;

		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner!=null && successfullyResolved;
		boolean ownerResolveResult = owner!=null && owner.resolve();

		if (!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		if (ownerResolveResult && owner != null && (owner instanceof NodeCharacter || owner instanceof EdgeCharacter)) {
			TypeNode ownerType = owner.getDeclType();
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner) ownerType;
				res = o.fixupDefinition(functionOrExternalFunctionUnresolved);

				Pair<FunctionDeclNode, ExternalFunctionDeclNode> resolved = resolver.resolve(functionOrExternalFunctionUnresolved, this);
				if(resolved == null) {
					functionOrExternalFunctionUnresolved.reportError("Unknown function called -- misspelled function name? Or procedure call intended (not possible in expression, assignment target must be given as (param,...)=call in this case)?");
					return false;
				}
				functionDecl = resolved.fst;
				externalFunctionDecl = resolved.snd;

				successfullyResolved = (functionDecl!=null || externalFunctionDecl!=null) && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge");
			successfullyResolved = false;
		}

		return successfullyResolved;
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
			FunctionMethodInvocationExpr ci = new FunctionMethodInvocationExpr(
					owner.checkIR(Entity.class),
					functionDecl.ret.checkIR(Type.class),
					functionDecl.checkIR(Function.class));
			for(ExprNode expr : arguments.getChildren()) {
				ci.addArgument(expr.checkIR(Expression.class));
			}
			return ci;
		} else {
			ExternalFunctionMethodInvocationExpr efi = new ExternalFunctionMethodInvocationExpr(
					owner.checkIR(Entity.class),
					externalFunctionDecl.ret.checkIR(Type.class),
					externalFunctionDecl.checkIR(ExternalFunction.class));
			for(ExprNode expr : arguments.getChildren()) {
				efi.addArgument(expr.checkIR(Expression.class));
			}
			return efi;
		}
	}
}
