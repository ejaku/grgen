/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ast.util;

import de.unika.ipd.grgen.parser.Coords;
import de.unika.ipd.grgen.parser.ParserEnvironment;
import de.unika.ipd.grgen.util.report.ErrorReporter;


public class ResolvingEnvironment
{
	public ResolvingEnvironment(ParserEnvironment env, ErrorReporter errorReporter, Coords coords)
	{
		this.env = env;
		this.errorReporter = errorReporter;
		this.coords = coords;
	}
	
	public void reportError(String message)
	{
		errorReporter.error(coords, message);
	}
	
	public Coords getCoords()
	{
		return coords;
	}
	
	public ParserEnvironment getParserEnvironment()
	{
		return env;
	}
	
	ParserEnvironment env;
	ErrorReporter errorReporter;
	Coords coords;
}
