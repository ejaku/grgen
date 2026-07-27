/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.List;

import de.unika.ipd.grgen.ir.executable.FilterFunction;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.executable.Sequence;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;

/**
 * Offers all the actions in the unit including all the packages for flat iteration.
 * TODO: offer this by implementing iterators instead of collection building
 */
public class ComposedActionsBearer implements ActionsBearer
{
	Unit unit;

	List<Rule> subpatRules;
	List<Rule> rules;
	List<FilterFunction> filterFunctions;
	List<DefinedMatchType> matchClasses;
	List<MatchClassFilterFunction> matchClassFilterFunctions;
	List<Function> functions;
	List<Procedure> procedures;
	List<Sequence> sequences;

	public ComposedActionsBearer(Unit unit)
	{
		this.unit = unit;
	}

	@Override
	public Collection<Rule> getSubpatternRules()
	{
		if(subpatRules == null) {
			ArrayList<Rule> subpatRules = new ArrayList<Rule>(unit.getSubpatternRules());
			for(ActionsBearer p : unit.getPackages()) {
				subpatRules.addAll(p.getSubpatternRules());
			}
			this.subpatRules = Collections.unmodifiableList(subpatRules);
		}
		return subpatRules;
	}

	@Override
	public Collection<Rule> getActionRules()
	{
		if(rules == null) {
			ArrayList<Rule> rules = new ArrayList<Rule>(unit.getActionRules());
			for(ActionsBearer p : unit.getPackages()) {
				rules.addAll(p.getActionRules());
			}
			this.rules = Collections.unmodifiableList(rules);
		}
		return rules;
	}

	@Override
	public Collection<FilterFunction> getFilterFunctions()
	{
		if(filterFunctions == null) {
			ArrayList<FilterFunction> filterFunctions = new ArrayList<FilterFunction>(unit.getFilterFunctions());
			for(ActionsBearer p : unit.getPackages()) {
				filterFunctions.addAll(p.getFilterFunctions());
			}
			this.filterFunctions = Collections.unmodifiableList(filterFunctions);
		}
		return filterFunctions;
	}

	@Override
	public Collection<DefinedMatchType> getMatchClasses()
	{
		if(matchClasses == null) {
			ArrayList<DefinedMatchType> matchClasses = new ArrayList<DefinedMatchType>(unit.getMatchClasses());
			for(ActionsBearer p : unit.getPackages()) {
				matchClasses.addAll(p.getMatchClasses());
			}
			this.matchClasses = Collections.unmodifiableList(matchClasses);
		}
		return matchClasses;
	}

	@Override
	public Collection<MatchClassFilterFunction> getMatchClassFilterFunctions()
	{
		if(matchClassFilterFunctions == null) {
			ArrayList<MatchClassFilterFunction> matchClassFilterFunctions =
					new ArrayList<MatchClassFilterFunction>(unit.getMatchClassFilterFunctions());
			for(ActionsBearer p : unit.getPackages()) {
				matchClassFilterFunctions.addAll(p.getMatchClassFilterFunctions());
			}
			this.matchClassFilterFunctions = Collections.unmodifiableList(matchClassFilterFunctions);
		}
		return matchClassFilterFunctions;
	}

	@Override
	public Collection<Function> getFunctions()
	{
		if(functions == null) {
			ArrayList<Function> functions = new ArrayList<Function>(unit.getFunctions());
			for(ActionsBearer p : unit.getPackages()) {
				functions.addAll(p.getFunctions());
			}
			this.functions = Collections.unmodifiableList(functions);
		}
		return functions;
	}

	@Override
	public Collection<Procedure> getProcedures()
	{
		if(procedures == null) {
			ArrayList<Procedure> procedures = new ArrayList<Procedure>(unit.getProcedures());
			for(ActionsBearer p : unit.getPackages()) {
				procedures.addAll(p.getProcedures());
			}
			this.procedures = Collections.unmodifiableList(procedures);
		}
		return procedures;
	}

	@Override
	public Collection<Sequence> getSequences()
	{
		if(sequences == null) {
			ArrayList<Sequence> sequences = new ArrayList<Sequence>(unit.getSequences());
			for(ActionsBearer p : unit.getPackages()) {
				sequences.addAll(p.getSequences());
			}
			this.sequences = Collections.unmodifiableList(sequences);
		}
		return sequences;
	}
}
