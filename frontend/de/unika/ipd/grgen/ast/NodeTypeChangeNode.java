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
 * @author Sebastian Hack, Adam Szalkowski
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.RetypedNode;

/**
 * 
 */
public class NodeTypeChangeNode extends NodeDeclNode implements NodeCharacter 
{
	static {
		setName(NodeTypeChangeNode.class, "node type change decl");
	}

	BaseNode old;

	public NodeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
		super(id, newType, TypeExprNode.getEmpty());
		this.old = oldid;
		becomeParent(this.old);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(typeUnresolved);
		children.add(constraints);
		children.add(old);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident"); 
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("old");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if (isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver nodeResolver = new DeclResolver(NodeDeclNode.class);
		BaseNode resolved = typeResolver.resolve(typeUnresolved);
		successfullyResolved = resolved!=null && successfullyResolved;
		typeUnresolved = ownedResolutionResult(typeUnresolved, resolved);
		resolved = nodeResolver.resolve(old);
		successfullyResolved = resolved!=null && successfullyResolved;
		old = ownedResolutionResult(old, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = typeUnresolved.resolve() && successfullyResolved;
		successfullyResolved = constraints.resolve() && successfullyResolved;
		successfullyResolved = old.resolve() && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if (!resolutionResult()) {
			return false;
		}
		if (isChecked()) {
			return getChecked();
		}

		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = ident.check() && childrenChecked;
			childrenChecked = typeUnresolved.check() && childrenChecked;
			childrenChecked = constraints.check() && childrenChecked;
			childrenChecked = old.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}

	/**
	 * @return the original node for this retyped node
	 */
	public NodeCharacter getOldNode() {
		return (NodeCharacter) old;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal()
			&& nodeChecker.check(this.old, error);
		if (!res) {
			return false;
		}
		// ok, since checked above
		DeclNode old = (DeclNode) this.old;

		// check if source node of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode)) {
			prev = curr;
			// doesn't matter which parent you choose, in the end you reach RuleDeclNode
			curr = curr.getParents().iterator().next();
		}
		if (prev == ((RuleDeclNode)curr).right) {
			reportError("Source node of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same node declaration occurs
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			// to be erroneous there must be another EdgeTypeChangeNode with the same OLD-child
			// TODO: p.old == old always true, since p is a parent (of type NodeTypeChangeNode) of old?
			if (p != this && p instanceof NodeTypeChangeNode && (((NodeTypeChangeNode)p).old == old)) {
				reportError("Two (and hence ambiguous) retype statements for the same node are forbidden,"
						+ " previous retype statement at " + p.getCoords());
				res = false;
			}
		}

		return res;
	}

	public Node getNode() {
		return (Node) checkIR(Node.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeCharacter oldNodeDecl = (NodeCharacter) old;

		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		RetypedNode res = new RetypedNode(ident.getIdent(), nt, ident.getAttributes());

		Node node = oldNodeDecl.getNode();
		node.setRetypedNode(res);
		res.setOldNode(node);

		if (inheritsType()) {
			res.setTypeof((Node) typeUnresolved.checkIR(Node.class));
		}

		return res;
	}
}
