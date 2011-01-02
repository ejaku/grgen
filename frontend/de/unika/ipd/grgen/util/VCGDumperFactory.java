/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
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

