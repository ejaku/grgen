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
	using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
	using Operator = de.unika.ipd.grgen.ast.decl.executable.Operator;
	using OperatorDeclNode = de.unika.ipd.grgen.ast.decl.executable.OperatorDeclNode;
	using OperatorEvaluator = de.unika.ipd.grgen.ast.decl.executable.OperatorEvaluator;
	using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
	using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
	using BasicTypeNode = de.unika.ipd.grgen.ast.type.basic.BasicTypeNode;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;
	using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;
	using Symbol = de.unika.ipd.grgen.parser.Symbol;
	using Occurrence = de.unika.ipd.grgen.parser.Symbol.Occurrence;

	public class MatchTypeActionNode : MatchTypeNode
	{
		static MatchTypeActionNode()
		{
			SetClassName(typeof(MatchTypeActionNode), "match type action");
		}

		private IdentNode actionUnresolved;
		private ActionDeclNode action;

		private MatchTypeActionNode(IdentNode actionIdent)
		{
			actionUnresolved = BecomeParent(actionIdent);
		}

		public static IdentNode DefineMatchType(ParserEnvironment env, IdentNode actionIdent)
		{
			string actionString = actionIdent.ToString();
			string matchTypeString = "match<" + actionString + ">";
			IdentNode matchTypeIdentNode = new IdentNode(
					env.Define(ParserEnvironment.TYPES, matchTypeString, actionIdent.Coords));
			MatchTypeActionNode matchTypeNode = new MatchTypeActionNode(actionIdent);
			TypeDeclNode typeDeclNode = new TypeDeclNode(matchTypeIdentNode, matchTypeNode);
			matchTypeIdentNode.Decl = typeDeclNode;
			return matchTypeIdentNode;
		}

		public static IdentNode GetMatchTypeIdentNode(ParserEnvironment env, IdentNode actionIdent)
		{
			Symbol.Occurrence actionOccurrence = actionIdent.occ;
			Symbol actionSymbol = actionOccurrence.Symbol;
			string actionString = actionSymbol.Text;
			string matchTypeString = "match<" + actionString + ">";
			if(actionIdent is PackageIdentNode)
			{
				PackageIdentNode packageActionIdent = (PackageIdentNode)actionIdent;
				Symbol.Occurrence packageOccurrence = packageActionIdent.owningPackage;
				Symbol packageSymbol = packageOccurrence.Symbol;
				return new PackageIdentNode(
						env.Occurs(ParserEnvironment.PACKAGES, packageSymbol.Text, packageOccurrence.Coords),
						env.Occurs(ParserEnvironment.TYPES, matchTypeString, actionOccurrence.Coords));
			}
			else
				return new IdentNode(env.Occurs(ParserEnvironment.TYPES, matchTypeString, actionOccurrence.Coords));
		}

		public override string TypeName
		{
			get
			{
				return "match<" + actionUnresolved.ToString() + ">";
			}
		}

		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				//children.add(getValidVersion(actionUnresolved, action));
				return children;
			}
		}

		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				//childrenNames.add("action");
				return childrenNames;
			}
		}

		private static readonly DeclarationResolver<ActionDeclNode> actionResolver =
				new DeclarationResolver<ActionDeclNode>(typeof(ActionDeclNode));

		protected internal override bool ResolveLocal()
		{
			if(!(actionUnresolved is PackageIdentNode))
				FixupDefinition(actionUnresolved, actionUnresolved.Scope);

			OperatorDeclNode.MakeBinOp(Operator.EQ, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.nullEvaluator);
			OperatorDeclNode.MakeBinOp(Operator.NE, BasicTypeNode.booleanType,
					this, this, OperatorEvaluator.nullEvaluator);

			action = actionResolver.Resolve(actionUnresolved, this);
			if(action == null)
				return false;
			return true;
		}

		public virtual ActionDeclNode Action
		{
			get
			{
				Debug.Assert((IsResolved()));
				return action;
			}
		}

		public override DeclNode TryGetMember(string name)
		{
			NodeDeclNode node = action.pattern.TryGetNode(name);
			if(node != null)
				return node;
			EdgeDeclNode edge = action.pattern.TryGetEdge(name);
			if(edge != null)
				return edge;
			return action.pattern.TryGetVar(name);
		}

		public override ISet<DeclNode> Entities
		{
			get
			{
				return action.pattern.Entities;
			}
		}

		/// <summary>
		/// Returns the IR object for this match type node. </summary>
		public virtual MatchType IRMatchType
		{
			get
			{
				return CheckIR(typeof(MatchType));
			}
		}

		protected internal override IR ConstructIR()
		{
			if(IsIRAlreadySet())
				return (MatchType)IR;

			MatchType matchType = new MatchType(action.ident.IRIdent);

			IR = matchType;

			Rule matchAction = action.IRMatcher;
			matchType.Action = matchAction;

			return matchType;
		}
	}

}
