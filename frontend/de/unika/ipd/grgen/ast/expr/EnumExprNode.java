/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.expr;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.*;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.model.decl.EnumItemDeclNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.EnumExpression;
import de.unika.ipd.grgen.ir.model.EnumItem;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.parser.Coords;

public class EnumExprNode extends QualIdentNode implements DeclaredCharacter
{
	static {
		setName(EnumExprNode.class, "enum access expression");
	}

	public EnumExprNode(Coords coords, IdentNode owner, IdentNode member)
	{
		super(coords, owner, member);
	}

	private EnumTypeNode owner;

	private EnumItemDeclNode member;

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member));
		return children;
	}
	// TODO Missing getChildrenNames()...

	private static final DeclarationTypeResolver<EnumTypeNode> ownerResolver =
			new DeclarationTypeResolver<EnumTypeNode>(EnumTypeNode.class);

	private static final DeclarationResolver<EnumItemDeclNode> memberResolver =
			new DeclarationResolver<EnumItemDeclNode>(EnumItemDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner != null && successfullyResolved;

		if(owner != null) {
			owner.fixupDefinition(memberUnresolved);

			member = memberResolver.resolve(memberUnresolved, this);
			successfullyResolved = member != null && successfullyResolved;
		} else {
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	@Override
	public EnumItemDeclNode getDecl()
	{
		assert isResolved();

		return member;
	}

	@Override
	public DeclNode getOwner()
	{
		assert isResolved();

		return DeclNode.getInvalid();
	}

	/**
	 * Build the IR of an enum expression.
	 * @return An enum expression IR object.
	 */
	@Override
	protected IR constructIR()
	{
		EnumType et = owner.checkIR(EnumType.class);
		EnumItem it = member.checkIR(EnumItem.class);
		return new EnumExpression(et, it);
	}
}
