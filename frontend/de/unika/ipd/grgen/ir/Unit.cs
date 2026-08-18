/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author shack
/// </summary>

namespace de.unika.ipd.grgen.ir
{

	using System;
	using System.Collections.Generic;
	using System.Text;

	using FilterFunction = de.unika.ipd.grgen.ir.executable.FilterFunction;
	using Function = de.unika.ipd.grgen.ir.executable.Function;
	using MatchClassFilterFunction = de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
	using Procedure = de.unika.ipd.grgen.ir.executable.Procedure;
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;
	using Sequence = de.unika.ipd.grgen.ir.executable.Sequence;
	using Model = de.unika.ipd.grgen.ir.model.Model;
	using NodeEdgeEnumBearer = de.unika.ipd.grgen.ir.model.NodeEdgeEnumBearer;
	using InheritanceType = de.unika.ipd.grgen.ir.model.type.InheritanceType;
	using PackageType = de.unika.ipd.grgen.ir.model.type.PackageType;
	using Edge = de.unika.ipd.grgen.ir.pattern.Edge;
	using Node = de.unika.ipd.grgen.ir.pattern.Node;
	using PatternGraphLhs = de.unika.ipd.grgen.ir.pattern.PatternGraphLhs;
	using Variable = de.unika.ipd.grgen.ir.pattern.Variable;
	using DefinedMatchType = de.unika.ipd.grgen.ir.type.DefinedMatchType;
	using PackageActionType = de.unika.ipd.grgen.ir.type.PackageActionType;
	using Util = de.unika.ipd.grgen.util.Util;

	/// <summary>
	/// A unit with all declared entities
	/// </summary>
	public class Unit : IR, ActionsBearer
	{
		private readonly List<Model> models = new List<Model>();

		private readonly List<Rule> subpatternRules = new List<Rule>();

		private readonly List<Rule> actionRules = new List<Rule>();

		private readonly List<FilterFunction> filterFunctions = new List<FilterFunction>();

		private readonly List<DefinedMatchType> matchClasses = new List<DefinedMatchType>();

		private readonly List<MatchClassFilterFunction> matchClassFilterFunctions = new List<MatchClassFilterFunction>();

		private readonly List<Function> functions = new List<Function>();

		private readonly List<Procedure> procedures = new List<Procedure>();

		private readonly List<Sequence> sequences = new List<Sequence>();

		private readonly List<PackageActionType> packages = new List<PackageActionType>();

		private string digest = "";

		private bool digestValid = false;

		/// <summary>
		/// The unit name of this unit. </summary>
		private string unitName;

		/// <summary>
		/// The source filename of this unit. </summary>
		private string filename;

		public Unit(string unitName, string filename)
			: base("unit")
		{
			this.unitName = unitName;
			this.filename = filename;
		}

		/// <summary>
		/// Add a model to the unit. </summary>
		public virtual void AddModel(Model model)
		{
			models.Add(model);
			digestValid = false;
		}

