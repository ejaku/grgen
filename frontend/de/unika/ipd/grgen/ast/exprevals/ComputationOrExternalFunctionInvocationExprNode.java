/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.6
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ir.exprevals.Computation;
import de.unika.ipd.grgen.ir.exprevals.ComputationInvocationExpr;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunction;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of a computation or an external function
 */
public class ComputationOrExternalFunctionInvocationExprNode extends ExprNode
{
	static {
		setName(ComputationOrExternalFunctionInvocationExprNode.class, "computation or external function invocation expression");
	}

	private IdentNode computationOrFunctionUnresolved;
	private ExternalFunctionDeclNode functionDecl;
	private ComputationDeclNode computationDecl;
	private CollectNode<ExprNode> arguments;

	public ComputationOrExternalFunctionInvocationExprNode(IdentNode computationOrFunctionUnresolved, CollectNode<ExprNode> arguments)
	{
		super(computationOrFunctionUnresolved.getCoords());
		this.computationOrFunctionUnresolved = becomeParent(computationOrFunctionUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(computationOrFunctionUnresolved, computationDecl, functionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("computation or function");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<ComputationDeclNode, ExternalFunctionDeclNode> resolver =
			new DeclarationPairResolver<ComputationDeclNode, ExternalFunctionDeclNode>(ComputationDeclNode.class, ExternalFunctionDeclNode.class);

	protected boolean resolveLocal() {
		fixupDefinition((IdentNode)computationOrFunctionUnresolved, computationOrFunctionUnresolved.getScope());
		Pair<ComputationDeclNode, ExternalFunctionDeclNode> resolved = resolver.resolve(computationOrFunctionUnresolved, this);
		if(resolved == null) {
			computationOrFunctionUnresolved.reportError("Unknown function called -- misspelled function name?");
			return false;
		}
		computationDecl = resolved.fst;
		functionDecl = resolved.snd;
		return true;
	}

	@Override
	protected boolean checkLocal() {
		return checkSignatureAdhered();
	}

	/** Check whether the usage adheres to the signature of the declaration */
	private boolean checkSignatureAdhered() {
		ComputationCharacter cc = computationDecl!=null ? computationDecl : functionDecl;
		
		// check if the number of parameters are correct
		int expected = cc.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = cc.ident.toString();
			computationOrFunctionUnresolved.reportError("The computation \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = cc.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				computationOrFunctionUnresolved.reportError("Cannot convert " + (i + 1) + ". computation argument from \""
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
		return computationDecl!=null ? computationDecl.getReturnType() : functionDecl.getReturnType();
	}

	@Override
	protected IR constructIR() {
		if(computationDecl!=null) {
			ComputationInvocationExpr ci = new ComputationInvocationExpr(
					computationDecl.ret.checkIR(Type.class),
					computationDecl.checkIR(Computation.class));
			for(ExprNode expr : arguments.getChildren()) {
				ci.addArgument(expr.checkIR(Expression.class));
			}
			return ci;
		} else {
			ExternalFunctionInvocationExpr efi = new ExternalFunctionInvocationExpr(
					functionDecl.ret.checkIR(Type.class),
					functionDecl.checkIR(ExternalFunction.class));
			for(ExprNode expr : arguments.getChildren()) {
				efi.addArgument(expr.checkIR(Expression.class));
			}
			return efi;
		}
	}
}
