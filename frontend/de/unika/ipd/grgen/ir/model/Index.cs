/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{
	using Ident = de.unika.ipd.grgen.ir.Ident;
	using Identifiable = de.unika.ipd.grgen.ir.Identifiable;

	/// <summary>
	/// An index, base class for attribute index and incidence index.
	/// </summary>
	public abstract class Index : Identifiable
	{
		/// <param name="name"> The name of the attribute index. </param>
		/// <param name="ident"> The identifier that identifies this object. </param>
		public Index(string name, Ident ident)
			: base(name, ident)
		{
		}
	}

}
