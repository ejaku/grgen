/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
import de.unika.ipd.grgen.ast.model.decl.MemberDeclNode;
import de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
import de.unika.ipd.grgen.ast.pattern.EdgeCharacter;
import de.unika.ipd.grgen.ast.pattern.NodeCharacter;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.expr.Qualification;
import de.unika.ipd.grgen.parser.Coords;

/**
 * AST node that represents a qualified identifier
 * i.e. expressions like this one: a.b.c.d
 */
public class QualIdentNode extends BaseNode implements DeclaredCharacter
{
	static {
		setName(QualIdentNode.class, "Qualification");
	}

	protected IdentNode ownerUnresolved;
	private DeclNode owner;

	protected IdentNode memberUnresolved;
	private DeclNode member;

	/**
	 * Make a new identifier qualify node.
	 * @param coords The coordinates.
	 */
	public QualIdentNode(Coords coords, IdentNode owner, IdentNode member)
	{
		super(coords);
		this.ownerUnresolved = owner;
		ownerUnresolved.getCoords();
		becomeParent(this.ownerUnresolved);
		this.memberUnresolved = member;
		becomeParent(this.memberUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(getValidVersion(ownerUnresolved, owner));
		children.add(getValidVersion(memberUnresolved, member));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("owner");
		childrenNames.add("member");
		return childrenNames;
	}

	private static final DeclarationResolver<DeclNode> ownerResolver =
			new DeclarationResolver<DeclNode>(DeclNode.class);
	private static final DeclarationResolver<MemberDeclNode> memberResolver =
			new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		/* 1) resolve left hand side identifier, yielding a declaration of a type owning a scope
		 * 2) the scope owned by the lhs allows the ident node of the right hand side to fix/find its definition therein
		 * 3) resolve now complete/correct right hand side identifier into its declaration */
		boolean res = fixupDefinition(ownerUnresolved, ownerUnresolved.getScope());
		if(!res)
			return false;

		boolean successfullyResolved = true;
		owner = ownerResolver.resolve(ownerUnresolved, this);
		successfullyResolved = owner != null && successfullyResolved;
		boolean ownerResolveResult = owner != null && owner.resolve();

		if(!ownerResolveResult) {
			// member can not be resolved due to inaccessible owner
			return false;
		}

		TypeNode ownerType = owner.getDeclType();

		if(owner instanceof NodeCharacter || owner instanceof EdgeCharacter) {
			if(ownerType instanceof ScopeOwner) {
				ScopeOwner o = (ScopeOwner)ownerType;
				o.fixupDefinition(memberUnresolved);
				member = memberResolver.resolve(memberUnresolved, this);
				successfullyResolved = member != null && successfullyResolved;
			} else {
				reportError("Left hand side of '.' does not own a scope");
				successfullyResolved = false;
			}
		} else if(owner instanceof VarDeclNode) {
			member = Resolver.resolveMember(ownerType, memberUnresolved);
			if(member == null)
				successfullyResolved = false;
		} else {
			reportError("Left hand side of '.' is neither a node nor an edge (nor a variable of node/edge or match or match class type)");
			successfullyResolved = false;
		}

		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return true;
	}

	/** @see de.unika.ipd.grgen.ast.DeclaredCharacter#getDecl() */
	@Override
	public MemberDeclNode getDecl()
	{
		assert isResolved();

		return member instanceof MemberDeclNode ? (MemberDeclNode)member : null;
	}

	public DeclNode getOwner()
	{
		assert isResolved();

		return owner;
	}

	public boolean isMatchAssignment()
	{
		assert isResolved();

		return !(member instanceof MemberDeclNode);
	}

	public boolean isTransientObjectAssignment()
	{
		assert isResolved();

		return owner.getDeclType() instanceof InternalTransientObjectTypeNode;
	}

	public DeclNode getMember()
	{
		assert isResolved();

		return member;
	}

	@Override
	protected IR constructIR()
	{
		Entity ownerIR = owner.checkIR(Entity.class);
		Entity memberIR = member.checkIR(Entity.class);
		return new Qualification(ownerIR, memberIR);
	}

	public static String getKindStr()
	{
		return "qualified identifier";
	}
}
