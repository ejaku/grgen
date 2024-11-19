/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.0
 * Copyright (C) 2003-2024 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.executable;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;

import de.unika.ipd.grgen.ir.Ident;
import de.unika.ipd.grgen.ir.NestingStatement;
import de.unika.ipd.grgen.ir.stmt.EvalStatement;

/**
 * An internal filter function
 * (is a top-level object that contains nested statements).
 */
public class FilterFunctionInternal extends FilterFunction implements NestingStatement
{
	/** The computation statements */
	private List<EvalStatement> computationStatements = new LinkedList<EvalStatement>();

	public FilterFunctionInternal(String name, Ident ident)
	{
		super(name, ident);
	}

	/** Add a computation statement to the filter function. */
	@Override
	public void addStatement(EvalStatement eval)
	{
		computationStatements.add(eval);
	}

	/** Get all computation statements of this filter function. */
	@Override
	public List<EvalStatement> getStatements()
	{
		return Collections.unmodifiableList(computationStatements);
	}
}
