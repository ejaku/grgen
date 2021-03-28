/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.executable.ExternalProcedureDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.QualIdentNode;
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Invocation of an external procedure method
 */
public class ExternalProcedureMethodInvocationNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ExternalProcedureMethodInvocationNode.class, "external procedure method invocation");
	}

	VarDeclNode targetVar = null;
	QualIdentNode targetQual = null;

	IdentNode externalProcedureUnresolved;
	ExternalProcedureDeclNode externalProcedureDecl;

	public ExternalProcedureMethodInvocationNode(VarDeclNode targetVar,
			IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords(), arguments, context);
		this.targetVar = becomeParent(targetVar);
		this.externalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
	}

	public ExternalProcedureMethodInvocationNode(QualIdentNode targetQual,
			IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords(), arguments, context);
		this.targetQual = becomeParent(targetQual);
		this.externalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(targetQual != null ? targetQual : targetVar);
		children.add(getValidVersion(externalProcedureUnresolved, externalProcedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("external procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<ExternalProcedureDeclNode> resolver =
			new DeclarationResolver<ExternalProcedureDeclNode>(ExternalProcedureDeclNode.class);

	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		TypeNode ownerType = targetVar != null ? targetVar.getDeclType() : targetQual.getDecl().getDeclType();
		if(ownerType instanceof ExternalObjectTypeNode) {
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner)ownerType;
				o.fixupDefinition(externalProcedureUnresolved);

				externalProcedureDecl = resolver.resolve(externalProcedureUnresolved, this);
				if(externalProcedureDecl == null) {
					externalProcedureUnresolved.reportError(
							"Unknown external procedure method called -- misspelled procedure name? Or function call intended (not possible when assignment target is given as (param,...)=call denoting a procedure call)?");
					return false;
				}

				successfullyResolved = externalProcedureDecl != null && successfullyResolved;
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
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			reportError("external procedure method call not allowed in function or lhs context");
			return false;
		}
		return checkSignatureAdhered(externalProcedureDecl, externalProcedureUnresolved, true);
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
		return externalProcedureDecl.getResultTypes();
	}

	public int getNumReturnTypes()
	{
		return externalProcedureDecl.resultTypesCollectNode.size();
	}

	@Override
	protected IR constructIR()
	{
		ExternalProcedureMethodInvocation epi;
		if(targetQual != null) {
			epi = new ExternalProcedureMethodInvocation(targetQual.checkIR(Qualification.class),
					externalProcedureDecl.checkIR(ExternalProcedure.class));
		} else {
			epi = new ExternalProcedureMethodInvocation(targetVar.checkIR(Variable.class),
					externalProcedureDecl.checkIR(ExternalProcedure.class));
		}
		for(ExprNode expr : arguments.getChildren()) {
			expr = expr.evaluate();
			epi.addArgument(expr.checkIR(Expression.class));
		}
		for(TypeNode type : externalProcedureDecl.resultTypesCollectNode.getChildren()) {
			epi.addReturnType(type.checkIR(Type.class));
		}
		return epi;
	}
}
