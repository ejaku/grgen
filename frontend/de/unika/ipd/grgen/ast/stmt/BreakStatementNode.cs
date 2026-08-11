/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

/// <summary>
/// @author Edgar Jakumeit
/// </summary>

namespace de.unika.ipd.grgen.ast.stmt
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using BreakStatement = de.unika.ipd.grgen.ir.stmt.BreakStatement;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a break statement.
	/// </summary>
	public class BreakStatementNode : EvalStatementNode
	{
		static BreakStatementNode()
		{
			SetClassName(typeof(BreakStatementNode), "BreakStatement");
		}

		public BreakStatementNode(Coords coords)
			: base(coords)
		{
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				return children;
			}
		}

		/// <summary>
		/// returns names of the children, same order as in getChildren </summary>
		public override ICollection<string> ChildrenNames
		{
			get
			{
				IList<string> childrenNames = new List<string>();
				return childrenNames;
			}
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			if(enclosingLoop == null)
			{
				ReportError("The break statement must be nested inside a loop (where to break out otherwise?).");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			return new BreakStatement();
		}
	}

}
