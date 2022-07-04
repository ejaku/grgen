/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.expr.Count;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A node yielding the count of instances of an iterated pattern.
 */
public class CountNode extends ExprNode
{
	static {
		setName(CountNode.class, "count");
	}

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	public CountNode(Coords coords, IdentNode iterated)
	{
		super(coords);
		this.iteratedUnresolved = iterated;
		becomeParent(this.iteratedUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(iteratedUnresolved, iterated));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("iterated");
		return childrenNames;
	}

	private static final DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(IteratedDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean res = fixupDefinition(iteratedUnresolved, iteratedUnresolved.getScope());

		iterated = iteratedResolver.resolve(iteratedUnresolved, this);

		return res && iterated != null;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	@Override
	protected IR constructIR()
	{
		return new Count(iterated.checkIR(Rule.class), getType().getType());
	}

	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.intType;
	}

	@Override
	public boolean noIteratedReference(String containingConstruct)
	{
		reportError("The matches of an iterated cannot be accessed with a count(" + iteratedUnresolved + ") from a "
				+ containingConstruct + ", only from a yield block or yield expression or eval.");
		return false;
	}

	@Override
	public boolean iteratedNotReferenced(String iterName)
	{
		if(iterated.getIdentNode().toString().equals(iterName)) {
			reportError("The iterated cannot be accessed by this nested count(" + iteratedUnresolved + ").");
			return false;
		}
		return true;
	}
}
