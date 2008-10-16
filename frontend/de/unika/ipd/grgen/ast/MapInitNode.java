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

import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.MapInit;
import de.unika.ipd.grgen.ir.MapItem;
import de.unika.ipd.grgen.parser.Coords;

public class MapInitNode extends BaseNode
{
	static {
		setName(MapInitNode.class, "map init");
	}

	BaseNode lhsUnresolved;
	DeclNode lhs;
	CollectNode<MapItemNode> mapItems = new CollectNode<MapItemNode>();

	public MapInitNode(Coords coords, IdentNode member) {
		super(coords);
		lhsUnresolved = becomeParent(member);
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(lhsUnresolved, lhs));
		children.add(mapItems);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("lhs");
		childrenNames.add("mapItems");
		return childrenNames;
	}

	public void addMapItem(MapItemNode item) {
		mapItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	protected boolean resolveLocal() {
		if(!lhsResolver.resolve(lhsUnresolved)) return false;
		lhs = lhsResolver.getResult(DeclNode.class);

		return lhsResolver.finish();
	}

	protected boolean checkLocal() {
		TypeNode type = lhs.getDeclType();
		assert type instanceof MapTypeNode: "Lhs should be a Map[Key]";
		MapTypeNode mapType = (MapTypeNode) type;

		for(MapItemNode item : mapItems.getChildren()) {
			if (item.keyExpr.getType() != mapType.keyType) {
				item.keyExpr.reportError("Key type \"" + item.keyExpr.getType()
						+ "\" of initializer doesn't fit to key type \""
						+ mapType.keyType + "\" of map.");
			}
			if (item.valueExpr.getType() != mapType.valueType) {
				item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
						+ "\" of initializer doesn't fit to value type \""
						+ mapType.valueType + "\" of map.");
			}
		}

		return true;
	}

	protected IR constructIR() {
		Vector<MapItem> items = new Vector<MapItem>();
		for(MapItemNode item : mapItems.getChildren()) {
			items.add(item.getMapItem());
		}
		return new MapInit(lhs.getEntity(), items);
	}

	public MapInit getMapInit() {
		return checkIR(MapInit.class);
	}
}
