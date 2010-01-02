/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.Constant;
import de.unika.ipd.grgen.ir.IR;

/**
 * A type const value.
 */
public class TypeConstNode extends ConstNode
{
	/** The name of the type. */
	private IdentNode id;

	/**
	 * @param coords The source code coordinates.
	 * @param id The name of the enum item.
	 * @param value The value of the enum item.
	 */
	public TypeConstNode(IdentNode id) {
		super(id.getCoords(), "type const", "DO NOT USE");
		this.id = id;
	}

	/** @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type)	{
		// TODO: ??? How would this be possible?
		if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		} else throw new UnsupportedOperationException();
	}

    /** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR() {
		return new Constant(getType().getType(), id.getDecl().getDeclType().getIR());
	}

	/** @see de.unika.ipd.grgen.ast.ExprNode#getType() */
	@Override
	public TypeNode getType() {
		return BasicTypeNode.typeType;
	}

	@Override
	public Object getValue() {
		return id.getDecl().getDeclType();
	}
}
