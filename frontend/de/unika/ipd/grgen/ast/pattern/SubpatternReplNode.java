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
			error.error("A dependent replacement can only be invoked for a lhs subpattern usage; "
					+ "a rhs subpattern usage gets instantiated and can't be rewritten");
			return false;
		}

		// check whether the used pattern contains one rhs
		if(right == null) {
			error.error(getCoords(), "No dependent replacement specified in \"" + patternName + "\" ");
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
			subpattern.ident.reportError("The dependent replacement specified in \"" + patternName + "\" needs "
					+ expected + " parameters, given by replacement usage " + subpattern.ident.toString() + " are "
					+ actual);
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
	
		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(!parameterElement.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is yielded to, "
						+ "but the rewrite parameter at this position is not declared as def"
						+ "(" + parameterElement.getIdentNode() + ")");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(!parameterVar.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is yielded to, "
						+ "but the rewrite parameter at this position is not declared as def"
						+ "(" + parameterVar.getIdentNode() + ")");
			}
		}
		
		BaseNode argument = ((IdentExprNode)actualParameter).getResolvedNode();
		if(argument instanceof VarDeclNode) {
			VarDeclNode argumentVar = (VarDeclNode)argument;
			if((argumentVar.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				res = false;
				subpatternUnresolved.reportError("can't yield from a RHS subpattern rewrite call to a LHS def variable "
						+ "(" + argumentVar.getIdentNode() + ")");
			}
		} else { //if(argument instanceof ConstraintDeclNode)
			ConstraintDeclNode argumentElement = (ConstraintDeclNode)argument;
			if((argumentElement.context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
				res = false;
				subpatternUnresolved.reportError("can't yield from a RHS subpattern rewrite call to a LHS def pattern graph element "
						+ "(" + argumentElement.getIdentNode() + ")");
			}
		}
		
		if(!formalParameterType.isCompatibleTo(actualParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern replacement argument of type \""
					+ exprTypeName + "\" can't be yielded to from the subpattern rewrite def parameter type \""
					+ paramTypeName + "\"");
		}
		
		return res;
	}

	private boolean checkParameter(int i, ExprNode actualParameter, DeclNode formalParameter)
	{
		boolean res = true;

		TypeNode actualParameterType = actualParameter.getType();
		TypeNode formalParameterType = formalParameter.getDeclType();

		if(formalParameter instanceof ConstraintDeclNode) {
			ConstraintDeclNode parameterElement = (ConstraintDeclNode)formalParameter;
			if(parameterElement.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is not yielded to, "
						+ "but the rewrite parameter at this position is declared as def"
						+ "(" + parameterElement.getIdentNode() + ")");
			}
		} else { //if(formalParameter instanceof VarDeclNode)
			VarDeclNode parameterVar = (VarDeclNode)formalParameter;
			if(parameterVar.defEntityToBeYieldedTo) {
				res = false;
				subpatternUnresolved.reportError("The " + (i + 1) + ". subpattern rewrite argument is not yielded to, "
						+ "but the rewrite parameter at this position is declared as def"
						+ "(" + parameterVar.getIdentNode() + ")");
			}
		}
		
		if(!actualParameterType.isCompatibleTo(formalParameterType)) {
			res = false;
			String exprTypeName = actualParameterType.getTypeName();
			String paramTypeName = formalParameterType.getTypeName();
			subpatternUnresolved.reportError("Cannot convert " + (i + 1) + ". subpattern replacement argument from \""
					+ exprTypeName + "\" to \"" + paramTypeName + "\"");
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
