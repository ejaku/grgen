/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Moritz Kroll
 * @version $Id$
 */
package de.unika.ipd.grgen.ir;

import java.util.LinkedHashSet;

public class Constructor extends IR {
	private LinkedHashSet<ConstructorParam> parameters;

	public Constructor(LinkedHashSet<ConstructorParam> parameters) {
		super("constructor");
		this.parameters = parameters;
	}

	public LinkedHashSet<ConstructorParam> getParameters() {
		return parameters;
	}
}
