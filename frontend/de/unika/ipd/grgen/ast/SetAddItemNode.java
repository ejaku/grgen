/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapAssignItemNode.java 23003 2008-10-19 00:13:05Z eja $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.SetAddItem;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class SetAddItemNode extends ExprNode
{
	static {
		setName(SetAddItemNode.class, "set add item");
	}

	MemberAccessExprNode target;
    ExprNode valueExpr;

	public SetAddItemNode(Coords coords, MemberAccessExprNode target, ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.valueExpr = becomeParent(valueExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		children.add(valueExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("valueExpr");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		boolean success = true;
		MemberDeclNode targetDecl = target.getDecl();

		if (targetDecl.isConst()) {
			error.error(getCoords(), "assignment to items of a const set is not allowed");
			success = false;
		}

		success &= typeCheckLocal();

		return success;
	}

	/**
	 * Checks whether the expression has a type equal, compatible or castable
	 * to the type of the target. Inserts implicit cast if compatible.
	 * @return true, if the types are equal or compatible, false otherwise
	 */
	protected boolean typeCheckLocal() {
		TypeNode targetDeclType = target.getDecl().getDeclType();
		assert targetDeclType instanceof SetTypeNode: targetDeclType + " should have a set type";
		SetTypeNode targetSetType = (SetTypeNode) targetDeclType;

		TypeNode targetType = targetSetType.valueType;
		TypeNode exprType = valueExpr.getType();

		if (exprType.isEqual(targetType))
			return true;

		valueExpr = becomeParent(valueExpr.adjustType(targetType, getCoords()));
		return valueExpr != ConstNode.getInvalid();
	}

	protected IR constructIR() {
		return new SetAddItem(target.checkIR(Qualification.class),
				valueExpr.checkIR(Expression.class));
	}
	
	public TypeNode getType() {
		return target.getDecl().getDeclType();
	}
	
	public MemberAccessExprNode getTarget() {
		return target;
	}
}
