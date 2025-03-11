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
import java.util.HashSet;
import java.util.Vector;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.expr.graph.FromIndexAccessFromToPartExprNode;
import de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
import de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
import de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
import de.unika.ipd.grgen.ast.type.TypeExprNode;
import de.unika.ipd.grgen.ast.type.TypeNode;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.pattern.Node;

public class MatchNodeByIndexAccessMultipleDeclNode extends NodeDeclNode
{
	static {
		setName(MatchNodeByIndexAccessMultipleDeclNode.class, "match node by index access multiple decl");
	}

	protected CollectNode<MatchNodeByIndexAccessOrderingPartNode> indexAccessParts = new CollectNode<MatchNodeByIndexAccessOrderingPartNode>();

	public MatchNodeByIndexAccessMultipleDeclNode(IdentNode id, BaseNode type, int context,
			PatternGraphLhsNode directlyNestingLHSGraph)
	{
		super(id, type, CopyKind.None, context, TypeExprNode.getEmpty(), directlyNestingLHSGraph);
	}

	public void addIndexAccessPart(MatchNodeByIndexAccessOrderingPartNode expr)
	{
		indexAccessParts.addChild(expr);
	}

	/** returns children of this node */
	@Override
	public Collection<BaseNode> getChildren()
	{
		Vector<BaseNode> children = new Vector<BaseNode>();
		children.add(ident);
		children.add(getValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.add(constraints);
		children.add(indexAccessParts);
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
		childrenNames.add("indexAccessParts");
		return childrenNames;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#resolveLocal() */
	@Override
	protected boolean resolveLocal()
	{
		boolean successfullyResolved = super.resolveLocal();
		return successfullyResolved;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#checkLocal() */
	@Override
	protected boolean checkLocal()
	{
		boolean res = super.checkLocal();
		
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS) {
			reportError("Cannot employ match node by index multiple in the rewrite part"
					+ " (as it occurs in match node" + emptyWhenAnonymousPostfix(" ") + " by multiple index access).");
			res = false;
		}
		
		TypeNode expectedEntityType = getDeclType();
		for(MatchNodeByIndexAccessOrderingPartNode indexAccessPart : indexAccessParts.getChildren()) {
			InheritanceTypeNode entityType = indexAccessPart.index.getType();
			if(!entityType.isCompatibleTo(expectedEntityType) && !expectedEntityType.isCompatibleTo(entityType)) {
				res = false; // the index type is checked with the parts, and an error is emitted there - we just skip the warning messages here in case of an index type mismatch
			}
		}
		
		if(!res)
			return false;
		
		for(int i = 0; i < indexAccessParts.getChildren().size(); ++i) {
			MatchNodeByIndexAccessOrderingPartNode indexAccessPart = indexAccessParts.get(i);
			InheritanceTypeNode entityType = indexAccessPart.index.getType();

			for(int j = i + 1; j < indexAccessParts.getChildren().size(); ++j) {
				MatchNodeByIndexAccessOrderingPartNode indexAccessPart2 = indexAccessParts.get(j);
				InheritanceTypeNode entityType2 = indexAccessPart2.index.getType();
				
				if(!InheritanceTypeNode.hasCommonSubtype(entityType, entityType2)) {
					reportWarning("The indexed type " + entityType.toStringWithDeclarationCoords()
									+ " and the indexed type " + entityType2.toStringWithDeclarationCoords()
									+ " have no common subtype, thus the content of these indices is disjoint, and the index join will always be empty.");
				}
			}
		}
		
		HashSet<IndexDeclNode> indicesUsed = new HashSet<IndexDeclNode>();
		for(MatchNodeByIndexAccessOrderingPartNode indexAccessPart : indexAccessParts.getChildren()) {
			if(indicesUsed.contains(indexAccessPart.index)) {
				reportWarning("The match node by index multiple uses the index " + indexAccessPart.index.toStringWithDeclarationCoords()
						+ " for another time (combine the queried ranges into one).");
			} else {
				indicesUsed.add(indexAccessPart.index);
			}
		}
		
		return res;
	}

	/** @see de.unika.ipd.grgen.ast.BaseNode#constructIR() */
	@Override
	protected IR constructIR()
	{
		if(isIRAlreadySet()) { // break endless recursion in case of cycle in usage
			return getIR();
		}

		Node node = (Node)super.constructIR();

		setIR(node);

		for(MatchNodeByIndexAccessOrderingPartNode partNode : indexAccessParts.getChildren()) {
			node.addIndex(partNode.constructIRPart());
		}
		return node;
	}
}
