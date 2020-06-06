/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeExprConst.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.type;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;

import de.unika.ipd.grgen.ir.model.type.InheritanceType;

public class TypeExprConst extends TypeExpr
{
	private final Collection<InheritanceType> types = new HashSet<InheritanceType>();

	public void addOperand(InheritanceType t)
	{
		types.add(t);
	}

	@Override
	public Collection<InheritanceType> evaluate()
	{
		return Collections.unmodifiableCollection(types);
	}
}
