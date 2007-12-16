/*
 GrGen: graph rewrite generator tool.
 Copyright (C) 2005  IPD Goos, Universit"at Karlsruhe, Germany

 This library is free software; you can redistribute it and/or
 modify it under the terms of the GNU Lesser General Public
 License as published by the Free Software Foundation; either
 version 2.1 of the License, or (at your option) any later version.

 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 Lesser General Public License for more details.

 You should have received a copy of the GNU Lesser General Public
 License along with this library; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
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

	/**
	 * @see de.unika.ipd.grgen.ast.ConstNode#doCastTo(de.unika.ipd.grgen.ast.TypeNode)
	 */
	protected ConstNode doCastTo(TypeNode type)	{
		ConstNode res = ConstNode.getInvalid();
		
		if(type.isEqual(BasicTypeNode.stringType)) {
			return new StringConstNode(getCoords(), id.toString());
		}
		
		return res;
	}
	
    /**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	protected IR constructIR() {
		return new Constant(getType().getType(), id.getDecl().getDeclType().getIR());
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.ExprNode#getType()
	 */
	public TypeNode getType() {
		return BasicTypeNode.typeType;
	}

	@Override
	public Object getValue() {
		return id.getDecl().getDeclType();
	}
}
