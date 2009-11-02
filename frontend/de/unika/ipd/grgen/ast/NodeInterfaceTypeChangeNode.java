/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Edgar Jakumeit
 */
package de.unika.ipd.grgen.ast;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Node;
import de.unika.ipd.grgen.ir.NodeType;

/**
 *
 */
public class NodeInterfaceTypeChangeNode extends NodeDeclNode implements NodeCharacter  {
	static {
		setName(NodeTypeChangeNode.class, "node interface type change decl");
	}

	private IdentNode interfaceTypeUnresolved;
	private TypeDeclNode interfaceType = null;

	public NodeInterfaceTypeChangeNode(IdentNode id, BaseNode type, int context, IdentNode interfaceType, PatternGraphNode directlyNestingLHSGraph) {
		super(id, type, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.interfaceTypeUnresolved = interfaceType;
		becomeParent(this.interfaceTypeUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren() {
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(interfaceTypeUnresolved, interfaceType));
		return children;
	}

	/** returns names of the children, same order as in getChildren */
	@Override
	public Collection<String> getChildrenNames() {
		Vector<String> childrenNames = new Vector<String>();
		childrenNames.add("ident");
		childrenNames.add("type");
		childrenNames.add("constraints");
		childrenNames.add("interfaceType");
		return childrenNames;
	}

	private static final DeclarationResolver<TypeDeclNode> typeResolver = new DeclarationResolver<TypeDeclNode>(TypeDeclNode.class);

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal() {
		boolean successfullyResolved = super.resolveLocal();
		interfaceType = typeResolver.resolve(interfaceTypeUnresolved, this);
		if(interfaceType==null) return false;
		if(!interfaceType.resolve()) return false;
		if(!(interfaceType.getDeclType() instanceof NodeTypeNode)) {
			interfaceTypeUnresolved.reportError("Interface type of node \"" + getIdentNode() + "\" must be a node type");
			return false;
		}
		NodeTypeNode interfaceNodeTypeNode = (NodeTypeNode)interfaceType.getDeclType();
		NodeTypeNode nodeTypeNode = (NodeTypeNode)typeTypeDecl.getDeclType();
		if(!nodeTypeNode.isA(interfaceNodeTypeNode)) {
			interfaceTypeUnresolved.reportWarning("parameter interface type of "+ident.toString()+" is not supertype of parameter type");
		}
		return successfullyResolved;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal() {
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal()
			& nodeChecker.check(interfaceType, error);
		if (!res) {
			return false;
		}

		return res & onlyPatternNodesCanChangeInterfaceType();
	}

	private boolean onlyPatternNodesCanChangeInterfaceType() {
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS) {
			return true;
		}

		constraints.reportError("replace nodes can't change interface type, only pattern nodes can");
		return false;
	}

	@Override
	public Node getNode() {
		return checkIR(Node.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR() {
		Node node = (Node)super.constructIR();
		NodeTypeNode ntn = (NodeTypeNode)interfaceType.getDeclType();
		NodeType nt = ntn.getNodeType();
		node.setParameterInterfaceType(nt);
		return node;
	}
}