		/// <returns> The type model of this unit. </returns>
		public virtual ICollection<Model> Models
		{
			get
			{
				return models.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a subpattern-rule to the unit. </summary>
		public virtual void AddSubpatternRule(Rule subpatternRule)
		{
			subpatternRules.Add(subpatternRule);
		}

		public virtual ICollection<Rule> SubpatternRules
		{
			get
			{
				return subpatternRules.AsReadOnly();
			}
		}

		/// <summary>
		/// Add an action-rule to the unit. </summary>
		public virtual void AddActionRule(Rule actionRule)
		{
			actionRules.Add(actionRule);
		}

		public virtual ICollection<Rule> ActionRules
		{
			get
			{
				return actionRules.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a filter function to the unit. </summary>
		public virtual void AddFilterFunction(FilterFunction filterFunction)
		{
			filterFunctions.Add(filterFunction);
		}

		public virtual ICollection<FilterFunction> FilterFunctions
		{
			get
			{
				return filterFunctions.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a match class to the unit. </summary>
		public virtual void AddMatchClass(DefinedMatchType matchClass)
		{
			matchClasses.Add(matchClass);
		}

		public virtual ICollection<DefinedMatchType> MatchClasses
		{
			get
			{
				return matchClasses.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a match filter function to the unit. </summary>
		public virtual void AddMatchClassFilterFunction(MatchClassFilterFunction matchClassFilterFunction)
		{
			matchClassFilterFunctions.Add(matchClassFilterFunction);
		}

		public virtual ICollection<MatchClassFilterFunction> MatchClassFilterFunctions
		{
			get
			{
				return matchClassFilterFunctions.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a function to the unit. </summary>
		public virtual void AddFunction(Function function)
		{
			functions.Add(function);
		}

		public virtual ICollection<Function> Functions
		{
			get
			{
				return functions.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a procedure to the unit. </summary>
		public virtual void AddProcedure(Procedure procedure)
		{
			procedures.Add(procedure);
		}

		public virtual ICollection<Procedure> Procedures
		{
			get
			{
				return procedures.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a sequence to the unit. </summary>
		public virtual void AddSequence(Sequence sequence)
		{
			sequences.Add(sequence);
		}

		public virtual ICollection<Sequence> Sequences
		{
			get
			{
				return sequences.AsReadOnly();
			}
		}

		/// <summary>
		/// Add a package to the unit. </summary>
		public virtual void AddPackage(PackageActionType packageActionType)
		{
			packages.Add(packageActionType);
		}

		public virtual ICollection<PackageActionType> Packages
		{
			get
			{
				return packages.AsReadOnly();
			}
		}

		public virtual Model ActionsGraphModel
		{
			get
			{
				return models[0];
			}
		}

		public virtual string ActionsGraphModelName
		{
			get
			{
				return models[0].Ident.ToString();
			}
		}

		/// <returns> The unit name of this unit. </returns>
		public virtual string UnitName
		{
			get
			{
				return unitName;
			}
		}

		/// <returns> The source filename corresponding to this unit. </returns>
		public virtual string Filename
		{
			get
			{
				return filename;
			}
		}

		public override void AddFields(IDictionary<string, object> fields)
		{
			base.AddFields(fields);
			fields["models"] = models.GetEnumerator();
		}

		protected internal override void CanonicalizeLocal()
		{
			//Collections.sort(models, Identifiable.COMPARATOR);
			//Collections.sort(actions, Identifiable.COMPARATOR);
			//Collections.sort(subpatterns, Identifiable.COMPARATOR);

			foreach(Model model in models)
				model.Canonicalize();
		}

		public virtual void AddToDigest(StringBuilder sb)
		{
			foreach(Model model in models)
				model.AddToDigest(sb);
		}

		/// <summary>
		/// Build the digest string of this type model. </summary>
		private void BuildDigest()
		{
			StringBuilder sb = new StringBuilder();

			AddToDigest(sb);

			try
			{
				sbyte[] serialData = sb.ToString().GetBytes(Encoding.ASCII);
				MessageDigest md = MessageDigest.GetInstance("MD5");
				digest = Util.HexString(md.Digest(serialData));
			}
			catch(Exception e)
			{
				e.PrintStackTrace(System.err);
				digest = "<error>";
			}

			digestValid = true;
		}

		/// <summary>
		/// Get the digest of this type model. </summary>
		public string TypeDigest
		{
			get
			{
				if(!digestValid)
					BuildDigest();

				return digest;
			}
		}

		public virtual void PostPatchIR()
		{
			foreach(Model model in models)
			{
				//model.forceFunctionsParallel(); // comment out to parallelize everything as far as possible, for testing - don't forget "uncomment to parallelize everything as far as possible, for testing"
				PostPatchIR(model);
				foreach(PackageType pt in model.Packages)
					PostPatchIR(pt);
			}
			PostPatchIR(new ComposedActionsBearer(this));
		}

		public static void PostPatchIR(NodeEdgeEnumBearer bearer)
		{
			// deferred step that has to be done after IR was built
			// filling in transitive members for inheritance types
			// can't be called during IR building because of dependencies (node/edge attributes of subtypes)
			foreach(InheritanceType type in bearer.NodeTypes)
			{
				ICollection<Entity> temp = type.AllMembers; // checks overwriting of attributes
			}
			foreach(InheritanceType type in bearer.EdgeTypes)
			{
				ICollection<Entity> temp = type.AllMembers; // checks overwriting of attributes
			}
			foreach(InheritanceType type in bearer.ObjectTypes)
			{
				ICollection<Entity> temp = type.AllMembers; // checks overwriting of attributes
			}
			foreach(InheritanceType type in bearer.TransientObjectTypes)
			{
				ICollection<Entity> temp = type.AllMembers; // checks overwriting of attributes
			}
		}

		public static void PostPatchIR(ActionsBearer bearer)
		{
			/*for(Rule actionRule : bearer.getActionRules()) {
				if(!actionRule.getAnnotations().containsKey("parallelize")) // uncomment to parallelize everything as far as possible, for testing
					actionRule.getAnnotations().put("parallelize", 2); // don't forget "comment out to parallelize everything as far as possible, for testing"
			}*/
		}

		public virtual void CheckForEmptyPatternsInIterateds()
		{
			CheckForEmptyPatternsInIterateds(new ComposedActionsBearer(this));
		}

		public static void CheckForEmptyPatternsInIterateds(ActionsBearer bearer)
		{
			// iterateds don't terminate if they match an empty pattern again and again
			// so we compute maybe empty/epsilon patterns and emit error messages if they occur inside an iterated
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.pattern.CheckForEmptyPatternsInIterateds();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.pattern.CheckForEmptyPatternsInIterateds();
		}

		public virtual void CheckForEmptySubpatternRecursions()
		{
			CheckForEmptySubpatternRecursions(new ComposedActionsBearer(this));
		}

		public static void CheckForEmptySubpatternRecursions(ActionsBearer bearer)
		{
			// subpatterns may not terminate if there is a recursion only involving empty terminal graphs
			// so we compute the subpattern derivation paths containing only empty graphs
			// and emit error messages if they contain a subpattern calling itself
			HashSet<PatternGraphLhs> subpatternsAlreadyVisited = new HashSet<PatternGraphLhs>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
			{
				subpatternsAlreadyVisited.Add(subpatternRule.pattern);
				subpatternRule.pattern.CheckForEmptySubpatternRecursions(subpatternsAlreadyVisited);
				subpatternsAlreadyVisited.Clear();
			}
		}

		public virtual void CheckForNeverSucceedingSubpatternRecursions()
		{
			CheckForNeverSucceedingSubpatternRecursions(new ComposedActionsBearer(this));
		}

		public static void CheckForNeverSucceedingSubpatternRecursions(ActionsBearer bearer)
		{
			// matching a subpattern never terminates successfully 
			// if there is no terminal pattern on any of its alternative branches/bodies
			// emit an error message in this case (it might be the case more often, this is what we can tell for sure)
			HashSet<PatternGraphLhs> subpatternsAlreadyVisited = new HashSet<PatternGraphLhs>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
			{
				subpatternsAlreadyVisited.Add(subpatternRule.pattern);
				if(subpatternRule.pattern.IsNeverTerminatingSuccessfully(subpatternsAlreadyVisited))
				{
					error.Warning(subpatternRule.Ident.Coords, "Matching the subpattern "
							+ subpatternRule.Ident
							+ " will never terminate successfully (endless recursion on any path, only (potentially) terminated by failing matching).");
				}
				subpatternsAlreadyVisited.Clear();
			}
		}

		public virtual void CheckForMultipleRetypes()
		{
			CheckForMultipleRetypes(new ComposedActionsBearer(this));
		}

		public static void CheckForMultipleRetypes(ActionsBearer bearer)
		{
			// an iterated may cause an element matched once to be retyped multiple times
			// check for this situation (collect elements on descending over nesting structure, 
			// spark checker on visiting an iterated to check its local and nested content)
			HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
			HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
			foreach(Rule actionRule in bearer.ActionRules)
			{
				if(actionRule.Right != null)
				{
					actionRule.pattern.CheckForMultipleRetypes(
							alreadyDefinedNodes, alreadyDefinedEdges, actionRule.Right);
					alreadyDefinedNodes.Clear();
					alreadyDefinedEdges.Clear();
				}
			}
			foreach(Rule subpatternRule in bearer.SubpatternRules)
			{
				if(subpatternRule.Right != null)
				{
					subpatternRule.pattern.CheckForMultipleRetypes(
							alreadyDefinedNodes, alreadyDefinedEdges, subpatternRule.Right);
					alreadyDefinedNodes.Clear();
					alreadyDefinedEdges.Clear();
				}
			}
		}

		public virtual void CheckForMultipleDeletesOrRetypes()
		{
			ComposedActionsBearer bearer = new ComposedActionsBearer(this);
			CheckForMultipleDeletesOrRetypesGlobal(bearer);
			CheckForMultipleRetypesLocal(bearer);
		}

		// protection against multiple changes (from retypes or deletes) of an element on a subpattern call path
		// (potentially homomorphically matched, inter-pattern)
		public static void CheckForMultipleDeletesOrRetypesGlobal(ActionsBearer bearer)
		{
			// an element may be deleted/retyped several times at different nesting levels
			// or even in a subpattern called and outside of this subpattern
			// so we check that on all nesting paths there is only one delete/retype occuring
			// and emit error messages if this is not the case

			// initial step: compute the subpatterns where a subpattern is used
			Dictionary<Rule, HashSet<Rule>> subpatternsDefToUse = new Dictionary<Rule, HashSet<Rule>>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternsDefToUse[subpatternRule] = new HashSet<Rule>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.ComputeUsageDependencies(subpatternsDefToUse, subpatternRule);
			// then: compute which parameters may get deleted/retyped, 
			// if this information changed from before, the used subpatterns are added to a worklist
			// which is processed step by step until it gets empty due to a fixpoint being reached
			Dictionary<Rule, Dictionary<Entity, Rule>> subpatternsToParametersToTheirDeletingOrRetypingPattern = new Dictionary<Rule, Dictionary<Entity, Rule>>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
			{
				subpatternsToParametersToTheirDeletingOrRetypingPattern[subpatternRule] = new Dictionary<Entity, Rule>();
				foreach(Entity param in subpatternRule.Parameters)
					subpatternsToParametersToTheirDeletingOrRetypingPattern[subpatternRule][param] = null;
			}
			LinkedList<Rule> subpatternsToProcess = new LinkedList<Rule>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternsToProcess.AddLast(subpatternRule);
			while(subpatternsToProcess.Count > 0)
			{
				Rule subpattern = subpatternsToProcess.First.Value;
				subpatternsToProcess.RemoveFirst();
				bool changed = subpattern.CheckForMultipleDeletesOrRetypes(new Dictionary<Entity, Rule>(),
						subpatternsToParametersToTheirDeletingOrRetypingPattern);
				if(changed)
				{
					foreach(Rule needsRecomputation in subpatternsDefToUse[subpattern])
					{
						if(!subpatternsToProcess.Contains(needsRecomputation))
							subpatternsToProcess.AddLast(needsRecomputation);
					}
				}
			}
			// finally: do the computation on the (non-callable) rules
			foreach(Rule actionRule in bearer.ActionRules)
			{
				actionRule.CheckForMultipleDeletesOrRetypes(new Dictionary<Entity, Rule>(),
						subpatternsToParametersToTheirDeletingOrRetypingPattern);
			}
		}

		// protection against multiple retypes of an element caused by a homomorphic match
		// (within/intra-pattern; note that a retype and a delete or multiple deletes 
		// of a homomorphically matched element are ok -- SPO-like deletion has priority,
		// while multiple deletes won't harm/are idempotent)
		public static void CheckForMultipleRetypesLocal(ActionsBearer bearer)
		{
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.CheckForMultipleRetypesLocal();
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.CheckForMultipleRetypesLocal();
		}

		public virtual void TransmitExecUsageToRules()
		{
			TransmitExecUsageToRules(new ComposedActionsBearer(this));
		}

		public static void TransmitExecUsageToRules(ActionsBearer bearer)
		{
			// if an alternative, iterated, or subpattern used from a rule employs an exec,
			// the execs are not executed directly but added to a to-be-executed-queue;
			// at the end the root rule must execute this queue.
			// determine for which root rules this is the case, 
			// so we generate the queue-executing code only for them

			// step 1a: compute the subpatterns and rules where a subpattern is used
			Dictionary<Rule, HashSet<Rule>> defToUse = new Dictionary<Rule, HashSet<Rule>>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				defToUse[subpatternRule] = new HashSet<Rule>();
			foreach(Rule actionRule in bearer.ActionRules)
				defToUse[actionRule] = new HashSet<Rule>();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.ComputeUsageDependencies(defToUse, subpatternRule);
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.ComputeUsageDependencies(defToUse, actionRule);
			// step 1b: compute which subpatterns and rules use non-direct execs (alternative,iterated,usage of subpattern with exec)
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.mightThereBeDeferredExecs = subpatternRule.IsUsingNonDirectExec(false);
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.mightThereBeDeferredExecs = actionRule.IsUsingNonDirectExec(true);
			// step 2: propagate the exec-using to the subpatterns and rules containing the exec-using-subpatterns
			// until nothing changes, i.e. a fixpoint was reached
			bool changed;
			do
			{
				changed = false;
				foreach(Rule subpatternRule in bearer.SubpatternRules)
				{
					if(subpatternRule.mightThereBeDeferredExecs)
					{
						foreach(Rule toBeMarkedAsNonDirectExecUser in defToUse[subpatternRule])
						{
							if(!toBeMarkedAsNonDirectExecUser.mightThereBeDeferredExecs)
							{
								toBeMarkedAsNonDirectExecUser.mightThereBeDeferredExecs = true;
								changed = true;
							}
						}
					}
				}
			} while(changed);

			// final step: remove the information again from the subpatterns to prevent the exec-dequeing code being called from there
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.mightThereBeDeferredExecs = false;
		}

		public virtual void ResolvePatternLockedModifier()
		{
			ResolvePatternLockedModifier(new ComposedActionsBearer(this));
		}

		public static void ResolvePatternLockedModifier(ActionsBearer bearer)
		{
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.pattern.ResolvePatternLockedModifier();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.pattern.ResolvePatternLockedModifier();
		}

		public virtual void EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern()
		{
			EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(new ComposedActionsBearer(this));
		}

		public static void EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(ActionsBearer bearer)
		{
			HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
			HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
			HashSet<Variable> alreadyDefinedVariables = new HashSet<Variable>();
			foreach(Rule actionRule in bearer.ActionRules)
			{
				actionRule.pattern.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodes, alreadyDefinedEdges, alreadyDefinedVariables,
						actionRule.Right);
				alreadyDefinedNodes.Clear();
				alreadyDefinedEdges.Clear();
				alreadyDefinedVariables.Clear();
			}
			foreach(Rule subpatternRule in bearer.SubpatternRules)
			{
				subpatternRule.pattern.EnsureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
						alreadyDefinedNodes, alreadyDefinedEdges, alreadyDefinedVariables,
						subpatternRule.Right);
				alreadyDefinedNodes.Clear();
				alreadyDefinedEdges.Clear();
				alreadyDefinedVariables.Clear();
			}
		}

		public virtual void CheckForRhsElementsUsedOnLhs()
		{
			CheckForRhsElementsUsedOnLhs(new ComposedActionsBearer(this));
		}

		public static void CheckForRhsElementsUsedOnLhs(ActionsBearer bearer)
		{
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.CheckForRhsElementsUsedOnLhs();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.CheckForRhsElementsUsedOnLhs();
		}

		public virtual void SetDependencyLevelOfInterElementDependencies()
		{
			DependencyLevelOfInterElementDependencies = new ComposedActionsBearer(this);
		}

		public static void setDependencyLevelOfInterElementDependencies(ActionsBearer bearer)
		{
			foreach(Rule actionRule in bearer.ActionRules)
				actionRule.SetDependencyLevelOfInterElementDependencies();
			foreach(Rule subpatternRule in bearer.SubpatternRules)
				subpatternRule.SetDependencyLevelOfInterElementDependencies();
		}

		public virtual void CheckForParallelizedModelIfParallelizedActionExists()
		{
			CheckForParallelizedModelIfParallelizedActionExists(new ComposedActionsBearer(this), ActionsGraphModel);
		}

		public static void CheckForParallelizedModelIfParallelizedActionExists(ActionsBearer bearer, Model model)
		{
			if(!model.AreFunctionsParallel())
			{
				foreach(Rule actionRule in bearer.ActionRules)
				{
					if(actionRule.Annotations.ContainsKey("parallelize"))
						error.Error(actionRule.Ident.Coords, "Parallelized matching is requested from the action "
								+ actionRule.Ident + ","
								+ " but parallelization is not requested in the model (\"for function[parallelize=true];\").");
				}
			}
		}
	}

}
