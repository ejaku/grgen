/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

/**
 * An edge in a graph.
 */
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

public class Edge extends GraphEntity {

	/** Type of the edge. */
	protected final EdgeType type;

	/** Point of definition, that is the pattern graph the edge was defined in*/
	protected PatternGraph pointOfDefinition;

	// in case of retyped edge thats the pattern graph of the old edge, otherwise of the edge itself
	public PatternGraph directlyNestingLHSGraph;

	protected boolean fixedDirection;
	
	protected boolean maybeNull;

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
			PatternGraph directlyNestingLHSGraph, 
			boolean maybeDeleted, boolean maybeRetyped, boolean isDefToBeYieldedTo, int context) {
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
			PatternGraph directlyNestingLHSGraph, 
			boolean maybeDeleted, boolean maybeRetyped, boolean isDefToBeYieldedTo, int context) {
		this(ident, type, EmptyAnnotations.get(), directlyNestingLHSGraph, 
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
	}

	public void setMaybeNull(boolean maybeNull) {
		this.maybeNull = maybeNull;
	}
	
	public boolean getMaybeNull() {
		return maybeNull;
	}
	
	/** @return The type of the edge. */
	public EdgeType getEdgeType() {
		return type;
	}

	/**
	 * Sets the corresponding retyped version of this edge
	 * @param retyped The retyped edge
	 * @param graph The graph where the edge gets retyped
	 */
	public void setRetypedEdge(Edge retyped, Graph graph) {
		super.setRetypedEntity(retyped, graph);
	}

	/**
	 * Returns the corresponding retyped version of this edge
	 * @param graph The graph where the edge might get retyped
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedEdge getRetypedEdge(Graph graph) {
		if(super.getRetypedEntity(graph)!=null)
			return (RetypedEdge)super.getRetypedEntity(graph);
		else
			return null;
	}

	/**
	 * @return whether the edge has a fixed direction (i.e. directed Edge) or
	 * not (all other edge kinds)
	 */
	public boolean hasFixedDirection() {
		return fixedDirection;
	}

	public void setPointOfDefinition(PatternGraph pointOfDefinition) {
		assert this.pointOfDefinition==null && pointOfDefinition!=null;
		this.pointOfDefinition = pointOfDefinition;
	}

	public PatternGraph getPointOfDefinition() {
		return pointOfDefinition;
	}
}
