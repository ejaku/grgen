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
using NestedMatcherDeclNode = de.unika.ipd.grgen.ast.decl.executable.NestedMatcherDeclNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using AlternativeCaseTypeNode = de.unika.ipd.grgen.ast.type.AlternativeCaseTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using RuleKind = de.unika.ipd.grgen.ir.executable.Rule.RuleKind;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using PatternGraphRhs = de.unika.ipd.grgen.ir.pattern.PatternGraphRhs;
using EvalStatements = de.unika.ipd.grgen.ir.stmt.EvalStatements;

/// <summary>
/// AST node for an alternative case pattern, maybe including replacements.
/// </summary>
public class AlternativeCaseDeclNode : NestedMatcherDeclNode
{
	static AlternativeCaseDeclNode()
	{
		SetClassName(typeof(AlternativeCaseDeclNode), "alternative case");
	}

	private AlternativeCaseTypeNode type;

	/// <summary>
	/// Type for this declaration. </summary>
	private static readonly TypeNode alternativeCaseType = new AlternativeCaseTypeNode();

	/// <summary>
	/// Make a new alternative case rule. </summary>
	/// <param name="id"> The identifier of this rule. </param>
	/// <param name="left"> The left hand side (The pattern to match). </param>
	/// <param name="right"> The right hand side. </param>
	public AlternativeCaseDeclNode(IdentNode id, PatternGraphLhsNode left, RhsDeclNode right)
		 : base(id, alternativeCaseType, left, right)
	{
	}

	/// <summary>
	/// returns children of this node </summary>
	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			children.Add(ident);
			children.Add(GetValidVersion(typeUnresolved, type));
			children.Add(pattern);
			if(right != null)
				children.Add(right);
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
			childrenNames.Add("pattern");
			if(right != null)
				childrenNames.Add("right");
			return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<AlternativeCaseTypeNode> typeResolver =
		new DeclarationTypeResolver<AlternativeCaseTypeNode>(typeof(AlternativeCaseTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		type = typeResolver.Resolve(typeUnresolved, this);

		return type != null;
	}

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
	protected internal override IR ConstructIR()
	{
		// return if the pattern graph already constructed the IR object
		// that may happen in recursive patterns (and other usages/references)
		if(IsIRAlreadySet())
			return IR;

		Rule altCaseRule = new Rule(Ident.IRIdent, Rule.RuleKind.AlternativeCase);

		// mark this node as already visited
		IR = altCaseRule;

		PatternGraphLhs left = pattern.IRPatternGraphLhs;

		PatternGraphRhs rightPattern = null;
		if(right != null)
			rightPattern = right.GetIRPatternGraph(left);

		altCaseRule.Initialize(left, rightPattern);

		ConstructImplicitNegs(left);
		ConstructIRaux(altCaseRule, right);

		// add Eval statements to the IR
		if(right != null)
		{
			foreach(EvalStatements evalStatement in right.RhsGraph.EvalStatements)
				altCaseRule.AddEval(evalStatement);
		}

		return altCaseRule;
	}

	public override TypeNode DeclType
	{
		get
		{
			Debug.Assert(IsResolved());

			return type;
		}
	}

	public static string KindStr
	{
		get
		{
			return "alternative case";
		}
	}
}

}
