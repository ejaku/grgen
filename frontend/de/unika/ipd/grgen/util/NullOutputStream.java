/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.5
 * Copyright (C) 2009 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * NullOutputStream.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.io.IOException;
import java.io.OutputStream;

public class NullOutputStream extends OutputStream {

	public static final OutputStream STREAM = new NullOutputStream();

	public void write(int p1) throws IOException {
		System.out.println("write to null stream");
	}

	private NullOutputStream() {
	}

}

