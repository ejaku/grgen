/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir
{
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A variable declared inside an "exec" statement containing nodes, edges or primitive types.
/// (due to being declared in the sequence, it can't be a defToBeYieldedTo variable, only entities from the outside can be)
/// </summary>
public class ExecVariable : Entity
{
	public ExecVariable(string name, Ident ident, Type type, int context)
		: base(name, ident, type, false, false, context)
	{
	}

	public override string Kind
	{
		get
		{
		return "exec variable";
		}
	}
}

}
