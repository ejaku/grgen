/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.NodeType;

/**
 * An incidence count index.
 */
public class IncidenceCountIndex extends Index
{
	private final NodeType startNodeType;
	private final EdgeType incidentEdgeType;
	private final int direction; // one of INCIDENT|INCOMING|OUTGOING in CountIncidentEdgeExpr
	private final NodeType adjacentNodeType;

	/**
	 * @param name The name of the incidence count index.
	 * @param ident The identifier that identifies this object.
	 */
	public IncidenceCountIndex(String name, Ident ident,
			NodeType startNodeType,
			EdgeType incidentEdgeType, int direction,
			NodeType adjacentNodeType)
	{
		super(name, ident);
		this.startNodeType = startNodeType;
		this.incidentEdgeType = incidentEdgeType;
		this.direction = direction;
		this.adjacentNodeType = adjacentNodeType;
	}

	public NodeType getStartNodeType()
	{
		return startNodeType;
	}

	public EdgeType getIncidentEdgeType()
	{
		return incidentEdgeType;
	}

	public int Direction()
	{
		return direction;
	}

	public NodeType getAdjacentNodeType()
	{
		return adjacentNodeType;
	}
}
