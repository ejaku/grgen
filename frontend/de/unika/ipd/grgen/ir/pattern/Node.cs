/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{
using System.Diagnostics;

using Ident = de.unika.ipd.grgen.ir.Ident;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using Annotations = de.unika.ipd.grgen.util.Annotations;
using EmptyAnnotations = de.unika.ipd.grgen.util.EmptyAnnotations;

/// <summary>
/// A node in a graph.
/// </summary>
public class Node : GraphEntity
{
	/// <summary>
	/// Type of the node. </summary>
	protected internal readonly new NodeType type;

	/// <summary>
	/// Point of definition, that is the pattern graph the node was defined in </summary>
	protected internal PatternGraphLhs pointOfDefinition;

	// in case of retyped node thats the pattern graph of the old node, otherwise of the node itself
	public PatternGraphLhs directlyNestingLHSGraph;

	protected internal bool maybeNull;

	/// <summary>
	/// Make a new node. </summary>
	/// <param name="ident"> The identifier for the node. </param>
	/// <param name="type"> The type of the node. </param>
	/// <param name="annots"> The annotations of this node. </param>
	/// <param name="maybeDeleted"> Indicates whether this element might be deleted due to homomorphy. </param>
	/// <param name="maybeRetyped"> Indicates whether this element might be retyped due to homomorphy. </param>
	/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
	/// <param name="context"> The context of the declaration </param>
	public Node(Ident ident, NodeType type, Annotations annots,
			PatternGraphLhs directlyNestingLHSGraph,
			bool maybeDeleted, bool maybeRetyped,
			bool isDefToBeYieldedTo, int context)
		: base("node", ident, type, annots,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context)
	{
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	/// <summary>
	/// Make a new node. </summary>
	/// <param name="ident"> The identifier for the node. </param>
	/// <param name="type"> The type of the node. </param>
	/// <param name="maybeDeleted"> Indicates whether this element might be deleted due to homomorphy. </param>
	/// <param name="maybeRetyped"> Indicates whether this element might be retyped due to homomorphy. </param>
	/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
	public Node(Ident ident, NodeType type,
			PatternGraphLhs directlyNestingLHSGraph,
			bool maybeDeleted, bool maybeRetyped,
			bool isDefToBeYieldedTo, int context)
		: this(ident, type, EmptyAnnotations.Get(), directlyNestingLHSGraph,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context)
	{
	}

	public virtual bool MaybeNull
	{
		set
		{
		this.maybeNull = value;
		}
		get
		{
		return maybeNull;
		}
	}


	/// <returns> The type of the node. </returns>
	public virtual NodeType NodeType
	{
		get
		{
		return type;
		}
	}

	/// <summary>
	/// Sets the corresponding retyped version of this node </summary>
	/// <param name="retyped"> The retyped node </param>
	/// <param name="patternGraph"> The pattern graph where the node gets retyped </param>
	public virtual void SetRetypedNode(Node retyped, PatternGraphBase patternGraph)
	{
		base.SetRetypedEntity(retyped, patternGraph);
	}

	/// <summary>
	/// Returns the corresponding retyped version of this node </summary>
	/// <param name="patternGraph"> The pattern graph where the node might get retyped </param>
	/// <returns> The retyped version or <code>null</code> </returns>
	public virtual RetypedNode GetRetypedNode(PatternGraphBase patternGraph)
	{
		if(base.GetRetypedEntity(patternGraph) != null)
			return (RetypedNode)base.GetRetypedEntity(patternGraph);
		else
			return null;
	}

	public virtual PatternGraphLhs PointOfDefinition
	{
		set
		{
		Debug.Assert(this.pointOfDefinition == null && value != null);
		this.pointOfDefinition = value;
		}
		get
		{
		return pointOfDefinition;
		}
	}


	public override string Kind
	{
		get
		{
		return "node";
		}
	}
}

}
