/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.0
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.decl;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.model.type.ArbitraryEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.DirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.model.type.UndirectedEdgeTypeNode;
import de.unika.ipd.grgen.ast.type.DeclaredTypeNode;
import de.unika.ipd.grgen.ast.util.DeclarationTypeResolver;
import de.unika.ipd.grgen.ir.IR;

/**
 * Declaration of a type.
 */
public class TypeDeclNode extends DeclNode
{
	static {
		setName(TypeDeclNode.class, "type declaration");
	}

	private DeclaredTypeNode type;

	public TypeDeclNode(IdentNode i, BaseNode t)
	{
		super(i, t);

		// Set the declaration of the declared type node to this node.
		if(t instanceof DeclaredTypeNode) {
			DeclaredTypeNode declTypeNode = (DeclaredTypeNode)t;
			declTypeNode.setDecl(this);
		}
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

	private static DeclarationTypeResolver<DeclaredTypeNode> typeResolver =
			new DeclarationTypeResolver<DeclaredTypeNode>(DeclaredTypeNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		type = typeResolver.resolve(typeUnresolved, this);

		return type != null;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		return checkNoConflictingEdgeParents();
	}

	/**
	 * Checks whether an edge class extends a directed and an undirected edge
	 * class.
	 *
	 * @return Check pass without an error.
	 */
	private boolean checkNoConflictingEdgeParents()
	{
		if(!(type instanceof EdgeTypeNode)) {
			return true;
		}

		EdgeTypeNode edgeType = (EdgeTypeNode)type;

		InheritanceTypeNode extendEdge = extendsEdge(edgeType);
		InheritanceTypeNode extendUEdge = extendsUEdge(edgeType);

		if(extendEdge!=null && extendUEdge!=null) {
			reportError("An edge class cannot extend a directed and an undirected edge class "
					+ "(but this occurs for " + getIdentNode()
					+ " with " + extendEdge.toStringWithDeclarationCoords()
					+ " and " + extendUEdge.toStringWithDeclarationCoords() + ")");
			return false;
		}
		if((type instanceof ArbitraryEdgeTypeNode) && extendEdge!=null) {
			reportError("An arbitrary edge class cannot extend a directed edge class "
					+ "(but this occurs for " + getIdentNode()
					+ " with " + extendEdge.toStringWithDeclarationCoords() + ")");
			return false;
		}
		if(type instanceof ArbitraryEdgeTypeNode && extendUEdge!=null) {
			reportError("An arbitrary edge class cannot extend an undirected edge class "
					+ "(but this occurs for " + getIdentNode()
					+ " with " + extendUEdge.toStringWithDeclarationCoords() + ")");
			return false;
		}
		if((type instanceof UndirectedEdgeTypeNode) && extendEdge!=null) {
			reportError("An undirected edge class cannot extend a directed edge class "
					+ "(but this occurs for " + getIdentNode()
					+ " with " + extendEdge.toStringWithDeclarationCoords() + ")");
			return false;
		}
		if(type instanceof DirectedEdgeTypeNode && extendUEdge!=null) {
			reportError("A directed edge class cannot extend an undirected edge class "
					+ "(but this occurs for " + getIdentNode()
					+ " with " + extendUEdge.toStringWithDeclarationCoords() + ")");
			return false;
		}

		return true;
	}

	private static InheritanceTypeNode extendsEdge(EdgeTypeNode edgeType)
	{
		for(InheritanceTypeNode inh : edgeType.getDirectSuperTypes()) {
			if(inh instanceof DirectedEdgeTypeNode) {
				return inh;
			}
		}
		return null;
	}
	
	private static InheritanceTypeNode extendsUEdge(EdgeTypeNode edgeType)
	{
		for(InheritanceTypeNode inh : edgeType.getDirectSuperTypes()) {
			if(inh instanceof UndirectedEdgeTypeNode) {
				return inh;
			}
		}
		return null;
	}
	
	/**
	 * A type declaration returns the declared type
	 * as result.
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		return getDeclType().getIR();
	}

	public static String getKindStr()
	{
		return "type";
	}

	@Override
	public DeclaredTypeNode getDeclType()
	{
		assert isResolved();

		return type;
	}
}
