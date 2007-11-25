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
 * PatternGraph.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.ir.GraphEntity;
import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;
import java.util.LinkedList;
import java.util.List;

/**
 * A pattern graph is a graph as it occurs in left hand rule
 * sides and negative parts. Additionally it can have
 * conditions referring to its items that restrict the set
 * of possible matchings.
 */
public class PatternGraph extends Graph {
	
	/** A list of all condition expressions. */
	private final List<Expression> conds = new LinkedList<Expression>();
	
	/** A list of all potentially homomorphic sets. */
	private final List<Collection<GraphEntity>> homs = new LinkedList<Collection<GraphEntity>>();
	
	/**
	 * A list of all pattern nodes, which may be homomorphically matched
	 * to any other pattern nodes.
	 **/
	private final HashSet<Node> homToAllNodes = new HashSet<Node>();

    /**
	 * A list of all pattern edges, which may be homomorphically matched
	 * to any other pattern edges.
	 **/
	private final HashSet<Edge> homToAllEdges = new HashSet<Edge>();

	/**
	 * Add a condition to the graph.
	 * @param expr The condition's expression.
	 */
	public void addCondition(Expression expr) {
		conds.add(expr);
	}

	/**
	 * Add a potentially homomorphic set to the graph.
	 * @param expr The condition's expression.
	 */
	public void addHomomorphic(Collection<GraphEntity> hom) {
		homs.add(hom);
	}
	
	public void addHomToAll(Node node) {
		homToAllNodes.add(node);
	}

	public void addHomToAll(Edge edge) {
		homToAllEdges.add(edge);
	}
	
	/**
	 * Get all conditions in this graph.
	 * @return A collection containing all conditions in this graph.
	 */
	public Collection<Expression> getConditions() {
		return Collections.unmodifiableCollection(conds);
	}

	/**
	 * Get all potentially homomorphic sets.
	 * @return A collection containing all conditions in this graph.
	 */
	public Collection<Collection<GraphEntity>> getHomomorphic() {
		return Collections.unmodifiableCollection(homs);
	}
	
	public Collection<Node> getHomomorphic(Node n) {
		for(Collection<? extends GraphEntity> c : homs) {
			if (c.contains(n)) {
				return (Collection<Node>)c;
			}
		}

		Collection<Node> c = new LinkedList<Node>();
		c.add(n);
		return c;
	}

	public Collection<Edge> getHomomorphic(Edge e) {
		for(Collection<? extends GraphEntity> c : homs) {
			if (c.contains(e)) {
				return (Collection<Edge>)c;
			}
		}
		
		Collection<Edge> c = new LinkedList<Edge>();
		c.add(e);
		return c;
	}
	
	public Collection<Node> getElemsHomToAllNodes() {
		return homToAllNodes;
	}

	public Collection<Edge> getElemsHomToAllEdges() {
		return homToAllEdges;
	}
	
	public boolean isHomomorphic(Node n1, Node n2) {
		return getHomomorphic(n1).contains(n2);
	}

	public boolean isHomomorphic(Edge e1, Edge e2) {
		return getHomomorphic(e1).contains(e2);
	}

	public boolean isHomToAll(Node node) {
		return homToAllNodes.contains(node);
	}

	public boolean isHomToAll(Edge edge) {
		return homToAllEdges.contains(edge);
	}
	
	public boolean isIsoToAll(Node node) {
		for(Collection<? extends GraphEntity> c : homs) {
			if (c.contains(node)) {
				return false;
			}
		}
		return true;
	}
	
	public boolean isIsoToAll(Edge edge) {
		for(Collection<? extends GraphEntity> c : homs) {
			if (c.contains(edge)) {
				return false;
			}
		}
		return true;
	}
}

