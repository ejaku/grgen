/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.PrimitiveType;
import de.unika.ipd.grgen.ir.exprevals.Procedure;

/**
 * A package type, for packages from the actions (in contrast to the models).
 */
public class PackageActionType extends PrimitiveType implements ActionsBearer {
	private final List<Rule> subpatternRules = new LinkedList<Rule>();

	private final List<Rule> actionRules = new LinkedList<Rule>();

	private final List<FilterFunction> filterFunctions = new LinkedList<FilterFunction>();

	private final List<Function> functions = new LinkedList<Function>();

	private final List<Procedure> procedures = new LinkedList<Procedure>();

	private final List<Sequence> sequences = new LinkedList<Sequence>();

	/** Make a new package action type.
	 *  @param ident The identifier of this package. */
	public PackageActionType(Ident ident) {
		super("package action type", ident);
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
}
