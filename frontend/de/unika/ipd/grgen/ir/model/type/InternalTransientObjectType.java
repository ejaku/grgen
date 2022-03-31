/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.6
 * Copyright (C) 2003-2022 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model.type;

import de.unika.ipd.grgen.ir.Ident;

/**
 * IR class that represents (internal non-node/edge) transient object types (i.e. classes).
 */
public class InternalTransientObjectType extends BaseInternalObjectType
{
	/**
	 * Make a new (internal) transient object type.
	 * @param ident The identifier that declares this type.
	 * @param modifiers The modifiers for this type.
	 */
	public InternalTransientObjectType(Ident ident, int modifiers)
	{
		super("internal transient object type", ident, modifiers);
	}

	/** @see de.unika.ipd.grgen.ir.type.Type#classify() */
	@Override
	public TypeClass classify()
	{
		return TypeClass.IS_INTERNAL_TRANSIENT_CLASS_OBJECT;
	}
}
