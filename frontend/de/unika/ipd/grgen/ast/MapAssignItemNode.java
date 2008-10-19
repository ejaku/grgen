/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapAssignItem;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MapAssignItemNode extends ExprNode
{
	static {
		setName(MapAssignItemNode.class, "map assign item");
	}

	MemberAccessExprNode target;
	ExprNode keyExpr;
    ExprNode valueExpr;

	public MapAssignItemNode(Coords coords, MemberAccessExprNode target, ExprNode keyExpr,
	                         ExprNode valueExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
		this.valueExpr = becomeParent(valueExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		children.add(keyExpr);
		children.add(valueExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
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
			error.error(getCoords(), "assignment to items of a const map is not allowed");
			success = false;
		}

		TypeNode targetType = targetDecl.getDeclType();
		assert targetType instanceof MapTypeNode: target + " should have a map type";
		MapTypeNode targetMapType = (MapTypeNode) targetType;
		TypeNode keyType = targetMapType.keyType;
		TypeNode keyExprType = keyExpr.getType();

		if (!keyExprType.isEqual(keyType)) {
			keyExpr = becomeParent(keyExpr.adjustType(keyType, getCoords()));

			if (keyExpr == ConstNode.getInvalid()) {
				success = false;
			}
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
		assert targetDeclType instanceof MapTypeNode: targetDeclType + " should have a map type";
		MapTypeNode targetMapType = (MapTypeNode) targetDeclType;

		TypeNode targetType = targetMapType.valueType;
		TypeNode exprType = valueExpr.getType();

		if (exprType.isEqual(targetType))
			return true;

		valueExpr = becomeParent(valueExpr.adjustType(targetType, getCoords()));
		return valueExpr != ConstNode.getInvalid();
	}

	protected IR constructIR() {
		return new MapAssignItem(target.checkIR(Qualification.class),
				keyExpr.checkIR(Expression.class), valueExpr.checkIR(Expression.class));
	}
	
	public TypeNode getType() {
		return target.getDecl().getDeclType();
	}
	
	public MemberAccessExprNode getTarget() {
		return target;
	}
}
