/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2014 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ast.exprevals;

import de.unika.ipd.grgen.parser.Coords;

public abstract class ProcedureMethodInvocationBaseNode extends ProcedureInvocationBaseNode
{
	static {
		setName(ProcedureMethodInvocationBaseNode.class, "procedure method invocation base");
	}
	
	public ProcedureMethodInvocationBaseNode(Coords coords)
	{
		super(coords);
	}
}
