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
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Expression = de.unika.ipd.grgen.ir.expr.Expression;
using Index = de.unika.ipd.grgen.ir.model.Index;
using IndexAccessEquality = de.unika.ipd.grgen.ir.pattern.IndexAccessEquality;
using Node = de.unika.ipd.grgen.ir.pattern.Node;

public class MatchNodeByIndexAccessEqualityDeclNode : MatchNodeByIndexDeclNode
{
	static MatchNodeByIndexAccessEqualityDeclNode()
	{
		SetClassName(typeof(MatchNodeByIndexAccessEqualityDeclNode), "match node by index access equality decl");
	}

	private ExprNode expr;

	public MatchNodeByIndexAccessEqualityDeclNode(IdentNode id, BaseNode type, int context,
			IdentNode index, ExprNode expr, PatternGraphLhsNode directlyNestingLHSGraph)
		: base(id, type, context, index, directlyNestingLHSGraph)
	{
		this.expr = expr;
		BecomeParent(this.expr);
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
		children.Add(GetValidVersion(indexUnresolved, index));
		children.Add(expr);
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
		childrenNames.Add("index");
		childrenNames.Add("expression");
		return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		successfullyResolved &= expr.Resolve();
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		bool res = base.CheckLocal();
		TypeNode expectedIndexAccessType = index.ExpectedAccessType;
		TypeNode indexAccessType = expr.Type;
		if(!indexAccessType.IsCompatibleTo(expectedIndexAccessType))
		{
			string expTypeName = expectedIndexAccessType.TypeName;
			string typeName = indexAccessType.TypeName;
			ident.ReportError("Cannot convert type used in accessing index from " + typeName
					+ " to the expected " + expTypeName
					+ " (in match node" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		TypeNode expectedEntityType = DeclType;
		InheritanceTypeNode entityType = index.Type;
		if(!entityType.IsCompatibleTo(expectedEntityType) && !expectedEntityType.IsCompatibleTo(entityType))
		{
			string expTypeName = expectedEntityType.ToStringWithDeclarationCoords();
			string typeName = entityType.ToStringWithDeclarationCoords();
			ident.ReportError("Cannot convert index type from " + typeName
					+ " to the expected pattern element type " + expTypeName
					+ " (in match node" + EmptyWhenAnonymousPostfix(" ") + " by index access of " + index.ToStringWithDeclarationCoords() + ").");
			return false;
		}
		return res;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet()) // break endless recursion in case of cycle in usage
			return IR;

		Node node = (Node)base.ConstructIR();

		IR = node;

		expr = expr.Evaluate();
		node.Index = new IndexAccessEquality(index.CheckIR(typeof(Index)), expr.CheckIR(typeof(Expression)));
		return node;
	}
}

}
