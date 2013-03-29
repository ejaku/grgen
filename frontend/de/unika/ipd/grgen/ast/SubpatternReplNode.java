/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.0
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.exprevals.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternReplNode extends OrderedReplacementNode {
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}

	private IdentNode subpatternUnresolved;
	private SubpatternUsageNode subpattern;
	private CollectNode<ExprNode> replConnections;


	public SubpatternReplNode(IdentNode n, CollectNode<ExprNode> c) {
		this.subpatternUnresolved = n;
		becomeParent(this.subpatternUnresolved);
		this.replConnections = c;
		becomeParent(this.replConnections);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(subpatternUnresolved, subpattern));
		children.add(replConnections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpattern");
		childrenNames.add("replConnections");
		return childrenNames;
	}

	private static final DeclarationResolver<SubpatternUsageNode> subpatternResolver =
		new DeclarationResolver<SubpatternUsageNode>(SubpatternUsageNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		subpattern = subpatternResolver.resolve(subpatternUnresolved, this);
		return subpattern!=null;
	}

	@Override
	protected boolean checkLocal() {
		Collection<RhsDeclNode> right = subpattern.type.right.getChildren();
		String patternName = subpattern.type.pattern.nameOfGraph;

		if((subpattern.context & CONTEXT_LHS_OR_RHS) != CONTEXT_LHS) {
			error.error("A dependent replacement can only be invoked for a lhs subpattern usage; a rhs subpattern usage gets instantiated and can't be rewritten");
			return false;
		}
		
		// check whether the used pattern contains one rhs
		if(right.size()!=1) {
			error.error(getCoords(), "No dependent replacement specified in \"" + patternName + "\" ");
			return false;
		}

		return checkSubpatternSignatureAdhered();
	}

	/** Check whether the subpattern replacement usage adheres to the signature of the subpattern replacement declaration */
	private boolean checkSubpatternSignatureAdhered() {
		// check if the number of parameters is correct
		String patternName = subpattern.type.pattern.nameOfGraph;
		Collection<RhsDeclNode> right = subpattern.type.right.getChildren();
		Vector<DeclNode> formalReplacementParameters = right.iterator().next().graph.getParamDecls();
		int expected = formalReplacementParameters.size();
		int actual = replConnections.children.size();
		if (expected != actual) {
			subpattern.ident.reportError("The dependent replacement specified in \"" + patternName + "\" needs "
			        + expected + " parameters, given by replacement usage " + subpattern.ident.toString() + " are " + actual);
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for (int i = 0; i < formalReplacementParameters.size(); ++i) {
			ExprNode actualParameter = replConnections.children.get(i);
			TypeNode actualParameterType = actualParameter.getType();
			DeclNode formalParameter = formalReplacementParameters.get(i);
			TypeNode formalParameterType = formalParameter.getDeclType();
			if(actualParameter instanceof IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo) {
				if(formalParameter instanceof ConstraintDeclNode) {
					if(!((ConstraintDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is yielded but the rewrite parameter at this position is not declared as def");
					}
				}
				if(formalParameter instanceof VarDeclNode) {
					if(!((VarDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is yielded but the rewrite parameter at this position is not declared as def");
					}
				}
				BaseNode argument = ((IdentExprNode)actualParameter).getResolvedNode();
				if(argument instanceof VarDeclNode) {
					if((((VarDeclNode)argument).context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
						subpatternUnresolved.reportError("can't yield from a RHS subpattern rewrite call to a LHS def variable ("+((VarDeclNode)argument).getIdentNode()+")");
						return false;
					}					
				}
				else if(argument instanceof ConstraintDeclNode) {
					if((((ConstraintDeclNode)argument).context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
						subpatternUnresolved.reportError("can't yield from a RHS subpattern rewrite call to a LHS def graph element ("+((ConstraintDeclNode)argument).getIdentNode()+")");
						return false;
					}
				}
				if(!formalParameterType.isCompatibleTo(actualParameterType)) {
					res = false;
					String exprTypeName = getTypeName(actualParameterType);
					String paramTypeName = getTypeName(formalParameterType);
					subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern replacement argument of type \""
							+ exprTypeName + "\" can't be yielded to from the subpattern rewrite def parameter type \"" + paramTypeName + "\"");
				}
			} else {
				if(formalParameter instanceof ConstraintDeclNode) {
					if(((ConstraintDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is not yielded but the rewrite parameter at this position is declared as def");
					}
				}
				if(formalParameter instanceof VarDeclNode) {
					if(((VarDeclNode)formalParameter).defEntityToBeYieldedTo) {
						res = false;
						subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is not yielded but the rewrite parameter at this position is declared as def");
					}
				}
				if(!actualParameterType.isCompatibleTo(formalParameterType)) {
					res = false;
					String exprTypeName = getTypeName(actualParameterType);
					String paramTypeName = getTypeName(formalParameterType);
					subpatternUnresolved.reportError("Cannot convert " + (i + 1) + ". subpattern replacement argument from \""
							+ exprTypeName + "\" to \"" + paramTypeName + "\"");
				}
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

	@Override
	protected IR constructIR() {
		List<Expression> replConnections = new LinkedList<Expression>();
    	for (ExprNode e : this.replConnections.getChildren()) {
    		replConnections.add(e.checkIR(Expression.class));
    	}
		return new SubpatternDependentReplacement("dependent replacement", subpatternUnresolved.getIdent(),
				subpattern.checkIR(SubpatternUsage.class), replConnections);
	}
}
