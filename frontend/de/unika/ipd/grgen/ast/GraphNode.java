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
import java.util.Set;

/**
 * A class which represents a graph pattern
 */
public class GraphNode extends BaseNode {
	
	/** Index of the connections collect node. */
	protected static final int CONNECTIONS = 0;
	protected static final int RETURN = CONNECTIONS+1;
	
	/** Connections checker. */
	private static final Checker connectionsChecker =
		new CollectChecker(new SimpleChecker(ConnectionCharacter.class));
	
	static {
		setName(GraphNode.class, "graph");
	}
	
	private static final String[] childrenNames = {
		"conn", "return"
	};
	
	/**
	 * A new pattern node
	 * @param connections A collection containing connection nodes
	 */
	public GraphNode(Coords coords, BaseNode connections, CollectNode returns) {
		super(coords);
		setChildrenNames(childrenNames);
		addChild(connections);
		addChild(returns);
	}
	
	/**
	 * A pattern node contains just a collect node with connection nodes
	 * as its children.
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		boolean connCheck = checkChild(CONNECTIONS, connectionsChecker);
		
		boolean edgeUsage = true;
		
		if(connCheck) {
			//check, that each named edge is only used once in a pattern
			CollectNode collect = (CollectNode) getChild(CONNECTIONS);
			HashSet<EdgeCharacter> edges = new HashSet<EdgeCharacter>();
			for (BaseNode n : collect.getChildren()) {
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
		return getChild(CONNECTIONS).getChildren();
	}
	
	/**
	 * Get a set of all nodes in this pattern.
	 * Use this function after this not has been checked with {@link #check()}
	 * to ensure, that the children have the right type.
	 * @return A set containing the declarations of all nodes occuring
	 * in this graph pattern.
	 */
	protected Set<BaseNode> getNodes() {
		Set<BaseNode> res = new HashSet<BaseNode>();
		
		for(BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addNodes(res);
		}
		
		return res;
	}
	
	protected Set<BaseNode> getEdges() {
		Set<BaseNode> res = new HashSet<BaseNode>();
		
		for(BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addEdge(res);
		}

		return res;
	}
	
	protected BaseNode getReturn() {
		return getChild(RETURN);
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
		
		for(BaseNode n : getChild(CONNECTIONS).getChildren()) {
			ConnectionCharacter conn = (ConnectionCharacter)n;
			conn.addToGraph(gr);
		}
		
		return gr;
	}
	
}
