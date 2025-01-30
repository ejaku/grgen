/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 7.1
 * Copyright (C) 2003-2025 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Edgar Jakumeit
 */

package de.unika.ipd.grgen.ir.model;

import java.util.Collection;

import de.unika.ipd.grgen.ir.model.type.EdgeType;
import de.unika.ipd.grgen.ir.model.type.EnumType;
import de.unika.ipd.grgen.ir.model.type.InternalObjectType;
import de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
import de.unika.ipd.grgen.ir.model.type.NodeType;

/**
 * A type bearing nodes, edges, and enums.
 */
public interface NodeEdgeEnumBearer
{
	public Collection<NodeType> getNodeTypes();

	public Collection<EdgeType> getEdgeTypes();

	public Collection<InternalObjectType> getObjectTypes();

	public Collection<InternalTransientObjectType> getTransientObjectTypes();

	public Collection<EnumType> getEnumTypes();
}
