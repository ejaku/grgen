/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.type
{

using System.Collections.Generic;
using System.Diagnostics;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
using PackageIdentNode = de.unika.ipd.grgen.ast.PackageIdentNode;
using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
using TopLevelMatcherDeclNode = de.unika.ipd.grgen.ast.decl.executable.TopLevelMatcherDeclNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
using de.unika.ipd.grgen.ast.util;
using IR = de.unika.ipd.grgen.ir.IR;
using Rule = de.unika.ipd.grgen.ir.executable.Rule;
using MatchTypeIterated = de.unika.ipd.grgen.ir.type.MatchTypeIterated;
using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;
using Symbol = de.unika.ipd.grgen.parser.Symbol;
using Occurrence = de.unika.ipd.grgen.parser.Symbol.Occurrence;

public class MatchTypeIteratedNode : MatchTypeNode
{
	static MatchTypeIteratedNode()
	{
		SetClassName(typeof(MatchTypeIteratedNode), "match type iterated");
	}

	private IdentNode topLevelMatcherUnresolved;
	private TopLevelMatcherDeclNode topLevelMatcher;

	private IdentNode iteratedUnresolved;
	private IteratedDeclNode iterated;

	private MatchTypeIteratedNode(IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		topLevelMatcherUnresolved = BecomeParent(topLevelMatcherIdent);
		iteratedUnresolved = BecomeParent(iteratedIdent);
	}

	public static IdentNode DefineMatchType(ParserEnvironment env, IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		string topLevelMatcherString = topLevelMatcherIdent.ToString();
		string iteratedString = iteratedIdent.ToString();
		string matchTypeString = "match<" + topLevelMatcherString + "." + iteratedString + ">";
		IdentNode matchTypeIteratedIdentNode = new IdentNode(
				env.Define(ParserEnvironment.TYPES, matchTypeString, iteratedIdent.Coords));
		MatchTypeIteratedNode matchTypeIteratedNode = new MatchTypeIteratedNode(topLevelMatcherIdent, iteratedIdent);
		TypeDeclNode typeDeclNode = new TypeDeclNode(matchTypeIteratedIdentNode, matchTypeIteratedNode);
		matchTypeIteratedIdentNode.Decl = typeDeclNode;
		return matchTypeIteratedIdentNode;
	}

	public static IdentNode GetMatchTypeIdentNode(ParserEnvironment env, IdentNode topLevelMatcherIdent, IdentNode iteratedIdent)
	{
		Symbol.Occurrence topLevelMatcherOccurrence = topLevelMatcherIdent.occ;
		Symbol topLevelMatcherSymbol = topLevelMatcherOccurrence.Symbol;
		string topLevelMatcherString = topLevelMatcherSymbol.Text;
		Symbol.Occurrence iteratedOccurrence = iteratedIdent.occ;
		string iteratedString = iteratedIdent.ToString();
		string matchTypeString = "match<" + topLevelMatcherString + "." + iteratedString + ">";
		if(topLevelMatcherIdent is PackageIdentNode)
		{
			PackageIdentNode packageTopLevelMatcherIdent = (PackageIdentNode)topLevelMatcherIdent;
			Symbol.Occurrence packageOccurrence = packageTopLevelMatcherIdent.owningPackage;
			Symbol packageSymbol = packageOccurrence.Symbol;
			return new PackageIdentNode(
					env.Occurs(ParserEnvironment.PACKAGES, packageSymbol.Text, packageOccurrence.Coords),
					env.Occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.Coords));
		}
		else
			return new IdentNode(env.Occurs(ParserEnvironment.TYPES, matchTypeString, iteratedOccurrence.Coords));
	}

	public override string TypeName
	{
		get
		{
			return "match<" + topLevelMatcherUnresolved.ToString() + "." + iteratedUnresolved.ToString() + "> type";
		}
	}

	public override ICollection<BaseNode> Children
	{
		get
		{
			IList<BaseNode> children = new List<BaseNode>();
			//children.add(getValidVersion(topLevelMatcherUnresolved, topLevelMatcher));
			//children.add(getValidVersion(iteratedUnresolved, iterated));
			return children;
		}
	}

	public override ICollection<string> ChildrenNames
	{
		get
		{
			IList<string> childrenNames = new List<string>();
			//childrenNames.add("topLevelMatcher");
			//childrenNames.add("iterated");
			return childrenNames;
		}
	}

	private static readonly DeclarationResolver<TopLevelMatcherDeclNode> topLevelMatcherResolver =
			new DeclarationResolver<TopLevelMatcherDeclNode>(typeof(TopLevelMatcherDeclNode));
	private static readonly DeclarationResolver<IteratedDeclNode> iteratedResolver =
			new DeclarationResolver<IteratedDeclNode>(typeof(IteratedDeclNode));

	protected internal override bool ResolveLocal()
	{
		if(!(topLevelMatcherUnresolved is PackageIdentNode))
			FixupDefinition(topLevelMatcherUnresolved, topLevelMatcherUnresolved.Scope);

		OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.nullEvaluator);
		OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType,
				this, this, OperatorEvaluator.nullEvaluator);

		topLevelMatcher = topLevelMatcherResolver.Resolve(topLevelMatcherUnresolved, this);
		if(topLevelMatcher == null)
			return false;
		iterated = iteratedResolver.Resolve(iteratedUnresolved, this);
		return iterated != null;
	}

	public virtual TopLevelMatcherDeclNode TopLevelMatcher
	{
		get
		{
			Debug.Assert((IsResolved()));
			return topLevelMatcher;
		}
	}

	public virtual IteratedDeclNode Iterated
	{
		get
		{
			Debug.Assert((IsResolved()));
			return iterated;
		}
	}

	public override DeclNode TryGetMember(string name)
	{
		NodeDeclNode node = iterated.pattern.TryGetNode(name);
		if(node != null)
			return node;
		EdgeDeclNode edge = iterated.pattern.TryGetEdge(name);
		if(edge != null)
			return edge;
		return iterated.pattern.TryGetVar(name);
	}

	public override ISet<DeclNode> Entities
	{
		get
		{
			return iterated.pattern.Entities;
		}
	}

	/// <summary>
	/// Returns the IR object for this match type node. </summary>
	public virtual MatchTypeIterated IRMatchTypeIterated
	{
		get
		{
			return CheckIR(typeof(MatchTypeIterated));
		}
	}

	protected internal override IR ConstructIR()
	{
		if(IsIRAlreadySet())
			return (MatchTypeIterated)IR;

		MatchTypeIterated matchTypeIterated = new MatchTypeIterated(iterated.ident.IRIdent);

		IR = matchTypeIterated;

		Rule matchAction = topLevelMatcher.IRMatcher;
		Rule iter = (Rule)iterated.IR;

		matchTypeIterated.Action = matchAction;
		matchTypeIterated.Iterated = iter;

		return matchTypeIterated;
	}
}

}
