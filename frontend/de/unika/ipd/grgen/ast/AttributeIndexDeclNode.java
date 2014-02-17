/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.2
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ir.AttributeIndex;
import de.unika.ipd.grgen.ir.Entity;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.InheritanceType;

import java.util.Collection;
import java.util.Vector;


/**
 * AST node class representing attribute index declarations
 */
public class AttributeIndexDeclNode extends DeclNode {
	static {
		setName(AttributeIndexDeclNode.class, "attribute index declaration");
	}

	protected IdentNode typeUnresolved;
	protected TypeNode type;
	protected IdentNode memberUnresolved;
	protected MemberDeclNode member;

	private static final AttributeIndexTypeNode attributeIndexType =
		new AttributeIndexTypeNode();

	public AttributeIndexDeclNode(IdentNode id, IdentNode type, IdentNode member) {
		super(id, attributeIndexType);
		this.typeUnresolved = type;
		becomeParent(this.typeUnresolved);
		this.memberUnresolved = member;
		becomeParent(this.memberUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, type));
		children.add(getValidVersion(memberUnresolved, member));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("member");
		return childrenNames;
	}

	private static DeclarationResolver<TypeDeclNode> typeResolver =
		new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);
	private static final DeclarationResolver<MemberDeclNode> memberResolver
		= new DeclarationResolver<MemberDeclNode>(MemberDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		TypeDeclNode resolved = typeResolver.resolve(typeUnresolved, this);
		if(resolved == null) return false;
		//if(!resolved.resolve()) return false;
		
		type = resolved.getDeclType();
		
		if(!(type instanceof InheritanceTypeNode)) {
			typeUnresolved.reportError("Type used for indexing with \"" + getIdentNode() + "\" must be a node or edge type ");
			return false;
		}

		ScopeOwner o = (ScopeOwner) type;
		o.fixupDefinition(memberUnresolved);
		member = memberResolver.resolve(memberUnresolved, this);

		return member != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean checkLocal() {
		return true;
	}
	
	@Override
	public TypeNode getDeclType() {
		assert isResolved();
	
		return attributeIndexType;
	}

	@Override
	protected IR constructIR() {
		AttributeIndex attributeIndex = new AttributeIndex(getIdentNode().toString(),
				getIdentNode().getIdent(), type.checkIR(InheritanceType.class), member.checkIR(Entity.class));
		return attributeIndex;
	}	
}


