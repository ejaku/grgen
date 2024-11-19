/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalFunction;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.ExternalFunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of an external function method
 */
public class ExternalFunctionMethodInvocationExprNode extends FunctionInvocationBaseNode
{
	static {
		setName(ExternalFunctionMethodInvocationExprNode.class, "external function method invocation expression");
	}

	private ExprNode owner;

	private IdentNode externalFunctionUnresolved;
	private ExternalFunctionDeclNode externalFunctionDecl;

	public ExternalFunctionMethodInvocationExprNode(ExprNode owner, IdentNode externalFunctionUnresolved,
			CollectNode<ExprNode> arguments)
	{
		super(externalFunctionUnresolved.getCoords(), arguments);
		this.owner = becomeParent(owner);
		this.externalFunctionUnresolved = becomeParent(externalFunctionUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(owner);
		children.add(getValidVersion(externalFunctionUnresolved, externalFunctionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("external function method");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<ExternalFunctionDeclNode> resolver =
			new DeclarationResolver<ExternalFunctionDeclNode>(ExternalFunctionDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		TypeNode ownerType = owner.getType();
		if(ownerType instanceof ExternalObjectTypeNode) {
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner)ownerType;
				o.fixupDefinition(externalFunctionUnresolved);

				externalFunctionDecl = resolver.resolve(externalFunctionUnresolved, this);
				if(externalFunctionDecl == null) {
					externalFunctionUnresolved.reportError("An external function method of name " + externalFunctionUnresolved + " is not known."
							+ " Is it a misspelled function name? Or is a procedure call intended (this is not possible in an expression, an assignment target must be given as (param,...)=call in that case)?");
					return false;
				}

				successfullyResolved = externalFunctionDecl != null && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope.");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is not an external type.");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		return checkSignatureAdhered(externalFunctionDecl, externalFunctionUnresolved, true);
	}

	@Override
	public TypeNode getType()
	{
		assert isResolved();
		return externalFunctionDecl.getResultType();
	}

	@Override
	protected IR constructIR()
	{
		owner = owner.evaluate();
		ExternalFunctionMethodInvocationExpr efi = new ExternalFunctionMethodInvocationExpr(
				owner.checkIR(Expression.class),
				externalFunctionDecl.resultType.checkIR(Type.class),
				externalFunctionDecl.checkIR(ExternalFunction.class));
		for(ExprNode expr : arguments.getChildren()) {
			expr = expr.evaluate();
			efi.addArgument(expr.checkIR(Expression.class));
		}
		return efi;
	}
}
