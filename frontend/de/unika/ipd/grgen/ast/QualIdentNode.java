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
		setName(QualIdentNode.class, "Qual");
	}
	
	/** Index of the owner node. */
	protected static final int OWNER = 0;
	
	/** Index of the member node. */
	protected static final int MEMBER = 1;
	
	private static final String[] childrenNames = {
		"owner", "member"
	};
	
	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, BaseNode owner, BaseNode member) {
		super(coords);
		setChildrenNames(childrenNames);
		addChild(owner);
		addChild(member);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		/* This AST node implies another way of name resolution.
		 * First of all, the left hand side (lhs) has to be resolved. It must be
		 * a declaration and its type must be an instance of {@link ScopeOwner},
		 * since qualification can only be done, if the lhs owns a scope.
		 *
		 * Then the right side (rhs) is tought to search the declarations
		 * of its identifiers in the scope owned by the lhs. This is done
		 * via {@link ExprNode#fixupDeclaration(ScopeOwner)}.
		 *
		 * Then, the rhs contains the rhs' ident nodes contains the
		 * right declarations and can be resolved either. */
		boolean successfullyResolved = false;
		IdentNode member = (IdentNode) getChild(MEMBER);
		
		Resolver ownerResolver = new DeclResolver(DeclNode.class);
		ownerResolver.resolve(this, OWNER);
		BaseNode owner = getChild(OWNER);
		successfullyResolved = owner.resolutionResult();
		
		if (owner instanceof DeclNode && (owner instanceof NodeCharacter || owner instanceof EdgeCharacter)) {
			TypeNode ownerType = (TypeNode) ((DeclNode) owner).getDeclType();
			
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner) ownerType;
				o.fixupDefinition(member);
				Resolver declResolver = new DeclResolver(DeclNode.class);
				declResolver.resolve(this, MEMBER);
				successfullyResolved = getChild(MEMBER).resolutionResult();
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
		
		successfullyResolved = getChild(OWNER).resolve() && successfullyResolved;
		successfullyResolved = getChild(MEMBER).resolve() && successfullyResolved;
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
			
			childrenChecked = getChild(OWNER).check() && childrenChecked;
			childrenChecked = getChild(MEMBER).check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return (new SimpleChecker(DeclNode.class)).check(getChild(OWNER), error)
			&& (new SimpleChecker(MemberDeclNode.class)).check(getChild(MEMBER), error);
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl()
	 */
	public DeclNode getDecl() {
		assert isResolved();
		BaseNode child = getChild(MEMBER);

		if (child instanceof DeclNode) {
			return (DeclNode) child;
		}

		return DeclNode.getInvalid();
	}
	
	protected DeclNode getOwner() {
		assert isResolved();
		BaseNode child = getChild(OWNER);

		if (child instanceof DeclNode) {
			return (DeclNode) child;
		}

		return DeclNode.getInvalid();
	}
	
	protected IR constructIR() {
		Entity owner = (Entity) getChild(OWNER).checkIR(Entity.class);
		Entity member = (Entity) getChild(MEMBER).checkIR(Entity.class);
		
		return new Qualification(owner, member);
	}

	public void reportChildError(int childNum, Class<?> cls) {
		switch (childNum) {
		case 0:
			reportError("Node or edge expected before '.'");
			break;
		case 1:
			reportError("Not a member of " + getChild(0));
			break;
		default:
			reportError("Internal error: " + getChild(childNum).getName()
					+ "has no child with number " + childNum);
		}
	}
}
