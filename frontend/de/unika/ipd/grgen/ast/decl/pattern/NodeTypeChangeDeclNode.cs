/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack, Adam Szalkowski
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
using NameOrAttributeInitializationNode = de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using Checker = de.unika.ipd.grgen.ast.util.Checker;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
using IR = de.unika.ipd.grgen.ir.IR;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;
using Node = de.unika.ipd.grgen.ir.pattern.Node;
using RetypedNode = de.unika.ipd.grgen.ir.pattern.RetypedNode;

/// <summary>
/// A node which is created by retyping, with the old node (old nodes in case of a merge)
/// </summary>
public class NodeTypeChangeDeclNode : NodeDeclNode
{
	static NodeTypeChangeDeclNode()
	{
		SetClassName(typeof(NodeTypeChangeDeclNode), "node type change decl");
	}

	private BaseNode oldUnresolved;
	private NodeDeclNode old = null;
	private CollectNode<IdentNode> mergeesUnresolved;
	private CollectNode<NodeDeclNode> mergees;

	public NodeTypeChangeDeclNode(IdentNode id, BaseNode newType, int context, BaseNode oldid,
			CollectNode<IdentNode> mergees, PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, newType, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
		this.oldUnresolved = oldid;
		BecomeParent(this.oldUnresolved);
		this.mergeesUnresolved = mergees;
		BecomeParent(this.mergeesUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ident);
		children.Add(GetValidVersion(typeUnresolved, typeNodeDecl, typeTypeDecl));
		children.Add(constraints);
		children.Add(GetValidVersion(oldUnresolved, old));
		children.Add(GetValidVersionCollectNode(mergeesUnresolved, mergees));
		return children;
		}
	}

	/// <summary>
	/// returns names of the children, same order as in getChildren </summary>
	public override ICollection<string> ChildrenNames
	{
		get
		{
		IList<string> childrenNames = new List<string>();
		childrenNames.Add("ident");
		childrenNames.Add("type");
		childrenNames.Add("constraints");
		childrenNames.Add("old");
		childrenNames.Add("mergees");
		return childrenNames;
		}
	}

	private static readonly DeclarationResolver<NodeDeclNode> nodeResolver =
			new DeclarationResolver<NodeDeclNode>(typeof(NodeDeclNode));
	private static readonly CollectResolver<NodeDeclNode> mergeesResolver =
			new CollectResolver<NodeDeclNode>(new DeclarationResolver<NodeDeclNode>(typeof(NodeDeclNode)));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();

		old = nodeResolver.Resolve(oldUnresolved, this);
		if(old != null)
			old.retypedElem = this;
		mergees = mergeesResolver.Resolve(mergeesUnresolved, this);

		return successfullyResolved && old != null && mergees != null;
	}

	/// <returns> the original node for this retyped node </returns>
	public NodeDeclNode OldNode
	{
		get
		{
		Debug.Assert(IsResolved());

		return old;
		}
	}

	/// <returns> the mergees of this (retyped) node </returns>
	public ICollection<NodeDeclNode> Mergees
	{
		get
		{
		Debug.Assert(IsResolved());

		return mergees.ChildrenExact;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		Checker nodeChecker = new TypeChecker(typeof(NodeTypeNode));
		bool res = base.CheckLocal() & nodeChecker.Check(old, error);
		if(!res)
			return false;

		if(nameOrAttributeInits.Size() > 0)
		{
			NameOrAttributeInitializationNode nameOrAttributeInit = nameOrAttributeInits.Get(0);
			if(nameOrAttributeInit.attributeUnresolved != null)
			{
				ReportError("An attribute initialization is not allowed for a retyped node"
						+ " (but occurs for " + nameOrAttributeInit.attributeUnresolved + EmptyWhenAnonymousPostfix(" of ") + ").");
			}
			else
				ReportError("A name initialization ($=) is not allowed for a retyped node" + EmptyWhenAnonymous(" (but occurs for " + Ident + ")."));
			return false;
		}

		// check if source node of retype is declared in the rewrite part - no retype of just created node
		if((old.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS
			&& !old.defEntityToBeYieldedTo)
		{
			ReportError("The original node of the retyping may not be declared in the rewrite part"
					+ " (this is violated by the original node " + old.Ident + EmptyWhenAnonymousPostfix(" of ") + ").");
			res = false;
		}

		foreach(NodeDeclNode mergee in mergees.ChildrenExact)
		{
			if((mergee.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS
				&& !mergee.defEntityToBeYieldedTo)
			{
				ReportError("An original node of a (retyping) merge may not be declared in the rewrite part"
						+ " (this is violated by the original node " + mergee.Ident + EmptyWhenAnonymousPostfix(" of ") + ").");
				res = false;
			}
		}

		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		NodeTypeNode tn = DeclNodeType;
		NodeType nt = tn.IRNodeType;
		IdentNode ident = Ident;

		RetypedNode res = new RetypedNode(ident.IRIdent, nt, ident.Annotations,
				IsMaybeDeleted(), IsMaybeRetyped(), false, context);

		Node oldNode = old.IRNode;
		res.OldNode = oldNode;

		if(InheritsType())
		{
			Debug.Assert(copyKind == CopyKind.None);
			res.SetTypeofCopy(typeNodeDecl.CheckIR(typeof(Node)), copyKind);
		}

		foreach(NodeDeclNode mergee in mergees.ChildrenExact)
			res.AddMergee(mergee.CheckIR(typeof(Node)));

		return res;
	}
}

}
