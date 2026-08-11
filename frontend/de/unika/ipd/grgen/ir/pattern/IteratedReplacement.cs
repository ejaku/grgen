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
	using Rule = de.unika.ipd.grgen.ir.executable.Rule;

	public class IteratedReplacement : Identifiable, OrderedReplacement
	{
		internal Rule iterated;

		public IteratedReplacement(string name, Ident ident,
				Rule iterated)
			: base(name, ident)
		{
			this.iterated = iterated;
		}

		public virtual Rule Iterated
		{
			get
			{
				return iterated;
			}
		}
	}

}
