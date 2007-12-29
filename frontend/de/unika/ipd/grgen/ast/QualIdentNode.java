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
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a qualified identifier
 * i.e. expressions like this one: a.b.c.d
 */
public class QualIdentNode extends BaseNode implements DeclaredCharacter
{
	static {
		setName(QualIdentNode.class, "Qualification");
	}
	
	BaseNode owner;
	BaseNode member;
		
	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, BaseNode owner, BaseNode member) {
		super(coords);
		this.owner = owner==null ? NULL : owner;
		becomeParent(this.owner);
		this.member = member==null ? NULL : member;
		becomeParent(this.member);
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(owner);
		children.add(member);
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("member");
		return childrenNames;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		boolean successfullyResolved = true;
		Resolver ownerResolver = new DeclResolver(DeclNode.class);
		BaseNode resolved = ownerResolver.resolve(owner);
		successfullyResolved = resolved!=null && successfullyResolved;
		owner = ownedResolutionResult(owner, resolved);
		
		if (owner instanceof DeclNode && (owner instanceof NodeCharacter || owner instanceof EdgeCharacter)) {
			TypeNode ownerType = (TypeNode) ((DeclNode) owner).getDeclType();
			
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner) ownerType;
				o.fixupDefinition((IdentNode)member);
				Resolver declResolver = new DeclResolver(DeclNode.class);
				resolved = declResolver.resolve(member);
				successfullyResolved = resolved!=null && successfullyResolved;
				member = ownedResolutionResult(member, resolved);
			} else {
				reportError("Left hand side of '.' does not own a scope");
				successfullyResolved = false;
			}
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge");
			successfullyResolved = false;
		}
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = owner.resolve() && successfullyResolved;
		successfullyResolved = member.resolve() && successfullyResolved;
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
			
			childrenChecked = owner.check() && childrenChecked;
			childrenChecked = member.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return (new SimpleChecker(DeclNode.class)).check(owner, error)
			&& (new SimpleChecker(MemberDeclNode.class)).check(member, error);
	}
	
	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public DeclNode getDecl() {
		assert isResolved();
		BaseNode child = member;

		if (child instanceof DeclNode) {
			return (DeclNode) child;
		}

		return DeclNode.getInvalid();
	}
	
	protected DeclNode getOwner() {
		assert isResolved();
		BaseNode child = owner;

		if (child instanceof DeclNode) {
			return (DeclNode) child;
		}

		return DeclNode.getInvalid();
	}
	
	protected IR constructIR() {
		Entity owner = (Entity) this.owner.checkIR(Entity.class);
		Entity member = (Entity) this.member.checkIR(Entity.class);
		
		return new Qualification(owner, member);
	}
}
