/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * An internal match class filter function
 * (is a top-level object that contains nested statements).
 */
public class MatchClassFilterFunctionInternal extends MatchClassFilterFunction implements NestingStatement
{
	/** The computation statements */
	private List<EvalStatement> computationStatements = new LinkedList<EvalStatement>();

	public MatchClassFilterFunctionInternal(String name, Ident ident)
	{
		super(name, ident);
	}

	/** Add a computation statement to the match class filter function. */
	@Override
	public void addStatement(EvalStatement eval)
	{
		computationStatements.add(eval);
	}

	/** Get all computation statements of this match class filter function. */
	@Override
	public List<EvalStatement> getStatements()
	{
		return Collections.unmodifiableList(computationStatements);
	}
}
