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
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using TypeExprNode = de.unika.ipd.grgen.ast.type.TypeExprNode;
using Checker = de.unika.ipd.grgen.ast.util.Checker;
using de.unika.ipd.grgen.ast.util;
using TypeChecker = de.unika.ipd.grgen.ast.util.TypeChecker;
using IR = de.unika.ipd.grgen.ir.IR;
using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using Edge = de.unika.ipd.grgen.ir.pattern.Edge;

public class EdgeInterfaceTypeChangeDeclNode : EdgeDeclNode
{
	static EdgeInterfaceTypeChangeDeclNode()
	{
		SetClassName(typeof(EdgeInterfaceTypeChangeDeclNode), "edge interface type change decl");
	}

	private IdentNode interfaceTypeUnresolved;
	public TypeDeclNode interfaceType = null;

	public EdgeInterfaceTypeChangeDeclNode(IdentNode id, BaseNode newType, int context, IdentNode interfaceType,
			PatternGraphLhsNode directlyNestingLHSGraph, bool maybeNull)
		: base(id, newType, CopyKind.None, context, TypeExprNode.Empty, directlyNestingLHSGraph, maybeNull, false)
	{
		this.interfaceTypeUnresolved = interfaceType;
		BecomeParent(this.interfaceTypeUnresolved);
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
		children.Add(GetValidVersion(interfaceTypeUnresolved, interfaceType));
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
		childrenNames.Add("interfaceType");
		return childrenNames;
		}
	}

	private new static readonly DeclarationResolver<TypeDeclNode> typeResolver =
			new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool successfullyResolved = base.ResolveLocal();
		interfaceType = typeResolver.Resolve(interfaceTypeUnresolved, this);
		if(interfaceType == null)
			return false;
		if(!interfaceType.Resolve())
			return false;
		if(!(interfaceType.DeclType is EdgeTypeNode))
		{
			interfaceTypeUnresolved.ReportError("The interface type of edge parameter " + Ident + " must be an edge type"
					+ " (given is " + interfaceType.DeclType.Kind + " " + interfaceType.DeclType.TypeName + ").");
			return false;
		}
		if(!successfullyResolved)
			return false;

		EdgeTypeNode interfaceEdgeTypeNode = (EdgeTypeNode)interfaceType.DeclType;
		EdgeTypeNode edgeTypeNode = (EdgeTypeNode)typeTypeDecl.DeclType;
		if(!edgeTypeNode.IsA(interfaceEdgeTypeNode))
		{
			interfaceTypeUnresolved.ReportWarning("The interface type " + interfaceEdgeTypeNode.ToStringWithDeclarationCoords()
					+ " of edge parameter " + ident.ToString()
					+ " is not a supertype of " + edgeTypeNode.ToStringWithDeclarationCoords() + ".");
		}
		return successfullyResolved;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		Checker edgeChecker = new TypeChecker(typeof(EdgeTypeNode));
		bool res = base.CheckLocal() & edgeChecker.Check(interfaceType, error);
		if(!res)
			return false;

		return res & OnlyPatternEdgesCanChangeInterfaceType();
	}

	private bool OnlyPatternEdgesCanChangeInterfaceType()
	{
		if((context & CONTEXT_LHS_OR_RHS) == CONTEXT_LHS)
			return true;

		ReportError("Rewrite part edge parameters cannot change the interface type, only pattern edges can"
				+ " (this is violated by " + Ident + ").");
		return false;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		Edge edge = (Edge)base.ConstructIR();
		EdgeTypeNode etn = (EdgeTypeNode)interfaceType.DeclType;
		EdgeType et = etn.IREdgeType;
		edge.ParameterInterfaceType = et;
		return edge;
	}
}

}
