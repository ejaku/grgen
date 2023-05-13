/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 6.7
 * Copyright (C) 2003-2023 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Buchwald
 */

package de.unika.ipd.grgen.ast.model.type;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.CollectNode;
import de.unika.ipd.grgen.ast.IdentNode;
import de.unika.ipd.grgen.ast.model.ConnAssertNode;
import de.unika.ipd.grgen.ir.model.type.EdgeType;

public class UndirectedEdgeTypeNode extends EdgeTypeNode
{
	static {
		setName(UndirectedEdgeTypeNode.class, "undirected edge type");
	}

	/**
	 * Make a new undirected edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public UndirectedEdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas,
			CollectNode<BaseNode> body, int modifiers, String externalName)
	{
		super(ext, cas, body, modifiers, externalName);
	}

	@Override
	protected void setDirectednessIR(EdgeType edgeType)
	{
		edgeType.setDirectedness(EdgeType.Directedness.Undirected);
	}

	public static String getKindStr()
	{
		return "undirected edge class";
	}
}
