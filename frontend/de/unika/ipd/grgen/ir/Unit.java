/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir;

import java.security.MessageDigest;
import java.util.ArrayDeque;
import java.util.Collection;
import java.util.Collections;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

import de.unika.ipd.grgen.ir.exprevals.*;
import de.unika.ipd.grgen.util.Util;

/**
 * A unit with all declared entities
 */
public class Unit extends IR implements ActionsBearer {

	private final List<Model> models = new LinkedList<Model>();

	private final List<Rule> subpatternRules = new LinkedList<Rule>();

	private final List<Rule> actionRules = new LinkedList<Rule>();

	private final List<FilterFunction> filterFunctions = new LinkedList<FilterFunction>();

	private final List<Function> functions = new LinkedList<Function>();

	private final List<Procedure> procedures = new LinkedList<Procedure>();

	private final List<Sequence> sequences = new LinkedList<Sequence>();
	
	private final List<PackageActionType> packages = new LinkedList<PackageActionType>();

	private String digest = "";

	private boolean digestValid = false;

	/** The unit name of this unit. */
	private String unitName;

	/** The source filename of this unit. */
	private String filename;
	
	private boolean isToBeParallelizedActionExisting = false;

	
	public Unit(String unitName, String filename) {
		super("unit");
		this.unitName = unitName;
		this.filename = filename;
	}

	/** Add a model to the unit. */
	public void addModel(Model model) {
		models.add(model);
		digestValid = false;
	}

	/** @return The type model of this unit. */
	public Collection<Model> getModels() {
		return Collections.unmodifiableCollection(models);
	}

	/** Add a subpattern-rule to the unit. */
	public void addSubpatternRule(Rule subpatternRule) {
		subpatternRules.add(subpatternRule);
	}

	public Collection<Rule> getSubpatternRules() {
		return Collections.unmodifiableCollection(subpatternRules);
	}

	/** Add an action-rule to the unit. */
	public void addActionRule(Rule actionRule) {
		actionRules.add(actionRule);
	}

	public Collection<Rule> getActionRules() {
		return Collections.unmodifiableCollection(actionRules);
	}
	
	/** Add a filter function to the unit. */
	public void addFilterFunction(FilterFunction filterFunction) {
		filterFunctions.add(filterFunction);
	}

	public Collection<FilterFunction> getFilterFunctions() {
		return Collections.unmodifiableCollection(filterFunctions);
	}
	
	/** Add a function to the unit. */
	public void addFunction(Function function) {
		functions.add(function);
	}

	public Collection<Function> getFunctions() {
		return Collections.unmodifiableCollection(functions);
	}

	/** Add a procedure to the unit. */
	public void addProcedure(Procedure procedure) {
		procedures.add(procedure);
	}

	public Collection<Procedure> getProcedures() {
		return Collections.unmodifiableCollection(procedures);
	}

	/** Add a sequence to the unit. */
	public void addSequence(Sequence sequence) {
		sequences.add(sequence);
	}

	public Collection<Sequence> getSequences() {
		return Collections.unmodifiableCollection(sequences);
	}

	/** Add a package to the unit. */
	public void addPackage(PackageActionType packageActionType) {
		packages.add(packageActionType);
	}

	public Collection<PackageActionType> getPackages() {
		return Collections.unmodifiableCollection(packages);
	}

	public Model getActionsGraphModel() {
		return models.get(0);
	}

	public String getActionsGraphModelName() {
		return models.get(0).getIdent().toString();
	}

	/** @return The unit name of this unit. */
	public String getUnitName() {
		return unitName;
	}

	/** @return The source filename corresponding to this unit. */
	public String getFilename() {
		return filename;
	}

	public void toBeParallelizedActionIsExisting() {
		isToBeParallelizedActionExisting = true;
	}

	public boolean isToBeParallelizedActionExisting() {
		return isToBeParallelizedActionExisting;
	}

	public void addFields(Map<String, Object> fields) {
		super.addFields(fields);
		fields.put("models", models.iterator());
	}

