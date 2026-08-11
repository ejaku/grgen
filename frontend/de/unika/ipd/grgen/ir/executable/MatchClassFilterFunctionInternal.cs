/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.executable
{

using System.Collections.Generic;

using Ident = de.unika.ipd.grgen.ir.Ident;
using NestingStatement = de.unika.ipd.grgen.ir.NestingStatement;
using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;

/// <summary>
/// An internal match class filter function
/// (is a top-level object that contains nested statements).
/// </summary>
public class MatchClassFilterFunctionInternal : MatchClassFilterFunction, NestingStatement
{
	/// <summary>
	/// The computation statements </summary>
	private List<EvalStatement> computationStatements = new List<EvalStatement>();

	public MatchClassFilterFunctionInternal(string name, Ident ident)
		: base(name, ident)
	{
	}

	/// <summary>
	/// Add a computation statement to the match class filter function. </summary>
	public virtual void AddStatement(EvalStatement eval)
	{
		computationStatements.Add(eval);
	}

	/// <summary>
	/// Get all computation statements of this match class filter function. </summary>
	public virtual ICollection<EvalStatement> Statements
	{
		get
		{
			return computationStatements.AsReadOnly();
		}
	}
}

}
