/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.security.MessageDigest;
import java.util.Collection;
import java.util.Collections;
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
		for(Rule actionRule : actionRules) {
			actionRule.pattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodes, alreadyDefinedEdges);
			alreadyDefinedNodes.clear();
			alreadyDefinedEdges.clear();
		}
		for(Rule subpatternRule : subpatternRules) {
			subpatternRule.pattern.ensureDirectlyNestingPatternContainsAllNonLocalElementsOfNestedPattern(
					alreadyDefinedNodes, alreadyDefinedEdges);
			alreadyDefinedNodes.clear();
			alreadyDefinedEdges.clear();
		}
	}
}
