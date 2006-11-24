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

import de.unika.ipd.grgen.ir.Entity;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/**
 * An action that represents something that does graph matching.
 */
public abstract class MatchingAction extends Action {
	
	/** Children names of this node. */
	private static final String[] childrenNames = {
		"pattern", "negative"
	};
	
	/** The graph pattern to match against. */
	protected final PatternGraph pattern;
	
	/** The NAC part of the rule. */
	private final Collection<PatternGraph> negs = new LinkedList<PatternGraph>();
	
	/** A list of the pattern parameter */
	private final List<Entity> params = new LinkedList<Entity>();
		
	/** A list of the replace return-parameters */
	private final List<Ident> returns = new LinkedList<Ident>();

	
	
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
	
	/**
	 * Get the graph pattern.
	 * @return The graph pattern.
	 */
	public PatternGraph getPattern() {
		return pattern;
	}
	
	public void addNegGraph(PatternGraph neg) {
		if(!neg.getNodes().isEmpty()) {
			neg.setName("N" + negs.size());
			negs.add(neg);
		}
	}
	
	/**
	 * Get the NAC part.
	 * @return The NAC graph of the rule.
	 */
	public Collection<PatternGraph> getNegs() {
		return Collections.unmodifiableCollection(negs);
	}
	
	/**
	 * Add a parameter to the graph.
	 * @param expr The parameter.
	 */
	public void addParameter(Entity expr) {
		params.add(expr);
	}
	
	
	/**
	 * Get all Parameters of this graph.
	 */
	public Collection<Entity> getParameters() {
		return Collections.unmodifiableCollection(params);
	}
	
	
	/**
	 * Add a return-value (named node or edge) to the graph.
	 * @param expr The parameter.
	 */
	public void addReturn(Ident id) {
		returns.add(id);
	}
	
	/**
	 * Get all Returns of this graph.
	 */
	public Collection<Ident> getReturns() {
		return Collections.unmodifiableCollection(returns);
	}
	
	
	/**
	 * Anonymous edges that connect the same nodes on both sides of rule
	 * shall also become the same Edge node. This not the case when
	 * the Rule is constructed, since the equality of edges is established
	 * by the coincidence of their identifiers. Anonymous edges have no
	 * identifiers, so they have to be coalesced right now, when both
	 * sides of the rule are known and set up.
	 */
	public void coalesceAnonymousEdges() {
		for(Edge e : pattern.getEdges()) {
			if (e.isAnonymous())
				for(Graph g : getAdditionalGraphs())
					g.replaceSimilarEdges(pattern, e);
		}
	}
	
	/**
	 * Get all graphs that are involved in this rule besides
	 * the pattern part.
	 * For an ordinary matching actions, these are the negative ones.
	 * @return A collection holding all additional graphs in this
	 * matching action.
	 */
	public Collection<? extends Graph> getAdditionalGraphs() {
		return negs;
	}
}
