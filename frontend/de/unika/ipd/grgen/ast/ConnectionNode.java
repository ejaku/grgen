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


import java.util.Collection;
import java.util.Set;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Graph;

/**
 * AST node that represents a Connection (an edge connecting two nodes)
 * children: LEFT:NodeDeclNode, EDGE:EdgeDeclNode, RIGHT:NodeDeclNode
 */
public class ConnectionNode extends BaseNode implements ConnectionCharacter {
	static {
		setName(ConnectionNode.class, "connection");
	}

	NodeDeclNode left;
	EdgeDeclNode edge;
	NodeDeclNode right;

	BaseNode leftUnresolved;
	BaseNode edgeUnresolved;
	BaseNode rightUnresolved;

	/** Construct a new connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param n1 First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param n2 Second node. */
	public ConnectionNode(BaseNode n1, BaseNode e, BaseNode n2) {
		super(e.getCoords());
		leftUnresolved = n1;
		becomeParent(leftUnresolved);
		edgeUnresolved = e;
		becomeParent(edgeUnresolved);
		rightUnresolved = n2;
		becomeParent(rightUnresolved);
	}

	/** Construct a new already resolved and checked connection node.
	 *  A connection node has two node nodes and one edge node
	 *  @param n1 First node
	 *  @param edge Edge that connects n1 with n2
	 *  @param n2 Second node. */
	public ConnectionNode(NodeDeclNode n1, EdgeDeclNode e, NodeDeclNode n2, boolean resolvedAndChecked) {
		this(n1, e, n2);
		assert(resolvedAndChecked);
		resolve();
		check();
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(leftUnresolved, left));
		children.add(getValidVersion(edgeUnresolved, edge));
		children.add(getValidVersion(rightUnresolved, right));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("src");
		childrenNames.add("edge");
		childrenNames.add("tgt");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		DeclarationResolver<NodeDeclNode> nodeResolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
		DeclarationResolver<EdgeDeclNode> edgeResolver = new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);
		left = nodeResolver.resolve(leftUnresolved, this);
		edge = edgeResolver.resolve(edgeUnresolved, this);
		right = nodeResolver.resolve(rightUnresolved, this);
		boolean successfullyResolved = left!=null && edge!=null && right!=null;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = left!=null && left.resolve() && successfullyResolved;
		successfullyResolved = edge!=null && edge.resolve() && successfullyResolved;
		successfullyResolved = right!=null && right.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * Check, if the AST node is correctly built.
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		return nodeChecker.check(left, error)
			& edgeChecker.check(edge, error)
			& nodeChecker.check(right, error)
			& areDanglingEdgesInReplacementDeclaredInPattern();
	}

	protected boolean areDanglingEdgesInReplacementDeclaredInPattern() {
		if(!(left instanceof DummyNodeDeclNode)
			&& !(right instanceof DummyNodeDeclNode))
		{
			return true; // edge not dangling
		}

		// edge dangling
		if(left instanceof DummyNodeDeclNode) {
			if(left.declLocation==NodeDeclNode.DECL_IN_PATTERN) {
				return true; // we're within the pattern, not the replacement
			}
		}
		if(right instanceof DummyNodeDeclNode) {
			if(right.declLocation==NodeDeclNode.DECL_IN_PATTERN) {
				return true; // we're within the pattern, not the replacement
			}
		}

		// edge dangling and located within the replacement
		if(edge.declLocation==EdgeDeclNode.DECL_IN_PATTERN) {
			return true; // edge was declared in the pattern
		}
		if(edge instanceof EdgeTypeChangeNode) {
			return true; // edge is a type change edge of an edge declared within the pattern
		}

		edge.reportError("dangling edges in replace/modify part must have been declared in pattern part");
		return false;
	 }

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternGraphNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr) {
		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdges(java.util.Set)
	 */
	public void addEdge(Set<BaseNode> set) {
		set.add(edge);
	}

	public EdgeDeclNode getEdge() {
		return edge;
	}

	public NodeDeclNode getSrc() {
		return left;
	}

	public void setSrc(NodeDeclNode n) {
		assert(n!=null);
		switchParenthood(left, n);
		left = n;
	}

	public NodeDeclNode getTgt() {
		return right;
	}

	public void setTgt(NodeDeclNode n) {
		assert(n != null);
		switchParenthood(right, n);
		right = n;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
	 */
	public void addNodes(Set<BaseNode> set) {
		set.add(left);
		set.add(right);
	}
}

