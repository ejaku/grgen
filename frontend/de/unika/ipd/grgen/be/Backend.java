/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.be;

import java.io.File;

import de.unika.ipd.grgen.Sys;
import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.Unit;

/**
 * Generic Backend interface.
 */
public interface Backend {

	/**
	 * Initialize the backend with the intermediate representation.
	 * @param unit The intermediate representation unit to
	 * generate code for.
	 * @param sys The system.
	 * @param outputPath The output path, where
	 * all generated files should go.
	 */
	void init(Unit unit, Sys system, File outputPath);

	/**
	 * Initiates the generation of code.
	 * It is always called after {@link #init(IR)}.
	 */
	void generate();

	/**
	 * Clearup some things, perhaps.
	 * Called after {@link #generate}.
	 */
	void done();

}

