/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Model;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.Sequence;
import de.unika.ipd.grgen.ir.Unit;

/**
 * The main node of the text. It is the root of the AST.
 */
public class UnitNode extends BaseNode {
	static {
		setName(UnitNode.class, "unit declaration");
	}

	private ModelNode stdModel;
	private CollectNode<ModelNode> models;

	// of type PatternTestDeclNode or PatternRuleDeclNode
	private CollectNode<SubpatternDeclNode> subpatterns;
	private CollectNode<IdentNode> subpatternsUnresolved;

	// of type TestDeclNode or RuleDeclNode
	private CollectNode<TestDeclNode> actions;
	private CollectNode<IdentNode> actionsUnresolved;

	// of type TestDeclNode or RuleDeclNode
	private CollectNode<SequenceDeclNode> sequences;
	private CollectNode<IdentNode> sequencesUnresolved;

	/**
	 * The name for this unit node
	 */
	private String unitname;

	/**
	 * The filename for this main node.
	 */
	private String filename;

	public UnitNode(String unitname, String filename, ModelNode stdModel, CollectNode<ModelNode> models,
			CollectNode<IdentNode> subpatterns, CollectNode<IdentNode> actions, CollectNode<IdentNode> sequences) {
		this.stdModel = stdModel;
		this.models = models;
		becomeParent(this.models);
		this.subpatternsUnresolved = subpatterns;
		becomeParent(this.subpatternsUnresolved);
		this.actionsUnresolved = actions;
		becomeParent(this.actionsUnresolved);
		this.sequencesUnresolved = sequences;
		becomeParent(this.sequencesUnresolved);
		this.unitname = unitname;
		this.filename = filename;
	}

	protected ModelNode getStdModel() {
		return stdModel;
	}

	public void addModel(ModelNode model) {
		models.addChild(model);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(models);
		children.add(getValidVersion(subpatternsUnresolved, subpatterns));
		children.add(getValidVersion(actionsUnresolved, actions));
		children.add(getValidVersion(sequencesUnresolved, sequences));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("models");
		childrenNames.add("subpatterns");
		childrenNames.add("actions");
		childrenNames.add("sequences");
		return childrenNames;
	}

	private static final CollectResolver<TestDeclNode> actionsResolver = new CollectResolver<TestDeclNode>(
			new DeclarationResolver<TestDeclNode>(TestDeclNode.class));

	private static final CollectResolver<SubpatternDeclNode> subpatternsResolver = new CollectResolver<SubpatternDeclNode>(
			new DeclarationResolver<SubpatternDeclNode>(SubpatternDeclNode.class));

	private static final CollectResolver<SequenceDeclNode> sequencesResolver = new CollectResolver<SequenceDeclNode>(
			new DeclarationResolver<SequenceDeclNode>(SequenceDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		actions     = actionsResolver.resolve(actionsUnresolved, this);
		subpatterns = subpatternsResolver.resolve(subpatternsUnresolved, this);
		sequences   = sequencesResolver.resolve(sequencesUnresolved, this);

		return actions != null && subpatterns != null && sequences != null;
	}

	/** Check the collect nodes containing the model declarations, subpattern declarations, action declarations
	 *  @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		Checker modelChecker = new CollectChecker(new SimpleChecker(ModelNode.class));
		return modelChecker.check(models, error);
	}

	/**
	 * Get the IR unit node for this AST node.
	 * @return The Unit for this AST node.
	 */
	public Unit getUnit() {
		return checkIR(Unit.class);
	}

	/**
	 * Construct the IR object for this AST node.
	 * For a main node, this is a unit.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		Unit res = new Unit(unitname, filename);

		for(ModelNode n : models.getChildren()) {
			Model model = n.getModel();
			res.addModel(model);
		}

		for(SubpatternDeclNode n : subpatterns.getChildren()) {
			Rule rule = n.getAction();
			res.addSubpatternRule(rule);
		}

		for(TestDeclNode n : actions.getChildren()) {
			Rule rule = n.getAction();
			res.addActionRule(rule);
		}

		for(SequenceDeclNode n : sequences.getChildren()) {
			Sequence sequence = n.getSequence();
			res.addSequence(sequence);
		}

		return res;
	}
}
