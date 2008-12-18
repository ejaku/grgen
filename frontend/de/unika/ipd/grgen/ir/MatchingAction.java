/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

/**
 * An action that represents something that does graph matching.
 */
public abstract class MatchingAction extends Action {
	/** Children names of this node. */
	private static final String[] childrenNames = {
		"pattern"
	};

	/** The graph pattern to match against. */
	protected final PatternGraph pattern;

	/** A list of the pattern parameters */
	private final List<Entity> params = new LinkedList<Entity>();

	/** A list of the return-parameters */
	private final List<Expression> returns = new LinkedList<Expression>();


	/**
	 * @param name The name of this action.
	 * @param ident The identifier that identifies this object.
	 * @param pattern The graph pattern to match against.
	 */
	public MatchingAction(String name, Ident ident, PatternGraph pattern) {
		super(name, ident);
		this.pattern = pattern;
		pattern.setNameSuffix("pattern");
		setChildrenNames(childrenNames);
	}

	/** @return The graph pattern. */
	public PatternGraph getPattern() {
		return pattern;
	}

	/** Add a parameter to the graph. */
	public void addParameter(Entity id) {
		params.add(id);
	}

	/** Get all Parameters of this graph. */
	public List<Entity> getParameters() {
		return Collections.unmodifiableList(params);
	}

	/** Add a return-value to the graph. */
	public void addReturn(Expression expr) {
		returns.add(expr);
	}

	/** Get all Returns of this graph. */
	public List<Expression> getReturns() {
		return Collections.unmodifiableList(returns);
	}
}
