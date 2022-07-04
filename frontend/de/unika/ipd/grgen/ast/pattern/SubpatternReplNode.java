/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ConstraintDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.IdentExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.pattern.SubpatternUsage;

public class SubpatternReplNode extends OrderedReplacementNode
{
	static {
		setName(SubpatternReplNode.class, "subpattern repl node");
	}

	private IdentNode subpatternUnresolved;
	private SubpatternUsageDeclNode subpattern;
	private CollectNode<ExprNode> replConnections;

	public SubpatternReplNode(IdentNode n, CollectNode<ExprNode> c)
	{
		this.subpatternUnresolved = n;
		becomeParent(this.subpatternUnresolved);
		this.replConnections = c;
		becomeParent(this.replConnections);
	}

	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(subpatternUnresolved, subpattern));
		children.add(replConnections);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("subpattern");
		childrenNames.add("replConnections");
		return childrenNames;
	}

	private static final DeclarationResolver<SubpatternUsageDeclNode> subpatternResolver =
			new DeclarationResolver<SubpatternUsageDeclNode>(SubpatternUsageDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		subpattern = subpatternResolver.resolve(subpatternUnresolved, this);
		return subpattern != null;
	}

	@Override
	protected boolean checkLocal()
	{
		RhsDeclNode right = subpattern.type.right;
		String patternName = subpattern.type.pattern.nameOfGraph;

		if((subpattern.context & CONTEXT_LHS_OR_RHS) != CONTEXT_LHS) {
			error.error("A subpattern rewrite application (" + subpatternUnresolved + ") can only be given for a subpattern entity declaration in the pattern part;"
					+ " a subpattern entity declaration in the rewrite part gets instantiated and cannot be rewritten (remove the parenthesis if the latter is intended).");
			return false;
		}

		// check whether the used pattern contains one rhs
		if(right == null) {
			error.error(getCoords(), "No rewrite part specified in subpattern " + patternName + " (which is referenced by the subpattern rewrite application " + subpatternUnresolved + ").");
			return false;
		}

		return checkSubpatternSignatureAdhered();
	}

	/** Check whether the subpattern replacement usage adheres to the signature of the subpattern replacement declaration */
	private boolean checkSubpatternSignatureAdhered()
	{
		// check if the number of parameters is correct
		String patternName = subpattern.type.pattern.nameOfGraph;
		RhsDeclNode right = subpattern.type.right;
		Vector<DeclNode> formalReplacementParameters = right.patternGraph.getParamDecls();
		int expected = formalReplacementParameters.size();
		int actual = replConnections.size();
		if(expected != actual) {
			subpattern.ident.reportError("The rewrite part specified in " + patternName + " comes with "
					+ expected + " parameters, but given by the subpattern rewrite application " + subpatternUnresolved + " are "
					+ actual + " arguments.");
			return false;
		}

		// check if the types of the parameters are correct
		boolean res = true;
		for(int i = 0; i < formalReplacementParameters.size(); ++i) {
			ExprNode actualParameter = replConnections.get(i);
			DeclNode formalParameter = formalReplacementParameters.get(i);
			if(actualParameter instanceof IdentExprNode && ((IdentExprNode)actualParameter).yieldedTo) {
				res &= checkYieldedToParameter(i, actualParameter, formalParameter);
			} else {
				res &= checkParameter(i, actualParameter, formalParameter);
			}
		}
		return res;
	}

	private boolean checkYieldedToParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		boolean res = true;
	
		String patternName = subpattern.type.pattern.nameOfGraph;
		
		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(!parameterElement.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". argument to the subpattern rewrite application " + subpatternUnresolved + " is yielded to,"
						+ " but the rewrite parameter at this position " + "(" + parameterElement.getIdentNode() + " in " + patternName + ")" + " is not declared as def.");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(!parameterVar.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". argument to the subpattern rewrite application " + subpatternUnresolved + " is yielded to,"
						+ " but the rewrite parameter at this position " + "(" + parameterVar.getIdentNode() + " in " + patternName + ")" + " is not declared as def.");
			}
		}
		
		BaseNode argument = ((IdentExprNode)actualParameter).getResolvedNode();
		if(argument instanceof VarDeclNode) {
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if((argumentVar.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				res = false;
				subpatternUnresolved.reportError("Cannot yield from a subpattern rewrite application (" + (i + 1) + " argument of " + subpatternUnresolved + ") in the rewrite part"
						+ " to a def variable " + "(" + argumentVar.getIdentNode() + ") in the pattern part.");
			}
		} else { //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if((argumentElement.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				res = false;
				subpatternUnresolved.reportError("Cannot yield from a subpattern rewrite application (" + (i + 1) + " argument of " + subpatternUnresolved + ") in the rewrite part"
						+ " to a def graph element " + "(" + argumentElement.getIdentNode() + ") in the pattern part.");
			}
		}
		
		if(!formalParameterType.isCompatibleTo(actualParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			subpatternUnresolved.reportError("The " + (i + 1) + ". argument of type " + exprTypeName
					+ " (of the subpattern rewrite application " + subpatternUnresolved + ")"
					+ " cannot be yielded to from the rewrite def parameter of incompatible type " + paramTypeName
					+ " (" + formalParameter.getIdentNode() + " of subpattern " + patternName + ").");
		}
		
		return res;
	}

	private boolean checkParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		boolean res = true;

		String patternName = subpattern.type.pattern.nameOfGraph;

		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(parameterElement.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " is not yielded to,"
						+ " but the rewrite parameter at this position (" + parameterElement.getIdentNode() + " in " + patternName + ")"
						+ " is declared as def.");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(parameterVar.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " is not yielded to,"
						+ " but the rewrite parameter at this position (" + parameterVar.getIdentNode() + " in " + patternName + ")"
						+ " is declared as def.");
			}
		}
		
		if(!actualParameterType.isCompatibleTo(formalParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			subpatternUnresolved.reportError("Cannot convert " + (i + 1) + ". argument of the subpattern rewrite application " + subpatternUnresolved + " from "
					+ exprTypeName + " to " + paramTypeName + " (expected by the rewrite parameter " + formalParameter.getIdentNode() + " of subpattern " + patternName + ").");
		}
		
		return res;
	}

	public IdentNode getSubpatternIdent()
	{
		return subpatternUnresolved;
	}

	@Override
	protected IR constructIR()
	{
		List<Expression> replConnections = new LinkedList<Expression>();
		for(ExprNode e : this.replConnections.getChildren()) {
			e = e.evaluate();
			replConnections.add(e.checkIR(Expression.class));
		}
		return new SubpatternDependentReplacement("dependent replacement", subpatternUnresolved.getIdent(),
				subpattern.checkIR(SubpatternUsage.class), replConnections);
	}
}
