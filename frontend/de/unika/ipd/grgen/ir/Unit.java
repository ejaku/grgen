/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
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

import de.unika.ipd.grgen.util.Util;

/**
 * A unit with all declared entities
 */
public class Unit extends IR {

	private final List<Rule> actionRules = new LinkedList<Rule>();

	private final List<Rule> subpatternRules = new LinkedList<Rule>();

	private final List<Model> models = new LinkedList<Model>();

	private String digest = "";

	private boolean digestValid = false;

	/** The unit name of this unit. */
	private String unitName;

	/** The source filename of this unit. */
	private String filename;

	public Unit(String unitName, String filename) {
		super("unit");
		this.unitName = unitName;
		this.filename = filename;
	}

	/** Add an action-rule to the unit. */
	public void addActionRule(Rule actionRule) {
		actionRules.add(actionRule);
	}

	public Collection<Rule> getActionRules() {
		return Collections.unmodifiableCollection(actionRules);
	}

	/** Add a subpattern-rule to the unit. */
	public void addSubpatternRule(Rule subpatternRule) {
		subpatternRules.add(subpatternRule);
	}

	public Collection<Rule> getSubpatternRules() {
		return Collections.unmodifiableCollection(subpatternRules);
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

	void addToDigest(StringBuffer sb) {
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

	public void checkForEmptyPatternsInIterateds()
	{
		// iterateds don't terminate if they match an empty pattern again and again
		// so we compute maybe empty/epsilon patterns and emit error messages if they occur inside an iterated
		for(Rule actionRule : actionRules) {
			actionRule.pattern.checkForEmptyPatternsInIterateds();
		}
		for(Rule subpatternRule : subpatternRules) {
			subpatternRule.pattern.checkForEmptyPatternsInIterateds();
		}
	}

	public void checkForEmptySubpatternRecursions()
	{
		// subpatterns may not terminate if there is a recursion only involving empty terminal graphs
		// so we compute the subpattern derivation paths containing only empty graphs
		// and emit error messages if they contain a subpattern calling itself
		HashSet<PatternGraph> subpatternsAlreadyVisited = new HashSet<PatternGraph>();
		for(Rule subpatternRule : subpatternRules) {
			subpatternsAlreadyVisited.add(subpatternRule.pattern);
			subpatternRule.pattern.checkForEmptySubpatternRecursions(subpatternsAlreadyVisited);
			subpatternsAlreadyVisited.clear();
		}
	}
	
	public void checkForNeverSucceedingSubpatternRecursions()
	{
		// matching a subpattern never terminates successfully 
		// if there is no terminal pattern on any of its alternative branches/bodies
		// emit an error message in this case (it might be the case more often, this is what we can tell for sure)
		HashSet<PatternGraph> subpatternsAlreadyVisited = new HashSet<PatternGraph>();
		for(Rule subpatternRule : subpatternRules) {
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
		// an iterated may cause an element matched once to be retyped multiple times
		// check for this situation (collect elements on descending over nesting structure, 
		// spark checker on visiting an iterated to check its local and nested content)
		HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
		HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
		for(Rule actionRule : actionRules) {
			if(actionRule.getRight()!=null) {
				actionRule.pattern.checkForMultipleRetypes(
						alreadyDefinedNodes, alreadyDefinedEdges, actionRule.getRight());
				alreadyDefinedNodes.clear();
				alreadyDefinedEdges.clear();
			}
		}
		for(Rule subpatternRule : subpatternRules) {
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
		// an element may be deleted/retyped several times at different nesting levels
		// or even in a subpattern called and outside of this subpattern
		// so we check that on all nesting paths there is only one delete/retype occuring
		// and emit error messages if this is not the case
		
		// initial step: compute the subpatterns where a subpattern is used
		HashMap<Rule, HashSet<Rule>> subpatternsDefToUse = 
			new HashMap<Rule, HashSet<Rule>>();
		for(Rule subpatternRule : subpatternRules) {
			subpatternsDefToUse.put(subpatternRule, new HashSet<Rule>());
		}
		for(Rule subpatternRule : subpatternRules) {
			subpatternRule.computeUsageDependencies(subpatternsDefToUse, subpatternRule);
		}
		// then: compute which parameters may get deleted/retyped, 
		// if this information changed from before, the used subpatterns are added to a worklist
		// which is processed step by step until it gets empty due to a fixpoint being reached
		HashMap<Rule, HashMap<Entity, Rule>> subpatternsToParametersToTheirDeletingOrRetypingPattern = 
			new HashMap<Rule, HashMap<Entity, Rule>>();
		for(Rule subpatternRule : subpatternRules) {
			subpatternsToParametersToTheirDeletingOrRetypingPattern.put(subpatternRule, new HashMap<Entity, Rule>());
			for(Entity param : subpatternRule.getParameters()) {
				subpatternsToParametersToTheirDeletingOrRetypingPattern.get(subpatternRule).put(param, null);
			}
		}
		ArrayDeque<Rule> subpatternsToProcess = new ArrayDeque<Rule>();
		for(Rule subpatternRule : subpatternRules) {
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
		for(Rule actionRule : actionRules) {
			actionRule.checkForMultipleDeletesOrRetypes(new HashMap<Entity, Rule>(), 
					subpatternsToParametersToTheirDeletingOrRetypingPattern);
		}
	}

	public void resolvePatternLockedModifier() {
		for(Rule actionRule : actionRules) {
			actionRule.pattern.resolvePatternLockedModifier();
		}
		for(Rule subpatternRule : subpatternRules) {
			subpatternRule.pattern.resolvePatternLockedModifier();
		}
	}

	public void ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern() {
		HashSet<Node> alreadyDefinedNodes = new HashSet<Node>();
		HashSet<Edge> alreadyDefinedEdges = new HashSet<Edge>();
		HashSet<Variable> alreadyDefinedVariables = new HashSet<Variable>();
		for(Rule actionRule : actionRules) {
			actionRule.pattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodes, alreadyDefinedEdges, alreadyDefinedVariables,
					actionRule.getRight());
			alreadyDefinedNodes.clear();
			alreadyDefinedEdges.clear();
			alreadyDefinedVariables.clear();
		}
		for(Rule subpatternRule : subpatternRules) {
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
		for(Rule actionRule : actionRules) {
			actionRule.checkForRhsElementsUsedOnLhs();
		}
		for(Rule subpatternRule : subpatternRules) {
			subpatternRule.checkForRhsElementsUsedOnLhs();
		}
	}
}
