/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.executable;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.ModifyDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.RhsDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.SubpatternUsageDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
import de.unika.ipd.grgen.ast.pattern.OrderedReplacementsNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.pattern.SubpatternReplNode;
import de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.executable.SubpatternTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;

/**
 * AST node for a pattern with replacements.
 */
public class SubpatternDeclNode extends MatcherDeclNode
{
	static {
		setName(SubpatternDeclNode.class, "subpattern declaration");
	}

	public RhsDeclNode right;
	private SubpatternTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode subpatternType = new SubpatternTypeNode();

	/**
	 * Make a new rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 */
	public SubpatternDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
	{
		super(id, subpatternType, left);
		this.right = right;
		becomeParent(this.right);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
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
	public Collection<String> getChildrenNames()
	{
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
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);

		if(right == null && rewritePartRequired()) {
			CollectNode<BaseNode> connections = new CollectNode<BaseNode>();
			CollectNode<VarDeclNode> defVariablesToBeYieldedTo = new CollectNode<VarDeclNode>();
			CollectNode<SubpatternUsageDeclNode> subpatterns = new CollectNode<SubpatternUsageDeclNode>();
			CollectNode<OrderedReplacementsNode> orderedReplacements = new CollectNode<OrderedReplacementsNode>();
			CollectNode<EvalStatementsNode> evalStatments = new CollectNode<EvalStatementsNode>();
			CollectNode<ExprNode> returnz = new CollectNode<ExprNode>();
			CollectNode<BaseNode> imperativeStmts = new CollectNode<BaseNode>();
			PatternGraphRhsNode patternGraph = new PatternGraphRhsNode(getIdentNode().toString(), getIdentNode().getCoords(),
					connections, new CollectNode<BaseNode>(), subpatterns, new CollectNode<SubpatternReplNode>(),
					orderedReplacements, returnz, imperativeStmts,
					BaseNode.CONTEXT_PATTERN | BaseNode.CONTEXT_RHS, pattern);
			patternGraph.addDefVariablesToBeYieldedTo(defVariablesToBeYieldedTo);
			patternGraph.addEvals(evalStatments);
			right = new ModifyDeclNode(getIdentNode(), patternGraph, new CollectNode<IdentNode>());
			getIdentNode().setDecl(this);
		}

		return type != null;
	}

	private boolean rewritePartRequired()
	{
		for(AlternativeDeclNode alt : pattern.alts.getChildren()) {
			for(AlternativeCaseDeclNode altCase : alt.getChildren()) {
				if(altCase.right != null) {
					return true;
				}
			}
		}

		for(IteratedDeclNode iter : pattern.iters.getChildren()) {
			if(iter.right != null) {
				return true;
			}
		}
		
		return false;
	}

	/**
	 * Check, if the rule type node is right.
	 * The children of a rule type are
	 * 1) a pattern for the left side.
	 * 2) a pattern for the right side.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		boolean leftHandGraphsOk = checkLeft();

		boolean rightHandGraphsOk = true;
		if(right != null)
			rightHandGraphsOk = right.checkAgainstLhsPattern(pattern);

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
		boolean noAmbiguousRetypes = true;
		if(right != null) {
			rhsReuseOk = checkRhsReuse(right);
			execParamsNotDeleted = checkExecParamsNotDeleted(right);
			noNestedRewriteParameters = noNestedRewriteParameters(right, "subpattern");
			abstr = noAbstractElementInstantiated(right);
			noAmbiguousRetypes = noAmbiguousRetypes(right);
		}

		return leftHandGraphsOk
				& rightHandGraphsOk
				& sameNumberOfRewriteParts 
				& noNestedRewriteParameters
				& rhsReuseOk
				& noReturnInPatternOk
				& execParamsNotDeleted
				& abstr
				& noAmbiguousRetypes;
	}

	public PatternGraphLhsNode getPattern()
	{
		assert isResolved();
		return pattern;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		// return if the pattern graph already constructed the IR object
		// that may happen in recursive patterns (and other usages/references)
		if(isIRAlreadySet()) {
			return getIR();
		}

		Rule rule = new Rule(getIdentNode().getIdent());

		// mark this node as already visited
		setIR(rule);

		PatternGraphLhs left = pattern.getPatternGraph();

		PatternGraphRhs rightPattern = null;
		if(this.right != null) {
			rightPattern = this.right.getPatternGraph(left);
		}

		rule.initialize(left, rightPattern);

		constructImplicitNegs(left);
		constructIRaux(rule, right);

		// add Eval statements to the IR
		if(this.right != null) {
			for(EvalStatements n : this.right.getRhsGraph().getEvalStatements()) {
				rule.addEval(n);
			}
		}

		return rule;
	}

	@Override
	public SubpatternTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public static String getKindStr()
	{
		return "subpattern";
	}
}
