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
import de.unika.ipd.grgen.ast.util.MemberInitResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
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
	
	BaseNode decl;

	/** whether an error has been reported for this enum item */
	private boolean typeAlreadyReported = false;
	
	/**
	 * Make a new declaration expression.
	 * @param coords The source code coordinates.
	 * @param declCharacter Some base node, that is a decl character.
	 */
	public DeclExprNode(BaseNode declCharacter) {
		super(declCharacter.getCoords());
		this.decl = declCharacter==null ? NULL : declCharacter;
		becomeParent(this.decl);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(decl);
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
		Resolver memberInitResolver = new MemberInitResolver(MemberDeclNode.class);
		BaseNode resolved = memberInitResolver.resolve(decl);
		successfullyResolved = resolved!=null && successfullyResolved;
		decl = ownedResolutionResult(decl, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = decl.resolve() && successfullyResolved;
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
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		
		successfullyChecked = decl.check() && successfullyChecked;
		return successfullyChecked;
	}
	
	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	public TypeNode getType() {
		DeclaredCharacter c = (DeclaredCharacter) decl;
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
		DeclaredCharacter c = (DeclaredCharacter) this.decl;
		DeclNode decl = c.getDecl();

		if(decl instanceof EnumItemNode) {
			res = ((EnumItemNode) decl).getValue();
		}

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return (new SimpleChecker(DeclaredCharacter.class)).check(decl, error);
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#isConstant() */
	public boolean isConst() {
		DeclaredCharacter c = (DeclaredCharacter) decl;
		return c.getDecl() instanceof EnumItemNode;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	protected IR constructIR() {
		BaseNode decl = this.decl;
		if(decl instanceof MemberDeclNode) {
			return new MemberExpression((Entity)decl.getIR());
		} else {
			return decl.getIR();
		}
	}
}

