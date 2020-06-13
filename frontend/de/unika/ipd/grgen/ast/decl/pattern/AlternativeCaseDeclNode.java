/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.AlternativeCaseTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.pattern.PatternGraph;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;

/**
 * AST node for an alternative case pattern, maybe including replacements.
 */
public class AlternativeCaseDeclNode extends ActionDeclNode
{
	static {
		setName(AlternativeCaseDeclNode.class, "alternative case");
	}

	public RhsDeclNode right;
	private AlternativeCaseTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode alternativeCaseType = new AlternativeCaseTypeNode();

	/**
	 * Make a new alternative case rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 */
	public AlternativeCaseDeclNode(IdentNode id, PatternGraphNode left, RhsDeclNode right)
	{
		super(id, alternativeCaseType, left);
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

	private static final DeclarationTypeResolver<AlternativeCaseTypeNode> typeResolver =
		new DeclarationTypeResolver<AlternativeCaseTypeNode>(AlternativeCaseTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);

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

		boolean noReturnInAlterntiveCaseReplacement = true;
		if(right != null) {
			if(right.graph.returns.size() > 0) {
				error.error(getCoords(), "No return statements in alternative cases allowed");
				noReturnInAlterntiveCaseReplacement = false;
			}
		}

		boolean rhsReuseOk = true;
		boolean execParamsNotDeleted = true;
		boolean sameNumberOfRewriteParts = sameNumberOfRewriteParts(right, "alternative case");
		boolean noNestedRewriteParameters = true;
		boolean abstr = true;
		if(right != null) {
			rhsReuseOk = checkRhsReuse(right);
			execParamsNotDeleted = checkExecParamsNotDeleted(right);
			noNestedRewriteParameters = noNestedRewriteParameters(right, "alternative case");
			abstr = noAbstractElementInstantiatedNestedPattern(right);
		}

		return leftHandGraphsOk
				& rightHandGraphsOk
				& sameNumberOfRewriteParts
				& noNestedRewriteParameters
				& rhsReuseOk
				& noReturnInPatternOk
				& noReturnInAlterntiveCaseReplacement
				& execParamsNotDeleted
				& abstr;
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

		Rule altCaseRule = new Rule(getIdentNode().getIdent());

		// mark this node as already visited
		setIR(altCaseRule);

		PatternGraph left = pattern.getPatternGraph();

		PatternGraph rightPattern = null;
		if(right != null) {
			rightPattern = right.getPatternGraph(left);
		}

		altCaseRule.initialize(left, rightPattern);

		constructImplicitNegs(left);
		constructIRaux(altCaseRule, right);

		// add Eval statements to the IR
		if(right != null) {
			for(EvalStatements evalStatement : right.getRhsGraph().getYieldEvalStatements()) {
				altCaseRule.addEval(evalStatement);
			}
		}

		return altCaseRule;
	}

	@Override
	public AlternativeCaseTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	public static String getKindStr()
	{
		return "alternative case";
	}
}
