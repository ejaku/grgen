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
import de.unika.ipd.grgen.ast.util.MemberPairResolver;
import de.unika.ipd.grgen.ast.util.Pair;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberExpression;

/**
 * An expression that results from a declared identifier.
 */
public class DeclExprNode extends ExprNode
{
	static {
		setName(DeclExprNode.class, "decl expression");
	}

	BaseNode declUnresolved;
	MemberDeclNode declMember;
	QualIdentNode declQualIdent;

	/** whether an error has been reported for this enum item */
	private boolean typeAlreadyReported = false;

	/**
	 * Make a new declaration expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(BaseNode declCharacter) {
		super(declCharacter.getCoords());
		this.declUnresolved = declCharacter;
		becomeParent(this.declUnresolved);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(declUnresolved, declMember, declQualIdent));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("decl");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}

		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		MemberPairResolver<MemberDeclNode, QualIdentNode> memberInitResolver = new MemberPairResolver<MemberDeclNode, QualIdentNode>(
		        MemberDeclNode.class, QualIdentNode.class);
		Pair<MemberDeclNode, QualIdentNode> resolved = memberInitResolver.resolve(declUnresolved, this);
		successfullyResolved = resolved!=null && successfullyResolved;
		if (resolved != null) {
			declMember = resolved.fst;
			declQualIdent = resolved.snd;
		}
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}

		if (declMember != null) {
			successfullyResolved = declMember.resolve() && successfullyResolved;
		}
		if (declQualIdent != null) {
			successfullyResolved = declQualIdent.resolve() && successfullyResolved;
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	public TypeNode getType() {
		DeclaredCharacter c = (DeclaredCharacter) getValidVersion(declUnresolved, declMember, declQualIdent);
		BaseNode type = c.getDecl().getDeclType();
		if ( ! (type instanceof TypeNode) ) {

			if (!typeAlreadyReported)
				reportError("Undefined entity \"" + c + "\"");

			typeAlreadyReported = true;
			type = BasicTypeNode.errorType;
		}
		return (TypeNode) type;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#evaluate() */
	public ExprNode evaluate() {
		ExprNode res = this;
		DeclaredCharacter c = (DeclaredCharacter) getValidVersion(declUnresolved, declMember, declQualIdent);
		DeclNode decl = c.getDecl();

		if(decl instanceof EnumItemNode) {
			res = ((EnumItemNode) decl).getValue();
		}

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#isConstant() */
	public boolean isConst() {
		DeclaredCharacter c = (DeclaredCharacter) getValidVersion(declUnresolved, declMember, declQualIdent);
		return c.getDecl() instanceof EnumItemNode;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		BaseNode decl = getValidResolvedVersion(declMember, declQualIdent);
		if(decl instanceof MemberDeclNode) {
			return new MemberExpression((Entity)decl.getIR());
		} else {
			return decl.getIR();
		}
	}
}

