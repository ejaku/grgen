/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.5
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;

/**
 * An action that represents something that does graph matching.
 */
public abstract class MatchingAction extends Action
{
	/** Children names of this node. */
	private static final String[] childrenNames = {
			"pattern"
	};

	/** The graph pattern to match against. */
	public PatternGraphLhs pattern;

	/** A list of the pattern parameters */
	private final List<Entity> params = new LinkedList<Entity>();

	/** A list of the pattern def parameters which get yielded */
	private final List<Entity> defParams = new LinkedList<Entity>();

	/** A list of the return-parameters */
	private final List<Expression> returns = new LinkedList<Expression>();

	/** A list of the filters */
	private final List<Filter> filters = new LinkedList<Filter>();

	/**
	 * @param name The name of this action.
	 * @param ident The identifier that identifies this object.
	 */
	protected MatchingAction(String name, Ident ident)
	{
		super(name, ident);
		setChildrenNames(childrenNames);
	}

	/**
	 * @param pattern The graph pattern to match against.
	 */
	protected void setPattern(PatternGraphLhs pattern)
	{
		assert(pattern != null);
		this.pattern = pattern;
		pattern.setNameSuffix("pattern");
	}

	/** @return The graph pattern. */
	public PatternGraphLhs getPattern()
	{
		return pattern;
	}

	/** Add a parameter to the graph. */
	public void addParameter(Entity id)
	{
		params.add(id);
	}

	/** Get all Parameters of this graph. */
	public List<Entity> getParameters()
	{
		return Collections.unmodifiableList(params);
	}

	/** Add a def parameter which gets yielded to the graph. */
	public void addDefParameter(Entity id)
	{
		defParams.add(id);
	}

	/** Get all def Parameters which get yielded of this graph. */
	public List<Entity> getDefParameters()
	{
		return Collections.unmodifiableList(defParams);
	}

	/** Add a return-value to the graph. */
	public void addReturn(Expression expr)
	{
		returns.add(expr);
	}

	/** Get all Returns of this graph. */
	public List<Expression> getReturns()
	{
		return Collections.unmodifiableList(returns);
	}

	/** Add a filter to the action. */
	public void addFilter(Filter filter)
	{
		filters.add(filter);
	}

	/** Get all filters of this action. */
	public List<Filter> getFilters()
	{
		return Collections.unmodifiableList(filters);
	}
}
