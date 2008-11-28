/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.0
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Moritz Kroll, Edgar Jakumeit
 * @version $Id$
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

public class MapInit extends Expression {
	private Collection<MapItem> mapItems;
	private Entity member;
	private MapType mapType;
	private boolean isConst;
	private int anonymousMapId;
	static private int anonymousMapCounter;
	
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
	}
	
	public Collection<MapItem> getMapItems() {
		return mapItems;
	}
	
	public Entity getMember() {
		return member;
	}
	
	public MapType getMapType() {
		return mapType;
	}
	
	public boolean isConstant() {
		return isConst;
	}
	
	public String getAnonymnousMapName() {
		return "anonymous_map_" + anonymousMapId;
	}
}
