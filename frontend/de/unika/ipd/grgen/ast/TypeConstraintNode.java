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

import java.util.Collection;
import java.util.Vector;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.DeclTypeResolver;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectChecker;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExprConst;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A type constraint expression AST node.
 * TODO: Only one operand, operands-collect node is senseless? - if yes remove it
 */
public class TypeConstraintNode extends TypeExprNode
{
	static {
		setName(TypeConstraintNode.class, "type expr constraint");
	}
	
	BaseNode operands;
	
	public TypeConstraintNode(Coords coords, CollectNode collect) {
		super(coords, SET);
		this.operands = collect==null ? NULL : collect;
		becomeParent(this.operands);
	}
	
	public TypeConstraintNode(IdentNode typeIdentUse) {
		super(typeIdentUse.getCoords(), SET);
		this.operands = new CollectNode();
		becomeParent(this.operands);
		operands.addChild(typeIdentUse);
	}
	
	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(operands);
		return children;
	}
	
	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("operands");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolve() */
	protected boolean resolve() {
		if(isResolved()) {
			return resolutionResult();
		}
		
		debug.report(NOTE, "resolve in: " + getId() + "(" + getClass() + ")");
		boolean successfullyResolved = true;
		Resolver typeResolver = new DeclTypeResolver(InheritanceTypeNode.class);
		successfullyResolved = ((CollectNode)operands).resolveChildren(typeResolver) && successfullyResolved;
		nodeResolvedSetResult(successfullyResolved); // local result
		if(!successfullyResolved) {
			debug.report(NOTE, "resolve error");
		}
		
		successfullyResolved = operands.resolve() && successfullyResolved;
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
			
			childrenChecked = operands.check() && childrenChecked;
		}
		
		boolean locallyChecked = checkLocal();
		nodeCheckedSetResult(locallyChecked);
		
		return childrenChecked && locallyChecked;
	}
	
	protected boolean checkLocal() {
		Checker typeChecker = new CollectChecker(new SimpleChecker(InheritanceTypeNode.class));
		return typeChecker.check(operands, error);
	}

	protected IR constructIR() {
		TypeExprConst cnst = new TypeExprConst();
		
		for(BaseNode n : operands.getChildren()) {
			InheritanceType inh = (InheritanceType) n.checkIR(InheritanceType.class);
			cnst.addOperand(inh);
		}
		
		return cnst;
	}
}

