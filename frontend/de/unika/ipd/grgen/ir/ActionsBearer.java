/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

import de.unika.ipd.grgen.ir.exprevals.Function;
import de.unika.ipd.grgen.ir.exprevals.Procedure;

/**
 * A type bearing all the different actions available in the rules language.
 */
public interface ActionsBearer {
	public Collection<Rule> getSubpatternRules();
	public Collection<Rule> getActionRules();
	public Collection<FilterFunction> getFilterFunctions();
	public Collection<Function> getFunctions();
	public Collection<Procedure> getProcedures();
	public Collection<Sequence> getSequences();
}
