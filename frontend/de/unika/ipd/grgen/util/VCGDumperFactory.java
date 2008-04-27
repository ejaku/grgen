/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET v2 beta
 * Copyright (C) 2008 Universität Karlsruhe, Institut für Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * VCGDUmperFactory.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;
import java.io.File;
import java.io.OutputStream;
import java.io.PrintStream;

import de.unika.ipd.grgen.Sys;



public class VCGDumperFactory implements GraphDumperFactory {

	private Sys system;

	public VCGDumperFactory(Sys system) {
		this.system = system;
	}

	public GraphDumper get(String fileNamePart) {

		String fileName = fileNamePart + ".vcg";
		OutputStream os = system.createDebugFile(new File(fileName));
		PrintStream ps = new PrintStream(os);
		return new VCGDumper(ps);
	}

}

