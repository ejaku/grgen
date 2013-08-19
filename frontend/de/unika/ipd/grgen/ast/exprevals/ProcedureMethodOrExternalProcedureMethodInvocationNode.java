/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedure;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.exprevals.Procedure;
import de.unika.ipd.grgen.ir.exprevals.ProcedureMethodInvocation;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of a procedure method or an external procedure method
 */
public class ProcedureMethodOrExternalProcedureMethodInvocationNode extends ProcedureMethodInvocationBaseNode
{
	static {
		setName(ProcedureMethodOrExternalProcedureMethodInvocationNode.class, "procedure method or external procedure method invocation");
	}

	private IdentNode ownerUnresolved;
	private DeclNode owner;

	private IdentNode procedureOrExternalProcedureUnresolved;
	private ExternalProcedureDeclNode externalProcedureDecl;
	private ProcedureDeclNode procedureDecl;
	
	private CollectNode<ExprNode> arguments;

	public ProcedureMethodOrExternalProcedureMethodInvocationNode(IdentNode owner, IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords());
		this.ownerUnresolved = becomeParent(owner);
		this.procedureOrExternalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(procedureOrExternalProcedureUnresolved, procedureDecl, externalProcedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("procedure or external procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver = new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode> resolver =
			new DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode>(ProcedureDeclNode.class, ExternalProcedureDeclNode.class);

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
				res = o.fixupDefinition(procedureOrExternalProcedureUnresolved);

				Pair<ProcedureDeclNode, ExternalProcedureDeclNode> resolved = resolver.resolve(procedureOrExternalProcedureUnresolved, this);
				if(resolved == null) {
					procedureOrExternalProcedureUnresolved.reportError("Unknown procedure called -- misspelled procedure name? Or function call intended (not possible when assignment target is given as (param,...)=call denoting a procedure call)?");
					return false;
				}
				procedureDecl = resolved.fst;
				externalProcedureDecl = resolved.snd;

				successfullyResolved = (procedureDecl!=null || externalProcedureDecl!=null) && successfullyResolved;
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

	public boolean checkStatementLocal(boolean isLHS, DeclNode root, EvalStatementNode enclosingLoop) {
		return true;
	}

	/** Check whether the usage adheres to the signature of the declaration */
	private boolean checkSignatureAdhered() {
		ProcedureBase pb = procedureDecl!=null ? procedureDecl : externalProcedureDecl;
		
		// check if the number of parameters are correct
		int expected = pb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = pb.ident.toString();
			procedureOrExternalProcedureUnresolved.reportError("The procedure \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = pb.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				procedureOrExternalProcedureUnresolved.reportError("Cannot convert " + (i + 1) + ". procedure argument from \""
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

	public Vector<TypeNode> getType() {
		assert isResolved();
		return procedureDecl!=null ? procedureDecl.getReturnTypes() : externalProcedureDecl.getReturnTypes();
	}
	
	public int getNumReturnTypes() {
		if(procedureDecl!=null)
			return procedureDecl.returnTypes.size();
		else
			return externalProcedureDecl.returnTypes.size();
	}

	@Override
	protected IR constructIR() {
		if(procedureDecl!=null) {
			ProcedureMethodInvocation pi = new ProcedureMethodInvocation(
					owner.checkIR(Entity.class),
					procedureDecl.checkIR(Procedure.class));
			for(ExprNode expr : arguments.getChildren()) {
				pi.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : procedureDecl.returnTypes.children) {
				pi.addReturnType(type.checkIR(Type.class));
			}
			return pi;
		} else {
			ExternalProcedureMethodInvocation epi = new ExternalProcedureMethodInvocation(
					owner.checkIR(Entity.class),
					externalProcedureDecl.checkIR(ExternalProcedure.class));
			for(ExprNode expr : arguments.getChildren()) {
				epi.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : externalProcedureDecl.returnTypes.children) {
				epi.addReturnType(type.checkIR(Type.class));
			}
			return epi;
		}
	}
}
