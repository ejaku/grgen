/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.expr;

import de.unika.ipd.grgen.ir.*;
import de.unika.ipd.grgen.ir.pattern.Edge;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.RetypedEdge;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;
import de.unika.ipd.grgen.ir.pattern.Variable;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * Abstract base class for expression nodes
 */
public abstract class Expression extends IR
{
	private static final String[] childrenNames = { "type" };

	/** The type of the expression. */
	protected Type type;

	public Expression(String name, Type type)
	{
		super(name);
		setChildrenNames(childrenNames);
		this.type = type;
	}

	/** @return The type of the expression. */
	public Type getType()
	{
		return type;
	}

	/**
	 * Method collectNeededEntities extracts the nodes, edges, and variables occurring in this Expression.
	 * We don't collect global variables (::-prefixed), as no entities and no processing are needed for them at all, they are only accessed.
	 * @param needs A NeededEntities instance aggregating the needed elements.
	 */
	public void collectNeededEntities(NeededEntities needs)
	{
		// default implementation for expressions without children that need to be collected
	}

	public static boolean isGlobalVariable(Entity entity)
	{
		if(entity instanceof Node && !(entity instanceof RetypedNode)) {
			return ((Node)entity).directlyNestingLHSGraph == null;
		} else if(entity instanceof Edge && !(entity instanceof RetypedEdge)) {
			return ((Edge)entity).directlyNestingLHSGraph == null;
		} else if(entity instanceof Variable) {
			return ((Variable)entity).directlyNestingLHSGraph == null;
		}
		return false;
	}
}
