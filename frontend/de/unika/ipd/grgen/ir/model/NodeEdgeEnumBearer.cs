/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ir.model
{

using System.Collections.Generic;

using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
using EnumType = de.unika.ipd.grgen.ir.model.type.EnumType;
using InternalObjectType = de.unika.ipd.grgen.ir.model.type.InternalObjectType;
using InternalTransientObjectType = de.unika.ipd.grgen.ir.model.type.InternalTransientObjectType;
using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;

/// <summary>
/// A type bearing nodes, edges, and enums.
/// </summary>
public interface NodeEdgeEnumBearer
{
	ICollection<NodeType> NodeTypes {get;}

	ICollection<EdgeType> EdgeTypes {get;}

	ICollection<InternalObjectType> ObjectTypes {get;}

	ICollection<InternalTransientObjectType> TransientObjectTypes {get;}

	ICollection<EnumType> EnumTypes {get;}
}

}
