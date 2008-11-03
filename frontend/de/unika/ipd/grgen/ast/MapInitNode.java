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
import de.unika.ipd.grgen.ir.MapType;
import de.unika.ipd.grgen.parser.Coords;

public class MapInitNode extends ExprNode
{
	static {
		setName(MapInitNode.class, "map init");
	}

	CollectNode<MapItemNode> mapItems = new CollectNode<MapItemNode>();

	// if map init node is used in model, for member init then lhs != null
	// if map init node is used in actions, for anonymous const map then mapType != null
	BaseNode lhsUnresolved;
	DeclNode lhs;
	MapTypeNode mapType;

	public MapInitNode(Coords coords, IdentNode member, MapTypeNode mapType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else { // mapType!=null
			this.mapType = mapType;
		}
	}

	public Collection<? extends BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(mapItems);
		return children;
	}

	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("mapItems");
		return childrenNames;
	}

	public void addMapItem(MapItemNode item) {
		mapItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	protected boolean resolveLocal() {
		if(lhsUnresolved!=null) {
			if(!lhsResolver.resolve(lhsUnresolved)) return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else {
			return mapType.resolve();
		}
	}

	protected boolean checkLocal() {
		boolean success = true;

		MapTypeNode mapType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof MapTypeNode: "Lhs should be a Map<Key,Value>";
			mapType = (MapTypeNode) type;
		} else {
			mapType = this.mapType;
		}

		for(MapItemNode item : mapItems.getChildren()) {
			if (item.keyExpr.getType() != mapType.keyType) {
				item.keyExpr.reportError("Key type \"" + item.keyExpr.getType()
						+ "\" of initializer doesn't fit to key type \""
						+ mapType.keyType + "\" of map.");
				success = false;
			}
			if (item.valueExpr.getType() != mapType.valueType) {
				item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
						+ "\" of initializer doesn't fit to value type \""
						+ mapType.valueType + "\" of map.");
				success = false;
			}
		}
		
		if(!isConstant()) {
			reportError("Only constant items allowed in map initialization");
			success = false;
		}

		return success;
	}

	/**
	 * Checks whether the map only contains constants.
	 * @return True, if all map items are constant.
	 */
	public boolean isConstant() {
		for(MapItemNode item : mapItems.getChildren()) {
			if(!(item.keyExpr instanceof ConstNode || isEnumValue(item.keyExpr)))
				return false;
			if(!(item.valueExpr instanceof ConstNode || isEnumValue(item.valueExpr)))
				return false;
		}
		return true;
	}
	
	public boolean isEnumValue(ExprNode expr) {
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).declUnresolved instanceof EnumExprNode))
			return false;
		return true;
	}

	public TypeNode getType() {
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			return (MapTypeNode) type;
		} else {
			return mapType;
		}
	}

	protected IR constructIR() {
		Vector<MapItem> items = new Vector<MapItem>();
		for(MapItemNode item : mapItems.getChildren()) {
			items.add(item.getMapItem());
		}
		return new MapInit(items, lhs!=null ? lhs.getEntity() : null, mapType!=null ? (MapType)mapType.getIR() : null);
	}

	public MapInit getMapInit() {
		return checkIR(MapInit.class);
	}
}
