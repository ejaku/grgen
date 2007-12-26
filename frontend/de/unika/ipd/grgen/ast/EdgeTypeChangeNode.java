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
import de.unika.ipd.grgen.ir.Edge;
import de.unika.ipd.grgen.ir.EdgeType;
import de.unika.ipd.grgen.ir.RetypedEdge;

/**
 * 
 */
public class EdgeTypeChangeNode extends EdgeDeclNode implements EdgeCharacter
{
	static {
		setName(EdgeTypeChangeNode.class, "edge type change decl");
	}

	private static final int OLD = CONSTRAINTS + 1;
		
	public EdgeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
		super(id, newType, TypeExprNode.getEmpty());
		addChild(oldid);
		setChildrenNames(new String[] { "ident", "type", "constraints", "old" });
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if (isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver edgeResolver = new DeclResolver(new Class[] { EdgeDeclNode.class});
		successfullyResolved = typeResolver.resolve(this, TYPE) && successfullyResolved;
		successfullyResolved = edgeResolver.resolve(this, OLD) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if (!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(IDENT).resolve() && successfullyResolved;
		successfullyResolved = getChild(TYPE).resolve() && successfullyResolved;
		successfullyResolved = getChild(CONSTRAINTS).resolve() && successfullyResolved;
		successfullyResolved = getChild(OLD).resolve() && successfullyResolved;
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
			
			childrenChecked = getChild(IDENT).check() && childrenChecked;
			childrenChecked = getChild(TYPE).check() && childrenChecked;
			childrenChecked = getChild(CONSTRAINTS).check() && childrenChecked;
			childrenChecked = getChild(OLD).check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}

	/**
	 * @return the original edge for this retyped edge
	 */
	public EdgeCharacter getOldEdge() {
		return (EdgeCharacter) getChild(OLD);
	}

	public IdentNode getOldEdgeIdent() {
		if (getChild(OLD) instanceof IdentNode) {
			return (IdentNode) getChild(OLD);
		}
		if (getChild(OLD) instanceof EdgeDeclNode) {
			return ((EdgeDeclNode) getChild(OLD)).getIdentNode();
		}

		return IdentNode.getInvalid();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		boolean res = super.checkLocal()
			&& edgeChecker.check(getChild(OLD), error);
		if (!res) {
			return false;
		}

		// ok, since checked above
		DeclNode old = (DeclNode) getChild(OLD);
		
		// check if source edge of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode)) {
			prev = curr;
			curr = curr.getParents().iterator().next();
		}
		if (prev == curr.getChild(RuleDeclNode.RIGHT)) {
			reportError("Source edge of retype may not be declared in replace/modify part");
			res = false;
		}

		// check if two ambiguous retyping statements for the same edge
		// declaration occurs
		Collection<BaseNode> parents = old.getParents();
		for (BaseNode p : parents) {
			// to be erroneous there must be another EdgeTypeChangeNode with the
			// same OLD-child
			// TODO: p.getChild(OLD) == old always true, since p is a parent (of
			// type EdgeTypeChangeNode) of old?
			if (p != this && p instanceof EdgeTypeChangeNode
					&& (p.getChild(OLD) == old)) {
				reportError("Two (and hence ambiguous) retype statements for the same edge are forbidden, previous retype statement at "
						+ p.getCoords());
				res = false;
			}
		}

		return res;
	}

	public Edge getEdge() {
		return (Edge) checkIR(Edge.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		// This cast must be ok after checking.
		EdgeCharacter oldEdgeDecl = (EdgeCharacter) getChild(OLD);

		// This cast must be ok after checking.
		EdgeTypeNode etn = (EdgeTypeNode) getDeclType();
		EdgeType et = etn.getEdgeType();
		IdentNode ident = getIdentNode();

		RetypedEdge res = new RetypedEdge(ident.getIdent(), et, ident
				.getAttributes());

		Edge oldEdge = oldEdgeDecl.getEdge();
		oldEdge.setRetypedEdge(res);
		res.setOldEdge(oldEdge);

		if (inheritsType()) {
			res.setTypeof((Edge) getChild(TYPE).checkIR(Edge.class));
		}

		return res;
	}
}
