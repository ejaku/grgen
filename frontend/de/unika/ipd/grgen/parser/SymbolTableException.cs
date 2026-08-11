/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// SymbolTableException.java
/// 
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser
{
	using System;

	/// <summary>
	/// A symbol table exception.
	/// </summary>
	public class SymbolTableException : Exception
	{
		private const long serialVersionUID = -7291849597287733435L;

		public SymbolTableException(string text)
			: base(text)
		{
		}

		public SymbolTableException(Coords coords, string text)
			: this(coords + ": " + text)
		{
		}
	}

}
