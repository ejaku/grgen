/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.expr.ExpressionPair;
import de.unika.ipd.grgen.ir.type.container.MapType;

public class MapInit extends Expression
{
	private Collection<ExpressionPair> mapItems;
	private Entity member;
	private MapType mapType;
	private boolean isConst;

	public MapInit(Collection<ExpressionPair> mapItems, Entity member, MapType mapType, boolean isConst)
	{
		super("map init", member != null ? member.getType() : mapType);
		this.mapItems = mapItems;
		this.member = member;
		this.mapType = mapType;
		this.isConst = isConst;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		for(ExpressionPair mapItem : mapItems) {
			mapItem.collectNeededEntities(needs);
		}
	}

	public Collection<ExpressionPair> getMapItems()
	{
		return mapItems;
	}

	public void setMember(Entity entity)
	{
		assert(member == null && entity != null);
		member = entity;
	}

	public Entity getMember()
	{
		return member;
	}

	public MapType getMapType()
	{
		return mapType;
	}

	public void forceNotConstant()
	{
		isConst = false;
	}

	public boolean isConstant()
	{
		return isConst;
	}

	public String getAnonymousMapName()
	{
		return "anonymous_map_" + getId();
	}
}
