/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 2.1
 * Copyright (C) 2008 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos
 * licensed under GPL v3 (see LICENSE.txt included in the packaging of this file)
 */

/**
 * @author Sebastian Hack
 * @version $Id$
 */
package de.unika.ipd.grgen.ast;

import de.unika.ipd.grgen.ir.EdgeType;

public class DirectedEdgeTypeNode extends EdgeTypeNode {
	static {
		setName(DirectedEdgeTypeNode.class, "directed edge type");
	}

	/**
	 * Make a new directed edge type node.
	 * @param ext The collect node with all edge classes that this one extends.
	 * @param cas The collect node with all connection assertion of this type.
	 * @param body The body of the type declaration. It consists of basic
	 * declarations.
	 * @param modifiers The modifiers for this type.
	 * @param externalName The name of the external implementation of this type or null.
	 */
	public DirectedEdgeTypeNode(CollectNode<IdentNode> ext, CollectNode<ConnAssertNode> cas, CollectNode<BaseNode> body,
						int modifiers, String externalName) {
		super(ext, cas, body, modifiers, externalName);
	}

	protected void setDirectednessIR(EdgeType edgeType) {
		edgeType.setDirectedness(EdgeType.Directedness.Directed);
    }

	public static String getKindStr() {
		return "directed edge type";
	}

	public static String getUseStr() {
		return "directed edge type";
	}
}
