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


import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.EdgeResolver;
import de.unika.ipd.grgen.ast.util.OptionalResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Graph;

/**
 * AST node that represents a Connection (an edge connecting two nodes)
 * children: LEFT:NodeDeclNode, EDGE:EdgeDeclNode, RIGHT:NodeDeclNode
 */
public class ConnectionNode extends BaseNode implements ConnectionCharacter
{
	static {
		setName(ConnectionNode.class, "connection");
	}

	/** edge names for the children. */
	private static final String[] childrenNames = {
		"src", "edge", "tgt"
	};

	/** Index of the source node. */
	private static final int LEFT = 0;

	/** Index of the edge node. */
	private static final int EDGE = 1;

	/** Index of the target node. */
	private static final int RIGHT= 2;


	/**
	 * Construct a new connection node.
	 * A connection node has two node nodes and one edge node
	 * @param n1 First node
	 * @param edge Edge that connects n1 with n2
	 * @param n2 Second node.
	 */
	public ConnectionNode(BaseNode n1, BaseNode edge, BaseNode n2) {
		super(edge.getCoords());
		addChild(n1);
		addChild(edge);
		addChild(n2);
		setChildrenNames(childrenNames);
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver nodeResolver = new OptionalResolver(new DeclResolver(new Class[] { NodeDeclNode.class }));		
		Resolver edgeResolver = new EdgeResolver();
		successfullyResolved = nodeResolver.resolve(this, LEFT) && successfullyResolved;
		successfullyResolved = edgeResolver.resolve(this, EDGE) && successfullyResolved;
		successfullyResolved = nodeResolver.resolve(this, RIGHT) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = getChild(LEFT).resolve() && successfullyResolved;
		successfullyResolved = getChild(EDGE).resolve() && successfullyResolved;
		successfullyResolved = getChild(RIGHT).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = getChild(LEFT).check() && childrenChecked;
			childrenChecked = getChild(EDGE).check() && childrenChecked;
			childrenChecked = getChild(RIGHT).check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		return nodeChecker.check(getChild(LEFT), error)
			&& edgeChecker.check(getChild(EDGE), error)
			&& nodeChecker.check(getChild(RIGHT), error);
	}

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternGraphNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr) {
		NodeCharacter left, right;
		EdgeCharacter edge;

		// After the AST is checked, these casts must succeed.
		left = (NodeCharacter) getChild(LEFT);
		right = (NodeCharacter) getChild(RIGHT);
		edge = (EdgeCharacter) getChild(EDGE);

		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdges(java.util.Set)
	 */
	public void addEdge(Set<BaseNode> set) {
		set.add(getChild(EDGE));
	}

	public EdgeCharacter getEdge() {
		return (EdgeCharacter) getChild(EDGE);
	}

	public NodeCharacter getSrc() {
		return (NodeCharacter) getChild(LEFT);
	}

	public void setSrc(NodeCharacter n) {
		setChild(LEFT, (BaseNode)n);
	}

	public NodeCharacter getTgt() {
		return (NodeCharacter) getChild(RIGHT);
	}

	public void setTgt(NodeCharacter n) {
		setChild(RIGHT, (BaseNode)n);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
	 */
	public void addNodes(Set<BaseNode> set) {
		set.add(getChild(LEFT));
		set.add(getChild(RIGHT));
	}
}

