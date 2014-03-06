/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.3
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.parser.antlr;

import org.antlr.runtime.*;

/**
 * An entry of the stack of subunits,
 * may contain an #include statement for a file
 * or a using statement for a model file
 */
public class SubunitInclude {
	public SubunitInclude(GrGenParser parser) {
		this.parser = parser;
	}
	
	public SubunitInclude(CharStream charStream, int marking) {
		this.charStream = charStream;
		this.marking = marking;
	}
	
	// in case of a model include the parser is != null
	public GrGenParser parser;
	
	// in case of a plain include the char stream is != null, 
	// and the marking gives the position where lexing the including file was interrupted
	public CharStream charStream;
	public int marking;
}
