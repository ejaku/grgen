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

import java.util.Set;

import de.unika.ipd.grgen.ir.Graph;

/**
 * Something that looks like a connection.
 * @see de.unika.ipd.grgen.ast.ConnectionNode
 */
public interface ConnectionCharacter {

	/**
	 * Add all nodes of this connection to a set.
	 * @param set The set.
	 */
	void addNodes(Set<BaseNode> set);

	/**
	 * Add all edges of this connection to a set.
	 * @param set The set.
	 */
	void addEdge(Set<BaseNode> set);

	EdgeCharacter getEdge();

	NodeCharacter getSrc();

	void setSrc(NodeDeclNode src);

	NodeCharacter getTgt();

	void setTgt(NodeDeclNode tgt);

	/**
	 * Add this connection character to an IR graph.
	 * @param gr The IR graph.
	 */
	void addToGraph(Graph gr);
}
