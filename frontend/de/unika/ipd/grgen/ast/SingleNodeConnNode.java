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

import java.util.Set;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.OptionalResolver;
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
	/** Index of the node in the children array. */
	private static final int NODE = 0;
	
	private static final String[] childrenNames = {
		"node"
	};
	
	private static final Checker nodeChecker =
		new TypeChecker(NodeTypeNode.class);
	
	private static final Resolver nodeResolver =
		new OptionalResolver(
				new DeclResolver(new Class[] { NodeDeclNode.class }));
	
	static {
		setName(SingleNodeConnNode.class, "single node");
	}
	
	/**
	 * @param n The node
	 */
	public SingleNodeConnNode(BaseNode n) {
		super(n.getCoords());
		addChild(n);
		setChildrenNames(childrenNames);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = nodeResolver.resolve(this, NODE) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = getChild(NODE).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(NODE).doCheck() && successfullyChecked;
	
		return successfullyChecked;
	}
	
	/**
	 * Get the node child of this node.
	 * @return The node child.
	 */
	public BaseNode getNode() {
		return getChild(NODE);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.GraphObjectNode#addToGraph(de.unika.ipd.grgen.ir.Graph)
	 */
	public void addToGraph(Graph gr) {
		NodeCharacter n = (NodeCharacter) getChild(NODE);
		gr.addSingleNode(n.getNode());
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		return checkChild(NODE, nodeChecker);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addEdge(java.util.Set)
	 */
	public void addEdge(Set<BaseNode> set) {
	}
	
	public EdgeCharacter getEdge() {
		return null;
	}
	
	public NodeCharacter getSrc() {
		return (NodeCharacter) getChild(NODE);
	}
	
	public void setSrc(NodeCharacter src) {
	}
	
	public NodeCharacter getTgt() {
		return null;
	}

	public void setTgt(NodeCharacter tgt) {
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#addNodes(java.util.Set)
	 */
	public void addNodes(Set<BaseNode> set) {
		set.add(getChild(NODE));
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ConnectionCharacter#isNegated()
	 */
	public boolean isNegated() {
		return false;
	}
}
