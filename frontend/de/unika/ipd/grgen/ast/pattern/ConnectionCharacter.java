/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 5.0
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Sebastian Hack
 */

package de.unika.ipd.grgen.ast.pattern;

import java.util.Set;

import de.unika.ipd.grgen.ast.BaseNode;
import de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
import de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
import de.unika.ipd.grgen.ir.Graph;
import de.unika.ipd.grgen.parser.Coords;

/**
 * Something that looks like a connection.
 * @see de.unika.ipd.grgen.ast.pattern.ConnectionNode
 */
public abstract class ConnectionCharacter extends BaseNode
{
	protected ConnectionCharacter(Coords coords)
	{
		super(coords);
	}

	/**
	 * Add all nodes of this connection to a set.
	 * @param set The set.
	 */
	public abstract void addNodes(Set<NodeDeclNode> set);

	/**
	 * Add all edges of this connection to a set.
	 * @param set The set.
	 */
	public abstract void addEdge(Set<EdgeDeclNode> set);

	public abstract EdgeCharacter getEdge();

	public abstract NodeCharacter getSrc();

	public abstract void setSrc(NodeDeclNode src);

	public abstract NodeCharacter getTgt();

	public abstract void setTgt(NodeDeclNode tgt);

	/**
	 * Add this connection character to an IR graph.
	 * @param gr The IR graph.
	 */
	public abstract void addToGraph(Graph gr);
}
