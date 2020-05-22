/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.expr.map;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.DeclExprNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.ExprPairNode;
import de.unika.ipd.grgen.ast.typedecl.MapTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.ExpressionPair;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.typedecl.MapType;
import de.unika.ipd.grgen.parser.Coords;

//TODO: there's a lot of code which could be handled in a common way regarding the containers set|map|array|deque 
//should be unified in abstract base classes and algorithms working on them

public class MapInitNode extends ExprNode
{
	static {
		setName(MapInitNode.class, "map init");
	}

	private CollectNode<ExprPairNode> mapItems = new CollectNode<ExprPairNode>();

	// if map init node is used in model, for member init
	//     then lhs != null, mapType == null
	// if map init node is used in actions, for anonymous const map with specified types
	//     then lhs == null, mapType != null -- adjust type of map items to this type
	private BaseNode lhsUnresolved;
	private DeclNode lhs;
	private MapTypeNode mapType;

	public MapInitNode(Coords coords, IdentNode member, MapTypeNode mapType)
	{
		super(coords);

		if(member != null) {
			lhsUnresolved = becomeParent(member);
		} else {
			this.mapType = mapType;
		}
	}

	@Override
	public Collection<? extends BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(mapItems);
		return children;
	}

	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("mapItems");
		return childrenNames;
	}

	public void addMapItem(ExprPairNode item)
	{
		mapItems.addChild(item);
	}

	private static final MemberResolver<DeclNode> lhsResolver = new MemberResolver<DeclNode>();

	@Override
	protected boolean resolveLocal()
	{
		if(lhsUnresolved != null) {
			if(!lhsResolver.resolve(lhsUnresolved))
				return false;
			lhs = lhsResolver.getResult(DeclNode.class);
			return lhsResolver.finish();
		} else {
			if(mapType == null)
				mapType = createMapType();
			return mapType.resolve();
		}
	}

	@Override
	protected boolean checkLocal()
	{
		boolean success = true;

		MapTypeNode mapType;
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			assert type instanceof MapTypeNode : "Lhs should be a Map<Key,Value>";
			mapType = (MapTypeNode)type;
		} else {
			mapType = this.mapType;
		}

		for(ExprPairNode item : mapItems.getChildren()) {
			if(item.keyExpr.getType() != mapType.keyType) {
				if(this.mapType != null) {
					ExprNode oldKeyExpr = item.keyExpr;
					item.keyExpr = item.keyExpr.adjustType(mapType.keyType, getCoords());
					item.switchParenthood(oldKeyExpr, item.keyExpr);
					if(item.keyExpr == ConstNode.getInvalid()) {
						success = false;
						item.keyExpr.reportError("Key type \"" + oldKeyExpr.getType()
								+ "\" of initializer doesn't fit to key type \"" + mapType.keyType + "\" of map.");
					}
				} else {
					success = false;
					item.keyExpr.reportError("Key type \"" + item.keyExpr.getType()
							+ "\" of initializer doesn't fit to key type \""
							+ mapType.keyType + "\" of map (all items must be of exactly the same type).");
				}
			}
			if(item.valueExpr.getType() != mapType.valueType) {
				if(this.mapType != null) {
					ExprNode oldValueExpr = item.valueExpr;
					item.valueExpr = item.valueExpr.adjustType(mapType.valueType, getCoords());
					item.switchParenthood(oldValueExpr, item.valueExpr);
					if(item.valueExpr == ConstNode.getInvalid()) {
						success = false;
						item.valueExpr.reportError("Value type \"" + oldValueExpr.getType()
								+ "\" of initializer doesn't fit to value type \"" + mapType.valueType + "\" of map.");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \"" + mapType.valueType
							+ "\" of map (all items must be of exactly the same type).");
				}
			}
		}

		if(lhs == null && this.mapType == null) {
			this.mapType = mapType;
		}

		if(!isConstant() && lhs != null) {
			reportError("Only constant items allowed in map initialization in model");
			success = false;
		}

		return success;
	}

	private MapTypeNode createMapType()
	{
		TypeNode keyTypeNode = mapItems.getChildren().iterator().next().keyExpr.getType();
		TypeNode valueTypeNode = mapItems.getChildren().iterator().next().valueExpr.getType();
		IdentNode keyTypeIdent = ((DeclaredTypeNode)keyTypeNode).getIdentNode();
		IdentNode valueTypeIdent = ((DeclaredTypeNode)valueTypeNode).getIdentNode();
		return new MapTypeNode(keyTypeIdent, valueTypeIdent);
	}

	/**
	 * Checks whether the map only contains constants.
	 * @return True, if all map items are constant.
	 */
	protected final boolean isConstant()
	{
		for(ExprPairNode item : mapItems.getChildren()) {
			if(!(item.keyExpr instanceof ConstNode || isEnumValue(item.keyExpr)))
				return false;
			if(!(item.valueExpr instanceof ConstNode || isEnumValue(item.valueExpr)))
				return false;
		}
		return true;
	}

	private boolean isEnumValue(ExprNode expr)
	{
		if(!(expr instanceof DeclExprNode))
			return false;
		if(!(((DeclExprNode)expr).isEnumValue()))
			return false;
		return true;
	}

	@Override
	public TypeNode getType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (MapTypeNode)type;
		} else {
			return mapType;
		}
	}

	public CollectNode<ExprPairNode> getItems()
	{
		return mapItems;
	}

	@Override
	protected IR constructIR()
	{
		Vector<ExpressionPair> items = new Vector<ExpressionPair>();
		for(ExprPairNode item : mapItems.getChildren()) {
			items.add(item.getExpressionPair());
		}
		Entity member = lhs != null ? lhs.getEntity() : null;
		MapType type = mapType != null ? mapType.checkIR(MapType.class) : null;
		return new MapInit(items, member, type, isConstant());
	}

	public MapInit getMapInit()
	{
		return checkIR(MapInit.class);
	}

	public static String getUseStr()
	{
		return "map initialization";
	}
}
