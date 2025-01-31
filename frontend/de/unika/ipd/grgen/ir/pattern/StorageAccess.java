/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.ir.pattern;

import de.unika.ipd.grgen.ir.expr.Qualification;

/**
 * Class for the different kinds of storages available for binding a pattern element by accessing
 */
public class StorageAccess
{
	public Variable storageVariable = null;
	public Qualification storageAttribute = null;
	//public GraphEntity storageGlobalVariable = null;

	public StorageAccess(Variable storageVariable)
	{
		this.storageVariable = storageVariable;
	}

	public StorageAccess(Qualification storageAttribute)
	{
		this.storageAttribute = storageAttribute;
	}

	//	public StorageAccess(GraphEntity storageGlobalVariable) {
	//		this.storageGlobalVariable = storageGlobalVariable;
	//	}
}
