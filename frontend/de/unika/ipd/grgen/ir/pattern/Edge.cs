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
using System.Collections.Generic;
using System.Diagnostics;

/// <summary>
/// An edge in a graph.
/// </summary>

using Ident = de.unika.ipd.grgen.ir.Ident;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using Annotations = de.unika.ipd.grgen.util.Annotations;
using EmptyAnnotations = de.unika.ipd.grgen.util.EmptyAnnotations;

public class Edge : GraphEntity
{
	/// <summary>
	/// Type of the edge. </summary>
	protected internal readonly new EdgeType type;

	/// <summary>
	/// Point of definition, that is the pattern graph the edge was defined in </summary>
	protected internal PatternGraphLhs pointOfDefinition;

	// in case of retyped edge thats the pattern graph of the old edge, otherwise of the edge itself
	public PatternGraphLhs directlyNestingLHSGraph;

	protected internal bool fixedDirection;

	protected internal bool maybeNull;

	/// <summary>
	/// The redirected source node of this edge if any. </summary>
	protected internal Dictionary<PatternGraphBase, Node> redirectedSource = null;

	/// <summary>
	/// The redirected target node of this edge if any. </summary>
	protected internal Dictionary<PatternGraphBase, Node> redirectedTarget = null;

	/// <summary>
	/// Make a new edge. </summary>
	/// <param name="ident"> The identifier for the edge. </param>
	/// <param name="type"> The type of the edge. </param>
	/// <param name="annots"> The annotations of this edge. </param>
	/// <param name="maybeDeleted"> Indicates whether this element might be deleted due to homomorphy. </param>
	/// <param name="maybeRetyped"> Indicates whether this element might be retyped due to homomorphy. </param>
	/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
	/// <param name="context"> The context of the declaration </param>
	public Edge(Ident ident, EdgeType type, Annotations annots,
			PatternGraphLhs directlyNestingLHSGraph,
			bool maybeDeleted, bool maybeRetyped,
			bool isDefToBeYieldedTo, int context)
		: base("edge", ident, type, annots,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context)
	{
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	/// <summary>
	/// Make a new edge. </summary>
	/// <param name="ident"> The identifier for the edge. </param>
	/// <param name="type"> The type of the edge. </param>
	/// <param name="maybeDeleted"> Indicates whether this element might be deleted due to homomorphy </param>
	/// <param name="maybeRetyped"> Indicates whether this element might be retyped due to homomorphy. </param>
	/// <param name="isDefToBeYieldedTo"> Is the entity a defined entity only, to be filled with yields from nested patterns. </param>
	/// <param name="context"> The context of the declaration </param>
	public Edge(Ident ident, EdgeType type,
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


	/// <returns> The type of the edge. </returns>
	public virtual EdgeType EdgeType
	{
		get
		{
			return type;
		}
	}

	/// <summary>
	/// Sets the corresponding retyped version of this edge </summary>
	/// <param name="retyped"> The retyped edge </param>
	/// <param name="patternGraph"> The pattern graph where the edge gets retyped </param>
	public virtual void SetRetypedEdge(Edge retyped, PatternGraphBase patternGraph)
	{
		base.SetRetypedEntity(retyped, patternGraph);
	}

	/// <summary>
	/// Returns the corresponding retyped version of this edge </summary>
	/// <param name="patternGraph"> The pattern graph where the edge might get retyped </param>
	/// <returns> The retyped version or <code>null</code> </returns>
	public virtual RetypedEdge GetRetypedEdge(PatternGraphBase patternGraph)
	{
		if(base.GetRetypedEntity(patternGraph) != null)
			return (RetypedEdge)base.GetRetypedEntity(patternGraph);
		else
			return null;
	}

	/// <returns> whether the edge has a fixed direction (i.e. directed Edge) or
	/// not (all other edge kinds) </returns>
	public virtual bool HasFixedDirection()
	{
		return fixedDirection;
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


	public virtual void SetRedirectedSource(Node redirectedSource, PatternGraphBase graph)
	{
		if(this.redirectedSource == null)
			this.redirectedSource = new Dictionary<PatternGraphBase, Node>();
		this.redirectedSource[graph] = redirectedSource;
	}

	public virtual void SetRedirectedTarget(Node redirectedTarget, PatternGraphBase graph)
	{
		if(this.redirectedTarget == null)
			this.redirectedTarget = new Dictionary<PatternGraphBase, Node>();
		this.redirectedTarget[graph] = redirectedTarget;
	}

	public virtual Node GetRedirectedSource(PatternGraphBase graph)
	{
		if(this.redirectedSource == null)
			return null;
		return this.redirectedSource[graph];
	}

	public virtual Node GetRedirectedTarget(PatternGraphBase graph)
	{
		if(this.redirectedTarget == null)
			return null;
		return this.redirectedTarget[graph];
	}

	public override string Kind
	{
		get
		{
			return "edge";
		}
	}
}

}
