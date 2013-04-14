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
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.exprevals.Computation;
import de.unika.ipd.grgen.ir.exprevals.ComputationInvocation;
import de.unika.ipd.grgen.ir.exprevals.ExternalComputation;
import de.unika.ipd.grgen.ir.exprevals.ExternalComputationInvocation;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Type;

/**
 * Invocation of a computation or an external computation
 */
public class ComputationOrExternalComputationInvocationNode extends ComputationInvocationBaseNode
{
	static {
		setName(ComputationOrExternalComputationInvocationNode.class, "computation or external computation invocation");
	}

	private IdentNode computationOrExternalComputationUnresolved;
	private ExternalComputationDeclNode externalComputationDecl;
	private ComputationDeclNode computationDecl;
	private CollectNode<ExprNode> arguments;

	public ComputationOrExternalComputationInvocationNode(IdentNode computationOrExternalComputationUnresolved, CollectNode<ExprNode> arguments)
	{
		super(computationOrExternalComputationUnresolved.getCoords());
		this.computationOrExternalComputationUnresolved = becomeParent(computationOrExternalComputationUnresolved);
		this.arguments = becomeParent(arguments);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(computationOrExternalComputationUnresolved, computationDecl, externalComputationDecl));
		children.add(arguments);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("computation or external computation");
		childrenNames.add("arguments");
		return childrenNames;
	}

	private static final DeclarationPairResolver<ComputationDeclNode, ExternalComputationDeclNode> resolver =
			new DeclarationPairResolver<ComputationDeclNode, ExternalComputationDeclNode>(ComputationDeclNode.class, ExternalComputationDeclNode.class);

	protected boolean resolveLocal() {
		fixupDefinition((IdentNode)computationOrExternalComputationUnresolved, computationOrExternalComputationUnresolved.getScope());
		Pair<ComputationDeclNode, ExternalComputationDeclNode> resolved = resolver.resolve(computationOrExternalComputationUnresolved, this);
		if(resolved == null) {
			computationOrExternalComputationUnresolved.reportError("Unknown computation called -- misspelled computation name? Or function call intended (not possible when assignment target is given as (param,...)=call denoting a computation call)?");
			return false;
		}
		computationDecl = resolved.fst;
		externalComputationDecl = resolved.snd;
		return true;
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
		ComputationBase cb = computationDecl!=null ? computationDecl : externalComputationDecl;
		
		// check if the number of parameters are correct
		int expected = cb.getParameterTypes().size();
		int actual = arguments.getChildren().size();
		if (expected != actual) {
			String patternName = cb.ident.toString();
			computationOrExternalComputationUnresolved.reportError("The computation \"" + patternName + "\" needs "
					+ expected + " parameters, given are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < arguments.children.size(); ++i) {
			ExprNode actualParameter = arguments.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			TypeNode formalParameterType = cb.getParameterTypes().get(i);
			
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName = getTypeName(actualParameterType);
				String paramTypeName = getTypeName(formalParameterType);
				computationOrExternalComputationUnresolved.reportError("Cannot convert " + (i + 1) + ". computation argument from \""
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
		return computationDecl!=null ? computationDecl.getReturnTypes() : externalComputationDecl.getReturnTypes();
	}
	
	public int getNumReturnTypes() {
		if(computationDecl!=null)
			return computationDecl.returnTypes.size();
		else
			return externalComputationDecl.returnTypes.size();
	}

	@Override
	protected IR constructIR() {
		if(computationDecl!=null) {
			ComputationInvocation ci = new ComputationInvocation(
					computationDecl.checkIR(Computation.class));
			for(ExprNode expr : arguments.getChildren()) {
				ci.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : computationDecl.returnTypes.children) {
				ci.addReturnType(type.checkIR(Type.class));
			}
			return ci;
		} else {
			ExternalComputationInvocation eci = new ExternalComputationInvocation(
					externalComputationDecl.checkIR(ExternalComputation.class));
			for(ExprNode expr : arguments.getChildren()) {
				eci.addArgument(expr.checkIR(Expression.class));
			}
			for(TypeNode type : computationDecl.returnTypes.children) {
				eci.addReturnType(type.checkIR(Type.class));
			}
			return eci;
		}
	}
}
