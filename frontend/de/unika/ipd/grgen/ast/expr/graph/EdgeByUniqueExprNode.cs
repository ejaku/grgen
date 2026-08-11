/*
 * GrGen: graph rewrite generator tool -- release GrGen.NET 8.1
 * Copyright (C) 2003-2026 Universitaet Karlsruhe, Institut fuer Programmstrukturen und Datenorganisation, LS Goos; and free programmers
 * licensed under LGPL v3, some components/parts use different licenses (see LICENSE.txt included in the packaging of this file)
 * www.grgen.de / www.grgen.net
 */

namespace de.unika.ipd.grgen.ast.expr.graph
{

	using System.Collections.Generic;

	using de.unika.ipd.grgen.ast;
	using BuiltinFunctionInvocationBaseNode = de.unika.ipd.grgen.ast.expr.BuiltinFunctionInvocationBaseNode;
	using ExprNode = de.unika.ipd.grgen.ast.expr.ExprNode;
	using EdgeTypeNode = de.unika.ipd.grgen.ast.model.type.EdgeTypeNode;
	using TypeNode = de.unika.ipd.grgen.ast.type.TypeNode;
	using IntTypeNode = de.unika.ipd.grgen.ast.type.basic.IntTypeNode;
	using IR = de.unika.ipd.grgen.ir.IR;
	using Expression = de.unika.ipd.grgen.ir.expr.Expression;
	using EdgeByUniqueExpr = de.unika.ipd.grgen.ir.expr.graph.EdgeByUniqueExpr;
	using Coords = de.unika.ipd.grgen.parser.Coords;

	/// <summary>
	/// A node retrieving an edge from a unique id.
	/// </summary>
	public class EdgeByUniqueExprNode : BuiltinFunctionInvocationBaseNode
	{
		static EdgeByUniqueExprNode()
		{
			SetClassName(typeof(EdgeByUniqueExprNode), "edge by unique expr");
		}

		private ExprNode unique;
		private ExprNode edgeType;

		public EdgeByUniqueExprNode(Coords coords, ExprNode unique, ExprNode edgeType)
			: base(coords)
		{
			this.unique = unique;
			BecomeParent(this.unique);
			this.edgeType = edgeType;
			BecomeParent(this.edgeType);
		}

		/// <summary>
		/// returns children of this node </summary>
		public override ICollection<BaseNode> Children
		{
			get
			{
				IList<BaseNode> children = new List<BaseNode>();
				children.Add(unique);
				children.Add(edgeType);
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
				childrenNames.Add("unique");
				childrenNames.Add("edgeType");
				return childrenNames;
			}
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.resolveLocal() "/>
		protected internal override bool ResolveLocal()
		{
			return true;
		}

		/// <seealso cref="de.unika.ipd.grgen.ast.BaseNode.checkLocal() "/>
		protected internal override bool CheckLocal()
		{
			if(!(unique.Type is IntTypeNode))
			{
				ReportError("The function edgeByUnique expects as 1. argument (uniqueIdToSearchFor) a value of type int"
						+ " (but is given a value of type " + unique.Type.TypeName + ").");
				return false;
			}
			if(!(edgeType.Type is EdgeTypeNode))
			{
				ReportError("The function edgeByUnique expects as 2. argument (typeToObtain) a value of type edge type"
						+ " (but is given a value of type " + edgeType.Type.TypeName + ").");
				return false;
			}
			if(!UnitNode.Root.Model.IsUniqueIndexDefined())
			{
				ReportError("The function edgeByUnique expects a model with a unique index, but the required index unique; declaration is missing in the model specification.");
				return false;
			}
			return true;
		}

		protected internal override IR ConstructIR()
		{
			unique = unique.Evaluate();
			edgeType = edgeType.Evaluate();
			return new EdgeByUniqueExpr(unique.CheckIR(typeof(Expression)),
					edgeType.CheckIR(typeof(Expression)), Type.IRType);
		}

		public override TypeNode Type
		{
			get
			{
				return edgeType.Type;
			}
		}
	}

}
