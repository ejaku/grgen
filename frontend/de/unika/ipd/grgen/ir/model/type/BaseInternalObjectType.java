/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
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
 * IR class that represents a base for internal (non-node/edge) object types (i.e. classes).
 */
public class BaseInternalObjectType extends InheritanceType implements ContainedInPackage
{
	private String packageContainedIn;

	/**
	 * Make a new base internal object type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 */
	public BaseInternalObjectType(String name, Ident ident, int modifiers)
	{
		super(name, ident, modifiers, null);
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