	protected void canonicalizeLocal() {
//		Collections.sort(models, Identifiable.COMPARATOR);
//		Collections.sort(actions, Identifiable.COMPARATOR);
//		Collections.sort(subpatterns, Identifiable.COMPARATOR);

		for(Iterator<Model> it = models.iterator(); it.hasNext();) {
			Model model = it.next();
			model.canonicalize();
		}
	}

	public void addToDigest(StringBuffer sb) {
		for(Iterator<Model> it = models.iterator(); it.hasNext();) {
			Model model = it.next();
			model.addToDigest(sb);
		}
	}

	/** Build the digest string of this type model. */
	private void buildDigest() {
		StringBuffer sb = new StringBuffer();

		addToDigest(sb);

		try {
			byte[] serialData = sb.toString().getBytes("US-ASCII");
			MessageDigest md = MessageDigest.getInstance("MD5");
			digest = Util.hexString(md.digest(serialData));
		} catch (Exception e) {
			e.printStackTrace(System.err);
			digest = "<error>";
		}

		digestValid = true;
	}

	/** Get the digest of this type model. */
	public final String getTypeDigest() {
		if(!digestValid)
			buildDigest();

		return digest;
	}

	public void postPatchIR() {
		for(Model model : models) {
			postPatchIR(model);
			for(PackageType pt : model.getPackages()) {
				postPatchIR(pt);
			}
		}
	}

	public static void postPatchIR(NodeEdgeEnumBearer bearer) {
		// deferred step that has to be done after IR was built
		// filling in transitive members for inheritance types
		// can't be called during IR building because of dependencies (node/edge attributes of subtypes)
		for(InheritanceType type : bearer.getNodeTypes()) {
			type.getAllMembers(); // checks overwriting of attributes
		}
		for(InheritanceType type : bearer.getEdgeTypes()) {
			type.getAllMembers(); // checks overwriting of attributes
		}
	}

	public void checkForEmptyPatternsInIterateds()
	{
		checkForEmptyPatternsInIterateds(new ComposedActionsBearer(this));
	}

