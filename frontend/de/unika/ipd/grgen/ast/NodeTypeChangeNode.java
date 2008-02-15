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
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;
import de.unika.ipd.grgen.ir.RetypedNode;

/**
 *
 */
public class NodeTypeChangeNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(NodeTypeChangeNode.class, "node type change decl");
	}

	BaseNode oldUnresolved;
	NodeDeclNode old = null;

	public NodeTypeChangeNode(IdentNode id, BaseNode newType, int context, BaseNode oldid) {
		super(id, newType, context, TypeExprNode.getEmpty());
		this.oldUnresolved = oldid;
		becomeParent(this.oldUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(oldUnresolved, old));
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

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		DeclarationResolver<NodeDeclNode> nodeResolver = new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
		Pair<NodeDeclNode, TypeDeclNode> resolved = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = (resolved != null) && successfullyResolved;
		if (resolved != null) {
			typeNodeDecl = resolved.fst;
			typeTypeDecl = resolved.snd;
		}
		old = nodeResolver.resolve(oldUnresolved, this);
		successfullyResolved = old!=null && successfullyResolved;
		return successfullyResolved;
	}

	/**
	 * @return the original node for this retyped node
	 */
	public NodeDeclNode getOldNode() {
		assert isResolved();
		return old;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal()
			& nodeChecker.check(old, error);
		if (!res) {
			return false;
		}

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

		return res & onlyReplacementNodesAreAllowedToChangeType();
	}

	protected boolean onlyReplacementNodesAreAllowedToChangeType() {
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			return true;
		}

		constraints.reportError("pattern nodes are not allowed to change type, only replacement nodes are");
		return false;
	}

	public Node getNode() {
		return (Node) checkIR(Node.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		RetypedNode res = new RetypedNode(ident.getIdent(), nt, ident.getAnnotations());

		Node node = old.getNode();
		node.setRetypedNode(res);
		res.setOldNode(node);

		if (inheritsType()) {
			res.setTypeof((Node) typeNodeDecl.checkIR(Node.class));
		}

		return res;
	}
}
