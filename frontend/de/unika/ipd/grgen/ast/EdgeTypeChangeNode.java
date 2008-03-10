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
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.RetypedEdge;

/**
 *
 */
public class EdgeTypeChangeNode extends EdgeDeclNode implements EdgeCharacter {
	static {
		setName(EdgeTypeChangeNode.class, "edge type change decl");
	}

	BaseNode oldUnresolved;
	EdgeDeclNode old = null;

	public EdgeTypeChangeNode(IdentNode id, BaseNode newType, int context, BaseNode oldid) {
		super(id, newType, context, TypeExprNode.getEmpty());
		this.oldUnresolved = oldid;
		becomeParent(this.oldUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
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

	private static final DeclarationResolver<EdgeDeclNode> edgeResolver = new DeclarationResolver<EdgeDeclNode>(EdgeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		Pair<EdgeDeclNode, TypeDeclNode> resolved = typeResolver.resolve(typeUnresolved, this);
		successfullyResolved = (resolved != null) && successfullyResolved;
		if (resolved != null) {
			typeEdgeDecl = resolved.fst;
			typeTypeDecl = resolved.snd;
		}
		old = edgeResolver.resolve(oldUnresolved, this);
		successfullyResolved = old != null && successfullyResolved;

		return successfullyResolved;
	}

	/** @return the original edge for this retyped edge */
	public EdgeDeclNode getOldEdge() {
		assert isResolved();
		return old;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		boolean res = super.checkLocal()
			& edgeChecker.check(old, error);
		if (!res) {
			return false;
		}

		// check if source edge of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode)) {
			prev = curr;
			// doesn't matter which parent you choose, in the end you reach RuleDeclNode
			curr = curr.getParents().iterator().next();
		}
		if (prev == ((RuleDeclNode)curr).getRight()) {
			reportError("Source edge of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same edge
		// declaration occurs
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			// to be erroneous there must be another EdgeTypeChangeNode with the same OLD-child
			// TODO: p.old == old always true, since p is a parent (of type EdgeTypeChangeNode) of old?
			if (p != this && p instanceof EdgeTypeChangeNode && (((EdgeTypeChangeNode)p).old == old)) {
				reportError("Two (and hence ambiguous) retype statements for the same edge are forbidden,"
								+ "previous retype statement at " + p.getCoords());
				res = false;
			}
		}

		return res & onlyReplacementEdgesAreAllowedToChangeType();
	}

	protected boolean onlyReplacementEdgesAreAllowedToChangeType() {
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			return true;
		}

		constraints.reportError("pattern edges are not allowed to change type, only replacement edges are");
		return false;
	}

	public Edge getEdge() {
		return (Edge) checkIR(Edge.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		EdgeTypeNode etn = (EdgeTypeNode) getDeclType();
		EdgeType et = etn.getEdgeType();
		IdentNode ident = getIdentNode();

		RetypedEdge res = new RetypedEdge(ident.getIdent(), et, ident.getAnnotations());

		Edge oldEdge = old.getEdge();
		oldEdge.setRetypedEdge(res);
		res.setOldEdge(oldEdge);

		if (inheritsType()) {
			res.setTypeof((Edge) typeEdgeDecl.checkIR(Edge.class));
		}

		return res;
	}
}
