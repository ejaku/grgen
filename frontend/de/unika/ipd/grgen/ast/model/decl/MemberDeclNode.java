/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */
package de.unika.ipd.grgen.ast.model.decl;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.PackageIdentNode;
import de.unika.ipd.grgen.ast.decl.DeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.EnumTypeNode;
import de.unika.ipd.grgen.ast.model.type.ExternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.InternalTransientObjectTypeNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
import de.unika.ipd.grgen.ast.type.container.ArrayTypeNode;
import de.unika.ipd.grgen.ast.type.container.DequeTypeNode;
import de.unika.ipd.grgen.ast.type.container.MapTypeNode;
import de.unika.ipd.grgen.ast.type.container.SetTypeNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ast.util.Resolver;
import de.unika.ipd.grgen.ast.util.SimpleChecker;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.type.Type;

/**
 * A compound type member declaration.
 */
public class MemberDeclNode extends DeclNode
{
	static {
		setName(MemberDeclNode.class, "member declaration");
	}

	public TypeNode type;
	private boolean isConst;
	private BaseNode constInitializer;

	/**
	 * @param n Identifier which declared the member.
	 * @param t Type with which the member was declared.
	 */
	public MemberDeclNode(IdentNode n, BaseNode t, boolean isConst)
	{
		super(n, t);
		this.isConst = isConst;
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		return childrenNames;
	}

	public boolean isConst()
	{
		return isConst;
	}

	public BaseNode getConstInitializer()
	{
		return constInitializer;
	}

	public void setConstInitializer(BaseNode init)
	{
		constInitializer = init;
	}

	private static final DeclarationTypeResolver<TypeNode> typeResolver =
			new DeclarationTypeResolver<TypeNode>(TypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		if(typeUnresolved instanceof PackageIdentNode)
			Resolver.resolveOwner((PackageIdentNode)typeUnresolved);
		else if(typeUnresolved instanceof IdentNode) {
			fixupDefinition((IdentNode)typeUnresolved, ((IdentNode)typeUnresolved).getScope().getIdentNode().getScope());
		}
		type = typeResolver.resolve(typeUnresolved, this);
		return type != null;
	}

	/** @return The type node of the declaration */
	@Override
	public TypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}

	private static final Checker typeChecker = new SimpleChecker(
			new Class[] { BasicTypeNode.class, EnumTypeNode.class,
					InternalObjectTypeNode.class, InternalTransientObjectTypeNode.class, ExternalObjectTypeNode.class,
					NodeTypeNode.class, EdgeTypeNode.class,
					MapTypeNode.class, SetTypeNode.class, ArrayTypeNode.class, DequeTypeNode.class });

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return typeChecker.check(type, error);
	}

	@Override
	protected IR constructIR()
	{
		Type type = getDeclType().checkIR(Type.class);
		return new Entity("entity", getIdentNode().getIdent(), type, isConst, false, 0);
	}

	public static String getKindStr()
	{
		return "member";
	}
}
