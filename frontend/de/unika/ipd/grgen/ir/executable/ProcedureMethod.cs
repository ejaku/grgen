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
using Ident = de.unika.ipd.grgen.ir.Ident;
using Type = de.unika.ipd.grgen.ir.type.Type;

/// <summary>
/// A procedure method.
/// </summary>
public class ProcedureMethod : Procedure
{
	/// <summary>
	/// The owner of the procedure method. </summary>
	protected internal Type owner = null;

	public ProcedureMethod(string name, Ident ident)
		: base(name, ident)
	{
	}

	public virtual Type Owner
	{
		get
		{
			return owner;
		}
		set
		{
			owner = value;
		}
	}

}

}
