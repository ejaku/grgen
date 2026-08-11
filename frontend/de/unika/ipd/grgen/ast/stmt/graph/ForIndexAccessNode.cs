/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>
namespace de.unika.ipd.grgen.ast.stmt.graph
{
	using de.unika.ipd.grgen.ast;
	using IndexDeclNode = de.unika.ipd.grgen.ast.model.decl.IndexDeclNode;
	using PatternGraphLhsNode = de.unika.ipd.grgen.ast.pattern.PatternGraphLhsNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	//deprecated, TODO: purge
	public abstract class ForIndexAccessNode : ForGraphQueryNode
	{
		static ForIndexAccessNode()
		{
			SetClassName(typeof(ForIndexAccessNode), "for index access loop");
		}

		protected internal IdentNode indexUnresolved;
		protected internal IndexDeclNode index;

		public ForIndexAccessNode(Coords coords, BaseNode iterationVariable, int context,
				IdentNode index, PatternGraphLhsNode directlyNestingLHSGraph,
				CollectNode<EvalStatementNode> loopedStatements)
			: base(coords, iterationVariable, loopedStatements)
		{
			this.indexUnresolved = index;
			BecomeParent(this.indexUnresolved);
		}
	}

}
