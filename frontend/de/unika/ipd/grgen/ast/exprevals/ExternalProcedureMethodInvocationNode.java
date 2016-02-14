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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedure;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.exprevals.Qualification;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;
import de.unika.ipd.grgen.ir.Variable;

/**
 * Invocation of an external procedure method
 */
public class ExternalProcedureMethodInvocationNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(ExternalProcedureMethodInvocationNode.class, "external procedure method invocation");
	}

	VarDeclNode targetVar = null;
	QualIdentNode targetQual = null;

	IdentNode externalProcedureUnresolved;
	ExternalProcedureDeclNode externalProcedureDecl;
	
	public ExternalProcedureMethodInvocationNode(VarDeclNode targetVar, IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords());
		this.targetVar = becomeParent(targetVar);
		this.externalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
		this.arguments = becomeParent(arguments);
		this.context = context;
	}

	public ExternalProcedureMethodInvocationNode(QualIdentNode targetQual, IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords());
		this.targetQual = becomeParent(targetQual);
		this.externalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
		this.arguments = becomeParent(arguments);
		this.context = context;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetQual!=null ? targetQual : targetVar);
		children.add(getValidVersion(externalProcedureUnresolved, externalProcedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("external procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<ExternalProcedureDeclNode> resolver = new DeclarationResolver<ExternalProcedureDeclNode>(ExternalProcedureDeclNode.class);

	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		TypeNode ownerType = targetVar!=null ? targetVar.getDeclType() : targetQual.getDecl().getDeclType();
		if(ownerType instanceof ExternalTypeNode) {
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner) ownerType;
				o.fixupDefinition(externalProcedureUnresolved);

				externalProcedureDecl = resolver.resolve(externalProcedureUnresolved, this);
				if(externalProcedureDecl == null) {
					externalProcedureUnresolved.reportError("Unknown external procedure method called -- misspelled procedure name? Or function call intended (not possible when assignment target is given as (param,...)=call denoting a procedure call)?");
					return false;
				}

				successfullyResolved = externalProcedureDecl!=null && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is not an external type");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	@Override
	protected boolean checkLocal() {
		if((context&BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE)==BaseNode.CONTEXT_FUNCTION) {
			reportError("external procedure method call not allowed in function or lhs context");
			return false;
		}
		return checkSignatureAdhered(externalProcedureDecl, externalProcedureUnresolved);
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	public Vector<TypeNode> getType() {
		assert isResolved();
		return externalProcedureDecl.getReturnTypes();
	}
	
	public int getNumReturnTypes() {
		return externalProcedureDecl.returnTypes.size();
	}

	@Override
	protected IR constructIR() {
		ExternalProcedureMethodInvocation epi;
		if(targetQual != null) {
			epi = new ExternalProcedureMethodInvocation(
					targetQual.checkIR(Qualification.class),
					externalProcedureDecl.checkIR(ExternalProcedure.class));
		} else {
			epi = new ExternalProcedureMethodInvocation(
					targetVar.checkIR(Variable.class),
					externalProcedureDecl.checkIR(ExternalProcedure.class));
		}
		for(ExprNode expr : arguments.getChildren()) {
			epi.addArgument(expr.checkIR(Expression.class));
		}
		for(TypeNode type : externalProcedureDecl.returnTypes.children) {
			epi.addReturnType(type.checkIR(Type.class));
		}
		return epi;
	}
}