	public static void checkForEmptyPatternsInIterateds(ActionsBearer bearer)
	{
		// iterateds don't terminate if they match an empty pattern again and again
		// so we compute maybe empty/epsilon patterns and emit error messages if they occur inside an iterated
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.pattern.checkForEmptyPatternsInIterateds();
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.pattern.checkForEmptyPatternsInIterateds();
		}
	}

	public void checkForEmptySubpatternRecursions()
	{
		checkForEmptySubpatternRecursions(new ComposedActionsBearer(this));
	}

	public static void checkForEmptySubpatternRecursions(ActionsBearer bearer)
	{
		// subpatterns may not terminate if there is a recursion only involving empty terminal graphs
		// so we compute the subpattern derivation paths containing only empty graphs
		// and emit error messages if they contain a subpattern calling itself
		HashSet<PatternGraph> subpatternsAlreadyVisited = new HashSet<PatternGraph>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternsAlreadyVisited.add(subpatternRule.pattern);
			subpatternRule.pattern.checkForEmptySubpatternRecursions(subpatternsAlreadyVisited);
			subpatternsAlreadyVisited.clear();
		}
	}

	public void checkForNeverSucceedingSubpatternRecursions()
	{
		checkForNeverSucceedingSubpatternRecursions(new ComposedActionsBearer(this));
	}

	public static void checkForNeverSucceedingSubpatternRecursions(ActionsBearer bearer)
	{
		// matching a subpattern never terminates successfully 
		// if there is no terminal pattern on any of its alternative branches/bodies
		// emit an error message in this case (it might be the case more often, this is what we can tell for sure)
		HashSet<PatternGraph> subpatternsAlreadyVisited = new HashSet<PatternGraph>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternsAlreadyVisited.add(subpatternRule.pattern);
			if(subpatternRule.pattern.isNeverTerminatingSuccessfully(subpatternsAlreadyVisited))
			{
				error.warning(subpatternRule.getIdent().getCoords(), "Matching the subpattern " +subpatternRule.getIdent() + " will never terminate successfully (endless recursion on any path, only (potentially) terminated by failing matching)");
			}
			subpatternsAlreadyVisited.clear();
		}
	}

	public void checkForMultipleRetypes()
	{
		checkForMultipleRetypes(new ComposedActionsBearer(this));
	}

	public static void checkForMultipleRetypes(ActionsBearer bearer)
	{
		// an iterated may cause an element matched once to be retyped multiple times
		// check for this situation (collect elements on descending over nesting structure, 
		// spark checker on visiting an iterated to check its local and nested content)
		HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
		HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
		for(Rule actionRule : bearer.getActionRules()) {
			if(actionRule.getRight()!=null) {
				actionRule.pattern.checkForMultipleRetypes(
						alreadyDefinedNodes, alreadyDefinedEdges, actionRule.getRight());
				alreadyDefinedNodes.clear();
				alreadyDefinedEdges.clear();
			}
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			if(subpatternRule.getRight()!=null) {
				subpatternRule.pattern.checkForMultipleRetypes(
						alreadyDefinedNodes, alreadyDefinedEdges, subpatternRule.getRight());
				alreadyDefinedNodes.clear();
				alreadyDefinedEdges.clear();
			}
		}
	}

	public void checkForMultipleDeletesOrRetypes()
	{
		checkForMultipleDeletesOrRetypes(new ComposedActionsBearer(this));
	}

	public static void checkForMultipleDeletesOrRetypes(ActionsBearer bearer)
	{
		// an element may be deleted/retyped several times at different nesting levels
		// or even in a subpattern called and outside of this subpattern
		// so we check that on all nesting paths there is only one delete/retype occuring
		// and emit error messages if this is not the case
		
		// initial step: compute the subpatterns where a subpattern is used
		HashMap<Rule, HashSet<Rule>> subpatternsDefToUse = 
			new HashMap<Rule, HashSet<Rule>>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternsDefToUse.put(subpatternRule, new HashSet<Rule>());
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.computeUsageDependencies(subpatternsDefToUse, subpatternRule);
		}
		// then: compute which parameters may get deleted/retyped, 
		// if this information changed from before, the used subpatterns are added to a worklist
		// which is processed step by step until it gets empty due to a fixpoint being reached
		HashMap<Rule, HashMap<Entity, Rule>> subpatternsToParametersToTheirDeletingOrRetypingPattern = 
			new HashMap<Rule, HashMap<Entity, Rule>>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternsToParametersToTheirDeletingOrRetypingPattern.put(subpatternRule, new HashMap<Entity, Rule>());
			for(Entity param : subpatternRule.getParameters()) {
				subpatternsToParametersToTheirDeletingOrRetypingPattern.get(subpatternRule).put(param, null);
			}
		}
		ArrayDeque<Rule> subpatternsToProcess = new ArrayDeque<Rule>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternsToProcess.add(subpatternRule);
		}
		while(subpatternsToProcess.size()>0) {
			Rule subpattern = subpatternsToProcess.remove();
			boolean changed = subpattern.checkForMultipleDeletesOrRetypes(new HashMap<Entity, Rule>(),
					subpatternsToParametersToTheirDeletingOrRetypingPattern);
			if(changed) {
				for(Rule needsRecomputation : subpatternsDefToUse.get(subpattern)) {
					if(!subpatternsToProcess.contains(needsRecomputation)) {
						subpatternsToProcess.add(needsRecomputation);
					}
				}
			}
		}
		// finally: do the computation on the (non-callable) rules
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.checkForMultipleDeletesOrRetypes(new HashMap<Entity, Rule>(), 
					subpatternsToParametersToTheirDeletingOrRetypingPattern);
		}
	}

	public void transmitExecUsageToRules()
	{
		transmitExecUsageToRules(new ComposedActionsBearer(this));
	}

	public static void transmitExecUsageToRules(ActionsBearer bearer)
	{
		// if an alternative, iterated, or subpattern used from a rule employs an exec,
		// the execs are not executed directly but added to a to-be-executed-queue;
		// at the end the root rule must execute this queue.
		// determine for which root rules this is the case, 
		// so we generate the queue-executing code only for them
		
		// step 1a: compute the subpatterns and rules where a subpattern is used
		HashMap<Rule, HashSet<Rule>> defToUse = new HashMap<Rule, HashSet<Rule>>();
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			defToUse.put(subpatternRule, new HashSet<Rule>());
		}
		for(Rule actionRule : bearer.getActionRules()) {
			defToUse.put(actionRule, new HashSet<Rule>());
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.computeUsageDependencies(defToUse, subpatternRule);
		}
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.computeUsageDependencies(defToUse, actionRule);
		}
		// step 1b: compute which subpatterns and rules use non-direct execs (alternative,iterated,usage of subpattern with exec)
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.mightThereBeDeferredExecs = subpatternRule.isUsingNonDirectExec(false);
		}
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.mightThereBeDeferredExecs = actionRule.isUsingNonDirectExec(true);
		}
		// step 2: propagate the exec-using to the subpatterns and rules containing the exec-using-subpatterns
		// until nothing changes, i.e. a fixpoint was reached
		boolean changed;
		do {
			changed = false;
			for(Rule subpatternRule : bearer.getSubpatternRules()) {
				if(subpatternRule.mightThereBeDeferredExecs) {
					for(Rule toBeMarkedAsNonDirectExecUser : defToUse.get(subpatternRule)) {
						if(!toBeMarkedAsNonDirectExecUser.mightThereBeDeferredExecs) {
							toBeMarkedAsNonDirectExecUser.mightThereBeDeferredExecs = true;
							changed = true;
						}
					}
				}
			}
		} while(changed);
		
		// final step: remove the information again from the subpatterns to prevent the exec-dequeing code being called from there
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.mightThereBeDeferredExecs = false;
		}
	}

	public void resolvePatternLockedModifier()
	{
		resolvePatternLockedModifier(new ComposedActionsBearer(this));
	}

	public static void resolvePatternLockedModifier(ActionsBearer bearer) {
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.pattern.resolvePatternLockedModifier();
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.pattern.resolvePatternLockedModifier();
		}
	}

	public void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern()
	{
		ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(new ComposedActionsBearer(this));
	}

	public static void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(ActionsBearer bearer) {
		HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
		HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
		HashSet<Variable> alreadyDefinedVariables = new HashSet<Variable>();
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.pattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodes, alreadyDefinedEdges, alreadyDefinedVariables,
					actionRule.getRight());
			alreadyDefinedNodes.clear();
			alreadyDefinedEdges.clear();
			alreadyDefinedVariables.clear();
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.pattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodes, alreadyDefinedEdges, alreadyDefinedVariables,
					subpatternRule.getRight());
			alreadyDefinedNodes.clear();
			alreadyDefinedEdges.clear();
			alreadyDefinedVariables.clear();
		}
	}
	
	public void checkForRhsElementsUsedOnLhs()
	{
		checkForRhsElementsUsedOnLhs(new ComposedActionsBearer(this));
	}

	public static void checkForRhsElementsUsedOnLhs(ActionsBearer bearer)
	{
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.checkForRhsElementsUsedOnLhs();
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.checkForRhsElementsUsedOnLhs();
		}
	}

	public void setDependencyLevelOfInterElementDependencies()
	{
		setDependencyLevelOfInterElementDependencies(new ComposedActionsBearer(this));
	}

	public static void setDependencyLevelOfInterElementDependencies(ActionsBearer bearer) {
		for(Rule actionRule : bearer.getActionRules()) {
			actionRule.setDependencyLevelOfInterElementDependencies();
		}
		for(Rule subpatternRule : bearer.getSubpatternRules()) {
			subpatternRule.setDependencyLevelOfInterElementDependencies();
		}
	}
}
