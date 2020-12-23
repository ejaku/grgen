/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.2
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model.type;

import de.unika.ipd.grgen.ir.ContainedInPackage;
import de.unika.ipd.grgen.ir.Ident;

/**
 * IR class that represents (internal non-node/edge) object types (i.e. classes).
 */
public class InternalObjectType extends InheritanceType implements ContainedInPackage
{
	private String packageContainedIn;

	/**
	 * Make a new (internal) object type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 */
	public InternalObjectType(Ident ident, int modifiers)
	{
		super("internal object type", ident, modifiers, null);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_INTERNAL_CLASS_OBJECT;
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
