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
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.PatternGraph;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Set;
import java.util.Vector;

/**
 * AST node that represents a graph pattern
 * as it appears within the replace/modify part of some rule
 * or to be used as base class for PatternGraphNode
 * representing the graph pattern of the pattern part of some rule
 */
public class GraphNode extends BaseNode {
	static {
		setName(GraphNode.class, "graph");
	}

	CollectNode connections;
	CollectNode returns;
	Vector<EmitNode> emits = new Vector();

	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public GraphNode(Coords coords, CollectNode connections, CollectNode subpatterns, CollectNode returns) {
		super(coords);
		this.connections = connections;
		becomeParent(this.connections);
		this.returns = returns;
		becomeParent(this.returns);
	}

	/** adds a emit statement to the graph */
	public void addEmit(EmitNode emit) {
		emits.add(emit);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(connections);
		children.add(returns);
		children.addAll(emits);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("connections");
		childrenNames.add("return");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		nodeResolvedSetResult(successfullyResolved); // local result

		successfullyResolved = connections.resolve() && successfullyResolved;
		successfullyResolved = returns.resolve() && successfullyResolved;
		for(EmitNode emit : emits)
			successfullyResolved = emit.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * A pattern node contains just a collect node with connection nodes
	 * as its children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker connectionsChecker = new CollectChecker(new SimpleChecker(ConnectionCharacter.class));
		boolean connCheck = connectionsChecker.check(connections, error);

		boolean edgeUsage = true;

		if(connCheck) {
			//check, that each named edge is only used once in a pattern
			HashSet<EdgeCharacter> edges = new HashSet<EdgeCharacter>();
			for (BaseNode n : connections.getChildren()) {
				ConnectionCharacter cc = (ConnectionCharacter)n;
				EdgeCharacter ec = cc.getEdge();

				// add returns false iff edges already contains ec
				if (ec != null && !edges.add(ec)) {
					((EdgeDeclNode) ec).reportError("This (named) edge is used more than once in a graph of this action");
					edgeUsage = false;
				}
			}
		}

		return edgeUsage && connCheck;
	}

	/**
	 * Get an iterator iterating over all connections characters
	 * in this pattern.
	 * These are the children of the collect node at position 0.
	 * @return The iterator.
	 */
	protected Collection<BaseNode> getConnections() {
		return connections.getChildren();
	}

	/**
	 * Get a set of all nodes in this pattern.
	 * Use this function after this node has been checked with {@link #checkLocal()}
	 * to ensure, that the children have the right type.
	 * @return A set containing the declarations of all nodes occurring
	 * in this graph pattern.
	 */
	protected Set<BaseNode> getNodes() {
		Set<BaseNode> res = new LinkedHashSet<BaseNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addNodes(res);
		}

		return res;
	}

	protected Set<BaseNode> getEdges() {
		Set<BaseNode> res = new LinkedHashSet<BaseNode>();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addEdge(res);
		}

		return res;
	}

	/**
	 * Get the correctly casted IR object.
	 * @return The IR object.
	 */
	public Graph getGraph() {
		return (PatternGraph) checkIR(Graph.class);
	}

	/**
	 * Construct the IR object.
	 * It is a Graph and all the connections (children of the pattern AST node)
	 * are put into it.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		Graph gr = new PatternGraph();

		for(BaseNode n : connections.getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}

		for(EmitNode emit : emits)
			emit.constructIR();

		return gr;
	}
}

