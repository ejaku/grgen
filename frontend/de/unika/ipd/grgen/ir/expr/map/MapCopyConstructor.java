/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.expr.map;

import de.unika.ipd.grgen.ir.NeededEntities;
import de.unika.ipd.grgen.ir.expr.Expression;
import de.unika.ipd.grgen.ir.type.container.MapType;

public class MapCopyConstructor extends Expression
{
	private Expression mapToCopy;
	private MapType mapType;

	public MapCopyConstructor(Expression mapToCopy, MapType mapType)
	{
		super("map copy construtor", mapType);
		this.mapToCopy = mapToCopy;
		this.mapType = mapType;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		needs.add(this);
		needs.needsGraph();
		mapToCopy.collectNeededEntities(needs);
	}

	public Expression getMapToCopy()
	{
		return mapToCopy;
	}

	public MapType getMapType()
	{
		return mapType;
	}
}
