/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.be;

import de.unika.ipd.grgen.ir.type.Type;
import de.unika.ipd.grgen.ir.type.model.EdgeType;
import de.unika.ipd.grgen.ir.type.model.NodeType;

/**
 * Something that can give IDs for types.
 */
public interface TypeID
{

	int getId(NodeType nt);

	int getId(EdgeType et);

	int getId(Type type, boolean forNode);

	short[][] getIsAMatrix(boolean forNode);
}
