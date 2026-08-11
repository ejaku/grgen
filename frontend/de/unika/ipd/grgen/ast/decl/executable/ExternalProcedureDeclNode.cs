/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{
using System.Collections.Generic;
using System.Diagnostics;

using de.unika.ipd.grgen.ast;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using ExternalProcedureTypeNode = de.unika.ipd.grgen.ast.type.executable.ExternalProcedureTypeNode;
using de.unika.ipd.grgen.ast.util;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using ExternalProcedure = de.unika.ipd.grgen.ir.executable.ExternalProcedure;
using ExternalProcedureMethod = de.unika.ipd.grgen.ir.executable.ExternalProcedureMethod;
using Type = de.unika.ipd.grgen.ir.type.Type;


/// <summary>
/// AST node class representing external procedure declarations
/// </summary>
public class ExternalProcedureDeclNode : ProcedureDeclBaseNode
{
	static ExternalProcedureDeclNode()
	{
		SetClassName(typeof(ExternalProcedureDeclNode), "external procedure declaration");
	}

	protected internal CollectNode<BaseNode> parameterTypesUnresolved;
	protected internal CollectNode<TypeNode> parameterTypesCollectNode;

	internal bool isMethod;

	private static readonly ExternalProcedureTypeNode externalProcedureType = new ExternalProcedureTypeNode();


	public ExternalProcedureDeclNode(IdentNode id, CollectNode<BaseNode> paramTypesUnresolved,
			CollectNode<BaseNode> rets, bool isMethod)
		: base(id, externalProcedureType)
	{
		this.parameterTypesUnresolved = paramTypesUnresolved;
		BecomeParent(this.parameterTypesUnresolved);
		this.resultsUnresolved = rets;
		BecomeParent(this.resultsUnresolved);
		this.isMethod = isMethod;
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersionCollectNode(parameterTypesUnresolved, parameterTypesCollectNode));
			children.Add(GetValidVersionCollectNode(resultsUnresolved, resultTypesCollectNode));
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
			childrenNames.Add("paramTypes");
			childrenNames.Add("ret");
			return childrenNames;
		}
	}

	private static readonly CollectResolver<TypeNode> parametersTypeResolver =
			new CollectResolver<TypeNode>(new DeclarationTypeResolver<TypeNode>(typeof(TypeNode)));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		parameterTypesCollectNode = parametersTypeResolver.Resolve(parameterTypesUnresolved, this);

		parameterTypes = parameterTypesCollectNode.ChildrenAsList;

		return parameterTypesCollectNode != null & base.ResolveLocal();
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return externalProcedureType;
		}
	}

	protected internal override IR ConstructIR()
	{
		ExternalProcedure externalProc = isMethod
				? new ExternalProcedureMethod(Ident.ToString(), Ident.IRIdent)
				: new ExternalProcedure(Ident.ToString(), Ident.IRIdent);
		foreach(TypeNode retType in resultTypesCollectNode.ChildrenExact)
			externalProc.AddReturnType(retType.CheckIR(typeof(Type)));
		foreach(TypeNode param in parameterTypesCollectNode.ChildrenExact)
			externalProc.AddParameterType(param.CheckIR(typeof(Type)));
		return externalProc;
	}

	public static string KindStr
	{
		get
		{
			return "external procedure";
		}
	}
}

}
