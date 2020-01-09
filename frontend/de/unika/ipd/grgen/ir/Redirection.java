/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 4.5
 * Copyright (C) 2003-2020 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3 (see LICENSE.txt included in the packaging of this file)
 * www.grgen.net
 */

/**
 * @author Rubino Geiss
 */

package de.unika.ipd.grgen.ir;


/** A class representing redirections in rules. */
public class Redirection {
	public final Node from;
	public final Node to;
	public final EdgeType edgeType;
	public final NodeType nodeType;
	public final boolean incoming;

	public Redirection(Node from, Node to, EdgeType edgeType,
					   NodeType nodeType, boolean incoming) {

		this.from = from;
		this.to = to;
		this.edgeType = edgeType;
		this.nodeType = nodeType;
		this.incoming = incoming;
	}
}
