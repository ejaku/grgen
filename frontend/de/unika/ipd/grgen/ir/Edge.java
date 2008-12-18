/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	protected boolean fixedDirection;

	/**
	 * Make a new edge.
	 * @param ident The identifier for the edge.
	 * @param type The type of the edge.
	 * @param annots The annotations of this edge.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Edge(Ident ident, EdgeType type, Annotations annots, boolean maybeDeleted, boolean maybeRetyped) {
		super("edge", ident, type, annots, maybeDeleted, maybeRetyped);
		this.type = type;
	}

	/**
	 * Make a new edge.
	 * @param ident The identifier for the edge.
	 * @param type The type of the edge.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Edge(Ident ident, EdgeType type, boolean maybeDeleted, boolean maybeRetyped) {
		this(ident, type, EmptyAnnotations.get(), maybeDeleted, maybeRetyped);
	}

	/** @return The type of the edge. */
	public EdgeType getEdgeType() {
		return type;
	}

	/** Get the edge from which this edge inherits its dynamic type */
	public Edge getTypeof() {
		return (Edge)typeof;
	}

	/**
	 * Sets the corresponding retyped version of this edge
	 * @param retyped The retyped edge
	 */
	public void setRetypedEdge(Edge retyped) {
		this.retyped = retyped;
	}

	/**
	 * Returns the corresponding retyped version of this edge
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedEdge getRetypedEdge() {
		return (RetypedEdge)this.retyped;
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
