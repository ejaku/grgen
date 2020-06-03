/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.decl.TypeDeclNode;
import de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
import de.unika.ipd.grgen.ast.pattern.EdgeCharacter;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.pattern.Edge;

public class EdgeInterfaceTypeChangeDeclNode extends EdgeDeclNode implements EdgeCharacter
{
	static {
		setName(EdgeInterfaceTypeChangeDeclNode.class, "edge interface type change decl");
	}

	private IdentNode interfaceTypeUnresolved;
	public TypeDeclNode interfaceType = null;

	public EdgeInterfaceTypeChangeDeclNode(IdentNode id, BaseNode newType, int context, IdentNode interfaceType,
			PatternGraphNode directlyNestingLHSGraph, boolean maybeNull)
	{
		super(id, newType, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, maybeNull, false);
		this.interfaceTypeUnresolved = interfaceType;
		becomeParent(this.interfaceTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(interfaceTypeUnresolved, interfaceType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames()
	{
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("interfaceType");
		return childrenNames;
	}

	private static final DeclarationResolver<TypeDeclNode> typeResolver =
			new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		interfaceType = typeResolver.resolve(interfaceTypeUnresolved, this);
		if(interfaceType == null)
			return false;
		if(!interfaceType.resolve())
			return false;
		if(!(interfaceType.getDeclType() instanceof EdgeTypeNode)) {
			interfaceTypeUnresolved.reportError("Interface type of edge \"" + getIdentNode() + "\" must be an edge type"
					+ "(not " + interfaceType.getDeclType().getTypeName() + ")");
			return false;
		}
		if(!successfullyResolved)
			return false;

		EdgeTypeNode interfaceEdgeTypeNode = (EdgeTypeNode)interfaceType.getDeclType();
		EdgeTypeNode edgeTypeNode = (EdgeTypeNode)typeTypeDecl.getDeclType();
		if(!edgeTypeNode.isA(interfaceEdgeTypeNode)) {
			interfaceTypeUnresolved.reportWarning("parameter interface type of " + ident.toString()
					+ " is not supertype of parameter type");
		}
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		Checker edgeChecker = new TypeChecker(EdgeTypeNode.class);
		boolean res = super.checkLocal() & edgeChecker.check(interfaceType, error);
		if(!res)
			return false;

		return res & onlyPatternEdgesCanChangeInterfaceType();
	}

	private boolean onlyPatternEdgesCanChangeInterfaceType()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			return true;

		constraints.reportError("replace edges can't change interface type, only pattern edges can");
		return false;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		Edge edge = (Edge)super.constructIR();
		EdgeTypeNode etn = (EdgeTypeNode)interfaceType.getDeclType();
		EdgeType et = etn.getEdgeType();
		edge.setParameterInterfaceType(et);
		return edge;
	}
}