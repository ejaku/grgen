/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ast
{

	using System;
	using System.Collections.Generic;

	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using TypeDeclNode = de.unika.ipd.grgen.ast.decl.TypeDeclNode;
	using ActionDeclNode = de.unika.ipd.grgen.ast.decl.executable.ActionDeclNode;
	using FilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FilterFunctionDeclNode;
	using FunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.FunctionDeclNode;
	using MatchClassFilterFunctionDeclNode = de.unika.ipd.grgen.ast.decl.executable.MatchClassFilterFunctionDeclNode;
	using ProcedureDeclNode = de.unika.ipd.grgen.ast.decl.executable.ProcedureDeclNode;
	using RuleDeclNode = de.unika.ipd.grgen.ast.decl.executable.RuleDeclNode;
	using SequenceDeclNode = de.unika.ipd.grgen.ast.decl.executable.SequenceDeclNode;
	using SubpatternDeclNode = de.unika.ipd.grgen.ast.decl.executable.SubpatternDeclNode;
	using AlternativeCaseDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeCaseDeclNode;
	using AlternativeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.AlternativeDeclNode;
	using IteratedDeclNode = de.unika.ipd.grgen.ast.decl.pattern.IteratedDeclNode;
	using ModelNode = de.unika.ipd.grgen.ast.model.decl.ModelNode;
	using InheritanceTypeNode = de.unika.ipd.grgen.ast.model.type.InheritanceTypeNode;
	using PackageTypeNode = de.unika.ipd.grgen.ast.model.type.PackageTypeNode;
	using PatternGraphRhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphRhsNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using EvalStatementsNode = de.unika.ipd.grgen.ast.stmt.EvalStatementsNode;
	using DefinedMatchTypeNode = de.unika.ipd.grgen.ast.type.DefinedMatchTypeNode;
	using MatchTypeIteratedNode = de.unika.ipd.grgen.ast.type.MatchTypeIteratedNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using MatchTypeActionNode = de.unika.ipd.grgen.ast.type.MatchTypeActionNode;
	using Checker = de.unika.ipd.grgen.ast.util.Checker;
	using CollectChecker = de.unika.ipd.grgen.ast.util.CollectChecker;
	using de.unika.ipd.grgen.ast.util;
	using SimpleChecker = de.unika.ipd.grgen.ast.util.SimpleChecker;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Unit = de.unika.ipd.grgen.ir.Unit;
	using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using MatchClassFilterFunction = de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using PackageActionType = de.unika.ipd.grgen.ir.type.PackageActionType;

	/// <summary>
	/// The main node of the text. It is the root of the AST.
	/// </summary>
	public class UnitNode : BaseNode
	{
		static UnitNode()
		{
			SetClassName(typeof(UnitNode), "unit declaration");
		}

		private static UnitNode root; // added this for quick access to some model flags, could be considered a bit smelly regarding architecture, but having no access to the root could be considered strange

		private ModelNode stdModel;
		private CollectNode<ModelNode> models;

		private CollectNode<SubpatternDeclNode> subpatterns;
		private CollectNode<IdentNode> subpatternsUnresolved;

		private CollectNode<ActionDeclNode> actions;
		private CollectNode<IdentNode> actionsUnresolved;

		private CollectNode<MatchTypeActionNode> matchTypes;
		private CollectNode<IdentNode> matchTypesUnresolved;

		private CollectNode<MatchTypeIteratedNode> matchTypesIterated;
		private CollectNode<IdentNode> matchTypesIteratedUnresolved;

		private CollectNode<FilterFunctionDeclNode> filterFunctions;
		private CollectNode<IdentNode> filterFunctionsUnresolved;

		private CollectNode<TypeDeclNode> matchClassDecls;
		private CollectNode<IdentNode> matchClassesUnresolved;

		private CollectNode<MatchClassFilterFunctionDeclNode> matchClassFilterFunctions;
		private CollectNode<IdentNode> matchClassFilterFunctionsUnresolved;

		private CollectNode<FunctionDeclNode> functions;
		private CollectNode<IdentNode> functionsUnresolved;

		private CollectNode<ProcedureDeclNode> procedures;
		private CollectNode<IdentNode> proceduresUnresolved;

		private CollectNode<SequenceDeclNode> sequences;
		private CollectNode<IdentNode> sequencesUnresolved;

		private CollectNode<TypeDeclNode> packages;
		private CollectNode<IdentNode> packagesUnresolved;

		/// <summary>
		/// The name for this unit node
		/// </summary>
		private string unitname;

		/// <summary>
		/// The filename for this main node.
		/// </summary>
		private string filename;

		public UnitNode(string unitname, string filename,
				ModelNode stdModel, CollectNode<ModelNode> models,
				CollectNode<IdentNode> subpatterns, CollectNode<IdentNode> actions,
				CollectNode<IdentNode> matchTypes, CollectNode<IdentNode> filterFunctions,
				CollectNode<IdentNode> matchClasses, CollectNode<IdentNode> matchClassFilterFunctions,
				CollectNode<IdentNode> matchTypesIterated,
				CollectNode<IdentNode> functions, CollectNode<IdentNode> procedures,
				CollectNode<IdentNode> sequences, CollectNode<IdentNode> packages)
		{
			this.stdModel = stdModel;
			this.models = models;
			BecomeParent(this.models);
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
			this.packagesUnresolved = packages;
			BecomeParent(this.packagesUnresolved);
			this.unitname = unitname;
			this.filename = filename;
		}

		protected internal virtual ModelNode StdModel
		{
			get
			{
				return stdModel;
			}
		}

		public virtual void AddModel(ModelNode model)
		{
			// the CSharp backend does not allow for multiple models (it will throw an exception in that case),
			// but for historic reasons there is code support for multiple models in the code that comes before it
			// multiple model references are combined into one model in the parser, there the model flags are merged,
			// neither the models nor the flags get merged that are added here with this function stemming from the by now unsupported command line arguments
			// this function will be used even nowadays in case the user supplies no rules file, but only a model file (in this case no further merging is needed)
			// other outdated backends used in the FIRM compiler may still support multiple models, but they don't support the features that require/support model flags
			// the grgen.net compiler using this JAVA frontend will not supply multiple models, this is only possible when it is used directly
			// TODO: reconsider this code during a code cleaning run
			models.AddChild(model);
		}

		public virtual ModelNode Model
		{
			get
			{
				return models.Get(0); // see comment above, there should be only/exactly one model when resolving starts, with flags combined from all models, when the CSharp backend is targeted
			}
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(models);
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
				children.Add(GetValidVersionCollectNode(packagesUnresolved, packages));
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
				childrenNames.Add("models");
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
				childrenNames.Add("packages");
				return childrenNames;
			}
		}

		private static readonly CollectResolver<SubpatternDeclNode> subpatternsResolver =
				new CollectResolver<SubpatternDeclNode>(new DeclarationResolver<SubpatternDeclNode>(typeof(SubpatternDeclNode)));

		private static readonly CollectResolver<ActionDeclNode> actionsResolver =
				new CollectResolver<ActionDeclNode>(new DeclarationResolver<ActionDeclNode>(typeof(ActionDeclNode)));

		private static readonly CollectResolver<MatchTypeActionNode> matchTypesResolver =
				new CollectResolver<MatchTypeActionNode>(new DeclarationTypeResolver<MatchTypeActionNode>(typeof(MatchTypeActionNode)));

		private static readonly CollectResolver<FilterFunctionDeclNode> filterFunctionsResolver =
				new CollectResolver<FilterFunctionDeclNode>(new DeclarationResolver<FilterFunctionDeclNode>(typeof(FilterFunctionDeclNode)));

		private static readonly CollectResolver<TypeDeclNode> matchClassesResolver =
				new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));

		private static readonly CollectResolver<MatchClassFilterFunctionDeclNode> matchClassFilterFunctionsResolver =
				new CollectResolver<MatchClassFilterFunctionDeclNode>(new DeclarationResolver<MatchClassFilterFunctionDeclNode>(typeof(MatchClassFilterFunctionDeclNode)));

		private static readonly CollectResolver<MatchTypeIteratedNode> matchTypesIteratedResolver =
				new CollectResolver<MatchTypeIteratedNode>(new DeclarationTypeResolver<MatchTypeIteratedNode>(typeof(MatchTypeIteratedNode)));

		private static readonly CollectResolver<FunctionDeclNode> functionsResolver =
				new CollectResolver<FunctionDeclNode>(new DeclarationResolver<FunctionDeclNode>(typeof(FunctionDeclNode)));

		private static readonly CollectResolver<ProcedureDeclNode> proceduresResolver =
				new CollectResolver<ProcedureDeclNode>(new DeclarationResolver<ProcedureDeclNode>(typeof(ProcedureDeclNode)));

		private static readonly CollectResolver<SequenceDeclNode> sequencesResolver =
				new CollectResolver<SequenceDeclNode>(new DeclarationResolver<SequenceDeclNode>(typeof(SequenceDeclNode)));

		private static readonly CollectResolver<TypeDeclNode> packagesResolver =
				new CollectResolver<TypeDeclNode>(new DeclarationResolver<TypeDeclNode>(typeof(TypeDeclNode)));

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
			packages = packagesResolver.Resolve(packagesUnresolved, this);

			return subpatterns != null && actions != null
					&& matchTypes != null && filterFunctions != null
					&& matchClassDecls != null && matchClassFilterFunctions != null
					&& matchTypesIterated != null
					&& functions != null && procedures != null
					&& sequences != null && packages != null;
		}

		/// <summary>
		/// Check the collect nodes containing the model declarations, subpattern declarations, action declarations </summary>
		///  <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			Checker modelChecker = new CollectChecker(new SimpleChecker(typeof(ModelNode)));
			bool res = modelChecker.Check(models, error);
			foreach(ModelNode model in models.ChildrenExact)
			{
				res = CheckModelTypes(res, model.TypeDecls);
				foreach(ModelNode usedModel in model.UsedModels.ChildrenExact)
				{
					res = CheckModelTypes(res, usedModel.TypeDecls);
					foreach(TypeDeclNode package_ in usedModel.Packages.ChildrenExact)
					{
						PackageTypeNode packageType = (PackageTypeNode)package_.DeclType;
						res = CheckModelTypes(res, packageType.TypeDecls);
					}
				}
				foreach(TypeDeclNode package_ in model.Packages.ChildrenExact)
				{
					PackageTypeNode packageType = (PackageTypeNode)package_.DeclType;
					res = CheckModelTypes(res, packageType.TypeDecls);
				}
			}
			foreach(SubpatternDeclNode subpattern in subpatterns.ChildrenExact)
			{
				res &= CheckStatementsLHS(subpattern, subpattern.pattern);
				if(subpattern.right != null)
					res &= CheckStatementsRHS(subpattern, subpattern.right.patternGraph);
			}
			foreach(ActionDeclNode action in actions.ChildrenExact)
			{
				res &= CheckStatementsLHS(action, action.pattern);
				if(action is RuleDeclNode)
				{
					RuleDeclNode rule = (RuleDeclNode)action;
					res &= CheckStatementsRHS(action, rule.right.patternGraph);
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
					res &= EvalStatementNode.CheckStatements(true, matchClassFilterFunction, null,
							matchClassFilterFunction.evalStatements, true);
			}
			foreach(FunctionDeclNode function in functions.ChildrenExact)
			{
				res &= EvalStatementNode.CheckStatements(true, function, null, function.evalStatements, true);
			}
			foreach(ProcedureDeclNode procedure in procedures.ChildrenExact)
			{
				res &= EvalStatementNode.CheckStatements(false, procedure, null, procedure.evalStatements, true);
			}
			return res;
		}

		private static bool CheckModelTypes(bool res, CollectNode<TypeDeclNode> typeDecls)
		{
			foreach(TypeDeclNode typeDecl in typeDecls.ChildrenExact)
			{
				TypeNode declType = typeDecl.DeclType;
				if(declType is InheritanceTypeNode)
				{
					InheritanceTypeNode inhType = (InheritanceTypeNode)declType;
					res &= inhType.CheckStatementsInMethods();
				}
			}
			return res;
		}

		public static bool CheckStatementsLHS(DeclNode root, PatternGraphLhsNode curPattern)
		{
			bool res = true;

			// traverse graph structure
			foreach(AlternativeDeclNode alt in curPattern.alts.ChildrenExact)
			{
				foreach(AlternativeCaseDeclNode altCase in alt.ChildrenExact)
				{
					res &= CheckStatementsLHS(root, altCase.pattern);
					if(altCase.right != null)
						res &= CheckStatementsRHS(root, altCase.right.patternGraph);
				}
			}
			foreach(IteratedDeclNode iter in curPattern.iters.ChildrenExact)
			{
				res &= CheckStatementsLHS(root, iter.pattern);
				if(iter.right != null)
					res &= CheckStatementsRHS(root, iter.right.patternGraph);
			}
			foreach(PatternGraphLhsNode idpt in curPattern.idpts.ChildrenExact)
			{
				res &= CheckStatementsLHS(root, idpt);
			}

			// spawn checking computation statement structure
			foreach(EvalStatementsNode yields in curPattern.yields.ChildrenExact)
			{
				res &= EvalStatementNode.CheckStatements(true, root, null, yields.evalStatements, true);
			}

			return res;
		}

		public static bool CheckStatementsRHS(DeclNode root, PatternGraphRhsNode curGraph)
		{
			bool res = true;

			// spawn checking computation statement structure
			foreach(EvalStatementsNode evals in curGraph.evals.ChildrenExact)
			{
				res &= EvalStatementNode.CheckStatements(false, root, null, evals.evalStatements, true);
			}

			return res;
		}

		/// <summary>
		/// Get the IR unit node for this AST node. </summary>
		/// <returns> The Unit for this AST node. </returns>
		public virtual Unit IRUnit
		{
			get
			{
				return CheckIR<Unit>(typeof(Unit));
			}
		}

		/// <summary>
		/// Construct the IR object for this AST node.
		/// For a main node, this is a unit. </summary>
		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.constructIR()"/>
		protected internal override IR ConstructIR()
		{
			Unit res = new Unit(unitname, filename);

			foreach(ModelNode model in models.ChildrenExact)
			{
				Model modelIR = model.IRModel;
				res.AddModel(modelIR);
			}

			foreach(SubpatternDeclNode subpattern in subpatterns.ChildrenExact)
			{
				Rule rule = subpattern.IRMatcher;
				res.AddSubpatternRule(rule);
			}

			foreach(ActionDeclNode action in actions.ChildrenExact)
			{
				Rule rule = action.IRMatcher;
				res.AddActionRule(rule);
			}

			foreach(FilterFunctionDeclNode filter in filterFunctions.ChildrenExact)
			{
				FilterFunction filterIR = filter.IRFilterFunction;
				res.AddFilterFunction(filterIR);
			}

			foreach(TypeDeclNode matchClass in matchClassDecls.ChildrenExact)
			{
				DefinedMatchTypeNode matchClassDecl = (DefinedMatchTypeNode)matchClass.DeclType;
				DefinedMatchType matchClassIR = matchClassDecl.IRDefinedMatchType;
				res.AddMatchClass(matchClassIR);
			}

			foreach(MatchClassFilterFunctionDeclNode matchClassFilter in matchClassFilterFunctions.ChildrenExact)
			{
				MatchClassFilterFunction matchClassFilterIR = matchClassFilter.IRMatchClassFilterFunction;
				res.AddMatchClassFilterFunction(matchClassFilterIR);
			}

			foreach(FunctionDeclNode function in functions.ChildrenExact)
			{
				Function functionIR = function.IRFunction;
				res.AddFunction(functionIR);
			}

			foreach(ProcedureDeclNode procedure in procedures.ChildrenExact)
			{
				Procedure procedureIR = procedure.IRProcedure;
				res.AddProcedure(procedureIR);
			}

			foreach(SequenceDeclNode sequence in sequences.ChildrenExact)
			{
				Sequence sequenceIR = sequence.IRSequence;
				res.AddSequence(sequenceIR);
			}

			foreach(TypeDeclNode packageType in packages.ChildrenExact)
			{
				PackageActionType packageActionType = (PackageActionType)packageType.DeclType.IRType;
				res.AddPackage(packageActionType);
			}

			return res;
		}

		public static UnitNode Root
		{
			get
			{
				return UnitNode.root;
			}
			set
			{
				if(UnitNode.root != null)
					throw new Exception("Internal error, change of root node");
				UnitNode.root = value;
			}
		}


		public static void ClearRoot()
		{
			UnitNode.root = null;
		}
	}

}
