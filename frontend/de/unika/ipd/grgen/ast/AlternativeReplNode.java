/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Alternative;
import de.unika.ipd.grgen.ir.AlternativeReplacement;
import de.unika.ipd.grgen.ir.IR;

public class AlternativeReplNode extends OrderedReplacementNode {
	static {
		setName(AlternativeReplNode.class, "alternative repl node");
	}

	private IdentNode alternativeUnresolved;
	private AlternativeNode alternative;
	

	public AlternativeReplNode(IdentNode n) {
		this.alternativeUnresolved = n;
		becomeParent(this.alternativeUnresolved);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(alternativeUnresolved, alternative));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("alternative");
		return childrenNames;
	}

	private static final DeclarationResolver<AlternativeNode> alternativeResolver =
		new DeclarationResolver<AlternativeNode>(AlternativeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		alternative = alternativeResolver.resolve(alternativeUnresolved, this);
		return alternative!=null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new AlternativeReplacement("alternative replacement", alternativeUnresolved.getIdent(),
				alternative.checkIR(Alternative.class));
	}
}
