/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
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

	File[] getModelPaths();

	ErrorReporter getErrorReporter();

	OutputStream createDebugFile(File file);

	boolean mayFireEvents();
}

