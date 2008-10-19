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
import de.unika.ipd.grgen.ir.MapRemoveItem;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MapRemoveItemNode extends ExprNode
{
	static {
		setName(MapRemoveItemNode.class, "map remove item");
	}

	MemberAccessExprNode target;
	ExprNode keyExpr;

	public MapRemoveItemNode(Coords coords, MemberAccessExprNode target, ExprNode keyExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		children.add(keyExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	protected boolean resolveLocal() {
		return true;
	}

	protected boolean checkLocal() {
		boolean success = true;
		MemberDeclNode targetDecl = target.getDecl();

		if (targetDecl.isConst()) {
			error.error(getCoords(), "removing items of a const map is not allowed");
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

		return success;
	}

	protected IR constructIR() {
		return new MapRemoveItem(target.checkIR(Qualification.class),
				keyExpr.checkIR(Expression.class));
	}
	
	public TypeNode getType() {
		return target.getDecl().getDeclType();
	}
	
	public MemberAccessExprNode getTarget() {
		return target;
	}
}
