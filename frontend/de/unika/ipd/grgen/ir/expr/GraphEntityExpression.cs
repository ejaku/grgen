/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
using de.unika.ipd.grgen.ir;
using GraphEntity = de.unika.ipd.grgen.ir.pattern.GraphEntity;

/// <summary>
/// A graph entity expression node.
/// </summary>
public class GraphEntityExpression : Expression
{
	private GraphEntity graphEntity;

	public GraphEntityExpression(GraphEntity graphEntity)
		: base("graph entity", graphEntity.Type)
	{
		this.graphEntity = graphEntity;
	}

	/// <summary>
	/// Returns the graph entity of this graph entity expression. </summary>
	public virtual GraphEntity GraphEntity
	{
		get
		{
		return graphEntity;
		}
	}

	public override void CollectNeededEntities(NeededEntities needs)
	{
		if(!IsGlobalVariable(graphEntity))
			needs.Add(graphEntity);
	}

	public override bool Equals(object other)
	{
		if(!(other is GraphEntityExpression))
			return false;
		return graphEntity == ((GraphEntityExpression)other).GraphEntity;
	}

	public override int GetHashCode()
	{
		return graphEntity.GetHashCode();
	}
}

}
