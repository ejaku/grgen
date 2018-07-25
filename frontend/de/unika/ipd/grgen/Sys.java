/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * Sys.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen;

import java.io.File;
import java.io.OutputStream;

import de.unika.ipd.grgen.util.report.ErrorReporter;

public interface Sys {

	File getModelPath();

	ErrorReporter getErrorReporter();

	OutputStream createDebugFile(File file);

	boolean mayFireEvents();
	boolean mayFireDebugEvents();
	
	boolean emitProfilingInstrumentation();
}

