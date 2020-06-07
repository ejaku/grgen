/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeConstraintExprNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.type;

import java.awt.Color;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing type expressions.
 */
public abstract class TypeExprNode extends BaseNode
{
	public enum TypeOperator
	{
		SET,
		UNION,
		DIFFERENCE,
		INTERSECT,
	}

	/** Opcode of the set operation. */
	protected final TypeOperator op;

	private static final TypeExprNode EMPTY = new TypeConstraintNode(Coords.getInvalid(), new CollectNode<IdentNode>());

	public static final TypeExprNode getEmpty()
	{
		return EMPTY;
	}

	protected TypeExprNode(Coords coords, TypeOperator op)
	{
		super(coords);
		this.op = op;
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor() */
	@Override
	public Color getNodeColor()
	{
		return Color.CYAN;
	}

	@Override
	public String getNodeLabel()
	{
		return "type expr " + op;
	}
}
