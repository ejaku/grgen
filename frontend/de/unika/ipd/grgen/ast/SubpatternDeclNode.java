/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.exprevals.*;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.exprevals.EvalStatements;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.ir.Rule;


/**
 * AST node for a pattern with replacements.
 */
public class SubpatternDeclNode extends ActionDeclNode  {
	static {
		setName(SubpatternDeclNode.class, "subpattern declaration");
	}

	protected RhsDeclNode right;
	private SubpatternTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode subpatternType = new SubpatternTypeNode();

	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 */
	public SubpatternDeclNode(IdentNode id, PatternGraphNode left, RhsDeclNode right) {
		super(id, subpatternType, left);
		this.right = right;
		becomeParent(this.right);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(pattern);
		if(right != null)
			children.add(right);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("pattern");
		if(right != null)
			childrenNames.add("right");
		return childrenNames;
	}

	private static DeclarationTypeResolver<SubpatternTypeNode> typeResolver =
		new DeclarationTypeResolver<SubpatternTypeNode>(SubpatternTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		type = typeResolver.resolve(typeUnresolved, this);

		boolean rewritePartRequired = false;
		for(AlternativeNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseNode altCase : alt.getChildren()) {
				if(altCase.right != null) {
					rewritePartRequired = true;
				}
			}
		}
		
		for(IteratedNode iter : pattern.iters.getChildren()) {
			if(iter.right != null) {
				rewritePartRequired = true;
			}
		}
		
		if(right == null && rewritePartRequired) {
			CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
			CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
			CollectNode<SubpatternUsageNode> subpatterns = new CollectNode<SubpatternUsageNode>();
			CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
			CollectNode<EvalStatementsNode> evals = new CollectNode<EvalStatementsNode>();
			CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
			CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
			GraphNode graph = new GraphNode(getIdentNode().toString(), getIdentNode().getCoords(), 
				connections, new CollectNode<BaseNode>(), subpatterns, new CollectNode<SubpatternReplNode>(),
				orderedReplacements, returnz, imperativeStmts,
				BaseNode.CONTEXT_PATTERN|BaseNode.CONTEXT_RHS, pattern);
			graph.addDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
			graph.addEvals(evals);
			right = new ModifyDeclNode(getIdentNode(), graph, new CollectNode<IdentNode>());
			getIdentNode().setDecl(this);
		}
		
		return type != null;
	}

	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		if(right != null)
			right.warnElemAppearsInsideAndOutsideDelete(pattern);

		boolean leftHandGraphsOk = checkLeft();

		boolean noReturnInPatternOk = true;
		if(pattern.returns.size() > 0) {
			error.error(getCoords(), "No return statements in pattern parts of rules allowed");
			noReturnInPatternOk = false;
		}

		boolean abstr = true;
		boolean rhsReuseOk = true;
		boolean execParamsNotDeleted = true;
		boolean sameNumberOfRewriteParts = sameNumberOfRewriteParts(right, "subpattern");
		boolean noNestedRewriteParameters = true;
		if(right != null) {
			rhsReuseOk = checkRhsReuse(right);
			execParamsNotDeleted = checkExecParamsNotDeleted(right);
			noNestedRewriteParameters = noNestedRewriteParameters(right, "subpattern");
			abstr = noAbstractElementInstantiated(right);
		}

		return leftHandGraphsOk & sameNumberOfRewriteParts && noNestedRewriteParameters
			& rhsReuseOk & noReturnInPatternOk
			& execParamsNotDeleted & abstr;
	}

	protected PatternGraphNode getPattern() {
		assert isResolved();
		return pattern;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	// TODO support only one rhs
	@Override
	protected IR constructIR() {
		PatternGraph left = pattern.getPatternGraph();

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			if(right != null) {
				addReplacementParamsToNestedAlternativesAndIterateds((Rule)getIR(), right);
			}
			return getIR();
		}

		PatternGraph rightPattern = null;
		if(this.right != null) {
			rightPattern = this.right.getPatternGraph(left);
		}

		// return if the pattern graph already constructed the IR object
		// that may happens in recursive patterns
		if (isIRAlreadySet()) {
			if(right != null) {
				addReplacementParamsToNestedAlternativesAndIterateds((Rule)getIR(), right);
			}
			return getIR();
		}

		Rule rule = new Rule(getIdentNode().getIdent(), left, rightPattern);
		
		constructImplicitNegs(left);
		constructIRaux(rule, right);

		// add Eval statements to the IR
		if(this.right != null) {
			for (EvalStatements n : this.right.getRHSGraph().getYieldEvalStatements()) {
				rule.addEval(n);
			}
		}

		return rule;
	}

	/**
	 * add NACs for induced- or DPO-semantic
	 */
	private void constructImplicitNegs(PatternGraph left) {
		PatternGraphNode leftNode = pattern;
		for (PatternGraph neg : leftNode.getImplicitNegGraphs()) {
			left.addNegGraph(neg);
		}
	}

	@Override
	public SubpatternTypeNode getDeclType() {
		assert isResolved();

		return type;
	}

	public static String getKindStr() {
		return "subpattern declaration";
	}

	public static String getUseStr() {
		return "subpattern";
	}
}
