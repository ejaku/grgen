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

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;
import java.util.Collection;
import java.util.Vector;

/**
 * AST node that represents a qualified identifier
 * i.e. expressions like this one: a.b.c.d
 */
public class QualIdentNode extends BaseNode implements DeclaredCharacter {
	static {
		setName(QualIdentNode.class, "Qualification");
	}

	protected IdentNode ownerUnresolved;
	private DeclNode owner;

	protected IdentNode memberUnresolved;
	private MemberDeclNode member;

	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, IdentNode owner, IdentNode member) {
		super(coords);
		this.ownerUnresolved = owner;
		becomeParent(this.ownerUnresolved);
		this.memberUnresolved = member;
		becomeParent(this.memberUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member));
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
		DeclarationResolver<DeclNode> ownerResolver = new DeclarationResolver<DeclNode>(DeclNode.class);
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner!=null && successfullyResolved;

		if (owner != null && (owner instanceof NodeCharacter || owner instanceof EdgeCharacter)) {
			TypeNode ownerType = (TypeNode)owner.getDeclType();

			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner) ownerType;
				o.fixupDefinition(memberUnresolved);
				DeclarationResolver<MemberDeclNode> memberResolver = new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);
				member = memberResolver.resolve(memberUnresolved, this);
				successfullyResolved = member!=null && successfullyResolved;
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

		successfullyResolved = (owner!=null ? owner.resolve() : false) && successfullyResolved;
		successfullyResolved = (member!=null ? member.resolve() : false) && successfullyResolved;
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public MemberDeclNode getDecl() {
		assert isResolved();

		return member;
	}

	public DeclNode getOwner() {
		assert isResolved();

		return owner;
	}

	protected IR constructIR() {
		Entity ownerIR = (Entity) owner.checkIR(Entity.class);
		Entity memberIR = (Entity) member.checkIR(Entity.class);

		return new Qualification(ownerIR, memberIR);
	}
}
