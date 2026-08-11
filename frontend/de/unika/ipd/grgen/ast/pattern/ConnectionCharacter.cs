/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Sebastian Hack
/// </summary>

namespace de.unika.ipd.grgen.ast.pattern
{

using System.Collections.Generic;

using BaseNode = de.unika.ipd.grgen.ast.BaseNode;
using EdgeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.EdgeDeclNode;
using NodeDeclNode = de.unika.ipd.grgen.ast.decl.pattern.NodeDeclNode;
using PatternGraphBase = de.unika.ipd.grgen.ir.pattern.PatternGraphBase;
using Coords = de.unika.ipd.grgen.parser.Coords;

/// <summary>
/// Something that looks like a connection. </summary>
/// <seealso cref="de.unika.ipd.grgen.ast.pattern.ConnectionNode"/>
public abstract class ConnectionCharacter : BaseNode
{
	protected internal ConnectionCharacter(Coords coords)
		: base(coords)
	{
	}

	/// <summary>
	/// Add all nodes of this connection to a set. </summary>
	/// <param name="set"> The set. </param>
	public abstract void AddNodes(ISet<NodeDeclNode> set);

	/// <summary>
	/// Add all edges of this connection to a set. </summary>
	/// <param name="set"> The set. </param>
	public abstract void AddEdge(ISet<EdgeDeclNode> set);

	public abstract EdgeDeclNode Edge {get;}

	public abstract NodeDeclNode Src {get;set;}


	public abstract NodeDeclNode Tgt {get;set;}


	/// <summary>
	/// Add this connection character to an IR pattern graph. </summary>
	/// <param name="patternGraph"> The IR pattern graph. </param>
	public abstract void AddToGraph(PatternGraphBase patternGraph);
}

}
