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
using CharStream = org.antlr.runtime.CharStream;

/// <summary>
/// Coordinates more suitable for an ANTLR parser.
/// </summary>
public class Coords : de.unika.ipd.grgen.parser.Coords
{
	/// <summary>
	/// Construct coordinates from an ANTLR token. </summary>
	/// <param name="tok"> The ANTLR token. </param>
	public Coords(org.antlr.runtime.Token tok)
	{
		if(tok != null)
		{
			line = tok.GetLine();
			column = tok.GetCharPositionInLine();

			CharStream stream = tok.GetInputStream();
			if(stream != null)
				filename = tok.GetInputStream().GetSourceName();
		}
	}

	/// <summary>
	/// Get the coordinates from an ANTLR recognition exception. </summary>
	/// <param name="e"> The ANTLR recognition exception. </param>
	public Coords(org.antlr.runtime.RecognitionException e)
	{
		if(e != null)
		{
			line = e.line;
			column = e.charPositionInLine;
			if(e.input != null)
				filename = e.input.GetSourceName();
		}
	}
}

}
