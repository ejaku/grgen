/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.GraphEntity;

/**
 * A graph entity expression node.
 */
public class GraphEntityExpression extends Expression
{
	private GraphEntity graphEntity;

	public GraphEntityExpression(GraphEntity graphEntity)
	{
		super("graph entity", graphEntity.getType());
		this.graphEntity = graphEntity;
	}

	/** Returns the graph entity of this graph entity expression. */
	public GraphEntity getGraphEntity()
	{
		return graphEntity;
	}

	@Override
	public void collectNeededEntities(NeededEntities needs)
	{
		if(!isGlobalVariable(graphEntity))
			needs.add(graphEntity);
	}

	@Override
	public boolean equals(Object other)
	{
		if(!(other instanceof GraphEntityExpression))
			return false;
		return graphEntity == ((GraphEntityExpression)other).getGraphEntity();
	}

	@Override
	public int hashCode()
	{
		return graphEntity.hashCode();
	}
}
