/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

	/** A list of the pattern parameter */
	private final List<Entity> params = new LinkedList<Entity>();

	/** A list of the replace return-parameters */
	private final List<Entity> returns = new LinkedList<Entity>();


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

	/** Add a return-value (named node or edge) to the graph. */
	public void addReturn(Entity id) {
		returns.add(id);
	}

	/** Get all Returns of this graph. */
	public List<Entity> getReturns() {
		return Collections.unmodifiableList(returns);
	}
}
