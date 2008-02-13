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

import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;
import de.unika.ipd.grgen.ir.TypeExprConst;
import de.unika.ipd.grgen.parser.Coords;

/**
 * A type constraint expression AST node.
 * TODO: Only one operand, operands-collect node is senseless? - if yes remove it
 */
public class TypeConstraintNode extends TypeExprNode {
	static {
		setName(TypeConstraintNode.class, "type expr constraint");
	}

	CollectNode<InheritanceTypeNode> operands;
	CollectNode<IdentNode> operandsUnresolved;

	public TypeConstraintNode(Coords coords, CollectNode<IdentNode> collect) {
		super(coords, SET);
		this.operandsUnresolved = collect;
		becomeParent(this.operandsUnresolved);
	}

	public TypeConstraintNode(IdentNode typeIdentUse) {
		super(typeIdentUse.getCoords(), SET);
		this.operandsUnresolved = new CollectNode<IdentNode>();
		becomeParent(this.operandsUnresolved);
		operandsUnresolved.addChild(typeIdentUse);
	}

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(operandsUnresolved, operands));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("operands");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		DeclarationTypeResolver<InheritanceTypeNode> typeResolver =
			new DeclarationTypeResolver<InheritanceTypeNode>(InheritanceTypeNode.class);
		CollectResolver<InheritanceTypeNode> operandsResolver =
			new CollectResolver<InheritanceTypeNode>(typeResolver);
		operands = operandsResolver.resolve(operandsUnresolved, this);
		successfullyResolved = operands!=null && successfullyResolved;
		return successfullyResolved;
	}


	protected boolean checkLocal() {
		return true;
	}

	protected IR constructIR() {
		TypeExprConst cnst = new TypeExprConst();

		for(InheritanceTypeNode n : operands.getChildren()) {
			InheritanceType inh = (InheritanceType) n.checkIR(InheritanceType.class);
			cnst.addOperand(inh);
		}

		return cnst;
	}
}

