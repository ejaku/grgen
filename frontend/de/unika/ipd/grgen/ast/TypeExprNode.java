/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * TypeConstraintExprNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import java.awt.Color;

import de.unika.ipd.grgen.ir.TypeExprSetOperator;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node representing type expressions.
 */
public abstract class TypeExprNode extends BaseNode
{
	public static final int SET = 0;
	public static final int SUBTYPES = 1;
	public static final int UNION = 2;
	public static final int DIFFERENCE = 3;
	public static final int INTERSECT = 4;
	public static final int LAST = INTERSECT;

	// TODO: opnames don't fit to the opcodes above - correct it
	protected static final String[] opName = {
		"const", "subtypes", "union", "diff", "intersect"
	};

	protected static final int[] irOp = {
		-1, -1, TypeExprSetOperator.UNION,
			TypeExprSetOperator.DIFFERENCE, TypeExprSetOperator.INTERSECT
	};

	/** Opcode of the set operation. */
	protected final int op;

	private static final TypeExprNode EMPTY =
		new TypeConstraintNode(Coords.getInvalid(), new CollectNode<IdentNode>());

	public static final TypeExprNode getEmpty() {
		return EMPTY;
	}

	protected TypeExprNode(Coords coords, int op) {
		super(coords);
		this.op = op;
		assert op >= 0 && op <= LAST : "Illegal type constraint expr opcode";
	}

	/** @see de.unika.ipd.grgen.util.GraphDumpable#getNodeColor() */
	public Color getNodeColor() {
		return Color.CYAN;
	}

	public String getNodeLabel() {
		return "type expr " + opName[op];
	}
}

