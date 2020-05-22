/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

import de.unika.ipd.grgen.ir.executable.FilterFunction;
import de.unika.ipd.grgen.ir.executable.Function;
import de.unika.ipd.grgen.ir.executable.MatchClassFilterFunction;
import de.unika.ipd.grgen.ir.executable.Procedure;
import de.unika.ipd.grgen.ir.executable.Rule;
import de.unika.ipd.grgen.ir.executable.Sequence;
import de.unika.ipd.grgen.ir.type.DefinedMatchType;

/**
 * A type bearing all the different actions available in the rules language.
 */
public interface ActionsBearer
{
	public Collection<Rule> getSubpatternRules();

	public Collection<Rule> getActionRules();

	public Collection<FilterFunction> getFilterFunctions();

	public Collection<DefinedMatchType> getMatchClasses();

	public Collection<MatchClassFilterFunction> getMatchClassFilterFunctions();

	public Collection<Function> getFunctions();

	public Collection<Procedure> getProcedures();

	public Collection<Sequence> getSequences();
}
