/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.4
 * Copyright (C) 2003-2016 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir;

import java.util.Collection;

import de.unika.ipd.grgen.ir.exprevals.EnumType;

/**
 * A type bearing nodes, edges, and enums.
 */
public interface NodeEdgeEnumBearer {
	public Collection<NodeType> getNodeTypes();
	public Collection<EdgeType> getEdgeTypes();
	public Collection<EnumType> getEnumTypes();
}
