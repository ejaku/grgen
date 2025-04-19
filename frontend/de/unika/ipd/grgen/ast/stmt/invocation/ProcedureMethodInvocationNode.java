/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.stmt.invocation;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of a procedure method
 */
public class ProcedureMethodInvocationNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureMethodInvocationNode.class, "procedure method invocation");
	}

	private IdentNode ownerUnresolved;
	private DeclNode owner;

	private IdentNode procedureUnresolved;
	private ProcedureDeclNode procedureDecl;

	public ProcedureMethodInvocationNode(IdentNode owner, IdentNode procedureOrExternalProcedureUnresolved,
			CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords(), arguments, context);
		this.ownerUnresolved = becomeParent(owner);
		this.procedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(procedureUnresolved, procedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver =
			new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationResolver<ProcedureDeclNode> resolver =
			new DeclarationResolver<ProcedureDeclNode>(ProcedureDeclNode.class);

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
				res = o.fixupDefinition(procedureUnresolved);

				procedureDecl = resolver.resolve(procedureUnresolved, this);
				if(procedureDecl == null) {
					procedureUnresolved.reportError("Unknown procedure method called."
							+ " (Maybe a misspelled procedure name? Or is a function call intended?"
							+ " An assignment target within parenthesis denotes a procedure call, as in "
							+ "(var) = " + owner + "." + procedureUnresolved + "(...)).");
					return false;
				}

				successfullyResolved = procedureDecl != null && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope"
						+ " (type " + ownerType.toStringWithDeclarationCoords() + ").");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge"
					+ (owner != null && owner.getDeclType() != null ? " (type " + owner.getDeclType().toStringWithDeclarationCoords() + ")." : ".") );
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			reportError("Procedure method call not allowed in function or pattern part context (attempted on " + procedureUnresolved + ").");
			return false;
		}
		return checkSignatureAdhered(procedureDecl, procedureUnresolved, true);
	}

	@Override
	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	@Override
	public Vector<TypeNode> getType()
	{
		assert isResolved();
		return procedureDecl.getResultTypes();
	}

	public int getNumReturnTypes()
	{
		return procedureDecl.resultTypesCollectNode.size();
	}

	@Override
	protected IR constructIR()
	{
		ProcedureMethodInvocation pmi = new ProcedureMethodInvocation(owner.checkIR(Entity.class),
				procedureDecl.checkIR(Procedure.class));
		for(ExprNode expr : arguments.getChildren()) {
			expr = expr.evaluate();
			pmi.addArgument(expr.checkIR(Expression.class));
		}
		for(TypeNode type : procedureDecl.resultTypesCollectNode.getChildren()) {
			pmi.addReturnType(type.checkIR(Type.class));
		}
		return pmi;
	}
}
