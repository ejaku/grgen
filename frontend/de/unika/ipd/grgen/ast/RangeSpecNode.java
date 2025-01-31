/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a range specification (used by ConnAssertNode).
 * children: none
 */
public class RangeSpecNode extends BaseNode
{
	static {
		setName(RangeSpecNode.class, "range spec");
	}

	/** Constant, signaling if upper bound is bounded. */
	public static final long UNBOUND = Integer.MAX_VALUE;

	/** The upper and lower bound. */
	private long lower, upper;

	/**
	 * @param coords
	 */
	public RangeSpecNode(Coords coords, long lower, long upper)
	{
		super(coords);
		this.lower = lower;
		this.upper = upper;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		// no children
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		// no children
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean good = true;
		if(lower < 0) {
			reportError("The lower bound of the range must be a positive number.");
			good = false;
		}
		if(upper < 0) {
			reportError("The upper bound of the range must be a positive number.");
			good = false;
		}
		if(lower > upper) {
			reportError("The lower bound must be less (or equal) than the upper bound of the range.");
			good = false;
		}
		return good;
	}

	@Override
	public String getName()
	{
		return super.getName() + " [" + lower + ":" + upper + "]";
	}

	/** @return the lower bound of the range. */
	public long getLower()
	{
		return lower;
	}

	/** @return the upper bound of the range. */
	public long getUpper()
	{
		return upper;
	}
}
