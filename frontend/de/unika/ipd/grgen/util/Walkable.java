/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.7
 * Copyright (C) 2003-2011 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author shack
 * @version $Id$
 */
package de.unika.ipd.grgen.util;

import java.util.Collection;

import de.unika.ipd.grgen.ast.BaseNode;

/**
 * Something you can walk on. This means, that there are children to visit.
 */
public interface Walkable {

	/**
	 * Get the children of this object
	 * Note: BaseNode implements Walkable
	 * @return The children
	 */
	Collection<? extends BaseNode> getWalkableChildren();
}
