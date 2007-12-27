/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2007  IPD Goos, Universit"at Karlsruhe, Germany

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
 * @author Rubino Geiss
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;


import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.MemberInitResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MemberInit;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing a member initialization.
 * children: LHS:IdentNode, RHS:ExprNode
 */
public class MemberInitNode extends BaseNode
{
	static {
		setName(MemberInitNode.class, "member init");
	}

	private static final int LHS = 0;
	private static final int RHS = 1;
	
	/**
	 * @param coords The source code coordinates of = operator.
	 * @param member The member to be initialized.
	 * @param expr The expression, that is assigned.
	 */
	public MemberInitNode(Coords coords, IdentNode member, ExprNode expr) {
		super(coords);
		addChild(member);
		addChild(expr);
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("rhs");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver lhsResolver = new MemberInitResolver(DeclNode.class);
		//Resolver rhsResolver = new OneOfResolver(new Resolver[] {new DeclResolver(DeclNode.class), new MemberInitResolver(DeclNode.class)});
		successfullyResolved = lhsResolver.resolve(this, LHS) && successfullyResolved;
		//successfullyResolved = rhsResolver.resolve(this, RHS) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(LHS).resolve() && successfullyResolved;
		successfullyResolved = getChild(RHS).resolve() && successfullyResolved;
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
		if(successfullyChecked) {
			successfullyChecked = typeCheckLocal();
		}
		nodeCheckedSetResult(successfullyChecked);
		
		successfullyChecked = getChild(LHS).check() && successfullyChecked;
		successfullyChecked = getChild(RHS).check() && successfullyChecked;
		return successfullyChecked;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		boolean lhsOk = (new SimpleChecker(DeclNode.class)).check(getChild(LHS), error);
		boolean rhsOk = (new SimpleChecker(ExprNode.class)).check(getChild(RHS), error);

		return lhsOk && rhsOk;
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	protected boolean typeCheckLocal() {
		ExprNode expr = (ExprNode) getChild(RHS);

		TypeNode targetType = (TypeNode) ((DeclNode) getChild(LHS)).getDeclType();
		TypeNode exprType = (TypeNode) expr.getType();

		if (! exprType.isEqual(targetType)) {
			expr = expr.adjustType(targetType);
			replaceChild(RHS, expr);

			if (expr == ConstNode.getInvalid()) {
				String msg;
				if (exprType.isCastableTo(targetType)) {
					msg = "Assignment of " + exprType + " to " + targetType + " without a cast";
				} else {
					msg = "Incompatible assignment from " + exprType + " to " + targetType;
				}
				error.error(getCoords(), msg);
				return false;
			}
		}
		return true;
	}

	/**
	 * Construct the intermediate representation from a member init.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return new MemberInit((Entity) getChild(LHS).getIR(), (Expression) getChild(RHS).getIR());
	}
}
