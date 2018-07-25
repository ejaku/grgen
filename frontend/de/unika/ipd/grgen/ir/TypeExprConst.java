/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2018 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * TypeExprConst.java
 *
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;
import java.util.Collections;
import java.util.HashSet;

public class TypeExprConst extends TypeExpr {

	private final Collection<InheritanceType> types = new HashSet<InheritanceType>();

	public void addOperand(InheritanceType t) {
		types.add(t);
	}

	public Collection<InheritanceType> evaluate() {
		return Collections.unmodifiableCollection(types);
	}
}

