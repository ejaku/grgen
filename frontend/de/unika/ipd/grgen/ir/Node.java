/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

/**
 * A node in a graph.
 */
public class Node extends GraphEntity {

	/** Type of the node. */
	protected final NodeType type;

	/** Point of definition, that is the pattern graph the node was defined in*/
	protected PatternGraph pointOfDefinition;

	// in case of retyped node thats the pattern graph of the old node, otherwise of the node itself
	public PatternGraph directlyNestingLHSGraph;
	
	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param annots The annotations of this node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Node(Ident ident, NodeType type, Annotations annots,
			PatternGraph directlyNestingLHSGraph, boolean maybeDeleted, boolean maybeRetyped) {
		super("node", ident, type, annots, maybeDeleted, maybeRetyped);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 */
	public Node(Ident ident, NodeType type, 
			PatternGraph directlyNestingLHSGraph, boolean maybeDeleted, boolean maybeRetyped) {
		this(ident, type, EmptyAnnotations.get(), directlyNestingLHSGraph, maybeDeleted, maybeRetyped);
	}

	/** @return The type of the node. */
	public NodeType getNodeType() {
		return type;
	}

	/** Get the node from which this node inherits its dynamic type */
	public Node getTypeof() {
		return (Node)typeof;
	}

	/**
	 * Sets the corresponding retyped version of this node
	 * @param retyped The retyped node
	 */
	public void setRetypedNode(Node retyped) {
		this.retyped = retyped;
	}

	/**
	 * Returns the corresponding retyped version of this node
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedNode getRetypedNode() {
		return (RetypedNode)this.retyped;
	}

	public void setPointOfDefinition(PatternGraph pointOfDefinition) {
		assert this.pointOfDefinition==null && pointOfDefinition!=null;
		this.pointOfDefinition = pointOfDefinition;
	}

	public PatternGraph getPointOfDefinition() {
		return pointOfDefinition;
	}
}
