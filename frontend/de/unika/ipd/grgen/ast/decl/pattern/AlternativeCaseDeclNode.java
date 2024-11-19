/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.executable.NestedMatcherDeclNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.AlternativeCaseTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.executable.Rule.RuleKind;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
import de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
import de.unika.ipd.grgen.ir.stmt.EvalStatements;

/**
 * AST node for an alternative case pattern, maybe including replacements.
 */
public class AlternativeCaseDeclNode extends NestedMatcherDeclNode
{
	static {
		setName(AlternativeCaseDeclNode.class, "alternative case");
	}

	private AlternativeCaseTypeNode type;

	/** Type for this declaration. */
	private static final TypeNode alternativeCaseType = new AlternativeCaseTypeNode();

	/**
	 * Make a new alternative case rule.
	 * @param id The identifier of this rule.
	 * @param left The left hand side (The pattern to match).
	 * @param right The right hand side.
	 */
	public AlternativeCaseDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
	{
		super(id, alternativeCaseType, left, right);
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

		Rule altCaseRule = new Rule(getIdentNode().getIdent(), RuleKind.AlternativeCase);

		// mark this node as already visited
		setIR(altCaseRule);

		PatternGraphLhs left = pattern.getPatternGraph();

		PatternGraphRhs rightPattern = null;
		if(right != null) {
			rightPattern = right.getPatternGraph(left);
		}

		altCaseRule.initialize(left, rightPattern);

		constructImplicitNegs(left);
		constructIRaux(altCaseRule, right);

		// add Eval statements to the IR
		if(right != null) {
			for(EvalStatements evalStatement : right.getRhsGraph().getEvalStatements()) {
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
