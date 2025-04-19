/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.2
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.expr.string.StringConstNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Constant;

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
	public TypeConstNode(IdentNode id)
	{
		super(id.getCoords(), "type const", "DO NOT USE");
		this.id = id;
	}

	/** @see de.unika.ipd.grgen.ast.expr.ConstNode#doCastTo(de.unika.ipd.grgen.ast.type.TypeNode) */
	@Override
	protected ConstNode doCastTo(TypeNode type)
	{
		if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		} else
			throw new UnsupportedOperationException();
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		return new Constant(getType().getType(), id.getDecl().getDeclType().getIR());
	}

	/** @see de.unika.ipd.grgen.ast.expr.ExprNode#getType() */
	@Override
	public TypeNode getType()
	{
		return BasicTypeNode.typeType;
	}

	@Override
	public Object getValue()
	{
		return id.getDecl().getDeclType();
	}
}
