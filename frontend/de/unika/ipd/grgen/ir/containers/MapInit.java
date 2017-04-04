/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.containers;

import java.util.Collection;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.exprevals.*;

public class MapInit extends Expression {
	private Collection<MapItem> mapItems;
	private Entity member;
	private MapType mapType;
	private boolean isConst;
	private int anonymousMapId;
	private static int anonymousMapCounter;

	public MapInit(Collection<MapItem> mapItems, Entity member, MapType mapType, boolean isConst) {
		super("map init", member!=null ? member.getType() : mapType);
		this.mapItems = mapItems;
		this.member = member;
		this.mapType = mapType;
		this.isConst = isConst;
		if(member==null) {
			anonymousMapId = anonymousMapCounter;
			++anonymousMapCounter;
		}
	}

	public void collectNeededEntities(NeededEntities needs) {
		needs.add(this);
		for(MapItem mapItem : mapItems) {
			mapItem.collectNeededEntities(needs);
		}
	}

	public Collection<MapItem> getMapItems() {
		return mapItems;
	}

	public void setMember(Entity entity) {
		assert(member==null && entity!=null);
		member = entity;
	}

	public Entity getMember() {
		return member;
	}

	public MapType getMapType() {
		return mapType;
	}

	public void forceNotConstant() {
		isConst = false;
	}

	public boolean isConstant() {
		return isConst;
	}

	public String getAnonymousMapName() {
		return "anonymous_map_" + anonymousMapId;
	}
}
