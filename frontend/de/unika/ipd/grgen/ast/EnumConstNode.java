/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.parser.Coords;

/**
 * An enum item value.
 */
public class EnumConstNode extends ConstNode
{
	/** The name of the enum item. */
	private IdentNode id;

	/**
	 * @param coords The source code coordinates.
	 * @param id The name of the enum item.
	 * @param value The value of the enum item.
	 */
	public EnumConstNode(Coords coords, IdentNode id, int value) {
		super(coords, "enum item", new Integer(value));
		this.id = id;
	}

	/** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
	protected ConstNode doCastTo(TypeNode type) {
		ConstNode res = ConstNode.getInvalid();

		if (type.isEqual(BasicTypeNode.intType)) {
			return new IntConstNode(getCoords(), ((Integer) getValue()).intValue());
		} else if (type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		}

		return res;
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	public TypeNode getType() {
		return BasicTypeNode.enumItemType;
	}
}
