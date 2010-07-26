/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @version $Id: YieldedEntities.java 26740 2010-01-02 11:21:07Z eja $
 */
package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;

/**
 * The yielded entities of a language constructs.
 */
public class YieldedEntities extends IR {
	private Set<GraphEntity> entities = new LinkedHashSet<GraphEntity>();
	private Set<Entity> neededEntities;
	private IR origin;

	public YieldedEntities(IR origin) {
		super("yielded entities");
		this.origin = origin;
	}

	public void AddEntity(GraphEntity entity) {
		entities.add(entity);
	}
	
	public Collection<GraphEntity> getEntities() {
		return Collections.unmodifiableSet(entities);
	}

	public Set<Entity> getNeededEntities() {
		if(neededEntities == null) {
			NeededEntities needs = new NeededEntities(false, false, false, true, false, false);  // collect all entities
			for(GraphEntity entity : entities)
				needs.add(entity);
			neededEntities = needs.entities;
		}
		return neededEntities;
	}
	
	public IR getOrigin() {
		return origin;
	}
}
