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

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclResolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.Typeof;

/**
 * A node representing the current type of a
 * certain node/edge.
 */
public class TypeofNode extends ExprNode
{
	static {
		setName(TypeofNode.class, "typeof");
	}
	
	/** Index of the entity node. */
	protected static final int ENTITY = 0;
	
	private static final String[] childrenNames = {
		"entity"
	};
	
	private static final Resolver entityResolver =
		new DeclResolver(new Class[] { NodeDeclNode.class, EdgeDeclNode.class});
	
	private static final Checker entityChecker =
		new SimpleChecker(new Class[] { NodeDeclNode.class, EdgeDeclNode.class});
	
	/**
	 * Make a new typeof node.
	 * @param coords The coordinates.
	 */
	public TypeofNode(Coords coords, BaseNode entity) {
		super(coords);
		setChildrenNames(childrenNames);
		addChild(entity);
	}
	
  	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		successfullyResolved = entityResolver.resolve(this, ENTITY) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = getChild(ENTITY).resolve() && successfullyResolved;
		return successfullyResolved;
	}
	
	/** @see de.unika.ipd.grgen.ast.BaseNode#check() */
	protected boolean check() {
		if(!resolutionResult()) {
			return false;
		}
		if(isChecked()) {
			return getChecked();
		}
		
		boolean successfullyChecked = checkLocal();
		nodeCheckedSetResult(successfullyChecked);
		if(successfullyChecked) {
			assert(!isTypeChecked());
			successfullyChecked = typeCheckLocal();
			nodeTypeCheckedSetResult(successfullyChecked);
		}
		
		successfullyChecked = getChild(ENTITY).check() && successfullyChecked;
		return successfullyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		return checkChild(ENTITY, entityChecker);
	}
	
	protected IR constructIR() {
		Entity entity = (Entity) getChild(ENTITY).checkIR(Entity.class);
		
		return new Typeof(entity);
	}
	
	public DeclNode getEntity() {
		return (DeclNode)getChild(ENTITY);
	}

	public TypeNode getType() {
		return BasicTypeNode.typeType;
	}
}
