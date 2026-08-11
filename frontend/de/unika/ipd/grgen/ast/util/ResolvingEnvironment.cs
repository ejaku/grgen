/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.util
{
using Coords = de.unika.ipd.grgen.parser.Coords;
using ParserEnvironment = de.unika.ipd.grgen.parser.ParserEnvironment;
using ErrorReporter = de.unika.ipd.grgen.util.report.ErrorReporter;


public class ResolvingEnvironment
{
	public ResolvingEnvironment(ParserEnvironment env, ErrorReporter errorReporter, Coords coords)
	{
		this.env = env;
		this.errorReporter = errorReporter;
		this.coords = coords;
	}

	public virtual void ReportError(string message)
	{
		errorReporter.Error(coords, message);
	}

	public virtual Coords Coords
	{
		get
		{
			return coords;
		}
	}

	public virtual ParserEnvironment ParserEnvironment
	{
		get
		{
			return env;
		}
	}

	internal ParserEnvironment env;
	internal ErrorReporter errorReporter;
	internal Coords coords;
}

}
