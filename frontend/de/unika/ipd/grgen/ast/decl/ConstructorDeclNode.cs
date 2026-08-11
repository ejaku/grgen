/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Moritz Kroll
/// </summary>

namespace de.unika.ipd.grgen.ast.decl
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using ConstructorParamNode = de.unika.ipd.grgen.ast.ConstructorParamNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using ConstructorTypeNode = de.unika.ipd.grgen.ast.type.ConstructorTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using Constructor = de.unika.ipd.grgen.ir.Constructor;
using ConstructorParam = de.unika.ipd.grgen.ir.ConstructorParam;
using IR = de.unika.ipd.grgen.ir.IR;

/// <summary>
/// A compound type constructor declaration.
/// </summary>
public class ConstructorDeclNode : DeclNode
{
	static ConstructorDeclNode()
	{
		SetClassName(typeof(ConstructorDeclNode), "constructor declaration");
	}

	private static readonly TypeNode constructorType = new ConstructorTypeNode();

	private CollectNode<ConstructorParamNode> parameters;

	public ConstructorDeclNode(IdentNode n, CollectNode<ConstructorParamNode> @params)
		: base(n, constructorType)
	{

		parameters = BecomeParent(@params);
	}

	public override TypeNode DeclType
	{
		get
		{
			return constructorType;
		}
	}

	protected internal override bool CheckLocal()
	{
		return true; // nothing to be checked locally
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(parameters);
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			childrenNames.Add("parameters");
			return childrenNames;
		}
	}

	public virtual CollectNode<ConstructorParamNode> Parameters
	{
		get
		{
			return parameters;
		}
	}

	protected internal override bool ResolveLocal()
	{
		return true; // nothing to be resolved locally
	}

	public static string KindStr
	{
		get
		{
			return "constructor";
		}
	}

	public virtual Constructor IRConstructor
	{
		get
		{
			return CheckIR(typeof(Constructor));
		}
	}

	protected internal override IR ConstructIR()
	{
		LinkedHashSet<ConstructorParam> @params = new LinkedHashSet<ConstructorParam>();
		foreach(ConstructorParamNode param in parameters.ChildrenExact)
			@params.Add(param.CheckIR(typeof(ConstructorParam)));

		return new Constructor(@params);
	}
}

}
