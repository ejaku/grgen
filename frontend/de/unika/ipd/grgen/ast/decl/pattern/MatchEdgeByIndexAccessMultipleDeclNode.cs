/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.decl.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;

public class MatchEdgeByIndexAccessMultipleDeclNode : EdgeDeclNode
{
	static MatchEdgeByIndexAccessMultipleDeclNode()
	{
		SetClassName(typeof(MatchEdgeByIndexAccessMultipleDeclNode), "match edge by index access multiple decl");
	}

	protected internal CollectNode<MatchByIndexAccessOrderingPartNode> indexAccessParts = new CollectNode<MatchByIndexAccessOrderingPartNode>();

	public MatchEdgeByIndexAccessMultipleDeclNode(IdentNode id, BaseNode type, int context,
			PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph)
	{
	}

	public virtual void AddIndexAccessPart(MatchByIndexAccessOrderingPartNode expr)
	{
		indexAccessParts.AddChild(expr);
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
		children.Add(indexAccessParts);
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
		childrenNames.Add("indexAccessParts");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();

		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_RHS)
		{
			ReportError("Cannot employ match edge by index multiple in the rewrite part"
					+ " (as it occurs in match edge" + EmptyWhenAnonymousPostfix(" ") + " by multiple index access).");
			res = false;
		}

		TypeNode expectedEntityType = DeclType;
		foreach(MatchByIndexAccessOrderingPartNode indexAccessPart in indexAccessParts.ChildrenExact)
		{
			InheritanceTypeNode entityType = indexAccessPart.index.Type;
			if(!entityType.IsCompatibleTo(expectedEntityType) && !expectedEntityType.IsCompatibleTo(entityType))
				res = false; // the index type is checked with the parts, and an error is emitted there - we just skip the warning messages here in case of an index type mismatch
		}

		if(!res)
			return false;

		for(int i = 0; i < indexAccessParts.ChildrenExact.Count; ++i)
		{
			MatchByIndexAccessOrderingPartNode indexAccessPart = indexAccessParts.Get(i);
			InheritanceTypeNode entityType = indexAccessPart.index.Type;

			for(int j = i + 1; j < indexAccessParts.ChildrenExact.Count; ++j)
			{
				MatchByIndexAccessOrderingPartNode indexAccessPart2 = indexAccessParts.Get(j);
				InheritanceTypeNode entityType2 = indexAccessPart2.index.Type;

				if(!InheritanceTypeNode.HasCommonSubtype(entityType, entityType2))
				{
					ReportWarning("The indexed type " + entityType.ToStringWithDeclarationCoords()
									+ " and the indexed type " + entityType2.ToStringWithDeclarationCoords()
									+ " have no common subtype, thus the content of these indices is disjoint, and the index join will always be empty.");
				}
			}
		}

		HashSet<IndexDeclNode> indicesUsed = new HashSet<IndexDeclNode>();
		foreach(MatchByIndexAccessOrderingPartNode indexAccessPart in indexAccessParts.ChildrenExact)
		{
			if(indicesUsed.Contains(indexAccessPart.index))
			{
				ReportWarning("The match edge by index multiple uses the index " + indexAccessPart.index.ToStringWithDeclarationCoords()
						+ " for another time (combine the queried ranges into one).");
			}
			else
				indicesUsed.Add(indexAccessPart.index);
		}

		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet()) // break endless recursion in case of cycle in usage
			return IR;

		Edge edge = (Edge)base.ConstructIR();

		IR = edge;

		foreach(MatchByIndexAccessOrderingPartNode partNode in indexAccessParts.ChildrenExact)
			edge.AddIndex(partNode.ConstructIRPart());
		return edge;
	}
}

}
