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
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Set;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.Graph;

/**
 * AST node representing nodes 
 * that occur without any edge connection to the rest of the graph.
 * children: NODE:NodeDeclNode|IdentNode
 */
public class SingleNodeConnNode extends BaseNode implements ConnectionCharacter
{
	static {
		setName(SingleNodeConnNode.class, "single node");
	}

	BaseNode node;
			
	public SingleNodeConnNode(BaseNode n) {
		super(n.getCoords());
		this.node = n;
		becomeParent(this.node);
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(node);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("node");
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
		BaseNode resolved = nodeResolver.resolve(node);
		node = ownedResolutionResult(node, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = node.resolve() && successfullyResolved;
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
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		
		successfullyChecked = node.check() && successfullyChecked;
		return successfullyChecked;
	}
	
	/** Get the node child of this node.
	 * @return The node child. */
	public BaseNode getNode() {
		return node;
	}
	
	/** @see de.unika.ipd.grgen.ast.GraphObjectNode#addToGraph(de.unika.ipd.grgen.ir.Graph) */
	public void addToGraph(Graph gr) {
		NodeCharacter n = (NodeCharacter) node;
		gr.addSingleNode(n.getNode());
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		return nodeChecker.check(node, error);
	}
	
	/** @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdge(java.util.Set) */
	public void addEdge(Set<BaseNode> set) {
	}
	
	public EdgeCharacter getEdge() {
		return null;
	}
	
	public NodeCharacter getSrc() {
		return (NodeCharacter) node;
	}
	
	public void setSrc(NodeCharacter src) {
	}
	
	public NodeCharacter getTgt() {
		return null;
	}

	public void setTgt(NodeCharacter tgt) {
	}
	
	/** @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set) */
	public void addNodes(Set<BaseNode> set) {
		set.add(node);
	}
	
	/** @see de.unika.ipd.grgen.ast.ConnectionCharacter#isNegated() */
	public boolean isNegated() {
		return false;
	}
}
