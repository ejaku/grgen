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
public class NodeTypeChangeNode extends NodeDeclNode implements NodeCharacter {
	static {
		setName(NodeTypeChangeNode.class, "node type change decl");
	}

	private static final int OLD = CONSTRAINTS + 1;

	private static final Resolver nodeResolver = new DeclResolver(
			new Class[] { NodeDeclNode.class });

	private static final Checker nodeChecker = new TypeChecker(
			NodeTypeNode.class);

	public NodeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
		super(id, newType, TypeExprNode.getEmpty());
		addChild(oldid);
		setChildrenNames(new String[] { "ident", "type", "constraints", "old" });
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if (isResolved()) {
			return getResolve();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = resolveType() && successfullyResolved;
		successfullyResolved = resolveOld() && successfullyResolved;
		setResolved(successfullyResolved); // local result

		successfullyResolved = getChild(IDENT).doResolve()
				&& successfullyResolved;
		successfullyResolved = getChild(TYPE).doResolve()
				&& successfullyResolved;
		successfullyResolved = getChild(CONSTRAINTS).doResolve()
				&& successfullyResolved;
		successfullyResolved = getChild(OLD).doResolve()
				&& successfullyResolved;
		return successfullyResolved;
	}

	protected boolean resolveOld() {
		if (!nodeResolver.resolve(this, OLD)) {
			debug.report(NOTE, "resolve error");
			return false;
		}
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		if (!getResolve()) {
			return false;
		}
		if (isChecked()) {
			return getChecked();
		}

		boolean successfullyChecked = getCheck();
		if (successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(IDENT).doCheck() && successfullyChecked;
		successfullyChecked = getChild(TYPE).doCheck() && successfullyChecked;
		successfullyChecked = getChild(CONSTRAINTS).doCheck()
				&& successfullyChecked;
		successfullyChecked = getChild(OLD).doCheck() && successfullyChecked;

		return successfullyChecked;
	}

	/**
	 * @return the original node for this retyped node
	 */
	public NodeCharacter getOldNode() {
		return (NodeCharacter) getChild(OLD);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#check()
	 */
	protected boolean check() {
		boolean res = super.check() && checkChild(OLD, nodeChecker);
		if (!res) {
			return false;
		}
		// ok, since checked above
		DeclNode old = (DeclNode) getChild(OLD);

		// check if source node of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode)) {
			prev = curr;
			curr = curr.getParents().iterator().next();
		}
		if (prev == curr.getChild(RuleDeclNode.RIGHT)) {
			reportError("Source node of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same node declaration occurs 
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			if (p != this && p instanceof NodeTypeChangeNode) {
				reportError("Two (ambiguous) retyping statements for the same node declaration are forbidden, previous retype statement at "
						+ p.getCoords());
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
		NodeCharacter oldNodeDecl = (NodeCharacter) getChild(OLD);

		// This cast must be ok after checking.
		NodeTypeNode tn = (NodeTypeNode) getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		RetypedNode res = new RetypedNode(ident.getIdent(), nt, ident
				.getAttributes());

		Node node = oldNodeDecl.getNode();
		node.setRetypedNode(res);
		res.setOldNode(node);

		if (inheritsType()) {
			res.setTypeof((Node) getChild(TYPE).checkIR(Node.class));
		}

		return res;
	}
}
