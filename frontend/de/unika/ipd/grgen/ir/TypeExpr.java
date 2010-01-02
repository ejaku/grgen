/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.6
 * Copyright (C) 2003-2010 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeExpr.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;

public abstract class TypeExpr extends IR {

	public TypeExpr() {
		super("type expr");
	}

	/**
	 * Evaluate this type expression by returning a set
	 * of all types that are represented by the expression.
	 * @return A collection of types that correspond to the expression.
	 */
	public abstract Collection<InheritanceType> evaluate();

	public static final TypeExpr EMPTY = new TypeExpr() {
		public Collection<InheritanceType> evaluate() {
			return Collections.emptySet();
		}
	};
}

