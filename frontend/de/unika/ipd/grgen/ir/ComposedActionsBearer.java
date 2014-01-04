/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.1
 * Copyright (C) 2003-2013 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;

import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.Procedure;

/**
 * Offers all the actions in the unit including all the packages for flat iteration.
 * TODO: offer this by implementing iterators instead of collection building
 */
public class ComposedActionsBearer implements ActionsBearer {
	Unit unit;

	Collection<Rule> subpatRules;
	Collection<Rule> rules;
	Collection<FilterFunction> filterFunctions;
	Collection<Function> functions;
	Collection<Procedure> procedures;
	Collection<Sequence> sequences;
	
	public ComposedActionsBearer(Unit unit) {
		this.unit = unit;
	}
	
	public Collection<Rule> getSubpatternRules() {
		if(subpatRules==null) {
			ArrayList<Rule> subpatRules = new ArrayList<Rule>(unit.getSubpatternRules());
			for(ActionsBearer p : unit.getPackages()) {
				subpatRules.addAll(p.getSubpatternRules());
			}
			this.subpatRules = Collections.unmodifiableCollection(subpatRules);
		}
		return subpatRules;
	}

	public Collection<Rule> getActionRules() {
		if(rules==null) {
			ArrayList<Rule> rules = new ArrayList<Rule>(unit.getActionRules());
			for(ActionsBearer p : unit.getPackages()) {
				rules.addAll(p.getActionRules());
			}
			this.rules = Collections.unmodifiableCollection(rules);
		}
		return rules;
	}
	
	public Collection<FilterFunction> getFilterFunctions() {
		if(filterFunctions==null) {
			ArrayList<FilterFunction> filterFunctions = new ArrayList<FilterFunction>(unit.getFilterFunctions());
			for(ActionsBearer p : unit.getPackages()) {
				filterFunctions.addAll(p.getFilterFunctions());
			}
			this.filterFunctions = Collections.unmodifiableCollection(filterFunctions);
		}
		return filterFunctions;
	}
	
	public Collection<Function> getFunctions() {
		if(functions==null) {
			ArrayList<Function> functions = new ArrayList<Function>(unit.getFunctions());
			for(ActionsBearer p : unit.getPackages()) {
				functions.addAll(p.getFunctions());
			}
			this.functions = Collections.unmodifiableCollection(functions);
		}
		return functions;
	}
	
	public Collection<Procedure> getProcedures() {
		if(procedures==null) {
			ArrayList<Procedure> procedures = new ArrayList<Procedure>(unit.getProcedures());
			for(ActionsBearer p : unit.getPackages()) {
				procedures.addAll(p.getProcedures());
			}
			this.procedures = Collections.unmodifiableCollection(procedures);
		}
		return procedures;
	}
	
	public Collection<Sequence> getSequences() {
		if(sequences==null) {
			ArrayList<Sequence> sequences = new ArrayList<Sequence>(unit.getSequences());
			for(ActionsBearer p : unit.getPackages()) {
				sequences.addAll(p.getSequences());
			}
			this.sequences = Collections.unmodifiableCollection(sequences);
		}
		return sequences;
	}
}
