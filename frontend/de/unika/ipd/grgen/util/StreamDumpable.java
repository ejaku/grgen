/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * Dumpable.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;


import java.io.PrintStream;

/**
 * Something that can dump itself onto a print stream.
 */
public interface StreamDumpable {

	void dump(PrintStream ps);

}

