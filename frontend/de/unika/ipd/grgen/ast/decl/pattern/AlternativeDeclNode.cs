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
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using AlternativeTypeNode = de.unika.ipd.grgen.ast.type.AlternativeTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Alternative = de.unika.ipd.grgen.ir.pattern.Alternative;

/// <summary>
/// AST node that represents an alternative, containing the alternative graph patterns
/// </summary>
public class AlternativeDeclNode : DeclNode
{
	static AlternativeDeclNode()
	{
		SetClassName(typeof(AlternativeDeclNode), "alternative");
	}

	/// <summary>
	/// Type for this declaration. </summary>
	private static AlternativeTypeNode alternativeType = new AlternativeTypeNode();

	private IList<AlternativeCaseDeclNode> children = new List<AlternativeCaseDeclNode>();

	public AlternativeDeclNode(IdentNode id)
		: base(id, alternativeType)
	{
	}

	public virtual void AddChild(AlternativeCaseDeclNode n)
	{
		Debug.Assert((!IsResolved()));
		BecomeParent(n);
		children.Add(n);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			return new List<BaseNode>(children);
		}
	}

	public virtual ICollection<AlternativeCaseDeclNode> ChildrenExact
	{
		get
		{
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
			// nameless children
			return childrenNames;
		}
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
	protected internal override bool CheckLocal()
	{
		if(children.Count == 0)
		{
			this.ReportError("The alternative pattern is empty.");
			return false;
		}

		return true;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
	protected internal override IR ConstructIR()
	{
		Alternative alternative = new Alternative(ident.IRIdent);
		foreach(AlternativeCaseDeclNode alternativeCaseNode in children)
		{
			Rule alternativeCaseRule = alternativeCaseNode.CheckIR(typeof(Rule));
			alternative.AddAlternativeCase(alternativeCaseRule);
		}
		return alternative;
	}

	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return alternativeType;
		}
	}
}

}
