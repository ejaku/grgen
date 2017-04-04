/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2017 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

package de.unika.ipd.grgen.util;

import de.unika.ipd.grgen.ir.Entity;

/**
 * @author adam
 * Something that is being retyped during the rewrite
 */
public interface Retyped {
	void setOldEntity(Entity old);

	Entity getOldEntity();
}
