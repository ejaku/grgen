/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 3.0
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

