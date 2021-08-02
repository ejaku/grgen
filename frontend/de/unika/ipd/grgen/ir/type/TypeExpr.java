/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.1
 * Copyright (C) 2003-2021 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeExpr.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir.type;

import java.util.Collection;
import java.util.Collections;

import de.unika.ipd.grgen.ir.IR;
import de.unika.ipd.grgen.ir.model.type.InheritanceType;

public abstract class TypeExpr extends IR
{
	public TypeExpr()
	{
		super("type expr");
	}

	/**
	 * Evaluate this type expression by returning a set
	 * of all types that are represented by the expression.
	 * @return A collection of types that correspond to the expression.
	 */
	public abstract Collection<InheritanceType> evaluate();

	public static final TypeExpr EMPTY = new TypeExpr() {
		@Override
		public Collection<InheritanceType> evaluate()
		{
			return Collections.emptySet();
		}
	};
}
