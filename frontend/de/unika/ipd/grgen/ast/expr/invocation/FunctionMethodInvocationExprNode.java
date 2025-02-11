/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.invocation.FunctionMethodInvocationExpr;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of a function method
 */
public class FunctionMethodInvocationExprNode extends FunctionInvocationBaseNode
{
	static {
		setName(FunctionMethodInvocationExprNode.class, "function method invocation expression");
	}

	private IdentNode ownerUnresolved;
	private DeclNode owner;

	private IdentNode functionUnresolved;
	private FunctionDeclNode functionDecl;

	public FunctionMethodInvocationExprNode(IdentNode owner, IdentNode functionUnresolved,
			CollectNode<ExprNode> arguments)
	{
		super(functionUnresolved.getCoords(), arguments);
		this.ownerUnresolved = becomeParent(owner);
		this.functionUnresolved = becomeParent(functionUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(functionUnresolved, functionDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("function method");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver =
			new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationResolver<FunctionDeclNode> resolver =
			new DeclarationResolver<FunctionDeclNode>(FunctionDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		boolean res = fixupDefinition(ownerUnresolved, ownerUnresolved.getScope());
		if(!res)
			return false;

		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner != null && successfullyResolved;
		boolean ownerResolveResult = owner != null && owner.resolve();

		if(!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		if(ownerResolveResult && owner != null
				&& (owner instanceof NodeDeclNode || owner instanceof EdgeDeclNode)) {
			TypeNode ownerType = owner.getDeclType();
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner)ownerType;
				res = o.fixupDefinition(functionUnresolved);

				functionDecl = resolver.resolve(functionUnresolved, this);
				if(functionDecl == null) {
					functionUnresolved.reportError("A function method of name " + functionUnresolved + " is not known."
							+ " Is it a misspelled function name? Or is a procedure call intended (this is not possible in an expression, an assignment target must be given as (param,...)=call in that case)?");
					return false;
				}

				successfullyResolved = functionDecl != null && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope.");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge.");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		return checkSignatureAdhered(functionDecl, functionUnresolved, true);
	}

	@Override
	public TypeNode getType()
	{
		assert isResolved();
		return functionDecl.getResultType();
	}

	@Override
	protected IR constructIR()
	{
		FunctionMethodInvocationExpr ci = new FunctionMethodInvocationExpr(owner.checkIR(Entity.class),
				functionDecl.resultType.checkIR(Type.class),
				functionDecl.checkIR(Function.class));
		for(ExprNode expr : arguments.getChildren()) {
			expr = expr.evaluate();
			ci.addArgument(expr.checkIR(Expression.class));
		}
		return ci;
	}
}
