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
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using NameOrAttributeInitializationNode = de.unika.ipd.grgen.ast.pattern.NameOrAttributeInitializationNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using Checker = de.unika.ipd.grgen.ast.util.Checker;
using de.unika.ipd.grgen.ast.util;
using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
using IR = de.unika.ipd.grgen.ir.IR;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
using RetypedEdge = de.unika.ipd.grgen.ir.pattern.RetypedEdge;

/// <summary>
/// An edge which is created by retyping, with the old edge
/// </summary>
public class EdgeTypeChangeDeclNode : EdgeDeclNode
{
	static EdgeTypeChangeDeclNode()
	{
		SetClassName(typeof(EdgeTypeChangeDeclNode), "edge type change decl");
	}

	private BaseNode oldUnresolved;
	private EdgeDeclNode old = null;

	public EdgeTypeChangeDeclNode(IdentNode id, BaseNode newType, int context, BaseNode oldid,
			PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, newType, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
		this.oldUnresolved = oldid;
		BecomeParent(this.oldUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
		IList<BaseNode> children = new List<BaseNode>();
		children.Add(ident);
		children.Add(GetValidVersion(typeUnresolved, typeEdgeDecl, typeTypeDecl));
		children.Add(constraints);
		children.Add(GetValidVersion(oldUnresolved, old));
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
		return childrenNames;
		}
	}

	private static readonly DeclarationResolver<EdgeDeclNode> edgeResolver =
			new DeclarationResolver<EdgeDeclNode>(typeof(EdgeDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();

		old = edgeResolver.Resolve(oldUnresolved, this);
		if(old != null)
			old.retypedElem = this;

		return successfullyResolved && old != null;
	}

	/// <returns> the original edge for this retyped edge </returns>
	public EdgeDeclNode OldEdge
	{
		get
		{
		Debug.Assert(IsResolved());

		return old;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		Checker edgeChecker = new TypeChecker(typeof(EdgeTypeNode));
		bool res = base.CheckLocal() & edgeChecker.Check(old, error);
		if(!res)
			return false;

		if(nameOrAttributeInits.Size() > 0)
		{
			NameOrAttributeInitializationNode nameOrAttributeInit = nameOrAttributeInits.Get(0);
			if(nameOrAttributeInit.attributeUnresolved != null)
			{
				ReportError("An attribute initialization is not allowed for a retyped edge"
						+ " (but occurs for " + nameOrAttributeInit.attributeUnresolved + EmptyWhenAnonymousPostfix(" of ") + ").");
			}
			else
				ReportError("A name initialization ($=) is not allowed for a retyped edge" + EmptyWhenAnonymous(" (but occurs for " + Ident + ")."));
			return false;
		}

		// check if source edge of retype is declared in the rewrite part - no retype of just created edge
		if((old.context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS
			&& !old.defEntityToBeYieldedTo)
		{
			ReportError("The original edge of the retyping may not be declared in the rewrite part"
					+ " (this is violated by the original edge " + old.Ident + EmptyWhenAnonymousPostfix(" of ") + ").");
			res = false;
		}

		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		EdgeTypeNode etn = DeclEdgeType;
		EdgeType et = etn.IREdgeType;
		IdentNode ident = Ident;

		RetypedEdge res = new RetypedEdge(ident.IRIdent, et, ident.Annotations,
				IsMaybeDeleted(), IsMaybeRetyped(), false, context);

		Edge oldEdge = old.IREdge;
		res.OldEdge = oldEdge;

		if(InheritsType())
		{
			Debug.Assert(copyKind == CopyKind.None);
			res.SetTypeofCopy(typeEdgeDecl.CheckIR(typeof(Edge)), copyKind);
		}

		return res;
	}
}

}
