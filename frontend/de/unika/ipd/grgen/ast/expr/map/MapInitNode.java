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
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.expr.ConstNode;
import de.unika.ipd.grgen.ast.expr.ContainerInitNode;
import de.unika.ipd.grgen.ast.expr.ExprNode;
import de.unika.ipd.grgen.ast.expr.ExprPairNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.util.MemberResolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.ExpressionPair;
import de.unika.ipd.grgen.ir.expr.map.MapInit;
import de.unika.ipd.grgen.ir.type.MapType;
import de.unika.ipd.grgen.parser.Coords;

public class MapInitNode extends ContainerInitNode
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

	public void addPairItem(ExprPairNode item)
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

		MapTypeNode mapType = getContainerType();
		for(ExprPairNode item : mapItems.getChildren()) {
			if(item.keyExpr.getType() != mapType.keyType) {
				if(!isInitInModel()) {
					ExprNode oldKeyExpr = item.keyExpr;
					item.keyExpr = item.keyExpr.adjustType(mapType.keyType, getCoords());
					item.switchParenthood(oldKeyExpr, item.keyExpr);
					if(item.keyExpr == ConstNode.getInvalid()) {
						success = false;
						item.keyExpr.reportError("Key type \"" + oldKeyExpr.getType()
								+ "\" of initializer doesn't fit to key type \"" + mapType.keyType
								+ "\" of the map (" + mapType + ").");
					}
				} else {
					success = false;
					item.keyExpr.reportError("Key type \"" + item.keyExpr.getType()
							+ "\" of initializer doesn't fit to key type \"" + mapType.keyType
							+ "\" of the map (" + mapType
							+ " -- all items must be of exactly the same type).");
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
								+ "\" of initializer doesn't fit to value type \"" + mapType.valueType
								+ "\" of the map (" + mapType + ").");
					}
				} else {
					success = false;
					item.valueExpr.reportError("Value type \"" + item.valueExpr.getType()
							+ "\" of initializer doesn't fit to value type \"" + mapType.valueType
							+ "\" of the map (" + mapType
							+ " -- all items must be of exactly the same type).");
				}
			}
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

	@Override
	public MapTypeNode getContainerType()
	{
		assert(isResolved());
		if(lhs != null) {
			TypeNode type = lhs.getDeclType();
			return (MapTypeNode)type;
		} else {
			return mapType;
		}
	}

	@Override
	public boolean isInitInModel()
	{
		return mapType == null;
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
