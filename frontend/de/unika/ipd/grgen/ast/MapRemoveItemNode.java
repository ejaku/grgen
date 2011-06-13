/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 * @version $Id: MapRemoveItemNode.java 22995 2008-10-18 14:51:18Z buchwald $
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ir.Expression;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapRemoveItem;
import de.unika.ipd.grgen.ir.Qualification;
import de.unika.ipd.grgen.parser.Coords;

public class MapRemoveItemNode extends EvalStatementNode
{
	static {
		setName(MapRemoveItemNode.class, "map remove item statement");
	}

	private QualIdentNode target;
	private ExprNode keyExpr;

	public MapRemoveItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
	{
		super(coords);
		this.target = becomeParent(target);
		this.keyExpr = becomeParent(keyExpr);
	}

	@Override
	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(target);
		children.add(keyExpr);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("target");
		childrenNames.add("keyExpr");
		return childrenNames;
	}

	@Override
	protected boolean resolveLocal() {
		return true;
	}

	@Override
	protected boolean checkLocal() {
		TypeNode targetType = target.getDecl().getDeclType();
		TypeNode targetKeyType = ((MapTypeNode)targetType).keyType;
		TypeNode keyType = keyExpr.getType();
		if (!keyType.isEqual(targetKeyType))
		{
			keyExpr = becomeParent(keyExpr.adjustType(targetKeyType, getCoords()));
			if(keyExpr == ConstNode.getInvalid()) {
				keyExpr.reportError("Argument (key) to "
						+ "map remove item statement must be of type " + targetKeyType.toString());
				return false;
			}
		}
		return true;
	}

	@Override
	protected IR constructIR() {
		return new MapRemoveItem(target.checkIR(Qualification.class),
				keyExpr.checkIR(Expression.class));
	}
}
