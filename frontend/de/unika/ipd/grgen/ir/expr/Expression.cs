/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ir.expr
{
using de.unika.ipd.grgen.ir;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using RetypedEdge = de.unika.ipd.grgen.ir.pattern.RetypedEdge;
using RetypedNode = de.unika.ipd.grgen.ir.pattern.RetypedNode;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// Abstract base class for expression nodes
/// </summary>
public abstract class Expression : IR
{
	private static readonly string[] childrenNames = new string[] { "type" };

	/// <summary>
	/// The type of the expression. </summary>
	protected internal Type type;

	public Expression(string name, Type type)
		: base(name)
	{
		ChildrenNames = childrenNames;
		this.type = type;
	}

	/// <returns> The type of the expression. </returns>
	public virtual Type Type
	{
		get
		{
			return type;
		}
	}

	/// <summary>
	/// Method collectNeededEntities extracts the nodes, edges, and variables occurring in this Expression.
	/// We don't collect global variables (::-prefixed), as no entities and no processing are needed for them at all, they are only accessed. </summary>
	/// <param name="needs"> A NeededEntities instance aggregating the needed elements. </param>
	public virtual void CollectNeededEntities(NeededEntities needs)
	{
		// default implementation for expressions without children that need to be collected
	}

	public static bool IsGlobalVariable(Entity entity)
	{
		if(entity is Node && !(entity is RetypedNode))
			return ((Node)entity).directlyNestingLHSGraph == null;
		else if(entity is Edge && !(entity is RetypedEdge))
			return ((Edge)entity).directlyNestingLHSGraph == null;
		else if(entity is Variable)
			return ((Variable)entity).directlyNestingLHSGraph == null;
		return false;
	}
}

}
