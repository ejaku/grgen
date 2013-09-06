/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
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
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedure;
import de.unika.ipd.grgen.ir.exprevals.ExternalProcedureInvocation;
import de.unika.ipd.grgen.ir.exprevals.Procedure;
import de.unika.ipd.grgen.ir.exprevals.ProcedureInvocation;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

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
	private CollectNode<ExprNode> arguments;
	
	private int context;

	public ProcedureOrExternalProcedureInvocationNode(IdentNode procedureOrExternalProcedureUnresolved, CollectNode<ExprNode> arguments, int context)
	{
		super(procedureOrExternalProcedureUnresolved.getCoords());
		this.procedureOrExternalProcedureUnresolved = becomeParent(procedureOrExternalProcedureUnresolved);
		this.arguments = becomeParent(arguments);
		this.context = context;
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(procedureOrExternalProcedureUnresolved, procedureDecl, externalProcedureDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("procedure or external procedure");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode> resolver =
			new DeclarationPairResolver<ProcedureDeclNode, ExternalProcedureDeclNode>(ProcedureDeclNode.class, ExternalProcedureDeclNode.class);

	protected boolean resolveLocal() {
		fixupDefinition((IdentNode)procedureOrExternalProcedureUnresolved, procedureOrExternalProcedureUnresolved.getScope());
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
	protected boolean checkLocal() {
		if((context&BaseNode.CONTEXT_FUNCTION_OR_PROCEDURE)==BaseNode.CONTEXT_FUNCTION) {
			reportError("procedure call not allowed in function or lhs context");
			return false;
		}
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
			ProcedureInvocation pi = new ProcedureInvocation(
					procedureDecl.checkIR(Procedure.class));
			for(ExprNode expr : arguments.getChildren()) {
				pi.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : procedureDecl.returnTypes.children) {
				pi.addReturnType(type.checkIR(Type.class));
			}
			return pi;
		} else {
			ExternalProcedureInvocation epi = new ExternalProcedureInvocation(
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
