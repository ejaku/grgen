/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.LinkedList;
import java.util.List;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SubpatternDependentReplacement;
import de.unika.ipd.grgen.ir.SubpatternUsage;

public class SubpatternReplNode extends BaseNode {
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
			if(!actualParameterType.isCompatibleTo(formalParameterType)) {
				res = false;
				String exprTypeName;
				if(actualParameterType instanceof InheritanceTypeNode)
					exprTypeName = ((InheritanceTypeNode) actualParameterType).getIdentNode().toString();
				else
					exprTypeName = actualParameterType.toString();
				subpatternUnresolved.reportError("Cannot convert " + (i + 1) + ". subpattern replacement argument from \""
						+ exprTypeName + "\" to \"" + formalParameterType.toString() + "\"");
			}
		}

		return res;
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
