/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.type;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;

import de.unika.ipd.grgen.ir.ActionsBearer;
import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.executable.FilterFunction;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.executable.Sequence;
import de.unika.ipd.grgen.ir.type.basic.PrimitiveType;

/**
 * A package type, for packages from the actions (in contrast to the models).
 */
public class PackageActionType extends PrimitiveType implements ActionsBearer
{
	private final ArrayList<Rule> subpatternRules = new ArrayList<Rule>();

	private final ArrayList<Rule> actionRules = new ArrayList<Rule>();

	private final ArrayList<FilterFunction> filterFunctions = new ArrayList<FilterFunction>();

	private final ArrayList<DefinedMatchType> matchClasses = new ArrayList<DefinedMatchType>();

	private final ArrayList<MatchClassFilterFunction> matchClassFilterFunctions = new ArrayList<MatchClassFilterFunction>();

	private final ArrayList<Function> functions = new ArrayList<Function>();

	private final ArrayList<Procedure> procedures = new ArrayList<Procedure>();

	private final ArrayList<Sequence> sequences = new ArrayList<Sequence>();

	/** Make a new package action type.
	 *  @param ident The identifier of this package. */
	public PackageActionType(Ident ident)
	{
		super("package action type", ident);
	}

	/** Add a subpattern-rule to the unit. */
	public void addSubpatternRule(Rule subpatternRule)
	{
		subpatternRules.add(subpatternRule);
	}

	@Override
	public Collection<Rule> getSubpatternRules()
	{
		return Collections.unmodifiableList(subpatternRules);
	}

	/** Add an action-rule to the unit. */
	public void addActionRule(Rule actionRule)
	{
		actionRules.add(actionRule);
	}

	@Override
	public Collection<Rule> getActionRules()
	{
		return Collections.unmodifiableList(actionRules);
	}

	/** Add a filter function to the unit. */
	public void addFilterFunction(FilterFunction filterFunction)
	{
		filterFunctions.add(filterFunction);
	}

	@Override
	public Collection<FilterFunction> getFilterFunctions()
	{
		return Collections.unmodifiableList(filterFunctions);
	}

	/** Add a match class to the unit. */
	public void addMatchClass(DefinedMatchType matchClass)
	{
		matchClasses.add(matchClass);
	}

	@Override
	public Collection<DefinedMatchType> getMatchClasses()
	{
		return Collections.unmodifiableList(matchClasses);
	}

	/** Add a match filter function to the unit. */
	public void addMatchClassFilterFunction(MatchClassFilterFunction matchClassFilterFunction)
	{
		matchClassFilterFunctions.add(matchClassFilterFunction);
	}

	@Override
	public Collection<MatchClassFilterFunction> getMatchClassFilterFunctions()
	{
		return Collections.unmodifiableList(matchClassFilterFunctions);
	}

	/** Add a function to the unit. */
	public void addFunction(Function function)
	{
		functions.add(function);
	}

	@Override
	public Collection<Function> getFunctions()
	{
		return Collections.unmodifiableList(functions);
	}

	/** Add a procedure to the unit. */
	public void addProcedure(Procedure procedure)
	{
		procedures.add(procedure);
	}

	@Override
	public Collection<Procedure> getProcedures()
	{
		return Collections.unmodifiableList(procedures);
	}

	/** Add a sequence to the unit. */
	public void addSequence(Sequence sequence)
	{
		sequences.add(sequence);
	}

	@Override
	public Collection<Sequence> getSequences()
	{
		return Collections.unmodifiableList(sequences);
	}
}
