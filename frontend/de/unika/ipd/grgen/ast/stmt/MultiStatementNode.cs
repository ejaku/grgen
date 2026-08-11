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
	using EvalStatement = de.unika.ipd.grgen.ir.stmt.EvalStatement;
	using MultiStatement = de.unika.ipd.grgen.ir.stmt.MultiStatement;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// AST node representing a multi statement.
	/// Just a container for statements invisible to the user, esp. it does _not_ open a block, 
	/// used to break a return assignment to declarations into a series of declarations and a return assignment.
	/// </summary>
	public class MultiStatementNode : EvalStatementNode
	{
		static MultiStatementNode()
		{
			SetClassName(typeof(MultiStatementNode), "MultiStatement");
		}

		internal CollectNode<EvalStatementNode> statements = new CollectNode<EvalStatementNode>();

		public MultiStatementNode()
			: base(Coords.Invalid)
		{
			BecomeParent(this.statements);
		}

		public virtual void AddStatement(EvalStatementNode statement)
		{
			statements.AddChild(statement);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(statements);
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
				childrenNames.Add("statements");
				return childrenNames;
			}
		}

		protected internal override bool CheckLocal()
		{
			return true;
		}

		protected internal override bool ResolveLocal()
		{
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			MultiStatement ms = new MultiStatement();
			foreach(EvalStatementNode statement in statements.ChildrenExact)
				ms.AddStatement(statement.CheckIR(typeof(EvalStatement)));
			return ms;
		}
	}

}
