/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ir.pattern
{

	using System;
	using System.Collections.Generic;

	using Entity = de.unika.ipd.grgen.ir.Entity;
	using ImperativeStmt = de.unika.ipd.grgen.ir.stmt.ImperativeStmt;

	/// <summary>
	/// Adapter wrapping an lhs pattern graph, yielding an rhs pattern graph.
	/// </summary>
	public class PatternGraphRhsFromLhs : PatternGraphRhs
	{
		internal PatternGraphLhs patternGraph; // wrapped and adapted lhs pattern graph

		/// <summary>
		/// Make a new pattern graph. </summary>
		public PatternGraphRhsFromLhs(PatternGraphLhs patternGraph)
			: base(patternGraph.NameOfGraph,
					patternGraph.nodes, patternGraph.edges, patternGraph.subpatternUsages)
		{
			this.patternGraph = patternGraph;
		}

		public override void AddDeletedElement(GraphEntity entity)
		{
			throw new Exception("not implemented");
		}

		public override HashSet<GraphEntity> DeletedElements
		{
			get
			{
				return new HashSet<GraphEntity>();
			}
		}

		/// <summary>
		/// Add a replacement parameter to the rule. </summary>
		public override void AddReplParameter(Entity entity)
		{
			throw new Exception("not implemented");
		}

		/// <summary>
		/// Get all replacement parameters of this rule (may currently contain only nodes). </summary>
		public override IList<Entity> ReplParameters
		{
			get
			{
				return (new List<Entity>()).AsReadOnly();
			}
		}

		public override bool ReplParametersContain(Entity entity)
		{
			return false;
		}

		public override void AddImperativeStmt(ImperativeStmt emit)
		{
			throw new Exception("not implemented");
		}

		public override ICollection<ImperativeStmt> ImperativeStmts
		{
			get
			{
				return (new List<ImperativeStmt>()).AsReadOnly();
			}
		}
	}

}
