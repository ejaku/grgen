/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{
using Ident = de.unika.ipd.grgen.ir.Ident;
using Identifiable = de.unika.ipd.grgen.ir.Identifiable;

public class AlternativeReplacement : Identifiable, OrderedReplacement
{
	internal Alternative alternative;

	public AlternativeReplacement(string name, Ident ident,
			Alternative alternative)
		: base(name, ident)
	{
		this.alternative = alternative;
	}

	public virtual Alternative Alternative
	{
		get
		{
			return alternative;
		}
	}
}

}
