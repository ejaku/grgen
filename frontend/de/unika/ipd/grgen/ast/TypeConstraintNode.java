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
 * TypeConstNode.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.*;

import de.unika.ipd.grgen.ast.InheritanceTypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExprConst;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A type expression constant.
 */
public class TypeConstraintNode extends TypeExprNode
{
	static {
		setName(TypeConstraintNode.class, "type expression const");
	}
	
	private static final int OPERANDS = 0;
	
	private static final Resolver typeResolver =
		new CollectResolver(new DeclTypeResolver(InheritanceTypeNode.class));
	
	private static final Checker typeChecker =
		new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));
	
	public TypeConstraintNode(Coords coords, CollectNode collect) {
		super(coords, SET);
		addChild(collect);
	}
	
	public TypeConstraintNode(IdentNode typeIdentUse) {
		this(typeIdentUse.getCoords(), new CollectNode());
		getChild(OPERANDS).addChild(typeIdentUse);
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#doResolve() */
	protected boolean doResolve() {
		if(isResolved()) {
			return getResolve();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = typeResolver.resolve(this, OPERANDS) && successfullyResolved;
		setResolved(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		for(int i=0; i<children(); ++i) {
			successfullyResolved = getChild(i).doResolve() && successfullyResolved;
		}
		return successfullyResolved;
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
		for(int i=0; i<children(); ++i) {
			successfullyChecked = getChild(i).doCheck() && successfullyChecked;
		}
		return successfullyChecked;
	}
	
	protected boolean check() {
		return checkChild(OPERANDS, typeChecker);
	}

	protected IR constructIR() {
		TypeExprConst cnst = new TypeExprConst();
		
		for(BaseNode n : getChild(OPERANDS).getChildren()) {
			InheritanceType inh = (InheritanceType) n.checkIR(InheritanceType.class);
			cnst.addOperand(inh);
		}
		
		return cnst;
	}
}

