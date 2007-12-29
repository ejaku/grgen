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

import java.util.Collection;
import java.util.Vector;
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
	
	BaseNode entity;
		
	public TypeofNode(Coords coords, BaseNode entity) {
		super(coords);
		this.entity= entity==null ? NULL : entity;
		becomeParent(this.entity);
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(entity);
		return children;
	}
	
	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("entity");
		return childrenNames;
	}
	
  	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver entityResolver = new DeclResolver(new Class[] { NodeDeclNode.class, EdgeDeclNode.class});
		BaseNode resolved = entityResolver.resolve(entity);
		successfullyResolved = resolved!=null && successfullyResolved;
		entity = ownedResolutionResult(entity, resolved);
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = entity.resolve() && successfullyResolved;
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
		
		boolean childrenChecked = true;
		if(!visitedDuringCheck()) {
			setCheckVisited();
			
			childrenChecked = entity.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	protected boolean checkLocal() {
		Checker entityChecker = new SimpleChecker(new Class[] { NodeDeclNode.class, EdgeDeclNode.class});
		return entityChecker.check(entity, error);
	}
	
	protected IR constructIR() {
		Entity entity = (Entity) this.entity.checkIR(Entity.class);
		
		return new Typeof(entity);
	}
	
	public DeclNode getEntity() {
		return (DeclNode)entity;
	}

	public TypeNode getType() {
		return BasicTypeNode.typeType;
	}
}
