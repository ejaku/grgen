/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.pattern;

/**
 * An edge in a graph.
 */
import java.util.HashMap;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

public class Edge extends GraphEntity
{
	/** Type of the edge. */
	protected final EdgeType type;

	/** Point of definition, that is the pattern graph the edge was defined in*/
	protected PatternGraphLhs pointOfDefinition;

	// in case of retyped edge thats the pattern graph of the old edge, otherwise of the edge itself
	public PatternGraphLhs directlyNestingLHSGraph;

	protected boolean fixedDirection;

	protected boolean maybeNull;

	/** The redirected source node of this edge if any. */
	protected HashMap<PatternGraphBase, Node> redirectedSource = null;

	/** The redirected target node of this edge if any. */
	protected HashMap<PatternGraphBase, Node> redirectedTarget = null;

	/**
	 * Make a new edge.
	 * @param ident The identifier for the edge.
	 * @param type The type of the edge.
	 * @param annots The annotations of this edge.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 * @param context The context of the declaration
	 */
	public Edge(Ident ident, EdgeType type, Annotations annots,
			PatternGraphLhs directlyNestingLHSGraph,
			boolean maybeDeleted, boolean maybeRetyped,
			boolean isDefToBeYieldedTo, int context)
	{
		super("edge", ident, type, annots,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	/**
	 * Make a new edge.
	 * @param ident The identifier for the edge.
	 * @param type The type of the edge.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 * @param context The context of the declaration
	 */
	public Edge(Ident ident, EdgeType type,
			PatternGraphLhs directlyNestingLHSGraph,
			boolean maybeDeleted, boolean maybeRetyped,
			boolean isDefToBeYieldedTo, int context)
	{
		this(ident, type, EmptyAnnotations.get(), directlyNestingLHSGraph,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
	}

	public void setMaybeNull(boolean maybeNull)
	{
		this.maybeNull = maybeNull;
	}

	public boolean getMaybeNull()
	{
		return maybeNull;
	}

	/** @return The type of the edge. */
	public EdgeType getEdgeType()
	{
		return type;
	}

	/**
	 * Sets the corresponding retyped version of this edge
	 * @param retyped The retyped edge
	 * @param patternGraph The pattern graph where the edge gets retyped
	 */
	public void setRetypedEdge(Edge retyped, PatternGraphBase patternGraph)
	{
		super.setRetypedEntity(retyped, patternGraph);
	}

	/**
	 * Returns the corresponding retyped version of this edge
	 * @param patternGraph The pattern graph where the edge might get retyped
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedEdge getRetypedEdge(PatternGraphBase patternGraph)
	{
		if(super.getRetypedEntity(patternGraph) != null)
			return (RetypedEdge)super.getRetypedEntity(patternGraph);
		else
			return null;
	}

	/**
	 * @return whether the edge has a fixed direction (i.e. directed Edge) or
	 * not (all other edge kinds)
	 */
	public boolean hasFixedDirection()
	{
		return fixedDirection;
	}

	public void setPointOfDefinition(PatternGraphLhs pointOfDefinition)
	{
		assert this.pointOfDefinition == null && pointOfDefinition != null;
		this.pointOfDefinition = pointOfDefinition;
	}

	public PatternGraphLhs getPointOfDefinition()
	{
		return pointOfDefinition;
	}

	public void setRedirectedSource(Node redirectedSource, PatternGraphBase graph)
	{
		if(this.redirectedSource == null) {
			this.redirectedSource = new HashMap<PatternGraphBase, Node>();
		}
		this.redirectedSource.put(graph, redirectedSource);
	}

	public void setRedirectedTarget(Node redirectedTarget, PatternGraphBase graph)
	{
		if(this.redirectedTarget == null) {
			this.redirectedTarget = new HashMap<PatternGraphBase, Node>();
		}
		this.redirectedTarget.put(graph, redirectedTarget);
	}

	public Node getRedirectedSource(PatternGraphBase graph)
	{
		if(this.redirectedSource == null) {
			return null;
		}
		return this.redirectedSource.get(graph);
	}

	public Node getRedirectedTarget(PatternGraphBase graph)
	{
		if(this.redirectedTarget == null) {
			return null;
		}
		return this.redirectedTarget.get(graph);
	}
	
	public String getKind()
	{
		return "edge";
	}
}
