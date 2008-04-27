/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents an alternative, containing the alternative graph patterns
 */
public class AlternativeNode extends BaseNode {
	static {
		setName(AlternativeNode.class, "alternative");
	}

	Vector<AlternativeCaseNode> children = new Vector<AlternativeCaseNode>();

	public AlternativeNode(Coords coords) {
		super(coords);
	}

	public void addChild(AlternativeCaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		children.add(n);
	}

	/** returns children of this node */
	public Collection<AlternativeCaseNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		if (children.isEmpty()) {
			this.reportError("alternative is empty");
			return false;
		}

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		Alternative alternative = new Alternative();
		for (AlternativeCaseNode alternativeCaseNode : children) {
			Rule alternativeCaseRule = alternativeCaseNode.checkIR(Rule.class);
			alternative.addAlternativeCase(alternativeCaseRule);
		}
		return alternative;
	}
}
