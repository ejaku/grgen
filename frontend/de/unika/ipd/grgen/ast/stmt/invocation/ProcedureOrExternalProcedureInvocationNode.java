/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclBaseNode;
import de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.ExternalProcedure;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.stmt.invocation.ExternalProcedureInvocation;
import de.unika.ipd.grgen.ir.stmt.invocation.ProcedureInvocation;

/**
 * Invocation of a procedure or an external procedure
 */
public class ProcedureOrExternalProcedureInvocationNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureOrExternalProcedureInvocationNode.class, "procedure or external procedure invocation");
	}

	private IdentNode procedureOrExternalProcedureUnresolved;
	private ExternalProcedureDeclNode externalProcedureDecl;
	private ProcedureDeclNode procedureDecl;

	public ProcedureOrExternalProcedureInvocationNode(IdentNode procedureOrExternalProcedureUnresolved,
			CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords(), arguments, context);
		this.procedureOrExternalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(procedureOrExternalProcedureUnresolved, procedureDecl, externalProcedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("procedure or external procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode> resolver =
			new DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode>(ProcedureDeclNode.class, ExternalProcedureDeclNode.class);

	protected boolean resolveLocal()
	{
		if(!(procedureOrExternalProcedureUnresolved instanceof PackageIdentNode)) {
			fixupDefinition((IdentNode)procedureOrExternalProcedureUnresolved, procedureOrExternalProcedureUnresolved.getScope());
		}
		Pair<ProcedureDeclNode, ExternalProcedureDeclNode> resolved = resolver.resolve(procedureOrExternalProcedureUnresolved, this);
		if(resolved == null) {
			procedureOrExternalProcedureUnresolved.reportError("Unknown procedure called -- misspelled procedure name? Or function call intended (not possible when assignment target is given as (param,...)=call denoting a procedure call)?");
			return false;
		}
		procedureDecl = resolved.fst;
		externalProcedureDecl = resolved.snd;
		return true;
	}

	@Override
	protected boolean checkLocal()
	{
		if((context & BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE) == BaseNode.CONTEXT_FUNCTION) {
			reportError("procedure call not allowed in function or lhs context");
			return false;
		}
		return checkSignatureAdhered();
	}

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop)
	{
		return true;
	}

	/** Check whether the usage adheres to the signature of the declaration */
	private boolean checkSignatureAdhered()
	{
		ProcedureDeclBaseNode pb = procedureDecl != null ? procedureDecl : externalProcedureDecl;
		return checkSignatureAdhered(pb, procedureOrExternalProcedureUnresolved, false);
	}

	public Vector<TypeNode> getType()
	{
		assert isResolved();
		return procedureDecl != null ? procedureDecl.getResultTypes() : externalProcedureDecl.getResultTypes();
	}

	public int getNumReturnTypes()
	{
		if(procedureDecl != null)
			return procedureDecl.resultTypesCollectNode.size();
		else
			return externalProcedureDecl.resultTypesCollectNode.size();
	}

	@Override
	protected IR constructIR()
	{
		if(procedureDecl != null) {
			ProcedureInvocation pi = new ProcedureInvocation(procedureDecl.checkIR(Procedure.class));
			for(ExprNode expr : arguments.getChildren()) {
				pi.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : procedureDecl.resultTypesCollectNode.getChildren()) {
				pi.addReturnType(type.checkIR(Type.class));
			}
			return pi;
		} else {
			ExternalProcedureInvocation epi = new ExternalProcedureInvocation(
					externalProcedureDecl.checkIR(ExternalProcedure.class));
			for(ExprNode expr : arguments.getChildren()) {
				epi.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : externalProcedureDecl.resultTypesCollectNode.getChildren()) {
				epi.addReturnType(type.checkIR(Type.class));
			}
			return epi;
		}
	}
}
