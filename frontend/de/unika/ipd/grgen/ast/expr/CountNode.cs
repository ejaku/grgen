/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr
{

using System.Collections.Generic;

using de.unika.ipd.grgen.ast;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using Count = de.unika.ipd.grgen.ir.expr.Count;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// A node yielding the count of instances of an iterated pattern.
/// </summary>
public class CountNode : ExprNode
{
	static CountNode()
	{
		SetClassName(typeof(CountNode), "count");
	}

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	public CountNode(Coords coords, IdentNode iterated)
		: base(coords)
	{
		this.iteratedUnresolved = iterated;
		BecomeParent(this.iteratedUnresolved);
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(GetValidVersion(iteratedUnresolved, iterated));
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
			childrenNames.Add("iterated");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool res = FixupDefinition(iteratedUnresolved, iteratedUnresolved.Scope);

		iterated = iteratedResolver.Resolve(iteratedUnresolved, this);

		return res && iterated != null;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal()"/>
	protected internal override bool CheckLocal()
	{
		return true;
	}

	protected internal override IR ConstructIR()
	{
		return new Count(iterated.CheckIR(typeof(Rule)), Type.IRType);
	}

	public override TypeNode Type
	{
		get
		{
			return BasicTypeNode.intType;
		}
	}

	public override bool NoIteratedReference(string containingConstruct)
	{
		ReportError("The matches of an iterated cannot be accessed with a count(" + iteratedUnresolved + ")"
				+ " from a " + containingConstruct + ", only from a yield block or yield expression or eval.");
		return false;
	}

	public override bool IteratedNotReferenced(string iterName)
	{
		if(iterated.Ident.ToString().Equals(iterName))
		{
			ReportError("A count of iterated matches cannot access an iterated it is contained in, as it occurs with count(" + iteratedUnresolved + ").");
			return false;
		}
		return true;
	}
}

}
