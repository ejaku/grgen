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

public class MapInit extends IR {
	private Entity member; 
	private Collection<MapItem> mapItems;
	
	public MapInit(Entity member, Collection<MapItem> mapItems) {
		super("map init");
		this.member = member;
		this.mapItems = mapItems;
	}
	
	public Entity getMember() {
		return member;
	}
	
	public Collection<MapItem> getMapItems() {
		return mapItems;
	}
}
