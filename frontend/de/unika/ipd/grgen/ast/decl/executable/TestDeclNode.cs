/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast.decl.executable
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using de.unika.ipd.grgen.ast;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using VarDeclNode = de.unika.ipd.grgen.ast.decl.pattern.VarDeclNode;
using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
using TestTypeNode = de.unika.ipd.grgen.ast.type.executable.TestTypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using RuleKind = de.unika.ipd.grgen.ir.executable.Rule.RuleKind;
using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;

/// <summary>
/// AST node class representing tests
/// </summary>
public class TestDeclNode : ActionDeclNode
{
	static TestDeclNode()
	{
		SetClassName(typeof(TestDeclNode), "test declaration");
	}

	/// <summary>
	/// Type for this declaration. </summary>
	private TestTypeNode type;
	private static readonly TypeNode testType = new TestTypeNode();


	public TestDeclNode(IdentNode id, PatternGraphLhsNode pattern,
			CollectNode<IdentNode> implementedMatchTypes, CollectNode<BaseNode> rets)
		: base(id, testType, pattern, implementedMatchTypes, rets)
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
		children.Add(GetValidVersionCollectNode(returnFormalParametersUnresolved, returnFormalParameters));
		children.Add(pattern);
		children.Add(GetValidVersionCollectNode(implementedMatchTypesUnresolved, implementedMatchTypes));
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
		childrenNames.Add("ret");
		childrenNames.Add("pattern");
		childrenNames.Add("implementedMatchTypes");
		return childrenNames;
		}
	}

	private static readonly DeclarationTypeResolver<TestTypeNode> typeResolver =
			new DeclarationTypeResolver<TestTypeNode>(typeof(TestTypeNode));

	/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
	protected internal override bool ResolveLocal()
	{
		bool matchAndReturnTypesAreOk = base.ResolveLocal();

		type = typeResolver.Resolve(typeUnresolved, this);

		return matchAndReturnTypesAreOk
				& type != null;
	}

	protected internal override bool CheckLocal()
	{
		bool leftHandGraphsOk = base.CheckLocal();

		bool noRewriteParts = SameNumberOfRewriteParts(null, "test");

		return leftHandGraphsOk
				& noRewriteParts
				& CheckReturns(pattern.returns);
	}

	public virtual bool CheckControlFlow()
	{
		return true;
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
		return "test";
		}
	}

	protected internal override IR ConstructIR()
	{
		// return if the pattern graph already constructed the IR object
		// that may happen in recursive patterns (and other usages/references)
		if(IsIRAlreadySet())
			return IR;

		Rule testRule = new Rule(Ident.IRIdent, Rule.RuleKind.Test);

		// mark this node as already visited
		IR = testRule;

		PatternGraphLhs left = pattern.IRPatternGraphLhs;
		foreach(DeclNode varCand in pattern.ParamDecls)
		{
			if(!(varCand is VarDeclNode))
				continue;
			VarDeclNode var = (VarDeclNode)varCand;
			left.AddVariable(var.CheckIR(typeof(Variable)));
		}

		testRule.Initialize(left, null);

		foreach(DefinedMatchTypeNode implementedMatchClassNode in implementedMatchTypes.ChildrenExact)
		{
			DefinedMatchType implementedMatchClass = implementedMatchClassNode.CheckIR(typeof(DefinedMatchType));
			testRule.AddImplementedMatchClass(implementedMatchClass);
		}

		ConstructImplicitNegs(left);
		ConstructIRaux(testRule, pattern.returns);

		return testRule;
	}
}

}
