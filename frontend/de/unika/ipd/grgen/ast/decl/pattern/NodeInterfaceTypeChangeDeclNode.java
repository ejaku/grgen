/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.pattern.Node;

public class NodeInterfaceTypeChangeDeclNode extends NodeDeclNode
{
	static {
		setName(NodeInterfaceTypeChangeDeclNode.class, "node interface type change decl");
	}

	private IdentNode interfaceTypeUnresolved;
	public TypeDeclNode interfaceType = null;

	public NodeInterfaceTypeChangeDeclNode(IdentNode id, BaseNode type, int context, IdentNode interfaceType,
			PatternGraphLhsNode directlyNestingLHSGraph, boolean maybeNull)
	{
		super(id, type, CopyKind.None, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph, maybeNull, false);
		this.interfaceTypeUnresolved = interfaceType;
		becomeParent(this.interfaceTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
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
		if(!(interfaceType.getDeclType() instanceof NodeTypeNode)) {
			interfaceTypeUnresolved.reportError("The interface type of node parameter " + getIdentNode() + " must be a node type"
					+ " (given is " + interfaceType.getDeclType().getKind() + " " + interfaceType.getDeclType().getTypeName()
					+ " - use -edge-> syntax for edges, var for variables, ref for containers).");
			return false;
		}
		if(!successfullyResolved)
			return false;

		NodeTypeNode interfaceNodeTypeNode = (NodeTypeNode)interfaceType.getDeclType();
		NodeTypeNode nodeTypeNode = (NodeTypeNode)typeTypeDecl.getDeclType();
		if(!nodeTypeNode.isA(interfaceNodeTypeNode)) {
			interfaceTypeUnresolved.reportWarning("The interface type " + interfaceNodeTypeNode.toStringWithDeclarationCoords()
					+ " of node parameter " + ident.toString()
					+ " is not a supertype of " + nodeTypeNode.toStringWithDeclarationCoords() + ".");
		}
		return successfullyResolved;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal() & nodeChecker.check(interfaceType, error);
		if(!res)
			return false;

		return res & onlyPatternNodesCanChangeInterfaceType();
	}

	private boolean onlyPatternNodesCanChangeInterfaceType()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			return true;

		reportError("Rewrite part node parameters cannot change the interface type, only pattern nodes can"
				+ " (this is violated by " + getIdentNode() + ").");
		return false;
	}
	
	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		Node node = (Node)super.constructIR();
		NodeTypeNode ntn = (NodeTypeNode)interfaceType.getDeclType();
		NodeType nt = ntn.getNodeType();
		node.setParameterInterfaceType(nt);
		return node;
	}
}
