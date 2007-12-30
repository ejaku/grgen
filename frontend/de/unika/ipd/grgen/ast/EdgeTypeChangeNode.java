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

	BaseNode old;
		
	public EdgeTypeChangeNode(IdentNode id, BaseNode newType, BaseNode oldid) {
		super(id, newType, TypeExprNode.getEmpty());
		this.old = oldid;
		becomeParent(this.old);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(type);
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
		Resolver edgeResolver = new DeclResolver(EdgeDeclNode.class);
		BaseNode resolved = typeResolver.resolve(type);
		successfullyResolved = resolved!=null && successfullyResolved;
		type = ownedResolutionResult(type, resolved);
		resolved = edgeResolver.resolve(old);
		successfullyResolved = resolved!=null && successfullyResolved;
		old = ownedResolutionResult(old, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if (!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = ident.resolve() && successfullyResolved;
		successfullyResolved = type.resolve() && successfullyResolved;
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
			childrenChecked = type.check() && childrenChecked;
			childrenChecked = constraints.check() && childrenChecked;
			childrenChecked = old.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}

	/**
	 * @return the original edge for this retyped edge
	 */
	public EdgeCharacter getOldEdge() {
		return (EdgeCharacter) old;
	}

	public IdentNode getOldEdgeIdent() {
		if (old instanceof IdentNode) {
			return (IdentNode) old;
		}
		if (old instanceof EdgeDeclNode) {
			return ((EdgeDeclNode) old).getIdentNode();
		}

		return IdentNode.getInvalid();
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		boolean res = super.checkLocal()
			&& edgeChecker.check(this.old, error);
		if (!res) {
			return false;
		}

		// ok, since checked above
		DeclNode old = (DeclNode) this.old;
		
		// check if source edge of retype is declared in replace/modify part
		BaseNode curr = old;
		BaseNode prev = null;

		while (!(curr instanceof RuleDeclNode)) {
			prev = curr;
			curr = curr.getParents().iterator().next();
			// TODO: bei mehreren Eltern wird ein Elter willkürlich gewählt - egal?
		}
		if (prev == ((RuleDeclNode)curr).right) {
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
		EdgeCharacter oldEdgeDecl = (EdgeCharacter) old;

		// This cast must be ok after checking.
		EdgeTypeNode etn = (EdgeTypeNode) getDeclType();
		EdgeType et = etn.getEdgeType();
		IdentNode ident = getIdentNode();

		RetypedEdge res = new RetypedEdge(ident.getIdent(), et, ident.getAttributes());

		Edge oldEdge = oldEdgeDecl.getEdge();
		oldEdge.setRetypedEdge(res);
		res.setOldEdge(oldEdge);

		if (inheritsType()) {
			res.setTypeof((Edge) type.checkIR(Edge.class));
		}

		return res;
	}
}
