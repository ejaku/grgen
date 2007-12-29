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
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.EdgeResolver;
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

	BaseNode left;
	BaseNode edge;
	BaseNode right;

	/**
	 * Construct a new connection node.
	 * A connection node has two node nodes and one edge node
	 * @param n1 First node
	 * @param edge Edge that connects n1 with n2
	 * @param n2 Second node.
	 */
	public ConnectionNode(BaseNode n1, BaseNode edge, BaseNode n2) {
		super(edge.getCoords());
		this.left = n1==null ? NULL : n1;
		becomeParent(this.left);
		this.edge = edge==null ? NULL : edge;
		becomeParent(this.edge);
		this.right = n2==null ? NULL : n2;
		becomeParent(this.right);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(left);
		children.add(edge);
		children.add(right);
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
		boolean successfullyResolved = true;
		Resolver nodeResolver = new DeclResolver(new Class[] { NodeDeclNode.class }); // optional
		Resolver edgeResolver = new EdgeResolver();
		BaseNode resolved = nodeResolver.resolve(left);
		left = ownedResolutionResult(left, resolved);
		resolved = edgeResolver.resolve(edge);
		successfullyResolved = resolved!=null && successfullyResolved;
		edge = ownedResolutionResult(edge, resolved);
		resolved = nodeResolver.resolve(right);
		right = ownedResolutionResult(right, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = left.resolve() && successfullyResolved;
		successfullyResolved = edge.resolve() && successfullyResolved;
		successfullyResolved = right.resolve() && successfullyResolved;
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
			
			childrenChecked = left.check() && childrenChecked;
			childrenChecked = edge.check() && childrenChecked;
			childrenChecked = right.check() && childrenChecked;
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
		return nodeChecker.check(left, error)
			&& edgeChecker.check(edge, error)
			&& nodeChecker.check(right, error);
	}

	/**
	 * This adds the connection to an IR graph.
	 * This method should only be used by {@link PatternGraphNode#constructIR()}.
	 * @param gr The IR graph.
	 */
	public void addToGraph(Graph gr) {
		// After the AST is checked, these casts must succeed.
		NodeCharacter left = (NodeCharacter) this.left;
		NodeCharacter right = (NodeCharacter) this.right;
		EdgeCharacter edge = (EdgeCharacter) this.edge;

		gr.addConnection(left.getNode(), edge.getEdge(), right.getNode());
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdges(java.util.Set)
	 */
	public void addEdge(Set<BaseNode> set) {
		set.add(edge);
	}

	public EdgeCharacter getEdge() {
		return (EdgeCharacter) edge;
	}

	public NodeCharacter getSrc() {
		return (NodeCharacter) left;
	}

	public void setSrc(NodeCharacter n) {
		assert(n!=null);
		BaseNode src = (BaseNode)n;
		assert(src!=null);
		switchParenthood(left, src);
		left = src;
	}

	public NodeCharacter getTgt() {
		return (NodeCharacter) right;
	}

	public void setTgt(NodeCharacter n) {
		assert(n!=null);
		BaseNode tgt = (BaseNode)n;
		assert(tgt!=null);
		switchParenthood(right, tgt);
		right = tgt;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
	 */
	public void addNodes(Set<BaseNode> set) {
		set.add(left);
		set.add(right);
	}
	
	// debug guards to protect again accessing wrong elements
	public void addChild(BaseNode n) {
		assert(false);
	}
	public void setChild(int pos, BaseNode n) {
		assert(false);
	}
	public BaseNode getChild(int i) {
		assert(false);
		return null;
	}
	public int children() {
		assert(false);
		return 0;
	}
	public BaseNode replaceChild(int i, BaseNode n) {
		assert(false);
		return null;
	}
}

