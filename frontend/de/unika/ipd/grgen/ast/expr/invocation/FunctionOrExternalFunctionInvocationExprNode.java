/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.executable.ExternalFunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionOrOperatorDeclBaseNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.expr.invocation.FunctionInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of a function or an external function
 */
public class FunctionOrExternalFunctionInvocationExprNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionOrExternalFunctionInvocationExprNode.class,
				"function or external function invocation expression");
	}

	private IdentNode functionOrExternalFunctionUnresolved;
	private ExternalFunctionDeclNode externalFunctionDecl;
	private FunctionDeclNode functionDecl;

	public FunctionOrExternalFunctionInvocationExprNode(IdentNode functionOrExternalFunctionUnresolved,
			CollectNode<ExprNode> arguments)
	{
		super(functionOrExternalFunctionUnresolved.getCoords(), arguments);
		this.functionOrExternalFunctionUnresolved = becomeParent(functionOrExternalFunctionUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(functionOrExternalFunctionUnresolved, functionDecl, externalFunctionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("function or external function");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode> resolver =
			new DeclarationPairResolver<FunctionDeclNode, ExternalFunctionDeclNode>(FunctionDeclNode.class, ExternalFunctionDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		if(!(functionOrExternalFunctionUnresolved instanceof PackageIdentNode)) {
			fixupDefinition(functionOrExternalFunctionUnresolved,
					functionOrExternalFunctionUnresolved.getScope());
		}
		Pair<FunctionDeclNode, ExternalFunctionDeclNode> resolved =
				resolver.resolve(functionOrExternalFunctionUnresolved, this);
		if(resolved == null) {
			functionOrExternalFunctionUnresolved.reportError("Unknown function called -- misspelled function name? Or procedure call intended (not possible in expression, assignment target must be given as (param,...)=call in this case)?");
			return false;
		}
		functionDecl = resolved.fst;
		externalFunctionDecl = resolved.snd;
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		FunctionOrOperatorDeclBaseNode fb = functionDecl != null ? functionDecl : externalFunctionDecl;
		return checkSignatureAdhered(fb, functionOrExternalFunctionUnresolved, false);
	}

	@Override
	public TypeNode getType()
	{
		assert isResolved();
		return functionDecl != null ? functionDecl.getResultType() : externalFunctionDecl.getResultType();
	}

	@Override
	protected IR constructIR()
	{
		if(functionDecl != null) {
			FunctionInvocationExpr fi = new FunctionInvocationExpr(
					functionDecl.resultType.checkIR(Type.class),
					functionDecl.checkIR(Function.class));
			for(ExprNode expr : arguments.getChildren()) {
				expr = expr.evaluate();
				fi.addArgument(expr.checkIR(Expression.class));
			}
			return fi;
		} else {
			ExternalFunctionInvocationExpr efi = new ExternalFunctionInvocationExpr(
					externalFunctionDecl.resultType.checkIR(Type.class),
					externalFunctionDecl.checkIR(ExternalFunction.class));
			for(ExprNode expr : arguments.getChildren()) {
				expr = expr.evaluate();
				efi.addArgument(expr.checkIR(Expression.class));
			}
			return efi;
		}
	}
}
