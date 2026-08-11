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

	using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
	using de.unika.ipd.grgen.ast;
	using IdentNode = de.unika.ipd.grgen.ast.IdentNode;
	using UnitNode = de.unika.ipd.grgen.ast.UnitNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
	using FilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using MatchClassFilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.MatchClassFilterFunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using RuleDeclNode = de.unika.ipd.grgen.ast.decl.executable.RuleDeclNode;
	using SequenceDeclNode = de.unika.ipd.grgen.ast.decl.executable.SequenceDeclNode;
	using SubpatternDeclNode = de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using de.unika.ipd.grgen.ast.util;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using MatchClassFilterFunction = de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using MatchType = de.unika.ipd.grgen.ir.type.MatchType;
	using MatchTypeIterated = de.unika.ipd.grgen.ir.type.MatchTypeIterated;
	using PackageActionType = de.unika.ipd.grgen.ir.type.PackageActionType;

	/// <summary>
	/// A package type AST node, for packages from the actions (in contrast to the models).
	/// </summary>
	public class PackageActionTypeNode : CompoundTypeNode
	{
		static PackageActionTypeNode()
		{
			SetClassName(typeof(PackageActionTypeNode), "package in actions type");
		}

		private CollectNode<SubpatternDeclNode> subpatterns;
		private CollectNode<IdentNode> subpatternsUnresolved;

		private CollectNode<ActionDeclNode> actions;
		private CollectNode<IdentNode> actionsUnresolved;

		private CollectNode<MatchTypeActionNode> matchTypes;
		private CollectNode<IdentNode> matchTypesUnresolved;

		private CollectNode<FilterFunctionDeclNode> filterFunctions;
		private CollectNode<IdentNode> filterFunctionsUnresolved;

		private CollectNode<TypeDeclNode> matchClassDecls;
		private CollectNode<IdentNode> matchClassesUnresolved;

		private CollectNode<MatchClassFilterFunctionDeclNode> matchClassFilterFunctions;
		private CollectNode<IdentNode> matchClassFilterFunctionsUnresolved;

		private CollectNode<MatchTypeIteratedNode> matchTypesIterated;
		private CollectNode<IdentNode> matchTypesIteratedUnresolved;

		private CollectNode<FunctionDeclNode> functions;
		private CollectNode<IdentNode> functionsUnresolved;

		private CollectNode<ProcedureDeclNode> procedures;
		private CollectNode<IdentNode> proceduresUnresolved;

		private CollectNode<SequenceDeclNode> sequences;
		private CollectNode<IdentNode> sequencesUnresolved;

		public PackageActionTypeNode(CollectNode<IdentNode> subpatterns, CollectNode<IdentNode> actions,
				CollectNode<IdentNode> matchTypes, CollectNode<IdentNode> filterFunctions,
				CollectNode<IdentNode> matchClasses, CollectNode<IdentNode> matchClassFilterFunctions,
				CollectNode<IdentNode> matchTypesIterated,
				CollectNode<IdentNode> functions, CollectNode<IdentNode> procedures,
				CollectNode<IdentNode> sequences)
		{
			this.subpatternsUnresolved = subpatterns;
			BecomeParent(this.subpatternsUnresolved);
			this.actionsUnresolved = actions;
			BecomeParent(this.actionsUnresolved);
			this.matchTypesUnresolved = matchTypes;
			BecomeParent(this.matchTypesUnresolved);
			this.filterFunctionsUnresolved = filterFunctions;
			BecomeParent(this.filterFunctionsUnresolved);
			this.matchClassesUnresolved = matchClasses;
			BecomeParent(this.matchClassesUnresolved);
			this.matchClassFilterFunctionsUnresolved = matchClassFilterFunctions;
			BecomeParent(this.matchClassFilterFunctionsUnresolved);
			this.matchTypesIteratedUnresolved = matchTypesIterated;
			BecomeParent(this.matchTypesIteratedUnresolved);
			this.functionsUnresolved = functions;
			BecomeParent(this.functionsUnresolved);
			this.proceduresUnresolved = procedures;
			BecomeParent(this.proceduresUnresolved);
			this.sequencesUnresolved = sequences;
			BecomeParent(this.sequencesUnresolved);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(GetValidVersionCollectNode(subpatternsUnresolved, subpatterns));
				children.Add(GetValidVersionCollectNode(actionsUnresolved, actions));
				children.Add(GetValidVersionCollectNode(matchTypesUnresolved, matchTypes));
				children.Add(GetValidVersionCollectNode(filterFunctionsUnresolved, filterFunctions));
				children.Add(GetValidVersionCollectNode(matchClassesUnresolved, matchClassDecls));
				children.Add(GetValidVersionCollectNode(matchClassFilterFunctionsUnresolved, matchClassFilterFunctions));
				children.Add(GetValidVersionCollectNode(matchTypesIteratedUnresolved, matchTypesIterated));
				children.Add(GetValidVersionCollectNode(functionsUnresolved, functions));
				children.Add(GetValidVersionCollectNode(proceduresUnresolved, procedures));
				children.Add(GetValidVersionCollectNode(sequencesUnresolved, sequences));
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
				childrenNames.Add("subpatterns");
				childrenNames.Add("actions");
				childrenNames.Add("match types");
				childrenNames.Add("filter functions");
				childrenNames.Add("match classes");
				childrenNames.Add("match class filter functions");
				childrenNames.Add("match types iterated");
				childrenNames.Add("functions");
				childrenNames.Add("procedures");
				childrenNames.Add("sequences");
				return childrenNames;
			}
		}

		private static readonly CollectResolver<SubpatternDeclNode> subpatternsResolver =
				new CollectResolver<SubpatternDeclNode>(new DeclarationResolver<SubpatternDeclNode>(typeof(SubpatternDeclNode)));

		private static readonly CollectResolver<ActionDeclNode> actionsResolver =
				new CollectResolver<ActionDeclNode>(new DeclarationResolver<ActionDeclNode>(typeof(ActionDeclNode)));

		private static CollectResolver<MatchTypeActionNode> matchTypesResolver =
				new CollectResolver<MatchTypeActionNode>(new DeclarationTypeResolver<MatchTypeActionNode>(typeof(MatchTypeActionNode)));

		private static readonly CollectResolver<FilterFunctionDeclNode> filterFunctionsResolver =
				new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(typeof(FilterFunctionDeclNode)));

		private static readonly CollectResolver<TypeDeclNode> matchClassesResolver =
				new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));

		private static readonly CollectResolver<MatchClassFilterFunctionDeclNode> matchClassFilterFunctionsResolver =
				new CollectResolver<MatchClassFilterFunctionDeclNode>(new DeclarationResolver<MatchClassFilterFunctionDeclNode>(typeof(MatchClassFilterFunctionDeclNode)));

		private static CollectResolver<MatchTypeIteratedNode> matchTypesIteratedResolver =
				new CollectResolver<MatchTypeIteratedNode>(new DeclarationTypeResolver<MatchTypeIteratedNode>(typeof(MatchTypeIteratedNode)));

		private static readonly CollectResolver<FunctionDeclNode> functionsResolver =
				new CollectResolver<FunctionDeclNode>(new DeclarationResolver<FunctionDeclNode>(typeof(FunctionDeclNode)));

		private static readonly CollectResolver<ProcedureDeclNode> proceduresResolver =
				new CollectResolver<ProcedureDeclNode>(new DeclarationResolver<ProcedureDeclNode>(typeof(ProcedureDeclNode)));

		private static readonly CollectResolver<SequenceDeclNode> sequencesResolver =
				new CollectResolver<SequenceDeclNode>(new DeclarationResolver<SequenceDeclNode>(typeof(SequenceDeclNode)));

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			subpatterns = subpatternsResolver.Resolve(subpatternsUnresolved, this);
			actions = actionsResolver.Resolve(actionsUnresolved, this);
			matchTypes = matchTypesResolver.Resolve(matchTypesUnresolved, this);
			filterFunctions = filterFunctionsResolver.Resolve(filterFunctionsUnresolved, this);
			matchClassDecls = matchClassesResolver.Resolve(matchClassesUnresolved, this);
			matchClassFilterFunctions = matchClassFilterFunctionsResolver.Resolve(matchClassFilterFunctionsUnresolved, this);
			matchTypesIterated = matchTypesIteratedResolver.Resolve(matchTypesIteratedUnresolved, this);
			functions = functionsResolver.Resolve(functionsUnresolved, this);
			procedures = proceduresResolver.Resolve(proceduresUnresolved, this);
			sequences = sequencesResolver.Resolve(sequencesUnresolved, this);

			return subpatterns != null && actions != null
					&& matchTypes != null && filterFunctions != null
					&& matchClassDecls != null && matchClassFilterFunctions != null
					&& matchTypesIterated != null
					&& functions != null && procedures != null && sequences != null;
		}

		/// <summary>
		/// Check the collect nodes containing the model declarations, subpattern declarations, action declarations </summary>
		///  <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			bool res = true;
			foreach(SubpatternDeclNode subpattern in subpatterns.ChildrenExact)
			{
				res &= UnitNode.CheckStatementsLHS(subpattern, subpattern.pattern);
				if(subpattern.right != null)
					res &= UnitNode.CheckStatementsRHS(subpattern, subpattern.right.patternGraph);
			}
			foreach(ActionDeclNode action in actions.ChildrenExact)
			{
				res &= UnitNode.CheckStatementsLHS(action, action.pattern);
				if(action is RuleDeclNode)
				{
					RuleDeclNode rule = (RuleDeclNode)action;
					res &= UnitNode.CheckStatementsRHS(action, rule.right.patternGraph);
				}
			}
			foreach(FilterFunctionDeclNode filterFunction in filterFunctions.ChildrenExact)
			{
				if(filterFunction.evalStatements != null) // otherwise external filter function without statements
					res &= EvalStatementNode.CheckStatements(true, filterFunction, null, filterFunction.evalStatements, true);
			}
			foreach(MatchClassFilterFunctionDeclNode matchClassFilterFunction in matchClassFilterFunctions.ChildrenExact)
			{
				if(matchClassFilterFunction.evalStatements != null) // otherwise external filter function without statements
				{
					res &= EvalStatementNode.CheckStatements(true, matchClassFilterFunction, null,
							matchClassFilterFunction.evalStatements, true);
				}
			}
			foreach(FunctionDeclNode function in functions.ChildrenExact)
				res &= EvalStatementNode.CheckStatements(true, function, null, function.evalStatements, true);
			foreach(ProcedureDeclNode procedure in procedures.ChildrenExact)
				res &= EvalStatementNode.CheckStatements(false, procedure, null, procedure.evalStatements, true);
			return res;
		}

		/// <summary>
		/// Returns the IR object for this package action type node. </summary>
		public virtual PackageActionType IRPackage
		{
			get
			{
				return CheckIR(typeof(PackageActionType));
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR() "/>
		protected internal override IR ConstructIR()
		{
			Ident id = Ident.CheckIR(typeof(Ident));
			PackageActionType res = new PackageActionType(id);

			foreach(SubpatternDeclNode subpattern in subpatterns.ChildrenExact)
			{
				Rule subRule = subpattern.IRMatcher;
				subRule.PackageContainedIn = id.ToString();
				res.AddSubpatternRule(subRule);
			}

			foreach(ActionDeclNode action in actions.ChildrenExact)
			{
				Rule rule = action.IRMatcher;
				rule.PackageContainedIn = id.ToString();
				res.AddActionRule(rule);
			}

			foreach(MatchTypeActionNode matchType in matchTypes.ChildrenExact)
			{
				MatchType matchTypeIR = matchType.IRMatchType;
				matchTypeIR.PackageContainedIn = id.ToString();
				//no adding to package as nothing needs to be generated from this type / already happens with action
			}

			foreach(FilterFunctionDeclNode filter in filterFunctions.ChildrenExact)
			{
				FilterFunction filterIR = filter.IRFilterFunction;
				filterIR.PackageContainedIn = id.ToString();
				res.AddFilterFunction(filterIR);
			}

			foreach(TypeDeclNode matchClass in matchClassDecls.ChildrenExact)
			{
				DefinedMatchTypeNode matchClassDecl = (DefinedMatchTypeNode)matchClass.DeclType;
				DefinedMatchType matchClassIR = matchClassDecl.IRDefinedMatchType;
				matchClassIR.PackageContainedIn = id.ToString();
				res.AddMatchClass(matchClassIR);
			}

			foreach(MatchClassFilterFunctionDeclNode matchClassFilter in matchClassFilterFunctions.ChildrenExact)
			{
				MatchClassFilterFunction matchClassFilterIR = matchClassFilter.IRMatchClassFilterFunction;
				matchClassFilterIR.PackageContainedIn = id.ToString();
				res.AddMatchClassFilterFunction(matchClassFilterIR);
			}

			foreach(MatchTypeIteratedNode matchTypeIterated in matchTypesIterated.ChildrenExact)
			{
				MatchTypeIterated matchTypeIteratedIR = matchTypeIterated.IRMatchTypeIterated;
				matchTypeIteratedIR.PackageContainedIn = id.ToString();
				//no adding to package as nothing needs to be generated from this type / already happens with action
			}

			foreach(FunctionDeclNode function in functions.ChildrenExact)
			{
				Function functionIR = function.IRFunction;
				functionIR.PackageContainedIn = id.ToString();
				res.AddFunction(functionIR);
			}

			foreach(ProcedureDeclNode procedure in procedures.ChildrenExact)
			{
				Procedure procedureIR = procedure.IRProcedure;
				procedureIR.PackageContainedIn = id.ToString();
				res.AddProcedure(procedureIR);
			}

			foreach(SequenceDeclNode sequence in sequences.ChildrenExact)
			{
				Sequence sequenceIR = sequence.IRSequence;
				sequenceIR.PackageContainedIn = id.ToString();
				res.AddSequence(sequenceIR);
			}

			return res;
		}

		public override string ToString()
		{
			return "package " + Ident;
		}

		public static string KindStr
		{
			get
			{
				return "package";
			}
		}
	}

}
