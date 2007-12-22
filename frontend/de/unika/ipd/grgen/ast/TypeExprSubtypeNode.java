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
 * TypeConstSubtypeNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExprSubtypes;
import de.unika.ipd.grgen.parser.Coords;

public class TypeExprSubtypeNode extends TypeExprNode
{
	static {
		setName(TypeExprSubtypeNode.class, "type expr subtype");
	}
	
	private static final int OPERAND = 0;
	
	private static final Resolver typeResolver =
		new DeclTypeResolver(InheritanceTypeNode.class);
	
	public TypeExprSubtypeNode(Coords coords, BaseNode type) {
		super(coords, SUBTYPES);
		addChild(type);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = resolveOperand() && successfullyResolved;
		setResolved(successfullyResolved); // local result

		for(int i=0; i<children(); ++i) {
			successfullyResolved = getChild(i).doResolve() && successfullyResolved;
		}
		return successfullyResolved;
	}
	
	protected boolean resolveOperand()
	{
		if(!typeResolver.resolve(this, OPERAND)) {
			debug.report(NOTE, "resolve error");
			return false;
		}
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#doCheck() */
	protected boolean doCheck() {
		assert(isResolved());
		if(!resolveResult) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = getCheck();
		if(successfullyChecked) {
			successfullyChecked = getTypeCheck();
		}
		successfullyChecked = getChild(OPERAND).doCheck() && successfullyChecked;
	
		return successfullyChecked;
	}
	
	protected boolean check() {
		int arity = children();
		boolean arityOk = true;

		if(arity != 1) {
			reportError("Illegal arity: " + arity + " (1 expected)");
			arityOk = false;
		}

		return arityOk && checkChild(OPERAND, InheritanceTypeNode.class);
	}

	protected IR constructIR() {
		InheritanceType inh =
			(InheritanceType) getChild(OPERAND).checkIR(InheritanceType.class);
		return new TypeExprSubtypes(inh);
	}
}

