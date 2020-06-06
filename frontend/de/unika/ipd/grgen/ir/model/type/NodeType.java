/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 */

package de.unika.ipd.grgen.ir.model.type;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Ident;

/**
 * IR class that represents node types.
 */
public class NodeType extends InheritanceType implements ContainedInPackage
{
	private String packageContainedIn;

	/**
	 * Make a new node type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public NodeType(Ident ident, int modifiers, String externalName)
	{
		super("node type", ident, modifiers, externalName);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public int classify()
	{
		return IS_NODE;
	}

	@Override
	public String getPackageContainedIn()
	{
		return packageContainedIn;
	}

	public void setPackageContainedIn(String packageContainedIn)
	{
		this.packageContainedIn = packageContainedIn;
	}
}
