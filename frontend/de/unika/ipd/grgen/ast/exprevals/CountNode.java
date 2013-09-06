/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.exprevals;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Rule;
import de.unika.ipd.grgen.ir.exprevals.Count;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the count of instances of an iterated pattern.
 */
public class CountNode extends ExprNode {
	static {
		setName(CountNode.class, "count");
	}

	private IdentNode iteratedUnresolved;
	private IteratedNode iterated;

	public CountNode(Coords coords, IdentNode iterated) {
		super(coords);
		this.iteratedUnresolved = iterated;
		becomeParent(this.iteratedUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
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
		boolean res = fixupDefinition(iteratedUnresolved, iteratedUnresolved.getScope());

		iterated = iteratedResolver.resolve(iteratedUnresolved, this);

		return res && iterated != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		return new Count(iterated.checkIR(Rule.class), getType().getType());
	}

	@Override
	public TypeNode getType() {
		return BasicTypeNode.intType;
	}
	
	public boolean noDefElementInCondition() {
		// this check is called only for conditions, and there a count can't be used as the match object does not exist yet
		reportError("The matches of an iterated (here: "+iterated+") can't be counted from an if condition, only from a yield block or eval");
		return false;
	}
}
