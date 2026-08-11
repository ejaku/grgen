/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.stmt.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using DeclNode = de.unika.ipd.grgen.ast.decl.DeclNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using NodeTypeNode = de.unika.ipd.grgen.ast.model.type.NodeTypeNode;
	using BuiltinProcedureInvocationBaseNode = de.unika.ipd.grgen.ast.stmt.BuiltinProcedureInvocationBaseNode;
	using EvalStatementNode = de.unika.ipd.grgen.ast.stmt.EvalStatementNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using GraphAddNodeProc = de.unika.ipd.grgen.ir.stmt.graph.GraphAddNodeProc;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node for adding a node to graph.
	/// </summary>
	public class GraphAddNodeProcNode : BuiltinProcedureInvocationBaseNode
	{
		static GraphAddNodeProcNode()
		{
			SetClassName(typeof(GraphAddNodeProcNode), "graph add node procedure");
		}

		private ExprNode nodeType;

		internal IList<TypeNode> returnTypes;

		public GraphAddNodeProcNode(Coords coords, ExprNode nodeType)
			: base(coords)
		{
			this.nodeType = nodeType;
			BecomeParent(this.nodeType);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(nodeType);
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
				childrenNames.Add("node type");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			TypeNode nodeTypeType = nodeType.Type;
			if(!(nodeTypeType is NodeTypeNode))
			{
				ReportError("The add procedure expects as argument (nodeType)"
						+ " a value of type node type"
						+ " (but is given a value of type " + nodeTypeType.ToStringWithDeclarationCoords() + ").");
				return false;
			}
			return true;
		}

		public override bool CheckStatementLocal(bool isLHS, DeclNode root, EvalStatementNode enclosingLoop)
		{
			return true;
		}

		protected internal override IR ConstructIR()
		{
			nodeType = nodeType.Evaluate();
			GraphAddNodeProc addNode = new GraphAddNodeProc(nodeType.CheckIR<Expression>(typeof(Expression)),
					nodeType.Type.IRType);
			return addNode;
		}

		public override IList<TypeNode> Type
		{
			get
			{
				if(returnTypes == null)
				{
					returnTypes = new List<TypeNode>();
					returnTypes.Add(nodeType.Type);
				}
				return returnTypes;
			}
		}
	}

}
