/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeConstraintNode.java
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

	private CollectNode<InheritanceTypeNode> operands;
	private CollectNode<IdentNode> operandsUnresolved;

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
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(operandsUnresolved, operands));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("operands");
		return childrenNames;
	}

	private static final CollectResolver<InheritanceTypeNode> operandsResolver = new CollectResolver<InheritanceTypeNode>(
			new DeclarationTypeResolver<InheritanceTypeNode>(InheritanceTypeNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		operands = operandsResolver.resolve(operandsUnresolved, this);

		return operands != null;
	}

	@Override
	protected boolean checkLocal() {
		return true;
	}

	@Override
	protected IR constructIR() {
		TypeExprConst cnst = new TypeExprConst();

		for(InheritanceTypeNode n : operands.getChildren()) {
			InheritanceType inh = n.checkIR(InheritanceType.class);
			cnst.addOperand(inh);
		}

		return cnst;
	}
}

