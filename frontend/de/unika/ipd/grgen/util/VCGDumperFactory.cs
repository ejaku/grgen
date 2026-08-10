/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
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

public class VCGDumperFactory implements GraphDumperFactory
{
	private Sys sys;

	public VCGDumperFactory(Sys sys)
	{
		this.sys = sys;
	}

	@Override
	public GraphDumper get(String fileNamePart)
	{
		String fileName = fileNamePart + ".vcg";
		OutputStream os = sys.createDebugFile(new File(fileName));
		PrintStream ps = new PrintStream(os);
		return new VCGDumper(ps);
	}
}
