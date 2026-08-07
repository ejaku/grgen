/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{
using Ident = de.unika.ipd.grgen.ir.Ident;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using Direction = de.unika.ipd.grgen.util.Direction;

/// <summary>
/// An incidence count index.
/// </summary>
public class IncidenceCountIndex : Index
{
	private readonly NodeType startNodeType;
	private readonly EdgeType incidentEdgeType;
	private readonly Direction direction;
	private readonly NodeType adjacentNodeType;

	/// <param name="name"> The name of the incidence count index. </param>
	/// <param name="ident"> The identifier that identifies this object. </param>
	public IncidenceCountIndex(string name, Ident ident,
			NodeType startNodeType,
			EdgeType incidentEdgeType, Direction direction,
			NodeType adjacentNodeType)
		: base(name, ident)
	{
		this.startNodeType = startNodeType;
		this.incidentEdgeType = incidentEdgeType;
		this.direction = direction;
		this.adjacentNodeType = adjacentNodeType;
	}

	public virtual NodeType StartNodeType
	{
		get
		{
		return startNodeType;
		}
	}

	public virtual EdgeType IncidentEdgeType
	{
		get
		{
		return incidentEdgeType;
		}
	}

	public virtual Direction Direction()
	{
		return direction;
	}

	public virtual NodeType AdjacentNodeType
	{
		get
		{
		return adjacentNodeType;
		}
	}
}

}
