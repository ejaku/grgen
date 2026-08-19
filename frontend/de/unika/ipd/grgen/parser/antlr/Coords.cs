/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.parser.antlr
{
	using ICharStream = Antlr.Runtime.ICharStream;

	/// <summary>
	/// Coordinates more suitable for an ANTLR parser.
	/// </summary>
	public class Coords : de.unika.ipd.grgen.parser.Coords
	{
		/// <summary>
		/// Construct coordinates from an ANTLR token. </summary>
		/// <param name="tok"> The ANTLR token. </param>
		public Coords(Antlr.Runtime.IToken tok)
		{
			if(tok != null)
			{
				line = tok.Line;
				column = tok.CharPositionInLine;

				ICharStream stream = tok.InputStream;
				if(stream != null)
					filename = tok.InputStream.SourceName;
			}
		}

		/// <summary>
		/// Get the coordinates from an ANTLR recognition exception. </summary>
		/// <param name="e"> The ANTLR recognition exception. </param>
		public Coords(Antlr.Runtime.RecognitionException e)
		{
			if(e != null)
			{
				line = e.Line;
				column = e.CharPositionInLine;
				if(e.Input != null)
					filename = e.Input.SourceName;
			}
		}
	}

}
