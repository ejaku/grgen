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

public class MapRemoveItemNode extends EvalStatementNode
{
	static {
		setName(MapRemoveItemNode.class, "map remove item");
	}
	
	QualIdentNode target;
	ExprNode keyExpr;
	
	public MapRemoveItemNode(Coords coords, QualIdentNode target, ExprNode keyExpr)
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
		return true;		// MAP TODO
	}
}
