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
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.IteratedReplacement;
import de.unika.ipd.grgen.ir.Rule;

public class IteratedReplNode extends OrderedReplacementNode {
	static {
		setName(IteratedReplNode.class, "iterated repl node");
	}

	private IdentNode iteratedUnresolved;
	private IteratedNode iterated;
	

	public IteratedReplNode(IdentNode n) {
		this.iteratedUnresolved = n;
		becomeParent(this.iteratedUnresolved);
	}

	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterated");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedNode> iteratedResolver =
		new DeclarationResolver<IteratedNode>(IteratedNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		iterated = iteratedResolver.resolve(iteratedUnresolved, this);
		return iterated!=null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new IteratedReplacement("iterated replacement", iteratedUnresolved.getIdent(),
				iterated.checkIR(Rule.class));
	}
}
