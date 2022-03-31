/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * Represents an IR object containing nested statements
 * (both top-level non-statement objects as well as block nesting statements).
 */
public interface NestingStatement
{
	public void addStatement(EvalStatement loopedStatement);
	public Collection<EvalStatement> getStatements();
}
