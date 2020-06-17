/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack, Adam Szalkowski
 */

package de.unika.ipd.grgen.ast.decl.pattern;

import java.util.Collection;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.util.Checker;
import de.unika.ipd.grgen.ast.util.CollectResolver;
import de.unika.ipd.grgen.ast.util.DeclarationResolver;
import de.unika.ipd.grgen.ast.util.TypeChecker;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.NodeType;
import de.unika.ipd.grgen.ir.pattern.Node;
import de.unika.ipd.grgen.ir.pattern.RetypedNode;

/**
 * A node which is created by retyping, with the old node (old nodes in case of a merge)
 */
public class NodeTypeChangeDeclNode extends NodeDeclNode
{
	static {
		setName(NodeTypeChangeDeclNode.class, "node type change decl");
	}

	private BaseNode oldUnresolved;
	private NodeDeclNode old = null;
	private CollectNode<IdentNode> mergeesUnresolved;
	private CollectNode<NodeDeclNode> mergees;

	public NodeTypeChangeDeclNode(IdentNode id, BaseNode newType, int context, BaseNode oldid,
			CollectNode<IdentNode> mergees, PatternGraphNode directlyNestingLHSGraph)
	{
		super(id, newType, false, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
		this.oldUnresolved = oldid;
		becomeParent(this.oldUnresolved);
		this.mergeesUnresolved = mergees;
		becomeParent(this.mergeesUnresolved);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(getValidVersion(oldUnresolved, old));
		children.add(getValidVersion(mergeesUnresolved, mergees));
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
		childrenNames.add("old");
		childrenNames.add("mergees");
		return childrenNames;
	}

	private static final DeclarationResolver<NodeDeclNode> nodeResolver =
			new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class);
	private static final CollectResolver<NodeDeclNode> mergeesResolver =
			new CollectResolver<NodeDeclNode>(new DeclarationResolver<NodeDeclNode>(NodeDeclNode.class));

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();

		old = nodeResolver.resolve(oldUnresolved, this);
		if(old != null)
			old.retypedElem = this;
		mergees = mergeesResolver.resolve(mergeesUnresolved, this);

		return successfullyResolved && old != null && mergees != null;
	}

	/**
	 * @return the original node for this retyped node
	 */
	public final NodeDeclNode getOldNode()
	{
		assert isResolved();

		return old;
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#checkLocal()
	 */
	@Override
	protected boolean checkLocal()
	{
		Checker nodeChecker = new TypeChecker(NodeTypeNode.class);
		boolean res = super.checkLocal() & nodeChecker.check(old, error);
		if(!res)
			return false;

		if(nameOrAttributeInits.size() > 0) {
			reportError("A name or attribute initialization is not allowed for a retyped node");
			return false;
		}

		// check if source node of retype is declared in replace/modify part - no retype of just created node
		if((old.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS
			&& !old.defEntityToBeYieldedTo) {
			reportError("Source node of retype may not be declared in replace/modify part");
			res = false;
		}

		for(NodeDeclNode mergee : mergees.getChildren()) {
			if((mergee.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS
				&& !mergee.defEntityToBeYieldedTo) {
				reportError("Node of (retype) merge may not be declared in replace/modify part");
				res = false;
			}
		}

		return res;
	}

	@Override
	public Node getNode()
	{
		return checkIR(Node.class);
	}

	/**
	 * @see de.unika.ipd.grgen.ast.BaseNode#constructIR()
	 */
	@Override
	protected IR constructIR()
	{
		NodeTypeNode tn = getDeclType();
		NodeType nt = tn.getNodeType();
		IdentNode ident = getIdentNode();

		RetypedNode res = new RetypedNode(ident.getIdent(), nt, ident.getAnnotations(),
				isMaybeDeleted(), isMaybeRetyped(), false, context);

		Node oldNode = old.getNode();
		res.setOldNode(oldNode);

		if(inheritsType()) {
			assert !isCopy;
			res.setTypeof(typeNodeDecl.checkIR(Node.class), false);
		}

		for(NodeDeclNode mergee : mergees.getChildren()) {
			res.addMergee(mergee.checkIR(Node.class));
		}

		return res;
	}
}
