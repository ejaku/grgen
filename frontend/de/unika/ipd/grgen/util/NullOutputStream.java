/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.0
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * NullOutputStream.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.util;

import java.io.IOException;
import java.io.OutputStream;

public class NullOutputStream extends OutputStream
{
	public static final OutputStream STREAM = new NullOutputStream();

	@Override
	public void write(int p1) throws IOException
	{
		System.out.println("write to null stream");
	}

	private NullOutputStream()
	{
	}
}
