/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Rubino Geiss
/// </summary>

namespace de.unika.ipd.grgen.ir.pattern
{
	using EdgeType = de.unika.ipd.grgen.ir.model.type.EdgeType;
	using NodeType = de.unika.ipd.grgen.ir.model.type.NodeType;

	/// <summary>
	/// A class representing redirections in rules. </summary>
	public class Redirection
	{
		public readonly Node from;
		public readonly Node to;
		public readonly EdgeType edgeType;
		public readonly NodeType nodeType;
		public readonly bool incoming;

		public Redirection(Node from, Node to, EdgeType edgeType,
				NodeType nodeType, bool incoming)
		{

			this.from = from;
			this.to = to;
			this.edgeType = edgeType;
			this.nodeType = nodeType;
			this.incoming = incoming;
		}
	}

}
