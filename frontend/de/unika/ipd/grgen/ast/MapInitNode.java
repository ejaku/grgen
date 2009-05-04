/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
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
import de.unika.ipd.grgen.ir.Entity;
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

	// if map init node is used in model, for member init 
	//     then lhs != null, mapType == null
	// if map init node is used in actions, for anonymous const map with specified types
	//     then lhs == null, mapType != null -- adjust type of map items to this type
	// if map init node is used in actions, for anonymous const map without specified types
	//     then lhs == null, mapType == null -- determine map type from first item, all items must be exactly of this type
	BaseNode lhsUnresolved;
	DeclNode lhs;
	MapTypeNode mapType;

	public MapInitNode(Coords coords, IdentNode member, MapTypeNode mapType) {
		super(coords);

		if(member!=null) {
			lhsUnresolved = becomeParent(member);
		} else {
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
		} else if(mapType!=null) {
			return mapType.resolve();
		} else {
			return true;
		}
	}

	protected boolean checkLocal() {
		boolean success = true;

		MapTypeNode mapType;
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof MapTypeNode: "Lhs should be a Map<Key,Value>";
			mapType = (MapTypeNode) type;
		} else if(this.mapType!=null) {
			mapType = this.mapType;
		} else {
			TypeNode mapTypeNode = getMapType();
			if(mapTypeNode instanceof MapTypeNode) {
				mapType = (MapTypeNode)mapTypeNode;
			} else {
				return false;
			}
		}

		for(MapItemNode item : mapItems.getChildren()) {
			if (item.keyExpr.getType() != mapType.keyType) {
				if(this.mapType!=null) {
					ExprNode oldKeyExpr = item.keyExpr;
					item.keyExpr = item.keyExpr.adjustType(mapType.keyType, getCoords());
					item.switchParenthood(oldKeyExpr, item.keyExpr);
					if(item.keyExpr == ConstNode.getInvalid()) {
						success = false;
						item.keyExpr.reportError("Key type \"" + oldKeyExpr.getType()
								+ "\" of initializer doesn't fit to key type \""
								+ mapType.keyType + "\" of map.");
					}
				} else {
					success = false;
					item.keyExpr.reportError("Key type \"" + item.keyExpr.getType()
							+ "\" of initializer doesn't fit to key type \""
							+ mapType.keyType + "\" of map (all items must be of exactly the same type).");
				}
			}
			if (item.valueExpr.getType() != mapType.valueType) {
				if(this.mapType!=null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(mapType.valueType, getCoords());
					item.switchParenthood(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \""
								+ mapType.valueType + "\" of map.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \""
							+ mapType.valueType + "\" of map (all items must be of exactly the same type).");
				}
			}
		}
		
		if(lhs==null && this.mapType==null) {
			this.mapType = mapType;
		}
		
		if(!isConstant() && lhs!=null) {
			reportError("Only constant items allowed in map initialization in model");
			success = false;
		}

		return success;
	}

	TypeNode getMapType() {
		TypeNode keyTypeNode = mapItems.getChildren().iterator().next().keyExpr.getType();
		TypeNode valueTypeNode = mapItems.getChildren().iterator().next().valueExpr.getType();
		if(!(keyTypeNode instanceof DeclaredTypeNode)
				|| !(valueTypeNode instanceof DeclaredTypeNode)) {
			reportError("Map items have to be of basic or enum type");
			return BasicTypeNode.errorType;
		}
		IdentNode keyTypeIdent = ((DeclaredTypeNode)keyTypeNode).getIdentNode();
		IdentNode valueTypeIdent = ((DeclaredTypeNode)valueTypeNode).getIdentNode();
		return MapTypeNode.getMapType(keyTypeIdent, valueTypeIdent);
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
		assert(isResolved());
		if(lhs!=null) {
			TypeNode type = lhs.getDeclType();
			return (MapTypeNode) type;
		} else if(mapType!=null) {
			return mapType;
		} else {
			return getMapType();
		}
	}

	public CollectNode<MapItemNode> getItems() {
		return mapItems; 
	}
	
	protected IR constructIR() {
		Vector<MapItem> items = new Vector<MapItem>();
		for(MapItemNode item : mapItems.getChildren()) {
			items.add(item.getMapItem());
		}
		Entity member = lhs!=null ? lhs.getEntity() : null;
		MapType type = mapType!=null ? (MapType)mapType.getIR() : null;
		return new MapInit(items, member, type, isConstant());
	}

	public MapInit getMapInit() {
		return checkIR(MapInit.class);
	}
}
