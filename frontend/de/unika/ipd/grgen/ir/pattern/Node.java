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

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.util.Annotations;
import de.unika.ipd.grgen.util.EmptyAnnotations;

/**
 * A node in a graph.
 */
public class Node extends GraphEntity
{
	/** Type of the node. */
	protected final NodeType type;

	/** Point of definition, that is the pattern graph the node was defined in*/
	protected PatternGraphLhs pointOfDefinition;

	// in case of retyped node thats the pattern graph of the old node, otherwise of the node itself
	public PatternGraphLhs directlyNestingLHSGraph;

	protected boolean maybeNull;

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param annots The annotations of this node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 * @param context The context of the declaration
	 */
	public Node(Ident ident, NodeType type, Annotations annots,
			PatternGraphLhs directlyNestingLHSGraph,
			boolean maybeDeleted, boolean maybeRetyped,
			boolean isDefToBeYieldedTo, int context)
	{
		super("node", ident, type, annots,
				maybeDeleted, maybeRetyped, isDefToBeYieldedTo, context);
		this.type = type;
		this.directlyNestingLHSGraph = directlyNestingLHSGraph;
	}

	/**
	 * Make a new node.
	 * @param ident The identifier for the node.
	 * @param type The type of the node.
	 * @param maybeDeleted Indicates whether this element might be deleted due to homomorphy.
	 * @param maybeRetyped Indicates whether this element might be retyped due to homomorphy.
	 * @param isDefToBeYieldedTo Is the entity a defined entity only, to be filled with yields from nested patterns.
	 */
	public Node(Ident ident, NodeType type,
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

	/** @return The type of the node. */
	public NodeType getNodeType()
	{
		return type;
	}

	/**
	 * Sets the corresponding retyped version of this node
	 * @param retyped The retyped node
	 * @param patternGraph The pattern graph where the node gets retyped
	 */
	public void setRetypedNode(Node retyped, PatternGraphBase patternGraph)
	{
		super.setRetypedEntity(retyped, patternGraph);
	}

	/**
	 * Returns the corresponding retyped version of this node
	 * @param patternGraph The pattern graph where the node might get retyped
	 * @return The retyped version or <code>null</code>
	 */
	public RetypedNode getRetypedNode(PatternGraphBase patternGraph)
	{
		if(super.getRetypedEntity(patternGraph) != null)
			return (RetypedNode)super.getRetypedEntity(patternGraph);
		else
			return null;
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
	
	public String getKind()
	{
		return "node";
	}
}
