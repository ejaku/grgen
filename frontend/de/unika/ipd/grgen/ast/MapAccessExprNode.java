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

import de.unika.ipd.grgen.parser.Coords;

public class MapAccessExprNode extends ExprNode
{
	static {
		setName(MapAccessExprNode.class, "map access expression");
	}

	QualIdentNode targetUnresolved;
	ExprNode keyExpr;

	DeclNode target;

	public MapAccessExprNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
	{
		super(coords);
		this.targetUnresolved = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(targetUnresolved, target));
		children.add(keyExpr);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

/*	private static MemberResolver<DeclNode> memberResolver = new MemberResolver<DeclNode>();

	protected boolean resolveLocal() {
		if(!memberResolver.resolve(target)) return false;

		target = memberResolver.getResult(MemberDeclNode.class);

		return memberResolver.finish();
	}*/

	protected boolean checkLocal() {
		boolean success = true;
		TypeNode targetType = target.getDeclType();
		assert targetType instanceof MapTypeNode: target + " should have a map type";
		MapTypeNode targetMapType = (MapTypeNode) targetType;

		if (keyExpr.getType() != targetMapType.keyType) {
			keyExpr.reportError("Type \"" + keyExpr.getType()
					+ "\" doesn't fit to key type \""
					+ targetMapType.keyType + "\".");
			success = false;
		}

		return success;
	}

	public TypeNode getType() {
		return target.getDecl().getDeclType();
	}
}
