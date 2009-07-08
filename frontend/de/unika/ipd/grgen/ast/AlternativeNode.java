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

	private Vector<AlternativeCaseNode> children = new Vector<AlternativeCaseNode>();

	public AlternativeNode(Coords coords) {
		super(coords);
	}

	public void addChild(AlternativeCaseNode n) {
		assert(!isResolved());
		becomeParent(n);
		children.add(n);
	}

	/** returns children of this node */
	@Override
	public Collection<AlternativeCaseNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	protected Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		// nameless children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal() {
		if (children.isEmpty()) {
			this.reportError("alternative is empty");
			return false;
		}

		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		Alternative alternative = new Alternative();
		for (AlternativeCaseNode alternativeCaseNode : children) {
			Rule alternativeCaseRule = alternativeCaseNode.checkIR(Rule.class);
			alternative.addAlternativeCase(alternativeCaseRule);
		}
		return alternative;
	}
}
