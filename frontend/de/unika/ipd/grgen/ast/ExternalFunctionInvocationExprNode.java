/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.ExternalFunction;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.ExternalFunctionInvocationExpr;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of an external function
 */
public class ExternalFunctionInvocationExprNode extends ExprNode
{
	static {
		setName(ExternalFunctionInvocationExprNode.class, "external function invocation expression");
	}

	private IdentNode functionUnresolved;
	private ExternalFunctionDeclNode functionDecl;
	private CollectNode<ExprNode> arguments;

	public ExternalFunctionInvocationExprNode(IdentNode functionUnresolved, CollectNode<ExprNode> arguments)
	{
		super(functionUnresolved.getCoords());
		this.functionUnresolved = becomeParent(functionUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(functionUnresolved, functionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("function");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final Resolver<ExternalFunctionDeclNode> resolver =
			new DeclarationResolver<ExternalFunctionDeclNode>(ExternalFunctionDeclNode.class);

	protected boolean resolveLocal() {
		functionDecl = resolver.resolve(functionUnresolved, this);
		return functionDecl != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	public TypeNode getType() {
		assert isResolved();
		return functionDecl.ret;
	}

	@Override
	protected IR constructIR() {
		ExternalFunctionInvocationExpr efi = new ExternalFunctionInvocationExpr(
				functionDecl.ret.checkIR(Type.class),
				functionDecl.checkIR(ExternalFunction.class));
		for(ExprNode expr : arguments.getChildren()) {
			efi.addArgument(expr.checkIR(Expression.class));
		}
		return efi;
	}
}
