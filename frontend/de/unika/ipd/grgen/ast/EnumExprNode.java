/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */

package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.EnumExpression;
import de.unika.ipd.grgen.ir.EnumItem;
import de.unika.ipd.grgen.ir.EnumType;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter {
	static {
		setName(EnumExprNode.class, "enum access expression");
	}

	public EnumExprNode(Coords coords, IdentNode owner, IdentNode member) {
		super(coords, owner, member);
	}

	private EnumTypeNode owner;

	private EnumItemNode member;

	/** returns children of this node */
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member));
		return children;
	}

	private static final DeclarationTypeResolver<EnumTypeNode> ownerResolver = new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	private static final DeclarationResolver<EnumItemNode> memberResolver = new DeclarationResolver<EnumItemNode>(EnumItemNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	protected boolean resolveLocal() {
		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner!=null && successfullyResolved;

		if(owner != null) {
			owner.fixupDefinition(memberUnresolved);

			member = memberResolver.resolve(memberUnresolved, this);
			successfullyResolved = member!=null && successfullyResolved;
		} else {
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	protected boolean checkLocal() {
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	public EnumItemNode getDecl() {
		assert isResolved();

		return member;
	}

	public DeclNode getOwner() {
		assert isResolved();

		return DeclNode.getInvalid();
	}

	/**
	 * Build the IR of an enum expression.
	 * @return An enum expression IR object.
	 */
	protected IR constructIR() {
		EnumType et = owner.checkIR(EnumType.class);
		EnumItem it = member.checkIR(EnumItem.class);
		return new EnumExpression(et, it);
	}
}

